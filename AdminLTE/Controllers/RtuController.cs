using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AdminLTE.ViewModel;

namespace AdminLTE.Controllers
{
    public class RtuController : Controller
    {
        TempEntities dbcontext = new TempEntities();

        // GET: Station
        public ActionResult Index(string searchString,string filter,int?page)
        {

            ViewBag.role = Session["role"].ToString().Trim();
            ViewBag.username = Session["username"].ToString();
            ViewBag.unit = Session["unit"].ToString();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = filter;
            }
            ViewBag.Filter = searchString;
            //关联为 用户表的用户unit==Rtu站点表的region
            var stationifos= from rtu in dbcontext.rtu
                             join user_ in dbcontext.user
                            on rtu.rtu_region equals user_.user_unit
                            into temp from tt in temp.DefaultIfEmpty()
                            select new  StationInfo{id=rtu.id, rtu_id = rtu.rtu_id,rtu_name= rtu.rtu_name,rtu_region= rtu.rtu_region,rtu_unit= rtu.rtu_unit,rtu_sim= rtu.rtu_sim,name=tt==null?"":tt.name,user_tel=tt==null?"":tt.user_tel};
            if (!string.IsNullOrEmpty(searchString)) 
            {

                //searchString = filter;
                stationifos = stationifos.Where(st => st.rtu_region.Contains(searchString));
            }
            stationifos = stationifos.OrderBy(u=>u.rtu_id);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(stationifos.ToPagedList(pageNumber, pageSize));
          
        }

        [HttpPost]
        public ActionResult Index(rtu stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(rtu stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            if (ModelState.IsValid)
            {
                try
                {
                    //rtu_id为用户输入 
                    //stationtab.rtu_id =Util.GetTimeStamp();
                    dbcontext.rtu.Add(stationtab);
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

        public ActionResult Edit(int id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            rtu st = dbcontext.rtu.Find(id);
             
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(rtu stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            dbcontext.Entry(stationtab).State= System.Data.Entity.EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int  id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            rtu stationtab = dbcontext.rtu.Find(id);
            return View(stationtab);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            rtu stationtab = dbcontext.rtu.Find(id);
            ViewBag.role = Session["role"].ToString();
            dbcontext.rtu.Remove(stationtab);
            dbcontext.Entry(stationtab).State = System.Data.Entity.EntityState.Deleted;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        public ActionResult Details(int id)
        {
            ViewBag.role = Session["role"].ToString();
            //页面跳转到data视图
            Session["rtuid"] = id;
            return RedirectToAction("Index", "Instrument");
            
        }

    }
}