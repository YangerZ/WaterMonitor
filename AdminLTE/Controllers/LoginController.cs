using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class LoginController : Controller
    {
        TempEntities dbcontext = new TempEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            Session.Clear();
            ViewBag.LoginState = "未登录";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string username = fc["inputusername"];
            string pwd = fc["inputpwd"];
           
            var user = dbcontext.user.Where(b => b.user_name == username & b.user_password == pwd);
            if (user.Count() > 0)
            {
               
                var   usertab=   user.SingleOrDefault();
                Session["role"] = usertab.user_role;
                Session["username"] = usertab.user_name;
                Session["unit"] = usertab.user_unit;
                return RedirectToAction("Index", "Rtu");
            }
            else
            {
                ViewBag.LoginState = username + "用户密码不正确";
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
            return RedirectToAction("Login", "Login");
            //return View();
        }

        #region
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
        #endregion
    }
}