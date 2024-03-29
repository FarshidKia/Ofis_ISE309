﻿using Ofis_ISE309.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.WebPages.Html;

namespace Ofis_ISE309.Controllers
{
    [Authorize(Roles = "A,U")]
    public class DepartmanController : Controller
    {
        OfisEntities4 db = new OfisEntities4();
        [Authorize]
        
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());

        }
       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            if (departman.Id == 0)
            {
                db.Departman.Add(departman);
            }
            else
            {
                var GuncellenecekDepartman = db.Departman.Find(departman.Id);
                if (GuncellenecekDepartman == null)
                {
                    return HttpNotFound();
                }
                GuncellenecekDepartman.Ad = departman.Ad;
            }
            //db.Departman.Add(departman);
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");

        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);
        }
        public ActionResult Sil(int id)
        {
            var SilinecekDepartman = db.Departman.Find(id);
            if (SilinecekDepartman == null)
                return HttpNotFound();
            db.Departman.Remove(SilinecekDepartman);
            //Entity validation hata tesbiti
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
