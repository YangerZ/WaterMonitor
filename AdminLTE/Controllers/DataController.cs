using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace AdminLTE.Controllers
{
    public class DataController : Controller
    {
        TempEntities dbcontext = new TempEntities();
       
        // GET: Data
        [HttpPost]
        public ActionResult Index(string instru_name,FormCollection fc,string filter, int? page)
        {
            //ViewBag.username = Session["username"].ToString();
            //ViewBag.unit = Session["unit"].ToString();
            string stime = fc["t1_s"];
            string etime = fc["t1_e"];
            
            DateTime dt_start = Convert.ToDateTime(stime);
            DateTime dt_end = Convert.ToDateTime(etime);
            ViewBag.stime = stime;
            ViewBag.etime = etime;
           
            
            ViewBag.role = Session["role"].ToString().Trim();
            var datas = from st in dbcontext.monitordata
                           select st;

            if (Session["instru_name"] != null)
            {
                ViewBag.instru_name = Session["instru_name"].ToString();
                instru_name= Session["instru_name"].ToString();
            }
            if (!String.IsNullOrEmpty(instru_name))
            {
                datas = datas.Where(d => d.instru_name.Trim() == instru_name);
            }
            if (stime != null && etime != null) {
                datas = datas.Where(data => data.instru_time > dt_start &&data.instru_time < dt_end);
            }
            datas = datas.OrderBy(d => d.instru_time);
            int pageSize =5;
            int pageNumber = (page ?? 1);
            return View(datas.ToPagedList(pageNumber, pageSize));
            //return View(dbcontext.DataTab);
        }

        public ActionResult Index(string instru_name,string filter1,string filter2, int? page)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            Session["instru_name"] = instru_name;
            ViewBag.instru_name = instru_name;
            var datas = from st in dbcontext.monitordata
                        select st;
            
            if (!String.IsNullOrEmpty(instru_name)) {
                 
                datas = datas.Where(d => d.instru_name.Trim() == instru_name);
            }
            if (filter1 != null && filter2 != null) {
                DateTime dt_start = Convert.ToDateTime(filter1);
                DateTime dt_end = Convert.ToDateTime(filter2);
                datas = datas.Where(data => data.instru_time > dt_start && data.instru_time < dt_end);
            }
            datas = datas.OrderBy(d => d.instru_time);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(datas.ToPagedList(pageNumber, pageSize));
            
        }
         
        public ActionResult Chart()
        {
            //return new EmptyResult();
            ViewBag.role = Session["role"].ToString().Trim();
            //POST
            //var param1 = Request.Form["sel_type"];
            //GET
            string type = Request.QueryString["sel_type"];
            switch (type) {
                  case "water_level":
                    type = "0";
                    break;
                case "water_volume":
                    type = "1";
                    break;
                case "water_flow":
                    type = "2";
                    break;
                case "water_pressure":
                    type = "3";
                    break;
                case "water_tem":
                    type = "4";
                    break;
                case "water_ph":
                    type = "5";
                    break;
                case "water_turb":
                    type = "6";
                    break;
            }

            string stime = Request.QueryString["t2_s"];
            string etime = Request.QueryString["t2_e"];
            DateTime dt_start = Convert.ToDateTime(stime);
            DateTime dt_end = Convert.ToDateTime(etime);
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            string instru_name = "";
            if (Session["instru_name"] != null)
            {
                ViewBag.instru_name = Session["instru_name"].ToString();
                instru_name = Session["instru_name"].ToString();
            }

            //查找类型对应的设备  名称和类型均符合条件的 设备name
            var instrus = dbcontext.instrument.Where(instru => instru.instru_name == instru_name);
            var instrus1= instrus.Where(instru => instru.instru_type == type);
            if (instrus1 == null)
            {
                return new EmptyResult();
            }
            var datas = from d in dbcontext.monitordata
                              join instru_ in instrus1
                              on d.instru_name equals instru_.instru_name
                              select d;
            
            List<monitordata> re = new List<monitordata>();
            if (datas != null)
            {
                if (stime != null && etime != null)
                {
                    //过滤时间条件
                    var results = datas.Where(data => data.instru_time > dt_start && data.instru_time < dt_end);
                    //遍历转换

                    return Json(results.ToList(), JsonRequestBehavior.AllowGet);
                }
                 
                //遍历转换

                return Json(datas.ToList(), JsonRequestBehavior.AllowGet);
                
            }
            else
            {
                return new EmptyResult();
            }
          

        }

       
    }
}