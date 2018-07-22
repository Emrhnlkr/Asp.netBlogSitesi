﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using System.Web.Helpers;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace MvcBlog.Controllers
{
    public class AdminMakaleController : Controller
    {
        mvcblogDB db = new mvcblogDB();

        // GET: AdminMakale
        public ActionResult Index(int Page=1) //1.sayfayı getirsin denir.
        {
            var makales = db.Makales.OrderByDescending(m=>m.MakaleId).ToPagedList(Page,10); //Admin makale sayfasında sayfalama ile her sayfada 10 tane makale olacak şekilde sıralama gerçekleşmiş.
            return View(makales);
        }

        // GET: AdminMakale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminMakale/Create
        public ActionResult Create()
        {


            //string[] Foto = Directory.GetFiles(Server.MapPath("~/Uploads/MakaleFoto/"));
            //string[] FotoNames = new string[Foto.Count()];
            //for (int i = 0; i < Foto.Count(); i++)
            //{
            //    FotoNames[i] = Foto[i].Substring(Foto[i].IndexOf("MakaleFoto"));
            //}
            //TempData["Foto"] = FotoNames;



            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAd");
            return View();
        }

        // POST: AdminMakale/Create
        [HttpPost] //kaydet tuşuna basıldıktan sonra olacaklar bu blogda verilecektir.
        public ActionResult Create(string desc,IEnumerable<HttpPostedFileBase> files, Makale makale, string etiketler,string Foto)
        {
            if (ModelState.IsValid)
            {
                //if (Foto != null)
                //{
               
                //    WebImage img = new WebImage(Foto.InputStream);
                //    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                //    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                //    img.Resize(800, 350);
                //    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                //    makale.Foto = "~/Uploads/MakaleFoto/" + newfoto;


                //}
                if (etiketler != null)
                {

                    string[] etiketdizi = etiketler.Split(',');
                    foreach (var i in etiketdizi)
                    {

                        var yenietiket = new Etiket { EtiketAd = i };
                        db.Etikets.Add(yenietiket);
                        makale.Etikets.Add(yenietiket);
                    }
                }
              
                makale.UyeId = Convert.ToInt32(Session["UyeId"]);
                makale.Okunma = 1;
                db.Makales.Add(makale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                            
                return View(makale);
            
        }

        // GET: AdminMakale/Edit/5
        public ActionResult Edit(int id)
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if(makale==null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAd", makale.KategoriId);

            return View(makale);
        }

        // POST: AdminMakale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string Foto,Makale makale)
        {
            try
            {
                var makales = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
                if(Foto!=null)
                {
                    //if(System.IO.File.Exists(Server.MapPath(makales.Foto)))
                    //{
                    //    System.IO.File.Delete(Server.MapPath(makales.Foto));
                    //}
                    

                    //WebImage img = new WebImage(Foto.InputStream);
                    //FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    //string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    //img.Resize(800, 350);
                    //img.Save("~/Uploads/MakaleFoto/" + newfoto);
                    //makales.Foto = "~/Uploads/MakaleFoto/" + newfoto;
                    makales.Baslik = makale.Baslik;
                    makales.Icerik = makale.Icerik;
                    makales.KategoriId = makale.KategoriId;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch //Herhangi bir hata durumundada kendine döndürecek.
            {
                ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAd", makale.KategoriId);

                return View(makale);
            }
        }

        // GET: AdminMakale/Delete/5
        public ActionResult Delete(int id) //Buradaki id sil butonundan geliyor
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();

            if (makale==null)
            {
                return HttpNotFound();

            }
            return View(makale);
        }

        // POST: AdminMakale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var makales = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
                if (makales == null)
                {
                    return HttpNotFound();

                }
                //if (System.IO.File.Exists(Server.MapPath(makales.Foto)))
                //{
                //    System.IO.File.Delete(Server.MapPath(makales.Foto));
                //}
                //foreach (var i in makales.Yorums.ToList())
                //{
                //    db.Yorums.Remove(i);
                //}
                foreach (var i in makales.Etikets.ToList())
                {
                    db.Etikets.Remove(i);

                }db.Makales.Remove(makales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}