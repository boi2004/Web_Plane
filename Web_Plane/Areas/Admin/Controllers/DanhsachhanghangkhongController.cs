using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Web_Plane.Models;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class DanhsachhanghangkhongController : Controller
    {
        private DBMayBayEntities db = new DBMayBayEntities();

        // GET: Admin/Danhsachhanghangkhong
         public ActionResult VeMayBay()
        {
            return View(); // Trả về view hiển thị form thêm hãng hàng không mới
        }
        public ActionResult Danhsachhanghangkhong()
        {
            var danhsachhanghangkhong = db.HANGHANGKHONGs.ToList(); // Lấy danh sách hãng hàng không từ cơ sở dữ liệu
            return View(danhsachhanghangkhong); // Truyền danh sách hãng hàng không đến view
        }
       
        public ActionResult Themdanhsachhanghangkhong()
        {
            return View(); // Trả về view hiển thị form thêm hãng hàng không mới
        }

        // POST: Admin/Themdanhsachhanghangkhong
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Themdanhsachhanghangkhong([Bind(Include = "IDHK,TenHang,IMG")] HANGHANGKHONG hanghangkhong)
        {
            try
            {
                if (ModelState.IsValid) // Kiểm tra tính hợp lệ của model
                {
                    db.HANGHANGKHONGs.Add(hanghangkhong); // Thêm hãng hàng không mới vào cơ sở dữ liệu
                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction("Danhsachhanghangkhong"); // Chuyển hướng đến trang danh sách hãng hàng không
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message); // Thêm lỗi vào ModelState để hiển thị lên view
            }
            return View(hanghangkhong); // Nếu model không hợp lệ hoặc có lỗi, trả về view cùng với model để hiển thị lỗi


        }

        // GET: Admin/Danhsachhanghangkhong/Chinhsuadanhsachhanghangkhong/{id}
        public ActionResult Chinhsuadanhsachhanghangkhong(string id)
        {
            // Tìm hãng hàng không theo id
            HANGHANGKHONG hanghangkhong = db.HANGHANGKHONGs.SingleOrDefault(h => h.IDHK == id);

          

            // Kiểm tra nếu id là null hoặc rỗng
            if (string.IsNullOrEmpty(id))
            {
                // Trả về mã lỗi BadRequest nếu id không hợp lệ
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Nếu không tìm thấy hãng hàng không, trả về lỗi NotFound
            if (hanghangkhong == null)
            {
                // Trả về mã lỗi NotFound nếu không tìm thấy hãng hàng không
                return HttpNotFound();
            }

            // Trả về view chỉnh sửa hãng hàng không với dữ liệu hãng hàng không tìm thấy
            return View(hanghangkhong);
        }



        // POST: Admin/Danhsachhanghangkhong/Chinhsuadanhsachhanghangkhong
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Chinhsuadanhsachhanghangkhong([Bind(Include = "IDHK,TenHang,IMG")] HANGHANGKHONG hanghangkhong)
        {
            try
            {
                if (ModelState.IsValid) // Kiểm tra tính hợp lệ của model
                {
                    db.Entry(hanghangkhong).State = System.Data.Entity.EntityState.Modified; // Đánh dấu hãng hàng không đã chỉnh sửa
                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    return RedirectToAction("Danhsachhanghangkhong"); // Chuyển hướng đến trang danh sách hãng hàng không
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message); // Thêm lỗi vào ModelState để hiển thị lên view
            }
            return View(hanghangkhong); // Nếu model không hợp lệ hoặc có lỗi, trả về view cùng với model để hiển thị lỗi
        }





        // GET: Admin/Danhsachhanghangkhong/Xoadanhsachhanghangkhong/{id}
        public ActionResult Xoadanhsachhanghangkhong(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Nếu không có id, trả về lỗi BadRequest
            }
            HANGHANGKHONG hanghangkhong = db.HANGHANGKHONGs.Find(id); // Tìm hãng hàng không theo id
            if (hanghangkhong == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy hãng hàng không, trả về lỗi NotFound
            }
            return View(hanghangkhong); // Trả về view xác nhận xóa hãng hàng không
        }

        // POST: Admin/Danhsachhanghangkhong/Xoadanhsachhanghangkhong/{id}
        [HttpPost, ActionName("Xoadanhsachhanghangkhong")]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanXoadanhsachhanghangkhong(string id)
        {
            try
            {
                HANGHANGKHONG hanghangkhong = db.HANGHANGKHONGs.Find(id); // Tìm hãng hàng không theo id
                db.HANGHANGKHONGs.Remove(hanghangkhong); // Xóa hãng hàng không khỏi cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return RedirectToAction("Danhsachhanghangkhong"); // Chuyển hướng đến trang danh sách hãng hàng không
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message); // Thêm lỗi vào ModelState để hiển thị lên view
                return View(db.HANGHANGKHONGs.Find(id)); // Trả về lại view xóa hãng hàng không với model tương ứng
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
