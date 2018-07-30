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
            return View(dbcontext.UsersTab);
        }
        [HttpPost]
        public ActionResult Index(UsersTab usertab)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UsersTab usertab)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int maxid=  dbcontext.UsersTab.Max(c => c.id);
                    usertab.id = maxid+1;
                    dbcontext.UsersTab.Add(usertab);
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
            UsersTab st = dbcontext.UsersTab.Find(id);
            
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(UsersTab usertab)
        {
            dbcontext.Entry(usertab).State = System.Data.Entity.EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int  id)
        {
            UsersTab usertab = dbcontext.UsersTab.Find(id);
            return View(usertab);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int  id)
        {
            UsersTab usertab = dbcontext.UsersTab.Find(id);

            dbcontext.UsersTab.Remove(usertab);
            dbcontext.Entry(usertab).State = System.Data.Entity.EntityState.Deleted;

            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        //根据所选用户 单位查询 该用户下对应的站点
        public ActionResult Details(string unit)
        {
            var stationtab = dbcontext.StationTab.Where(n => n.sta_name==unit);
            return View(stationtab);
        }

    }
}