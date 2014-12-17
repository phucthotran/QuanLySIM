using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySIM.Data;
using QuanLySIM.Entities;
using QuanLySIM.Services;

namespace QuanLySIM.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IDbService db) : base(db) { }
        
        //
        // GET: /Customer/Profile

        public ActionResult Profile()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            KhachHang dbKhach = Db.Customer.Find(base.MaKH);
            ViewBag.IsProfileNotCompleted = dbKhach.IsNotCompleted();

            return View(dbKhach);
        }

        [HttpPost, ActionName("Profile")]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUpdate(KhachHang Customer)
        {
            if (IsNotLogin())
                return View(NOT_ALLOWED_AREA);

            if (!ModelState.IsValid)
                return View(Customer);

            KhachHang dbCustomer = Db.Customer.Find(MaKH);

            if (dbCustomer.Email != Customer.Email && Db.Customer.IsEmailExist(Customer.Email))
            {
                ViewBag.FailMsg = "Email đã tồn tại trên hệ thống";

                return View(Customer);
            }

            if (dbCustomer.CMND != Customer.CMND && Db.Customer.IsIdCardExist(Customer.CMND))
            {
                ViewBag.FailMsg = "Số CMND đã tồn tại trên hệ thống";

                return View(Customer);
            }

            if (dbCustomer.SDT != Customer.SDT && Db.Customer.IsPhoneNumExist(Customer.SDT))
            {
                ViewBag.FailMsg = "SĐT đã tồn tại trên hệ thống";

                return View(Customer);
            }

            Customer.MaKH = base.MaKH;
            Customer.MaNV = dbCustomer.MaNV;

            Db.Customer.Save(Customer, PasswordNotHash: true);

            ViewBag.SuccessMsg = "Cập nhật thông tin thành công";

            return View(Customer);
        }

        //
        // GET: /Customer/Login

        public ActionResult Login()
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);
                        
            return View(new KhachHang());
        }

        //
        // POST: /Customer/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang Customer)
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);

            if (!ModelState.IsValidField("TenTK") || !ModelState.IsValidField("MatKhau"))
                return View(Customer);

            int customerId = Db.Customer.Available(Customer.TenTK, Customer.MatKhau);

            if (customerId != 0)
            {
                Session["customer_login_id"] = customerId;

                if (Request.QueryString["ref"] != null)
                    return RedirectPermanent(Request.QueryString["ref"]);
                                
                return RedirectToAction("Index", "Default");
            }

            ViewBag.FailMsg = "Sai thông tin đăng nhập";
            return View(Customer);
        }

        //
        // GET: /Customer/Logout
        public ActionResult Logout()
        {
            if (IsNotLogin())
                return View(NOT_ALLOWED_AREA);

            Session["customer_login_id"] = null;

            return RedirectToAction("Index", "Default");
        }
   
        //
        // GET: /Customer/SignUp

        public ActionResult Register()
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);

            return View(new KhachHang());
        }

        //
        // POST: /Customer/SignUp

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang Customer)
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);

            if (!ModelState.IsValidField("TenTK") || !ModelState.IsValidField("MatKhau") || !ModelState.IsValidField("Email"))
                return View(Customer);

            if (Db.Customer.IsAccountExist(Customer.TenTK))
            {
                ViewBag.FailMsg = "Tên tài khoản đã tồn tại trên hệ thống";
                return View(Customer);
            }

            if (Db.Customer.IsEmailExist(Customer.Email))
            {
                ViewBag.FailMsg = "Email đã tồn tại trên hệ thống";
                return View(Customer);
            }

            List<int> staffIds = Db.Staff.GetStaffIds().ToList();
            Random rd = new Random();
            Customer.MaNV = staffIds[rd.Next(staffIds.Count)];

            if (Db.Customer.LittleSave(Customer) > 0)
                return RedirectToAction("CreateSuccess");

            ViewBag.FailMsg = "Không thể tạo tài khoản lúc này. Vui lòng thử lại sau";
            return View(Customer);
        }

        //
        // GET : /Customer/CreateSuccess

        public ActionResult CreateSuccess(KhachHang Customer)
        {
            if (IsLogin())
                return View(NOT_ALLOWED_AREA);

            return View(Customer);
        }

        //
        // GET: /Customer/ChangePassword

        public ActionResult ChangePassword()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            return View();
        }

        //
        // POST: /Customer/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(KhachHang Customer)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!ModelState.IsValidField("MatKhau"))              
                return View(Customer);

            String newPassword = Request.Params["MatKhauMoi"];
            String confirmPassword = Request.Params["XacNhanMatKhau"];

            if (newPassword.Length < 8 || newPassword.Length > 45)
            {
                ViewBag.FailMsg = "Mật khẩu mới từ 8 đến 45 kí tự";

                return View(Customer);
            }

            if (confirmPassword.Length < 8 || confirmPassword.Length > 45)
            {
                ViewBag.FailMsg = "Mật khẩu xác nhận từ 8 đến 45 kí tự";

                return View(Customer);
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.FailMsg = "Mật khẩu và mật khẩu xác nhận không khớp nhau";

                return View(Customer);
            }

            KhachHang rCustomer = Db.Customer.Find(base.MaKH);

            if (Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau) != rCustomer.MatKhau)
            {
                ViewBag.FailMsg = "Mật khẩu cũ không chính xác";

                return View(Customer);
            }

            rCustomer.MatKhau = newPassword;
            Db.Customer.Save(rCustomer);            

            ViewBag.SuccessMsg = "Thay đổi mật khẩu thành công";
            
            return View(Customer);
        }

    }
}
