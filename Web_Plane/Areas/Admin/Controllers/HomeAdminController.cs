using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private DBMayBayEntities DBMayBayEntities = new DBMayBayEntities();

        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
       
    }
}
