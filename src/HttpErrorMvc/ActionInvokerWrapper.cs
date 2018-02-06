namespace HttpErrorMvc
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Wraps another IActionInvoker except it handles the case of an action method
    /// not being found and invokes the NotFoundController instead.
    /// </summary>
    internal class ActionInvokerWrapper : IActionInvoker
    {
        private readonly IActionInvoker actionInvoker;

        public ActionInvokerWrapper(IActionInvoker actionInvoker)
        {
            this.actionInvoker = actionInvoker;
        }

        public bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            int statusCode;
            if (this.InvokeActionWithErrorCatch(controllerContext, actionName, out statusCode))
            {
                return true;
            }

            // No action method was found, or it was, but threw a HttpException.
            ExecuteNotFoundControllerAction(controllerContext);
            return true;
        }

        private static void ExecuteNotFoundControllerAction(ControllerContext controllerContext)
        {
            IController controller;
            if (GlobalErrorHandler.CreateErrorController != null)
            {
                controller = GlobalErrorHandler.CreateErrorController(controllerContext.RequestContext) ?? new GlobalErrorController();
            }
            else
            {
                controller = new GlobalErrorController();
            }

            controller.Execute(controllerContext.RequestContext);
        }

        private bool InvokeActionWithErrorCatch(ControllerContext controllerContext, string actionName, out int statusCode)
        {
            statusCode = 200;
            try
            {
                return this.actionInvoker.InvokeAction(controllerContext, actionName);
            }
            catch (HttpException ex)
            {
                if (GlobalErrorConfig.CustomData.StatusCodes.IndexOf(ex.GetHttpCode()) > -1)
                {
                    statusCode = ex.GetHttpCode();
                    return false;
                }

                throw;
            }
        }
    }
}
