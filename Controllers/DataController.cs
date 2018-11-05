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
        public ActionResult Index(FormCollection fc,string filter, int? page)
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
            var datas = from st in dbcontext.DataTab
                           select st;
            string staid = Session["staid"].ToString();
            if (!String.IsNullOrEmpty(staid))
            {
                datas = datas.Where(d => d.rtu_id.Trim() == staid);
            }
            if (stime != null && etime != null) {
                datas = datas.Where(data => data.rtu_time > dt_start &&data.rtu_time<dt_end);
            }
            datas = datas.OrderBy(d => d.rtu_time);
            int pageSize =5;
            int pageNumber = (page ?? 1);
            return View(datas.ToPagedList(pageNumber, pageSize));
            //return View(dbcontext.DataTab);
        }

        public ActionResult Index(string filter1,string filter2, int? page)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            var datas = from st in dbcontext.DataTab
                        select st;
            string detail = Session["staid"].ToString();
            if (!String.IsNullOrEmpty(detail)) {
                
                datas = datas.Where(d => d.rtu_id.Trim() == detail);
            }
            if (filter1 != null && filter2 != null) {
                DateTime dt_start = Convert.ToDateTime(filter1);
                DateTime dt_end = Convert.ToDateTime(filter2);
                datas = datas.Where(data => data.rtu_time > dt_start && data.rtu_time < dt_end);
            }
            datas = datas.OrderBy(d => d.rtu_time);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(datas.ToPagedList(pageNumber, pageSize));
            
        }
         
        public ActionResult Chart()
        {
            ViewBag.role = Session["role"].ToString().Trim();
            //POST
            //var param1 = Request.Form["sel_type"];
            //GET
            string type = Request.QueryString["sel_type"];
            switch (type) {
                case "water_level":
                    type = "1";
                    break;
                case "water_flow":
                    type = "超声波流量计";
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
            //查找类型对应的设备id
            var ids = from st in dbcontext.StationTab
                        where st.rtu_type==type
                        select st.rtu_id;

           string staid = Session["staid"].ToString(); ;
            
            var datas = from d in dbcontext.DataTab
                        select d;
            if (!String.IsNullOrEmpty(staid))
            {
                datas = datas.Where(d => d.rtu_id.Trim() == staid);
            }
            List<DataTab> re = new List<DataTab>();
            if (stime != null && etime != null)
            {
                //过滤时间条件
                datas = datas.Where(data => data.rtu_time > dt_start && data.rtu_time < dt_end);
            }

            if (datas != null)
            {
                //查找设备id对应的数据
               
                foreach (var item in ids.ToList())
                {
                   var  data = datas.Where(d => d.rtu_id.Trim() == item.Trim());
                    re.AddRange(data.ToList());
                }
                
                
                return Json(re, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new EmptyResult();
            }

             
        }

       
    }
}