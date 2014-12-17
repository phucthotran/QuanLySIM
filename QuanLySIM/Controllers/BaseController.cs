using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Data;
using QuanLySIM.Services;

namespace QuanLySIM.Controllers
{
    public class BaseController : Controller
    {
        protected static IDbService Db { get; private set; }

        public BaseController() { }

        public BaseController(IDbService db)
        {
            if(Db == null)
                Db = db;
        }

        protected const String NOT_ALLOWED_AREA = "~/Views/Shared/_NotAllowedArea.cshtml";
        protected const String MUST_LOGIN_AREA = "~/Views/Shared/_MustLoginArea.cshtml";               

        public int MaKH
        {
            get
            {
                if (Session["customer_login_id"] != null)
                    return Int32.Parse(Session["customer_login_id"].ToString());

                return 0;
            }
        }

        protected bool IsNotLogin()
        {
            return Session["customer_login_id"] == null;
        }

        protected bool IsLogin()
        {
            return Session["customer_login_id"] != null;
        }
                
        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }
    }
}