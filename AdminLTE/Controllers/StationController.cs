using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class StationController : Controller
    {
        TempEntities dbcontext = new TempEntities();

        // GET: Station
        public ActionResult Index()
        {

            ViewBag.username = Session["username"].ToString();
            ViewBag.unit = Session["unit"].ToString();
            return View(dbcontext.StationTab);
        }

        [HttpPost]
        public ActionResult Index(StationTab stationtab)
        {

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StationTab stationtab)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    stationtab.rtu_id =Util.GetTimeStamp();
                    dbcontext.StationTab.Add(stationtab);
                    dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
               
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            StationTab st = dbcontext.StationTab.Find(id.ToString());
            st.rtu_id = st.rtu_id.Trim();
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(StationTab stationtab)
        {
            dbcontext.Entry(stationtab).State= System.Data.Entity.EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string  id)
        {
            StationTab stationtab = dbcontext.StationTab.Find(id.ToString());
            return View(stationtab);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            StationTab stationtab = dbcontext.StationTab.Find(id.ToString());
          
            dbcontext.StationTab.Remove(stationtab);
            dbcontext.Entry(stationtab).State = System.Data.Entity.EntityState.Deleted;
      
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

       
        public ActionResult Details(string id)
        {
            var datatab = dbcontext.DataTab.Where(n=>n.rtu_id.Trim()==id);
            return View(datatab);
        }

    }
}