using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AdminLTE.ViewModel
{
    public class StationInfo
    {
        [Display(Name = "自增号")]
        public int id { get; set; }
        [Display(Name = "设备编号")]
        public string rtu_id { get; set; }
        [Display(Name = "设备名称")]
        public string rtu_name { get; set; }
        [Display(Name = "安装地点")]
        public string rtu_region { get; set; }
        [Display(Name = "SIM卡号")]

        public string rtu_sim{ get; set; }
        [Display(Name = "检测点名称")]
         
        public string rtu_type { get; set; }
        [Display(Name = "安装单位")]
        public string rtu_unit { get; set; }

        [Display(Name = "联系人")]
        public string name { get; set; }
        [Display(Name = "电话")]
        public string user_tel { get; set; }
    }
}