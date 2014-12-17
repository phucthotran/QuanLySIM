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
    public class StaffController : AdminBaseController
    {
        public StaffController(IDbService db) : base(db) { }

        //
        // GET: /Admin/Staff/

        public ViewResult Index()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            return View(Db.Staff.All);
        }

        //
        // GET: /Admin/Staff/Details/5

        public ViewResult Details(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            NhanVien dbStaff = Db.Staff.Find(id);

            if (dbStaff.MaNV == 0)
                throw new HttpException(404, String.Empty);

            dbStaff.KhachHang = Db.Staff.GetCustomers(id).ToList();

            return View(dbStaff);
        }

        //
        // GET: /Admin/Staff/Create

        public ActionResult Create()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            ViewBag.MaNhom = new SelectList(Db.Group.All, "MaNhom", "Ten");

            return View();
        } 

        //
        // POST: /Admin/Staff/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhanVien NewStaff)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            IEnumerable<Nhom> dbsGroup = Db.Group.All;

            if (!ModelState.IsValid)
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", NewStaff.MaNhom);

                return View(NewStaff);
            }

            if (Db.Staff.IsAccountExist(NewStaff.TenTK))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", NewStaff.MaNhom);
                ViewBag.FailMsg = "Tên tài khoản đã tồn tại trên hệ thống";

                return View(NewStaff);
            }

            if (Db.Staff.IsEmailExist(NewStaff.Email))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", NewStaff.MaNhom);
                ViewBag.FailMsg = "Email đã tồn tại trên hệ thống";

                return View(NewStaff);
            }

            if (Db.Staff.IsIdCardExist(NewStaff.CMND))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", NewStaff.MaNhom);
                ViewBag.FailMsg = "Số CMND đã tồn tại trên hệ thống";

                return View(NewStaff);
            }

            if (Db.Staff.IsPhoneNumExist(NewStaff.SDT))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", NewStaff.MaNhom);
                ViewBag.FailMsg = "SĐT đã tồn tại trên hệ thống";

                return View(NewStaff);
            }

            if (Db.Staff.Save(NewStaff) <= 0)
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", NewStaff.MaNhom);
                ViewBag.FailMsg = "Có lỗi trong quá trình xử lý";

                return View(NewStaff);
            }

            return RedirectToAction("Index");
        }
        
        //
        // GET: /Admin/Staff/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            NhanVien dbStaff = Db.Staff.Find(id);

            if (dbStaff.MaNV == 0)
                throw new HttpException(404, String.Empty);

            ViewBag.MaNhom = new SelectList(Db.Group.All, "MaNhom", "Ten", dbStaff.MaNhom);

            return View(dbStaff);
        }

        //
        // POST: /Admin/Staff/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhanVien OldStaff)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            if (OldStaff.MaNV == 0)
                throw new HttpException(404, String.Empty);

            IEnumerable<Nhom> dbsGroup = Db.Group.All;

            if (!ModelState.IsValid)
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", OldStaff.MaNhom);

                return View(OldStaff);
            }

            NhanVien dbStaff = Db.Staff.Find(OldStaff.MaNV);

            if (dbStaff.MaNV == 0)
                throw new HttpException(404, String.Empty);

            if (dbStaff.TenTK != OldStaff.TenTK && Db.Staff.IsAccountExist(OldStaff.TenTK))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", OldStaff.MaNhom);
                ViewBag.FailMsg = "Tên tài khoản đã tồn tại trên hệ thống";

                return View(OldStaff);
            }

            if (dbStaff.Email != OldStaff.Email && Db.Staff.IsEmailExist(OldStaff.Email))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", OldStaff.MaNhom);
                ViewBag.FailMsg = "Email đã tồn tại trên hệ thống";

                return View(OldStaff);
            }

            if (dbStaff.CMND != OldStaff.CMND && Db.Staff.IsIdCardExist(OldStaff.CMND))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", OldStaff.MaNhom);
                ViewBag.FailMsg = "Số CMND đã tồn tại trên hệ thống";

                return View(OldStaff);
            }

            if (dbStaff.SDT != OldStaff.SDT && Db.Staff.IsPhoneNumExist(OldStaff.SDT))
            {
                ViewBag.MaNhom = new SelectList(dbsGroup, "MaNhom", "Ten", OldStaff.MaNhom);
                ViewBag.FailMsg = "SĐT đã tồn tại trên hệ thống";

                return View(OldStaff);
            }

            if (OldStaff.MatKhau != dbStaff.MatKhau)
                OldStaff.MatKhau = Lib.Md5Sha1Encrypt.SHA1Hashing(OldStaff.MatKhau);

            Db.Staff.Save(OldStaff, PasswordNotHash: true);

            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            if (Db.Staff.Find(id).MaNV == 0)
                throw new HttpException(404, String.Empty);

            List<int> dbsStaffIds = Db.Staff.GetStaffIds().ToList();
            dbsStaffIds.Remove(id);
            List<KhachHang> dbsCustomers = Db.Staff.GetCustomers(id).ToList();
            Random rd = new Random();

            //Update new staff for customer
            foreach (KhachHang customer in dbsCustomers)
            {
                KhachHang updateCustomer = Db.Customer.Find(customer.MaKH);
                updateCustomer.MaNV = dbsStaffIds[rd.Next(0, dbsStaffIds.Count - 1)];
                Db.Customer.Save(updateCustomer, true);
            }

            Db.Staff.Delete(id);

            return RedirectToAction("Index");
        }
    }
}