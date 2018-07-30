using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AdminLTE.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AdminLTE.DAL
{
    //DataBase first  ModelFirst CodeFirst 
    //This is Code first

    //Query LINQ to Entities 表达式和函数两种方式
    public class AccountContext:DbContext
    {
        public AccountContext():base("AccountContext")
        {

        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // base.OnModelCreating(modelBuilder);
        }

    }
}