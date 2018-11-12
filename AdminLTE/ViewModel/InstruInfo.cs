using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLTE.ViewModel
{
    public class InstruInfo
    {
        [Display(Name = "自增id")]
        public int id { get; set; }
        [Display(Name = "设备名称")]
        public string instru_name { get; set; }
        [Display(Name = "RTU站点编号")]
        public string rtu_id { get; set; }
        [Display(Name = "类型编码")]
        public string instru_type { get; set; }
        [Display(Name = "状态")]
        public string instru_state { get; set; }
        [Display(Name = "设备类型")]
        public string other { get; set; }

        [Display(Name = "数据值")]
        public string instru_value { get; set; }
        [Display(Name = "时间")]
        public Nullable<System.DateTime> instru_time { get; set; }



    }
    public class RealTIMEInfo
    {
       
        public string statu { get; set; }
      
       public monitordata dt { get; set; }

    }
}