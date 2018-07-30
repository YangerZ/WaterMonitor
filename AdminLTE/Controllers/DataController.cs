using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class DataController : Controller
    {
        TempEntities dbcontext = new TempEntities();
        // GET: Data
        public ActionResult Index()
        {
            
            return View(dbcontext.DataTab);
        }


        [HttpPost]
        public ActionResult Index(DataTab datatab)
        {

            return View();
        }

       
    }
}