﻿namespace HttpErrorMvc
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    internal class ControllerFactoryWrapper : IControllerFactory
    {
        private readonly IControllerFactory factory;

        public ControllerFactoryWrapper(IControllerFactory factory)
        {
            this.factory = factory;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var controller = this.factory.CreateController(requestContext, controllerName);
                WrapControllerActionInvoker(controller);
                return controller;
            }
            catch (HttpException ex)
            {
                if (GlobalErrorConfig.CustomData.StatusCodes.IndexOf(ex.GetHttpCode()) > -1)
                {
                    return GlobalErrorHandler.CreateErrorController(requestContext);
                }

                throw;
            }
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return this.factory.GetControllerSessionBehavior(requestContext, controllerName);
        }

        public void ReleaseController(IController controller)
        {
            this.factory.ReleaseController(controller);
        }

        private static void WrapControllerActionInvoker(IController controller)
        {
            var controllerWithInvoker = controller as Controller;
            if (controllerWithInvoker != null)
            {
                controllerWithInvoker.ActionInvoker = ActionInvokerSelector.Current(controllerWithInvoker.ActionInvoker);
            }
        }
    }
}