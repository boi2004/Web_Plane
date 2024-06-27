using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;
using Web_Plane.Models.ModelClass;

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
                // Kiểm tra đăng nhập cho admin
                if (model.Email == "admin@example.com" && model.Password == "1")
                {
                    // Đăng nhập thành công với vai trò admin
                    Session["IsAdmin"] = true;
                    // return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    return RedirectToAction("TrangChu", "Home");
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
         public ActionResult QuenMatKhau() 
         { 
            return View(); 

         }

    }
}