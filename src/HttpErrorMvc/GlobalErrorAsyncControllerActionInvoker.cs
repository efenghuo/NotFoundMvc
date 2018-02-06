namespace HttpErrorMvc
{
    using System.Web.Mvc;
    using System.Web.Mvc.Async;

    public class GlobalErrorAsyncControllerActionInvoker : AsyncControllerActionInvoker
    {
        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            var result = base.FindAction(controllerContext, controllerDescriptor, actionName);

            return new GlobalErrorActionDescriptorWrapper(result);
        }
    }
}