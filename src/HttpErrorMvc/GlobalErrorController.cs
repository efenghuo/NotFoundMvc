namespace HttpErrorMvc
{
    using System.Net;
    using System.Web.Mvc;

    public class GlobalErrorController : ControllerBase, IGlobalErrorController
    {

        public GlobalErrorController()
        {
        }

        public ActionResult GlobalError(int statusCode)
        {
            return new GlobalErrorViewResult(statusCode);
        }

        protected override void ExecuteCore()
        {
            new GlobalErrorViewResult(200).ExecuteResult(this.ControllerContext);
        }
    }
}
