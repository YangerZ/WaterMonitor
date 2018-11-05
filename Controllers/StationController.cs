using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AdminLTE.ViewModel;

namespace AdminLTE.Controllers
{
    public class StationController : Controller
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
            //单位与设备名称一致
            var stationifos= from st in dbcontext.StationTab
                             join user_ in dbcontext.UsersTab
                            on st.sta_name equals user_.user_unit
                            into temp from tt in temp.DefaultIfEmpty()
                            select new  StaionInfo{ rtu_id =st.rtu_id,rtu_name=st.rtu_name,rtu_region=st.rtu_region,rtu_type=st.rtu_type,rtu_unit=st.rtu_unit,SIM=st.SIM,sta_name=st.sta_name,name=tt==null?"":tt.name,user_tel=tt==null?"":tt.user_tel};

          
             

            if (!string.IsNullOrEmpty(searchString)) 
            {

                //searchString = filter;
                stationifos = stationifos.Where(st => st.sta_name.Contains(searchString));
            }
            stationifos = stationifos.OrderBy(u=>u.rtu_id);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(stationifos.ToPagedList(pageNumber, pageSize));
          
        }

        [HttpPost]
        public ActionResult Index(StationTab stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StationTab stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
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
            ViewBag.role = Session["role"].ToString().Trim();
            StationTab st = dbcontext.StationTab.Find(id.ToString());
            st.rtu_id = st.rtu_id.Trim();
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(StationTab stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            dbcontext.Entry(stationtab).State= System.Data.Entity.EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string  id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            StationTab stationtab = dbcontext.StationTab.Find(id.ToString());
            return View(stationtab);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            StationTab stationtab = dbcontext.StationTab.Find(id.ToString());
            ViewBag.role = Session["role"].ToString();
            dbcontext.StationTab.Remove(stationtab);
            dbcontext.Entry(stationtab).State = System.Data.Entity.EntityState.Deleted;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(string id)
        {
            ViewBag.role = Session["role"].ToString();
            //页面跳转到data视图
            Session["staid"] = id;
            return RedirectToAction("Index", "Data");
            
        }

    }
}