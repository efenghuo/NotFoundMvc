﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleAppMvc5.Controllers
{
    using HttpErrorMvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int id)
        {
            // NotFoundViewResults inherits from HttpNotFoundResult
            return new GlobalErrorViewResult(404);
        }

        [Route("thisistheroute")]
        public ActionResult ThisIsNotTheRoute()
        {
            return this.View();
        }

        public ActionResult Fail()
        {
            Response.Write("Attempt to write some content."); // Expecting the NotFoundViewResult to clear the response before sending its output.

            throw new HttpException(404, "Not found!");
        }
        public ActionResult Error()
        {
            Response.Write("Attempt to write some content."); // Expecting the NotFoundViewResult to clear the response before sending its output.

            throw new HttpException(500, "custom error!");
        }
        public ActionResult PermissionError()
        {
            Response.Write("Attempt to write some content."); // Expecting the NotFoundViewResult to clear the response before sending its output.

            throw new HttpException(403, "permission error!");
        }
    }
}