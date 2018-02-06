﻿using System.Web;
using System.Web.Mvc;
using NotFoundMvc;

namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int id)
        {
            // NotFoundViewResults inherits from HttpNotFoundResult
            return new GlobalErrorViewResult();
        }

        public ActionResult Fail()
        {
            Response.Write("Attempt to write some content."); // Expecting the NotFoundViewResult to clear the response before sending its output.

            throw new HttpException(404, "Not found!");
        }
    }
}
