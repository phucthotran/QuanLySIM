using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Entities;
using QuanLySIM.Services;

namespace QuanLySIM.Controllers
{
    public class DefaultController : BaseController
    {
        public DefaultController(IDbService db) : base(db) { }

        //
        // GET: /Default/

        public ActionResult Index()
        {
            IEnumerable<SIM> dbsSIM = Db.SIM.GetNewest();
            ViewBag.SimCount = dbsSIM.Count();

            return View(dbsSIM);
        }

        public ActionResult PageNotFound()
        {
            return View();
        }    

    }
}
