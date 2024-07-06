using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class DanhsachkhachhangController : Controller
    {
        private DBMayBayEntities db = new DBMayBayEntities();
        // GET: Admin/Danhsachkhachhang
        public ActionResult Danhsachkhachhang()
        {
            var danhsachkhachhang = db.KHACHHANGs.ToList();
            return View(danhsachkhachhang);
        }
        public ActionResult Xoadanhsachkhachhang()
        {
            return View();
        }
        public ActionResult Themdanhsachkhachhang()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Themdanhsachkhachhang([Bind(Include = "CCCD,HoTen,Sdt,Email,DiaChi,LoaiKH,NgaySinh,GioiTinh,QuocTich,Password,IMG")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Danhsachkhachhang");
            }

            return View(kHACHHANG);
        }
        public ActionResult Chinhsuadanhsachkhachhang(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Chinhsuadanhsachkhachhang([Bind(Include = "CCCD,HoTen,Sdt,Email,DiaChi,LoaiKH,NgaySinh,GioiTinh,QuocTich,Password,IMG")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Danhsachkhachhang");
            }
            return View(kHACHHANG);
        }

    } 
}