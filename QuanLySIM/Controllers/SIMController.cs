using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Entities;
using QuanLySIM.Services;

namespace QuanLySIM.Controllers
{
    public class SIMController : BaseController
    {
        public SIMController(IDbService db) : base(db) { }

        //
        // GET: /SIM/

        public ActionResult Index()
        {
            ViewBag.SimCount = Db.SIM.Count;

            return View(Db.SIM.All);
        }

        //
        // GET: /SIM/Details/5
        public ActionResult Details(int id)
        {
            SIM dbSIM = Db.SIM.Find(id);

            if (dbSIM.SimId == 0)
                throw new HttpException(404, String.Empty);

            ViewBag.IsOrderReachLimit = Db.Order.IsCustomerOutOfOrderTimes(base.MaKH);

            return View(dbSIM);
        }

        //
        // GET: /SIM/Order/5

        public ActionResult Order(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            ViewBag.IsSIMSold = false;
            ViewBag.IsSIMOrdered = false;
            ViewBag.IsOrderReachLimit = false;
                        
            SIM dbSIM = Db.SIM.Find(id);

            if (dbSIM.SimId == 0)
                throw new HttpException(404, String.Empty);

            if (dbSIM.TinhTrang == SIM.SOLD)
            {
                ViewBag.IsSIMSold = true;

                return View();
            }
            else if (dbSIM.TinhTrang == SIM.NOT_PAID)
            {
                ViewBag.IsSIMOrdered = true;

                return View();
            }
            else if (Db.Order.IsCustomerOutOfOrderTimes(base.MaKH))
            {
                ViewBag.IsOrderReachLimit = true;

                return View();
            }

            ViewBag.KhachHang = Db.Customer.Find(MaKH);

            return View(dbSIM);
        }

        //
        // POST: /SIM/Order/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(SIM sim)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (sim.SimId == 0)
                throw new HttpException(404, String.Empty);

            if (Db.Order.IsCustomerOutOfOrderTimes(base.MaKH))
            {
                ViewBag.IsOrderReachLimit = true;

                return View();
            }
            else if(Db.Order.IsOrdered(sim.SimId))
            {
                ViewBag.IsSIMOrdered = true;

                return View();
            }

            SIM dbSIM = Db.SIM.Find(sim.SimId);

            if (dbSIM.SimId == 0)
                throw new HttpException(404, String.Empty);

            PhieuMua newOrder = new PhieuMua();
            newOrder.SimId = dbSIM.SimId;
            newOrder.MaKH = MaKH;

            if (Db.Order.LittleSave(newOrder) <= 0) 
                return RedirectToAction("OrderError");
            
            dbSIM.TinhTrang = SIM.NOT_PAID;

            Db.SIM.Save(dbSIM);

            Db.Customer.UpdateOrderAmount(base.MaKH, Increase: true);

            return RedirectToAction("OrderSuccess");            
        }

        //
        // POST: /SIM/CancelOrder/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int OrderCancel(int id)
        {
            if (IsNotLogin())
                return -1;

            PhieuMua dbOrder = Db.Order.FindOrderOfCustomerBySIM(base.MaKH, id);

            if (Db.Order.Delete(dbOrder.MaPM) > 0)
            {
                dbOrder.SIM.TinhTrang = SIM.AVAILABLE;
                Db.SIM.Save(dbOrder.SIM);
                Db.Customer.UpdateOrderAmount(base.MaKH, Increase: false);

                return Db.Order.FindCustomerOrderSIMs(base.MaKH).Count();
            }

            return -1;
        }

        //
        // GET: /SIM/OrderSuccess
        public ActionResult OrderSuccess()
        {
            return View();
        }

        //
        // GET: /SIM/OrderError
        public ActionResult OrderError()
        {
            return View();
        }
    }
}
