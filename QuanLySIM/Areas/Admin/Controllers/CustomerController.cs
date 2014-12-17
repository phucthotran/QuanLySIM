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
    public class CustomerController : AdminBaseController
    {
        public CustomerController(IDbService db) : base(db) { }

        //
        // GET: /Admin/Customer/

        public ViewResult Index()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            return View(Db.Customer.All);
        }

        //
        // GET: /Admin/Customer/Details/5

        public ViewResult Details(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            KhachHang dbCustomer = Db.Customer.Find(id);

            if (dbCustomer.MaKH == 0)
                throw new HttpException(404, String.Empty);

            dbCustomer.PhieuMua = Db.Order.FindOrdersByCustomer(id).ToList();

            return View(dbCustomer);
        }

        //
        // GET: /Admin/Customer/Create
        
        public ActionResult Create()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            List<NhanVien> dbsStaff = Db.Staff.All.ToList();
            dbsStaff.Insert(0, new NhanVien { MaNV = 0, TenNV = "Chọn Ngẫu Nhiên" });

            ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV");

            return View(new KhachHang());
        } 

        //
        // POST: /Admin/Customer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KhachHang NewCustomer)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            List<NhanVien> dbsStaff = Db.Staff.All.ToList();
            dbsStaff.Insert(0, new NhanVien { MaNV = 0, TenNV = "Chọn Ngẫu Nhiên" });

            if (!ModelState.IsValid)
            {
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", NewCustomer.MaNV);

                return View(NewCustomer);
            }

            if (Db.Customer.IsAccountExist(NewCustomer.TenTK))
            {
                ViewBag.FailMsg = "Tên tài khoản đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", NewCustomer.MaNV);

                return View(NewCustomer);
            }

            if (Db.Customer.IsEmailExist(NewCustomer.Email))
            {
                ViewBag.FailMsg = "Email đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", NewCustomer.MaNV);

                return View(NewCustomer);
            }

            if (Db.Customer.IsIdCardExist(NewCustomer.CMND))
            {
                ViewBag.FailMsg = "Số CMND đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", NewCustomer.MaNV);

                return View(NewCustomer);
            }

            if (Db.Customer.IsPhoneNumExist(NewCustomer.SDT))
            {
                ViewBag.FailMsg = "SĐT đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", NewCustomer.MaNV);

                return View(NewCustomer);
            }
                            
            if (NewCustomer.MaNV == 0)
            {
                List<int> dbsStaffIds = Db.Staff.GetStaffIds().ToList();
                Random rd = new Random();

                NewCustomer.MaNV = dbsStaffIds[rd.Next(dbsStaffIds.Count)];
            }

            if(Db.Customer.Save(NewCustomer) <= 0) {
                ViewBag.FailMsg = "Có lỗi trong quá trình xử lý";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", NewCustomer.MaNV);

                return View(NewCustomer);
            }

            return RedirectToAction("Index");          
        }
        
        //
        // GET: /Admin/Customer/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            KhachHang dbCustomer = Db.Customer.Find(id);

            if (dbCustomer.MaKH == 0)
                throw new HttpException(404, String.Empty);

            List<NhanVien> dbsStaff = Db.Staff.All.ToList();
            dbsStaff.Insert(0, new NhanVien { MaNV = 0, TenNV = "Chọn Ngẫu Nhiên" });                       

            ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", dbCustomer.MaNV);

            return View(dbCustomer);
        }

        //
        // POST: /Admin/Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KhachHang OldCustomer)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (OldCustomer.MaKH == 0)
                throw new HttpException(404, String.Empty);

            List<NhanVien> dbsStaff = Db.Staff.All.ToList();
            dbsStaff.Insert(0, new NhanVien { MaNV = 0, TenNV = "Chọn Ngẫu Nhiên" });

            if (!ModelState.IsValid)
            {
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", OldCustomer.MaNV);

                return View(OldCustomer);
            }

            KhachHang dbCustomer = Db.Customer.Find(OldCustomer.MaKH);

            if (dbCustomer.MaKH == 0)
                throw new HttpException(404, String.Empty);

            if (dbCustomer.TenTK != OldCustomer.TenTK && Db.Customer.IsAccountExist(OldCustomer.TenTK))
            {
                ViewBag.FailMsg = "Tên tài khoản đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", OldCustomer.MaNV);

                return View(OldCustomer);
            }

            if (dbCustomer.Email != OldCustomer.Email && Db.Customer.IsEmailExist(OldCustomer.Email))
            {
                ViewBag.FailMsg = "Email đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", OldCustomer.MaNV);

                return View(OldCustomer);
            }

            if (dbCustomer.CMND != OldCustomer.CMND && Db.Customer.IsIdCardExist(OldCustomer.CMND))
            {
                ViewBag.FailMsg = "Số CMND đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", OldCustomer.MaNV);

                return View(OldCustomer);
            }

            if (dbCustomer.SDT != OldCustomer.SDT && Db.Customer.IsPhoneNumExist(OldCustomer.SDT))
            {
                ViewBag.FailMsg = "SĐT đã tồn tại trên hệ thống";
                ViewBag.MaNV = new SelectList(dbsStaff, "MaNV", "TenNV", OldCustomer.MaNV);

                return View(OldCustomer);
            }

            if (OldCustomer.MaNV == 0)
            {
                List<int> dbsStaffIds = Db.Staff.GetStaffIds().ToList();
                Random rd = new Random();

                OldCustomer.MaNV = dbsStaffIds[rd.Next(dbsStaffIds.Count)];
            }

            if (OldCustomer.MatKhau != dbCustomer.MatKhau)
                OldCustomer.MatKhau = Lib.Md5Sha1Encrypt.SHA1Hashing(OldCustomer.MatKhau);

            Db.Customer.Save(OldCustomer, PasswordNotHash: true);

            return RedirectToAction("Index");            
        }

        //
        // POST: /Admin/Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);                        

            if (Db.Customer.Find(id).MaKH == 0)
                throw new HttpException(404, String.Empty);

            Db.Customer.Delete(id);
            
            return RedirectToAction("Index");
        }
    }
}