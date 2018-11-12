using AdminLTE.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class InstrumentController : Controller
    {
        TempEntities dbcontext = new TempEntities();
        // GET: Instrument
        //把other 存储仪器的中文名称 然后按照数字存储在type下  显示时显示other 创建或更新时中文提交到other 类型更新至type
        //Query
        public ActionResult Index(string rtu_id, string searchString, string filter, int? page)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            ViewBag.username = Session["username"].ToString();
            ViewBag.unit = Session["unit"].ToString();
            Session["instru_name"] = null;
            ViewBag.rtu_id = rtu_id;
            //此处需要确认是rtu页面的跳转还是直接左侧点击的跳转

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = filter;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Filter = searchString;
            var instruments = dbcontext.instrument.OrderBy(u => u.id);
            //查找对应id
            if (!String.IsNullOrEmpty(rtu_id))
            {
                instruments = instruments.Where(instru => instru.rtu_id == rtu_id).OrderBy(u => u.id);
            }
            //过滤仪器名称条件
            if (!string.IsNullOrEmpty(searchString))
            {


                //searchString = filter;
                var instrus = instruments.Where(instru => instru.instru_name.Contains(searchString));
                if(instrus.Count()>0)
                {
                    IList<InstruInfo> infos = new List<InstruInfo>();
                    foreach (var item in instrus)
                    {
                        
                        var monitors = dbcontext.monitordata.Where(d => d.instru_name == item.instru_name).OrderByDescending(r => r.instru_time).FirstOrDefault();
                        if (monitors != null)
                            infos.Add(new InstruInfo { id=item.id,rtu_id=item.rtu_id,other=item.other,instru_name=item.instru_name,instru_type=item.instru_type,instru_state=item.instru_state,instru_time=monitors.instru_time,instru_value=monitors.instru_value});
                         else
                            infos.Add(new InstruInfo { id = item.id, rtu_id = item.rtu_id, other = item.other,instru_name = item.instru_name, instru_type = item.instru_type, instru_state = item.instru_state, instru_time =null, instru_value = null });
                    }
                    return View(infos.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    IList<InstruInfo> infos = new List<InstruInfo>();
                    return View(infos.ToPagedList(pageNumber, pageSize));
                }
               

                
            }
            else
            {
                if(instruments.Count()>0)
                {
                    IList<InstruInfo> infos = new List<InstruInfo>();
                    foreach (var item in instruments)
                    {
                        var monitors = dbcontext.monitordata.Where(d => d.instru_name == item.instru_name).OrderByDescending(r => r.instru_time).FirstOrDefault();
                        if(monitors!=null)
                        {
                            infos.Add(new InstruInfo { id = item.id, rtu_id = item.rtu_id, other = item.other, instru_name = item.instru_name, instru_type = item.instru_type, instru_state = item.instru_state, instru_time = monitors.instru_time, instru_value = monitors.instru_value });

                        }
                        else
                        {
                            infos.Add(new InstruInfo { id = item.id, rtu_id = item.rtu_id, other = item.other, instru_name = item.instru_name, instru_type = item.instru_type, instru_state = item.instru_state, instru_time = null, instru_value = null });
                        }
                       
                    }
                    return View(infos.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    IList<InstruInfo> infos = new List<InstruInfo>();
                    return View(infos.ToPagedList(pageNumber, pageSize));
                }
            }
        }

        [HttpPost]
        public ActionResult Index(rtu stationtab)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
             
        }
        //Add 
        public ActionResult Create()
        {
            ViewBag.role = Session["role"].ToString().Trim();
            return View();
        }
        [HttpPost]
        public ActionResult Create(instrument instru)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            if (ModelState.IsValid)
            {
                try
                {
                    instru.instru_state = "0";
                    instru.instru_type=ConvertZHTypeToNumber(instru.other);
                    dbcontext.instrument.Add(instru);
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
        //Delete
        public ActionResult Delete(int id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            instrument instru = dbcontext.instrument.Find(id);
            return View(instru);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            instrument instru = dbcontext.instrument.Find(id);

            dbcontext.instrument.Remove(instru);
            dbcontext.Entry(instru).State = System.Data.Entity.EntityState.Deleted;

            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        //Update
        public ActionResult Edit(int id)
        {
            ViewBag.role = Session["role"].ToString().Trim();
            instrument st = dbcontext.instrument.Find(id);
             
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit(instrument instru)
        {
            ViewBag.role = Session["role"].ToString().Trim();
             
            string t = ConvertZHTypeToNumber(instru.other);
            instru.instru_type = t;
            dbcontext.Entry(instru).State = System.Data.Entity.EntityState.Modified;
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        //Details=>Data view 
        public string ConvertZHTypeToNumber(string inputtype)
        {
            string type = "";
            if(String.IsNullOrEmpty(inputtype))
            {
                return "";
            }
            if (inputtype.Contains("位"))
            {
                type = "0";
            }
            if (inputtype.Contains("量"))
            {
                type = "1";
            }
            if (inputtype.Contains("流"))
            {
                type = "2";
            }
            if (inputtype.Contains("压"))
            {
                type = "3";
            }
            if (inputtype.Contains("温"))
            {
                type = "4";
            }
            if (inputtype.Contains("ph"))
            {
                type = "5";
            }
            if (inputtype.Contains("浊"))
            {
                type = "6";
            }
            return type;

        }
        public string ConvertNumberTypeToZh(string inputtype)
        {
            string type = "";
            if (String.IsNullOrEmpty(inputtype))
            {
                return "";
            }
            
            switch (inputtype)
            {
                case "0":
                    type = "水位计";
                    break;
                case "1":
                    type = "水量计";
                    break;
                case "2":
                    type = "流速计";
                    break;
                case "3":
                    type = "压力计";
                    break;
                case "4":
                    type = "温度计";
                    break;
                case "5":
                    type = "酸碱度计";
                    break;
                case "6":
                    type = "浊度计";
                    break;
            }
            return type;

        }

    }
}