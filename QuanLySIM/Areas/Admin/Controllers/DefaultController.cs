using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Entities;
using QuanLySIM.Data;
using QuanLySIM.Services;

namespace QuanLySIM.Areas.Admin.Controllers
{
    public class DefaultController : AdminBaseController
    {
        public DefaultController(IDbService db) : base(db) { }

        //
        // GET: /Default/Admin/

        public ActionResult Index()
        {
            if (IsNotLogin())
                return RedirectToAction("Login");

            return View();
        }

        //
        // GET: /Default/Login

        public ActionResult Login()
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);

            return View(new NhanVien());
        }

        //
        // POST: /Default/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NhanVien Staff)
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);

            if (!ModelState.IsValidField("TenTK") || !ModelState.IsValidField("MatKhau"))
                return View(Staff);

            String role = String.Empty;
            int staffId = Db.Staff.Available(Staff.TenTK, Staff.MatKhau, out role);

            if (staffId != 0)
            {
                DateTime cookieTime = DateTime.Now.AddDays(1);

                HttpCookie cookUserId = new HttpCookie("user_login_id", staffId.ToString());
                cookUserId.Expires = cookieTime;

                HttpCookie cookUserRole = new HttpCookie("user_role", role);
                cookUserRole.Expires = cookieTime;
                                
                Response.Cookies.Add(cookUserId);
                Response.Cookies.Add(cookUserRole);
                
                //Session["user_login_id"] = staffId;
                //Session["user_role"] = role;

                if (Request.QueryString["ref"] != null)
                    return RedirectPermanent(Request.QueryString["ref"]);

                return RedirectToAction("Index");
            }

            ViewBag.FailMsg = "Sai thông tin đăng nhập";

            return View(Staff);
        }

        //
        // GET: /Default/Logout
        public ActionResult Logout()
        {
            if (IsNotLogin())
                return View(NOT_ALLOWED_AREA);

            HttpCookie cookUserId = Request.Cookies["user_login_id"];
            cookUserId.Value = null;
            cookUserId.Expires = DateTime.Now.AddDays(-1);

            HttpCookie cookUserRole = Request.Cookies["user_role"];
            cookUserRole.Value = null;
            cookUserRole.Expires = DateTime.Now.AddDays(-1);

            if (Request.Cookies["user_login_id"] == null)
                Response.Cookies.Add(cookUserId);
            else
                Response.Cookies.Set(cookUserId);

            if (Request.Cookies["user_role"] == null)
                Response.Cookies.Add(cookUserRole);
            else
                Response.Cookies.Set(cookUserRole);

            Request.Cookies.Remove("user_login_id");
            Request.Cookies.Remove("user_role");

            //Session["user_login_id"] = null;
            //Session["user_role"] = null;

            return View();
        }

        //
        // GET: /Default/PageNotFound
        public ActionResult PageNotFound()
        {
            return View();
        }   

    }
}
