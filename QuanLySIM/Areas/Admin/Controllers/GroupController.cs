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
    public class GroupController : AdminBaseController
    {
        public GroupController(IDbService db) : base(db) { }

        //
        // GET: /Admin/Group/

        public ViewResult Index()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            return View(Db.Group.All);
        }

        //
        // GET: /Admin/Group/Details/5

        public ViewResult Details(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            Nhom dbGroup = Db.Group.Find(id);

            if (dbGroup.MaNhom == 0)
                throw new HttpException(404, String.Empty);

            dbGroup.NhanVien = Db.Group.GetStaffs(id).ToList();

            return View(dbGroup);
        }

        //
        // GET: /Admin/Group/Create

        public ActionResult Create()
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            ViewBag.RoleId = new SelectList(Db.Role.All, "RoleId", "Name");

            return View();
        } 

        //
        // POST: /Admin/Group/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nhom NewGroup)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            IEnumerable<Role> dbsRole = Db.Role.All;

            if (!ModelState.IsValid)
            {
                ViewBag.RoleId = new SelectList(dbsRole, "RoleId", "Name", NewGroup.RoleId);

                return View(NewGroup);
            }

            if (Db.Group.IsExist(NewGroup.Ten))
            {
                ViewBag.FailMsg = "Nhóm này đã tồn tại trên hệ thống";
                ViewBag.RoleId = new SelectList(dbsRole, "RoleId", "Name", NewGroup.RoleId);

                return View(NewGroup);
            }

            Db.Group.Save(NewGroup);

            return RedirectToAction("Index");                        
        }
        
        //
        // GET: /Admin/Group/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            Nhom dbGroup = Db.Group.Find(id);

            if (dbGroup.MaNhom == 0)
                throw new HttpException(404, String.Empty);

            ViewBag.RoleId = new SelectList(Db.Role.All, "RoleId", "Name", dbGroup.RoleId);

            return View(dbGroup);
        }

        //
        // POST: /Admin/Group/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nhom OldGroup)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            if (OldGroup.MaNhom == 0)
                throw new HttpException(404, String.Empty);

            IEnumerable<Role> dbsRole = Db.Role.All;

            if (!ModelState.IsValid)
            {
                ViewBag.RoleId = new SelectList(dbsRole, "RoleId", "Name", OldGroup.RoleId);

                return View(OldGroup);
            }

            Nhom dbGroup = Db.Group.Find(OldGroup.MaNhom);

            if (dbGroup.MaNhom == 0)
                throw new HttpException(404, String.Empty);

            if (dbGroup.Ten != OldGroup.Ten && Db.Group.IsExist(OldGroup.Ten))
            {
                ViewBag.FailMsg = "Nhóm này đã tồn tại trên hệ thống";
                ViewBag.RoleId = new SelectList(dbsRole, "RoleId", "Name", OldGroup.RoleId);

                return View(OldGroup);
            }

            Db.Group.Save(OldGroup);

            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/Group/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsNotLogin())
                return View(MUST_LOGIN_AREA);

            if (!IsAdmin())
                return View(NOT_ALLOWED_AREA);

            if (Db.Group.Find(id).MaNhom == 0)
                throw new HttpException(404, String.Empty);

            Db.Group.Delete(id);

            return RedirectToAction("Index");
        }
    }
}