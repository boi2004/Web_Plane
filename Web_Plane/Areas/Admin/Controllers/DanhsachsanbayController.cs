using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Web_Plane.Models;
using Web_Plane.Models.ModelClass;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class DanhsachsanbayController : Controller
    {
        private DBMayBayEntities db = new DBMayBayEntities();

        // GET: Admin/Danhsachsanbay
        public ActionResult Danhsachsanbay()
        {
            var danhsachsanbay = db.SANBAYs.ToList(); // Lấy danh sách sân bay từ cơ sở dữ liệu
            return View(danhsachsanbay); // Truyền danh sách sân bay đến view
        }

        // GET: Admin/Danhsachsanbay/Themdanhsachhanghangkhong
        public ActionResult Themdanhsachsanbay()
        {
            return View(); // Trả về view hiển thị form thêm sân bay mới
        }

        // POST: Admin/Danhsachsanbay/Themdanhsachhanghangkhong
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Themdanhsachsanbay([Bind(Include = "IATA,TenSanBay,QuocGia,ThanhPho,DiaChi")] SANBAY sanbay)
        {
            try
            {
                if (ModelState.IsValid) // Kiểm tra tính hợp lệ của model
                {
                    db.SANBAYs.Add(sanbay); // Thêm sân bay mới vào cơ sở dữ liệu
                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction("Danhsachsanbay"); // Chuyển hướng đến trang danh sách sân bay
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message); // Thêm lỗi vào ModelState để hiển thị lên view
            }
            return View(sanbay); // Nếu model không hợp lệ hoặc có lỗi, trả về view cùng với model để hiển thị lỗi
        }


        // GET: Admin/Danhsachsanbay/Chinhsuadanhsachsanbay/{id}
        public ActionResult Chinhsuadanhsachsanbay(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANBAY sanbay = db.SANBAYs.Find(id);
            if (sanbay == null)
            {
                return HttpNotFound();
            }
            return View(sanbay);
        }
        // POST: Admin/Danhsachsanbay/Chinhsuadanhsachsanbay/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Chinhsuadanhsachsanbay([Bind(Include = "IATA,TenSanBay,QuocGia,ThanhPho,DiaChi")] SANBAY sanbay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanbay).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Danhsachsanbay");
            }
            return View(sanbay);
        }

        // GET: Admin/Danhsachsanbay/Xoadanhsachsanbay/{id}
        public ActionResult Xoadanhsachsanbay(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANBAY sanbay = db.SANBAYs.Find(id);
            if (sanbay == null)
            {
                return HttpNotFound();
            }
            return View(sanbay);
        }

        /// POST: Admin/Danhsachsanbay/Xoadanhsachsanbay/{id}
        [HttpPost, ActionName("Xoadanhsachsanbay")]
        [ValidateAntiForgeryToken]
        public ActionResult Xacnhanxoadanhsachsanbay(string id)
        {
            try
            {
                SANBAY sanbay = db.SANBAYs.Find(id);
                if (sanbay != null)
                {
                    db.SANBAYs.Remove(sanbay);
                    db.SaveChanges();
                }
                return RedirectToAction("Danhsachsanbay");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                return View();
            }
        }


    }
}
