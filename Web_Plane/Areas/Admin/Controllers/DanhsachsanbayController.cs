using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Web_Plane.Models;

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Nếu không có id, trả về lỗi BadRequest
            }
            SANBAY sanbay = db.SANBAYs.Find(id); // Tìm sân bay theo id
            if (sanbay == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy sân bay, trả về lỗi NotFound
            }
            return View(sanbay); // Trả về view chỉnh sửa sân bay
        }

        // POST: Admin/Danhsachsanbay/Chinhsuadanhsachsanbay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Chinhsuadanhsachsanbay([Bind(Include = "IATA,TenSanBay,QuocGia,ThanhPho,DiaChi")] SANBAY sanbay)
        {
            try
            {
                if (ModelState.IsValid) // Kiểm tra tính hợp lệ của model
                {
                    db.Entry(sanbay).State = System.Data.Entity.EntityState.Modified; // Đánh dấu sân bay đã chỉnh sửa
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

        // GET: Admin/Danhsachsanbay/Xoadanhsachsanbay/{id}
        public ActionResult Xoadanhsachsanbay(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Nếu không có id, trả về lỗi BadRequest
            }
            SANBAY sanbay = db.SANBAYs.Find(id); // Tìm sân bay theo id
            if (sanbay == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy sân bay, trả về lỗi NotFound
            }
            return View(sanbay); // Trả về view xác nhận xóa sân bay
        }

        // POST: Admin/Danhsachsanbay/Xoadanhsachsanbay/{id}
        [HttpPost, ActionName("Xoadanhsachsanbay")]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanXoadanhsachsanbay(string id)
        {
            try
            {
                SANBAY sanbay = db.SANBAYs.Find(id); // Tìm sân bay theo id
                db.SANBAYs.Remove(sanbay); // Xóa sân bay khỏi cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction("Danhsachsanbay"); // Chuyển hướng đến trang danh sách sân bay
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message); // Thêm lỗi vào ModelState để hiển thị lên view
                return View(db.SANBAYs.Find(id)); // Trả về lại view xóa sân bay với model tương ứng
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
