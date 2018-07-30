using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminLTE.Models
{
    public class SysUser
    {
        public int ID { get; set; }
        
        [StringLength(10,ErrorMessage ="别太长了名字不好记")]
        [Column("LoginName")]
        
        public string  UserName { get; set; }
        public string PassWord { get; set; }

        public string Email { get; set; }
     
        public int? SysDepartmentID { get; set; }

        public virtual SysDepartment SysDepartment { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
        
    }
}