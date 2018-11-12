using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class UserController : Controller
    {
        TempEntities dbcontext = new TempEntities();
        // GET: User
        public ActionResult Index()
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View(dbcontext.user);
        }
        [HttpPost]
        public ActionResult Index(user usertab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
        }
        [HttpPost]
        public ActionResult Create(user usertab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            if (ModelState.IsValid)
            {
                try
                {
                    int maxid=  dbcontext.user.Max(c => c.id);
                    usertab.id = maxid+1;
                    dbcontext.user.Add(usertab);
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

        public ActionResult Edit(int  id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            user st = dbcontext.user.Find(id);
            
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(user usertab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            dbcontext.Entry(usertab).State = System.Data.Entity.EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int  id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            user usertab = dbcontext.user.Find(id);
            return View(usertab);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int  id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            user usertab = dbcontext.user.Find(id);

            dbcontext.user.Remove(usertab);
            dbcontext.Entry(usertab).State = System.Data.Entity.EntityState.Deleted;

            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        //根据所选用户 单位查询 该用户下对应的站点
        public ActionResult Details(string unit)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            var stationtab = dbcontext.rtu.Where(n => n.rtu_region==unit);
            return View(stationtab);
        }

    }
}