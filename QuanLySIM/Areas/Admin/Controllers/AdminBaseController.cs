using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Data;
using QuanLySIM.Services;

namespace QuanLySIM.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        protected static IDbService Db { get; private set; }

        public AdminBaseController() { }

        public AdminBaseController(IDbService db)
        {
            if(Db == null)
                Db = db;
        }

        protected const String NOT_ALLOWED_AREA = "~/Areas/Admin/Views/Shared/_NotAllowedArea.cshtml";
        protected const String MUST_LOGIN_AREA = "~/Areas/Admin/Views/Shared/_MustLoginArea.cshtml";

        protected bool IsNotLogin()
        {            
            return Request.Cookies["user_login_id"] == null || Request.Cookies["user_role"] == null;
            //return Session["user_login_id"] == null && Session["user_role"] == null;
        }

        protected bool IsLogin()
        {            
            return (Request.Cookies["user_login_id"] != null && Request.Cookies["user_login_id"].Value != null) && (Request.Cookies["user_role"] != null && Request.Cookies["user_role"].Value != null);
            //return Session["user_login_id"] != null && Session["user_role"] != null;
        }

        protected bool IsAdmin()
        {            
            return Request.Cookies["user_role"] != null && Request.Cookies["user_role"].Value == "Admin";
            //return Session["user_role"].ToString() == "Admin";
        }

        protected bool IsStaff()
        {            
            return Request.Cookies["user_role"] != null && Request.Cookies["user_role"].Value == "Staff";
            //return Session["user_role"].ToString() == "Staff";
        }

        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }
    }
}