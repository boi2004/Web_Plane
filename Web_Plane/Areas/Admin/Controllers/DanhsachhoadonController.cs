﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class DanhsachhoadonController : Controller
    {
        private DBMayBayEntities db = new DBMayBayEntities();
        // GET: Admin/Danhsachhoadon
        public ActionResult Danhsachhoadon()
        {
            var danhsachhoadon= db.HOADONs.ToList();
            return View(danhsachhoadon);
        }

    }
}