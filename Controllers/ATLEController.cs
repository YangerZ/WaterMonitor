using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class ATLEController : Controller
    {
        // GET: ATLE
        public ActionResult Index()
        {
            return View();
        }
        //PartialWidet Action 方式
        [ChildActionOnly]
        public ActionResult ShowWidget()
        {
            return PartialView("~/Views/Shared/_PartialWidget.cshtml");
        }
    }
}