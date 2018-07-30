using AdminLTE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdminLTE.DAL
{
    public class AccountInitializer:DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser {UserName="Tom",Email="Tom@souhu.com",PassWord="123"  },
                new SysUser {UserName="Jerry",Email="Jerry@souhu.com",PassWord="456"  }
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();
            var sysRoles = new List<SysRole>
            {
                new SysRole {RoleName="Admin",RoleDesc="Administrator all" },
                new SysRole {RoleName="GeneralUser",RoleDesc="Common User" }
            };
            sysRoles.ForEach(m => context.SysRoles.Add(m));
            context.SaveChanges();
        }
    }
}