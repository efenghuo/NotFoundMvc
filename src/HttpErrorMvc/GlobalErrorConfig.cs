namespace HttpErrorMvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Routing;

    public static class GlobalErrorConfig
    {
        private static readonly Action<HttpRequestBase, Uri> NullOnNotFound = (req, uri) => { /*noop*/ };

        private static Action<HttpRequestBase, Uri> onErrorHandling;

        private static GlobalErrorConfigCustomData customData = new GlobalErrorConfigCustomData();

        /// <summary>
        /// Gets or sets the action to execute when a 404 has occurred.
        /// Here you can pass the 404 on to your own logging (NLog, log4net) or error handling (ELMAH).
        /// </summary>
        /// <value>
        /// The action to execute when a 404 has occurred.
        /// </value>
        public static Action<HttpRequestBase, Uri> OnErrorHandling
        {
            get
            {
                return onErrorHandling ?? NullOnNotFound;
            }

            set
            {
                onErrorHandling = value;
            }
        }

        /// <summary>
        /// Gets or sets NotFoundHandler.CreateNotFoundController
        /// </summary>
        public static Func<RequestContext, IGlobalErrorController> CreateErrorController
        {
            get
            {
                return GlobalErrorHandler.CreateErrorController;
            }

            set
            {
                GlobalErrorHandler.CreateErrorController = value;
            }
        }

        public static GlobalErrorConfigCustomData CustomData
        {
            get
            {
                return customData;
            }
        }

        public static void Init(GlobalErrorConfigCustomData customData)
        {
            GlobalErrorConfig.customData = customData;
        }
    }
}
