namespace HttpErrorMvc
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    public class GlobalErrorActionDescriptorWrapper : ActionDescriptor
    {
        private ActionDescriptor descriptor;

        public GlobalErrorActionDescriptorWrapper(ActionDescriptor descriptor)
        {
            this.descriptor = descriptor;
        }

        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            int statusCode = 404;
            if (this.descriptor != null)
            {
                try
                {
                    return this.descriptor.Execute(controllerContext, parameters);
                }
                catch (HttpException ex)
                {
                    if (GlobalErrorConfig.CustomData.StatusCodes.IndexOf(ex.GetHttpCode()) > -1)
                    {
                        statusCode = ex.GetHttpCode();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            IGlobalErrorController errorController = GlobalErrorHandler.CreateErrorController(controllerContext.RequestContext);
            controllerContext.RouteData.Values["action"] = GlobalErrorConfig.CustomData.ErrorActionName;
            return errorController.GlobalError(statusCode);
        }

        public override string ActionName
        {
            get { return descriptor?.ActionName ?? GlobalErrorConfig.CustomData.ErrorActionName; }
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return descriptor?.GetParameters() ?? new ParameterDescriptor[0];
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get { return descriptor?.ControllerDescriptor ?? new ReflectedControllerDescriptor(typeof(GlobalErrorController)); }
        }
    }
}