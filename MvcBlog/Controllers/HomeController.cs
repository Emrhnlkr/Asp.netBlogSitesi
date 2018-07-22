using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using PagedList;
using PagedList.Mvc;

namespace MvcBlog.Controllers
{
    public class HomeController : Controller
    {

        mvcblogDB db = new mvcblogDB();
        public ActionResult Index(int Page=1)
        {
            var makale = db.Makales.OrderByDescending(m => m.MakaleId).ToPagedList(Page,5); //Son ekleneni ilk gösteren kod.
            return View(makale);
        }
        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.Makales.Where(m => m.Baslik.Contains(Ara)).ToList(); 
            return View(aranan.OrderByDescending(m=>m.Tarih)); //Tarihe göre tersten sıralıyor.(Orderbydessindng)
        }
         public ActionResult SonYorumlar()
        {


            return View(db.Yorums.OrderByDescending(m => m.YorumId).Take(5)); //Take ise 5 tane yorumu getiriyor.
        }

        public ActionResult PopulerMakaleler()
        {


            return View(db.Makales.OrderByDescending(m => m.Okunma).Take(5)); //Take ise 5 tane yorumu getiriyor.
        }


        public ActionResult KategoriMakale(int id)
        {
            

            var makaleler = db.Makales.Where(m => m.Kategori.KategoriId == id).ToList(); //(GRİDMVC)Burada kategorilere tıkladığımızda o kategori ile ilgili makaleleri bize getirecek.

           
            return View(makaleler);

        }

        public ActionResult MakaleDetay(int id)
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if (makale==null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }
        public ActionResult Hakkımızda()
        {
            return View();
        }
        public ActionResult İletişim()
        {

            return View();
        }

        public ActionResult KategoriPartial()
        {
            return View(db.Kategoris.ToList());
        }
        
        public JsonResult YorumYap(string yorum,int Makaleid)
        {
            var uyeid = Session["uyeid"];
            if (yorum ==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
               
            } db.Yorums.Add(new Yorum { UyeId = Convert.ToInt32(uyeid), MakaleId = Makaleid, Icerik = yorum });
              db.SaveChanges();
              //db.Yorums.ToList();
            
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult OkunmaArttir(int Makaleid)
        {
    
            var makale = db.Makales.Where(m => m.MakaleId != Makaleid).SingleOrDefault();
            makale.Okunma += 1;
            db.SaveChanges();
            return View();
        }
    }
}