using AdminLTE.DAL;
using AdminLTE.Models;
using AdminLTE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace AdminLTE.Controllers
{
    public class AccountController : Controller
    {

        private AccountContext db = new AccountContext();
        // GET: Account
        public ActionResult Index(string sortOrder,string searchKey,string curFilter,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            //test 
            ISysUserRepository ur = new ClsSysUserRepository();
            var user = ur.SelectAll();
            
            if (!string.IsNullOrEmpty(searchKey))
            {
                page = 1;
                user = user.Where(u => u.UserName.Contains(searchKey));
            }
            else
            {
                searchKey = curFilter;
            }
            ViewBag.CurrentFilter = searchKey;
            switch(sortOrder)
            {
                case "name_desc":
                    user = user.OrderByDescending(u => u.UserName);
                    break;
                default:
                    user = user.OrderBy(u => u.UserName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(user.ToPagedList(pageNumber,pageSize));
            //return View(user.ToList());
        }

        public ActionResult Details(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            
            return View(sysUser);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
                db.SysUsers.Add(sysUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }
        [HttpPost]
        public ActionResult Edit(SysUser sysUser)
        {
            db.Entry(sysUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysUser);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "未登录";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["inputemail"];
            string pwd = fc["inputpwd"];
            //Verify User confirm
            //ViewBag.LoginState = email + "欢迎";
            //Db EF
            //Query
            //var allusers = from u in db.SysUsers
            //               where u.UserName=="Tom"
            //               select u;//表达式
            //allusers = db.SysUsers.Where(u=>u.UserName=="Tom"); //函数方式
            //sort and pages 先排序才能分页
            //var users = (from u in db.SysUsers
            //             orderby u.UserName
            //             select u).Skip(0).Take(5);//表达式
            //users = db.SysUsers.OrderBy(u => u.UserName).Skip(0).Take(5);
            //aggregate query 
            //var num = db.SysUsers.Count();
            //var minId = db.SysUsers.Min(u => u.ID);
            //join union query
            //1.return sysuserrole modelclass
            //var singleuser = from ur in db.SysUserRoles
            //           join u in db.SysUsers
            //           on ur.SysUserID equals u.ID
            //           select ur;
            //2. return other column from different table
            var user = db.SysUsers.Where(b => b.Email == email & b.PassWord == pwd);
            if(user.Count()>0)
            {
                ViewBag.LoginState = "欢迎" + email;
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ViewBag.LoginState = email + "用户不存在";
                return View();
            }
            //
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            return RedirectToAction("Login", "Account");
            //return View();
        }

        public ActionResult EFUpdateData()
        {
            var sysUser = db.SysUsers.FirstOrDefault(u => u.UserName == "Tom");
            if (sysUser != null)
            {
                sysUser.UserName = "Tom2";
            }
            db.SaveChanges();
            return View();
        }

        public ActionResult EFAddOrDeleteData()
        {
            //add
            var newSysUser = new SysUser()
            {
                UserName = "Scott",
                Email = "Scott@126.com",
                PassWord = "sct",

            };
            db.SysUsers.Add(newSysUser);
            db.SaveChanges();
            //delete
            var deluser = db.SysUsers.FirstOrDefault(u => u.UserName == "Scott");
            if (deluser != null)
            {
                db.SysUsers.Remove(deluser);
            }
            db.SaveChanges();
            return View("EFQueryData");
        }
        //
        //RedirectToAction("Index");//一个参数时在本Controller下，不传入参数。
        //RedirectToAction(ActionName, ControllerName) //可以直接跳到别的Controller.
        //RedirectToRoute(new {controller="Home",action="Index"});//可跳到其他controller
        //RedirectToRoute(new {controller="Home",action="Index"， id=param
        //});//可跳到其他controller,带参数。
        //Response.Redirect("Index?id=1");//适用于本controller下的方法名称,可带参数。
        //return Redirect("Index");//适用于本controller下的方法名称。
        //return View("Index"); //直接显示对应的页面 不经过执行Controller的方法。 
        //return View("~/Views/Home/Index.aspx");//这种方法是写全路径,直接显示页面,不经过Controller方法
        //return View();//直接显示页面,不经过Controller方法
    }
}