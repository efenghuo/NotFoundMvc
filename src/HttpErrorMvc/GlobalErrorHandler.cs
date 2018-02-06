namespace HttpErrorMvc
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using HttpErrorMvc;

    public class GlobalErrorHandler : IHttpHandler
    {
        private static Func<RequestContext, IGlobalErrorController> _createErrorController = (context) => new GlobalErrorController();

        public static Func<RequestContext, IGlobalErrorController> CreateErrorController
        {
            get
            {
                return _createErrorController;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _createErrorController = value;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var requestContext = CreateRequestContext(new HttpContextWrapper(context));
            var controller = _createErrorController(requestContext);
            controller.Execute(requestContext);
        }

        private static RequestContext CreateRequestContext(HttpContextBase context)
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", GlobalErrorConfig.CustomData.ErrorControllerName);
            var requestContext = new RequestContext(context, routeData);
            return requestContext;
        }
    }
}