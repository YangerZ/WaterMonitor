using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLTE.ViewModel
{
    public class StaionInfo
    {
        [Display(Name = "设备编号")]
        public string rtu_id { get; set; }
        [Display(Name = "设备名称")]
        public string rtu_name { get; set; }
        [Display(Name = "安装地点")]
        public string rtu_region { get; set; }
        [Display(Name = "检测点名称")]
        public string sta_name { get; set; }
        [Display(Name = "SIM卡号")]
        public string SIM { get; set; }
        [Display(Name = "传感器类型号")]
        public string rtu_type { get; set; }
        [Display(Name = "安装单位")]
        public string rtu_unit { get; set; }

        [Display(Name = "联系人")]
        public string name { get; set; }
        [Display(Name = "电话")]
        public string user_tel { get; set; }

    }
    public class RealTIMEInfo
    {
       
        public string statu { get; set; }
      
       public DataTab dt { get; set; }

    }
}