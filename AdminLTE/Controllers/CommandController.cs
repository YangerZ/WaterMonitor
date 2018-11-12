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
            var datas = from rtu in dbcontext.rtu
                        select rtu;
            if (!String.IsNullOrEmpty(searchkey))
            {
               datas=  datas.Where(d => d.rtu_name.Contains(searchkey));
            }
            return Json(datas.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeviceInfo(string deviceid)
        {
            //全部数据
            var rtus = dbcontext.rtu.Where(rtu=>rtu.rtu_id==deviceid.ToString());
            
            return Json(rtus.ToList(), JsonRequestBehavior.AllowGet);
        }

        //查询当前RTU下某一类设备类型数据
        public ActionResult DeviceRealTime(string rtu_id,string instru_type)
        {
            //全部数据
            //socket 通讯
           // string socketstatu= SynchronousSocketClient.StartClient(instru_type + " "+ rtu_id);
            //按最新时间查找对应的数据 
            var instru1 = dbcontext.instrument.Where(d => d.rtu_id.Contains(rtu_id)).OrderByDescending(r => r.id);
            //请求数据库里最新的一行数据
            var datas = from d in instru1
                        where d.instru_type.Equals(instru_type)
                        select d;
            if (datas == null||datas.Count()==0)
            {

                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            List<monitordata> results = new List<monitordata>();
            foreach (var item in datas)
            {
                var d = dbcontext.monitordata.Where(m => m.instru_name == item.instru_name);
                if(d.Count()==0)
                {
                    continue;
                }
                monitordata dd= d.OrderByDescending(m => m.instru_time).First();

                results.Add(dd);
            }
            
           
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}