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
    public class OrderController : AdminBaseController
    {
        public OrderController(IDbService db) : base(db) { }

        //
        // GET: /Admin/Order/

        public ViewResult Index()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            return View(Db.Order.All);
        }

        //
        // GET: /Admin/Order/Details/5

        public ViewResult Details(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            PhieuMua dbOrder = Db.Order.Find(id);

            if (dbOrder.MaPM == 0)
                throw new HttpException(404, String.Empty);

            return View(dbOrder);
        }

        //
        // GET: /Admin/Order/Create

        public ActionResult Create()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            ViewBag.SimId = new SelectList(Db.SIM.All, "SimId", "SoThueBao");            
            ViewBag.MaKH = new SelectList(Db.Customer.All, "MaKH", "TenKH");

            return View();
        } 

        //
        // POST: /Admin/Order/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhieuMua NewOrder)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            IEnumerable<SIM> dbsSIM = Db.SIM.All;
            IEnumerable<KhachHang> dbsCustomer = Db.Customer.All;

            if (!ModelState.IsValid)
            {
                ViewBag.SimId = new SelectList(dbsSIM, "SimId", "SoThueBao", NewOrder.SimId);
                ViewBag.MaKH = new SelectList(dbsCustomer, "MaKH", "TenKH", NewOrder.MaKH);

                return View(NewOrder);
            }

            if (Db.Order.IsOrdered(NewOrder.SimId))
            {
                ViewBag.FailMsg = "SIM này đã được đặt hàng";
                ViewBag.SimId = new SelectList(dbsSIM, "SimId", "SoThueBao", NewOrder.SimId);
                ViewBag.MaKH = new SelectList(dbsCustomer, "MaKH", "TenKH", NewOrder.MaKH);

                return View(NewOrder);
            }

            if (Db.Order.Save(NewOrder) <= 0)
            {
                ViewBag.FailMsg = "Có lỗi trong quá trình xử lý";
                ViewBag.SimId = new SelectList(dbsSIM, "SimId", "SoThueBao", NewOrder.SimId);
                ViewBag.MaKH = new SelectList(dbsCustomer, "MaKH", "TenKH", NewOrder.MaKH);

                return View(NewOrder);                
            }

            SIM dbSIM = Db.SIM.Find(NewOrder.SimId);
            dbSIM.TinhTrang = SIM.NOT_PAID;
                        
            Db.SIM.Save(dbSIM);
            Db.Customer.UpdateOrderAmount(NewOrder.MaKH, true);

            return RedirectToAction("Index");
        }
        
        //
        // GET: /Admin/Order/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            PhieuMua dbOrder = Db.Order.Find(id);

            if (dbOrder.MaPM == 0)
                throw new HttpException(404, String.Empty);

            ViewBag.SimId = new SelectList(Db.SIM.All, "SimId", "SoThueBao", dbOrder.SimId);
            ViewBag.KhachHang = Db.Customer.Find(dbOrder.MaKH);

            return View(dbOrder);
        }

        //
        // POST: /Admin/Order/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhieuMua OldOrder)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (OldOrder.MaPM == 0)
                throw new HttpException(404, String.Empty);

            IEnumerable<SIM> dbsSIM = Db.SIM.All;

            if (!ModelState.IsValid)
            {
                ViewBag.SimId = new SelectList(dbsSIM, "SimId", "SoThueBao", OldOrder.SimId);
                ViewBag.KhachHang = Db.Customer.Find(OldOrder.MaKH);

                return View(OldOrder);
            }

            if (!Db.Order.IsTheSame(OldOrder.MaPM, OldOrder.SimId) && Db.Order.IsOrdered(OldOrder.SimId))
            {
                ViewBag.FailMsg = "SIM này đã được đặt hàng";
                ViewBag.SimId = new SelectList(dbsSIM, "SimId", "SoThueBao", OldOrder.SimId);
                ViewBag.KhachHang = Db.Customer.Find(OldOrder.MaKH);

                return View(OldOrder);
            }

            PhieuMua dbOrder = Db.Order.Find(OldOrder.MaPM);

            if (dbOrder.MaPM == 0)
                throw new HttpException(404, String.Empty);

            //Update sim ordered before
            dbOrder.SIM.TinhTrang = SIM.AVAILABLE;
            Db.SIM.Save(dbOrder.SIM);
            Db.Customer.UpdateOrderAmount(dbOrder.MaKH, false);

            //Update sim ordered currently
            SIM newOrderSIM = Db.SIM.Find(OldOrder.SimId);
            newOrderSIM.TinhTrang = SIM.NOT_PAID;
            Db.SIM.Save(newOrderSIM);
            Db.Customer.UpdateOrderAmount(OldOrder.MaKH, true);

            Db.Order.Save(OldOrder);

            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/Order/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            PhieuMua dbOrder = Db.Order.Find(id);

            if (dbOrder.MaPM == 0)
                throw new HttpException(404, String.Empty);

            dbOrder.SIM.TinhTrang = SIM.SOLD;

            Db.SIM.Save(dbOrder.SIM);
            Db.Customer.UpdateOrderAmount(dbOrder.MaKH, true);
            Db.Order.Delete(id);

            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/Order/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            PhieuMua dbOrder = Db.Order.Find(id);

            if (dbOrder.MaPM == 0)
                throw new HttpException(404, String.Empty);

            dbOrder.SIM.TinhTrang = SIM.AVAILABLE;

            Db.SIM.Save(dbOrder.SIM);

            Db.Customer.UpdateOrderAmount(dbOrder.MaKH, false);

            Db.Order.Delete(id);

            return RedirectToAction("Index");
        }
    }
}