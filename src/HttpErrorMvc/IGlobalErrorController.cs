namespace HttpErrorMvc
{
    using System.Web.Mvc;

    public interface IGlobalErrorController : IController
    {
        ActionResult GlobalError(int statusCode);
    }
}