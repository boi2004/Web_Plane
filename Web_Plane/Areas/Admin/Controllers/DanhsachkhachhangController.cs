using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;
using Web_Plane.Models.ModelClass;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class DanhsachkhachhangController : Controller
    {
        private DBMayBay1Entities db = new DBMayBay1Entities();
        // GET: Admin/Danhsachkhachhang
        public ActionResult Danhsachkhachhang()
        {
            var danhsachkhachhang = db.KHACHHANGs.ToList();
            return View(danhsachkhachhang);
        }
        public ActionResult Xoadanhsachkhachhang(string id)
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

        [HttpPost, ActionName("Xoadanhsachkhachhang")]
        [ValidateAntiForgeryToken]
        public ActionResult XoadanhsachkhachhangConfirmed(string id)
        {
            try
            {
                KHACHHANG khachhang = db.KHACHHANGs.Find(id); // Tìm khách hàng theo id
                db.KHACHHANGs.Remove(khachhang); // Xóa khách hàng khỏi cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction("Danhsachkhachhang"); // Chuyển hướng đến trang danh sách khách hàng
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message); // Thêm lỗi vào ModelState để hiển thị lên view
                return View(db.KHACHHANGs.Find(id)); // Trả về lại view xóa khách hàng với model tương ứng
            }
        }

        public ActionResult Themdanhsachkhachhang()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Themdanhsachkhachhang([Bind(Include = "CCCD,HoTen,Sdt,Email,DiaChi,LoaiKH,NgaySinh,GioiTinh,QuocTich,Password")] KHACHHANG kHACHHANG)
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
        public ActionResult Chinhsuadanhsachkhachhang([Bind(Include = "CCCD,HoTen,Sdt,Email,DiaChi,LoaiKH,NgaySinh,GioiTinh,QuocTich,Password")] KHACHHANG kHACHHANG)
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