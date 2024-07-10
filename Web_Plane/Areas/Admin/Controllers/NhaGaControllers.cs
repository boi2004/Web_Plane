using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Plane.Models;

namespace Web_Plane.Areas.Admin.Controllers
{
    public class NhaGaControllers : Controller
    {
        private DBMayBay1Entities DBMayBayEntities = new DBMayBay1Entities();

        // GET: Admin/NhaGa
        public ActionResult DanhSachNhaGa()
        {
            var nhaGas = DBMayBayEntities.NHAGAs.ToList();
            return View(nhaGas);
        }

        // GET: Admin/NhaGa/ThemNhaGa
        public ActionResult ThemNhaGa()
        {
            return View();
        }

        // POST: Admin/NhaGa/ThemNhaGa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhaGa(NHAGA nhaGa)
        {
            if (ModelState.IsValid)
            {
                DBMayBayEntities.NHAGAs.Add(nhaGa);
                DBMayBayEntities.SaveChanges();
                return RedirectToAction("DanhSachNhaGa");
            }
            return View(nhaGa);
        }

        // GET: Admin/NhaGa/SuaNhaGa/5
        public ActionResult SuaNhaGa(int id)
        {
            var nhaGa = DBMayBayEntities.NHAGAs.Find(id);
            if (nhaGa == null)
            {
                return HttpNotFound();
            }
            return View(nhaGa);
        }

        // POST: Admin/NhaGa/SuaNhaGa/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaNhaGa(NHAGA nhaGa)
        {
            if (ModelState.IsValid)
            {
                DBMayBayEntities.Entry(nhaGa).State = System.Data.Entity.EntityState.Modified;
                DBMayBayEntities.SaveChanges();
                return RedirectToAction("DanhSachNhaGa");
            }
            return View(nhaGa);
        }

        // GET: Admin/NhaGa/XoaNhaGa/5
        public ActionResult XoaNhaGa(int id)
        {
            var nhaGa = DBMayBayEntities.NHAGAs.Find(id);
            if (nhaGa == null)
            {
                return HttpNotFound();
            }
            return View(nhaGa);
        }

        // POST: Admin/NhaGa/XoaNhaGa/5
        [HttpPost, ActionName("XoaNhaGa")]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanXoaNhaGa(int id)
        {
            var nhaGa = DBMayBayEntities.NHAGAs.Find(id);
            if (nhaGa != null)
            {
                DBMayBayEntities.NHAGAs.Remove(nhaGa);
                DBMayBayEntities.SaveChanges();
            }
            return RedirectToAction("DanhSachNhaGa");
        }
    }
}
