using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Entities;
using QuanLySIM.Data;
using QuanLySIM.Services;

namespace QuanLySIM.Areas.Admin.Controllers
{ 
    public class SIMController : AdminBaseController
    {
        public SIMController(IDbService db) : base(db) { }

        //
        // GET: /Admin/SIM/

        public ViewResult Index()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            return View(Db.SIM.All);
        }

        //
        // GET: /Admin/SIM/Details/5

        public ViewResult Details(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            SIM dbSIM = Db.SIM.Find(id);

            if (dbSIM.SimId == 0)
                throw new HttpException(404, String.Empty);

            return View(dbSIM);
        }

        //
        // GET: /Admin/SIM/Create

        public ActionResult Create()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            return View();
        } 

        //
        // POST: /Admin/SIM/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SIM NewSIM)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!ModelState.IsValid)
                return View(NewSIM);        

            if (Db.SIM.IsSerialExist(NewSIM.MaSIM))
            {
                ViewBag.FailMsg = "Mã SIM đã tồn tại trên hệ thống";

                return View(NewSIM);
            }

            if (Db.SIM.IsNumExist(NewSIM.SoThueBao))
            {
                ViewBag.FailMsg = "Số thuê bao đã tồn tại trên hệ thống";

                return View(NewSIM);
            }

            Db.SIM.Save(NewSIM);

            return RedirectToAction("Index");
        }
        
        //
        // GET: /Admin/SIM/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            SIM dbSIM = Db.SIM.Find(id);

            if (dbSIM.SimId == 0)
                throw new HttpException(404, String.Empty);
            
            ViewBag.TinhTrang = new SelectList(dbSIM.CacTinhTrang, "Value", "Text", dbSIM.TinhTrang);

            return View(dbSIM);
        }

        //
        // POST: /Admin/SIM/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SIM OldSIM)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (OldSIM.SimId == 0)
                throw new HttpException(404, String.Empty);

            if (!ModelState.IsValid)
            {
                ViewBag.TinhTrang = new SelectList(OldSIM.CacTinhTrang, "Value", "Text", OldSIM.TinhTrang);

                return View(OldSIM);
            }

            Db.SIM.Save(OldSIM);

            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/SIM/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (Db.SIM.Find(id).SimId == 0)
                throw new HttpException(404, String.Empty);

            Db.SIM.Delete(id);

            return RedirectToAction("Index");
        }
    }
}