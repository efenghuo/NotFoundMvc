namespace HttpErrorMvc
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// custom description data.
    /// </summary>
    public class GlobalErrorConfigCustomData
    {
        private List<int> _statusCodesNeedRaise = new List<int> { 404 };

        /// <summary>
        /// Gets or sets allowed to define statusCodes which ones need to process.
        /// </summary>
        public List<int> StatusCodes
        {
            get
            {
                return _statusCodesNeedRaise;
            }
            set { _statusCodesNeedRaise = value; }
        }

        /// <summary>
        /// Gets error controller name.
        /// </summary>
        public string ErrorControllerName { get; } = "GlobalError";

        /// <summary>
        /// Gets error controller name.
        /// </summary>
        public string ErrorActionName { get; } = "GlobalError";

        /// <summary>
        /// Gets or sets gets error view name.default is Error
        /// </summary>
        public string ErrorViewName { get; set; } = "Error";
    }
}
