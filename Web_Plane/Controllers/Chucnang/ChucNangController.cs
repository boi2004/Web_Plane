using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Plane.Controllers.Chucnang
{
    public class ChucNangController : Controller
    {
        // GET: ChucNang
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangKy() 
        { 
            return View(); 
        }
    }
}