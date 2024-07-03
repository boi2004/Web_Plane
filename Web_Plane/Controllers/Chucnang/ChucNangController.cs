using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;
using Web_Plane.Models.ModelClass;
using Web_Plane.ViewModels;

namespace Web_Plane.Controllers.Chucnang
{

    public class ChucNangController : Controller
    {
        DBMayBayEntities DBMayBayEntities = new DBMayBayEntities();
        // GET: ChucNang
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(dangnhap model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra đăng nhập cho adminmm
                if (model.Email == "admin@example.com" && model.Password == "1")
                {
                    // Đăng nhập thành công với vai trò admin
                    Session["IsAdmin"] = true;
                    return RedirectToAction("Danhsachsanbay", "HomeAdmin", new { area = "Admin" });
                    // return RedirectToAction("TrangChu", "Home");
                }

                // Kiểm tra đăng nhập cho người dùng thông thường
                var user = DBMayBayEntities.KHACHHANGs.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Đăng nhập thành công cho người dùng
                    Session["UserID"] = user.CCCD;
                    Session["UserName"] = user.HoTen;
                    return RedirectToAction("TrangChu", "Home");
                }
                else
                {
                    // Đăng nhập thất bại, hiển thị thông báo lỗi
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
                }
            }

            // Nếu có lỗi, trả lại view đăng nhập với các thông báo lỗi
            return View(model);
        }
        public ActionResult DangKy()
        {
            return View();
        }
        // POST: DangKy
        // POST: ChucNang/DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult DangKy(KHACHHANG khachhang)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra CCCD trùng lặp
                var existingCCCD = DBMayBayEntities.KHACHHANGs.Any(kh => kh.CCCD == khachhang.CCCD);
                if (existingCCCD)
                {
                    ModelState.AddModelError("CCCD", "CCCD đã được sử dụng.");
                    Console.WriteLine("1");
                }

                // Kiểm tra email trùng lặp
                var existingEmail = DBMayBayEntities.KHACHHANGs.Any(kh => kh.Email == khachhang.Email);
                if (existingEmail)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    Console.WriteLine("2");
                }

                // Kiểm tra độ dài mật khẩu
                if (khachhang.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Mật khẩu phải có ít nhất 8 ký tự.");
                    Console.WriteLine("3");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Mã hóa mật khẩu
                        khachhang.Password = HashPassword(khachhang.Password);

                        // Thêm người dùng vào cơ sở dữ liệu
                        DBMayBayEntities.KHACHHANGs.Add(khachhang);
                        DBMayBayEntities.SaveChanges();

                        // Chuyển hướng tới trang thành công
                        return RedirectToAction("DangKySuccess");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                ModelState.AddModelError(string.Empty, $"Lỗi xác thực - Thuộc tính: {validationError.PropertyName}, Lỗi: {validationError.ErrorMessage}");
                            }
                        }
                    }
                }
            }

            // Nếu có lỗi, trả lại view đăng ký với các thông báo lỗi
            return View(khachhang);
        }



        public ActionResult QuenMatKhau() 
         { 
            return View(); 

         }
        private string HashPassword(string password)
        {
            // Mã hóa mật khẩu bằng SHA256
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        

    }
}