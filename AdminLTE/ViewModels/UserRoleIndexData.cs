using AdminLTE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLTE.ViewModels
{
    public class UserRoleIndexData
    {
        public IEnumerable<SysUser> SysUsers { get; set; }
        public IEnumerable<SysUserRole> SysUserRoels { get; set; }
        public IEnumerable<SysRole> SysRoles { get; set; }
    }
}