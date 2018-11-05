using AdminLTE.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class CommandController : Controller
    {
        TempEntities dbcontext = new TempEntities();
        // GET: Command
        public ActionResult Index()
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
        }
        public ActionResult DeviceList(string searchkey)
        {
            //全部数据
            var datas = from st in dbcontext.StationTab
                        select st;
            if (!String.IsNullOrEmpty(searchkey))
            {
               datas=  datas.Where(d => d.rtu_name.Contains(searchkey));
            }
            return Json(datas.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeviceInfo(string deviceid)
        {
            //全部数据
            StationTab stationtab = dbcontext.StationTab.Find(deviceid.ToString());
            
            return Json(stationtab, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeviceRealTime(string stu_id,string stu_type)
        {
            //全部数据
            // StationTab stationtab = dbcontext.StationTab.Find(deviceid.ToString());
            //string cc = cmd + " " + "dsasadas" + " " + "asdsadsadsad";
            string socketstatu= SynchronousSocketClient.StartClient(stu_type+" "+stu_id);
            //请求数据库里最新的一行数据
            var datas = from st in dbcontext.DataTab
                        select st;
            //按最新时间查找对应的数据 
            datas = datas.Where(d => d.rtu_id.Contains(stu_id)).OrderByDescending(r => r.rtu_time);
            DataTab dt = null;
            if (datas.Count()!= 0)
            {
                 dt = datas.ToList()[0];
            }
           
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
}