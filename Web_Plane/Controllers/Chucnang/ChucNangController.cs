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
        DBMayBay1Entities DBMayBayEntities = new DBMayBay1Entities();
        // GET: ChucNang
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(dangnhap model)
        {
            // Kiểm tra đăng nhập cho admin
            if (model.Email == "admin@example.com" && model.Password == "1")
            {
                // Đăng nhập thành công với vai trò admin
                Session["IsAdmin"] = true;
                return RedirectToAction("Danhsachsanbay", "HomeAdmin", new { area = "Admin" });
                // return RedirectToAction("TrangChu", "Home");
            }
            var kh = DBMayBayEntities.KHACHHANGs.FirstOrDefault(k => k.Email == model.Email);
            if (kh != null)
            {
                bool isValid = VerifyPassword(model.Password, kh.Password);
                if (isValid)
                {
                    Session["TaiKhoan"] = kh;
                    Session["HoTen"] = kh.HoTen;
                    return RedirectToAction("TrangChu", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Mật khẩu không đúng !!";
                    return View();
                }
            }
            else
            {
                ViewBag.ThongBao = "Email không đúng !!";
                return View();
            }
        }
        [HttpGet]
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
                var existingCCCD = DBMayBayEntities.KHACHHANGs.FirstOrDefault(kh => kh.CCCD == khachhang.CCCD);
                if (existingCCCD != null)
                {
                    ModelState.AddModelError("CCCD", "CCCD đã được sử dụng.");
                    Console.WriteLine("1");
                }

                // Kiểm tra email trùng lặp
                var existingEmail = DBMayBayEntities.KHACHHANGs.FirstOrDefault(kh => kh.Email == khachhang.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    Console.WriteLine("2");
                }
                else
                {
                    DBMayBayEntities.Configuration.ValidateOnSaveEnabled = false;
                    khachhang.IMG = "default";
                    khachhang.LoaiKH = "Thành viên";
                    khachhang.Password = HashPassword(khachhang.Password);
                    DBMayBayEntities.KHACHHANGs.Add(khachhang);
                    DBMayBayEntities.SaveChanges();
                    return RedirectToAction("DangNhap");
                }
            }

            // Nếu có lỗi, trả lại view đăng ký với các thông báo lỗi
            return View(khachhang);
        }


        [HttpGet]
        public ActionResult QuenMatKhau(string cccd) 
        {
            KHACHHANG kh = DBMayBayEntities.KHACHHANGs.FirstOrDefault(k => k.CCCD == cccd);
            return View(kh); 
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult QuenMatKhau(string cccd)
        //{
        //    //if (ModelState.IsValid)
        //    //{

        //    //}
        //    return View();
        //}
        private string HashPassword(string password)
        {
            // Mã hóa mật khẩu bằng SHA256
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                var base64Hash = Convert.ToBase64String(hash);

                // Cắt ngắn hash để phù hợp với nvarchar(40)
                return base64Hash.Length > 40 ? base64Hash.Substring(0, 40) : base64Hash;
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Băm mật khẩu đã nhập
            string enteredPasswordHash = HashPassword(enteredPassword);

            // So sánh hash của mật khẩu đã nhập với hash đã lưu trữ
            return enteredPasswordHash == storedHash;
        }
    }
}