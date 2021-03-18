using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
namespace Admin.Controllers
{
    [SessionExpire]
    public class SlideShowController : Controller
    {
        // GET: SlideShow
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult CreateSlide()
        {
            var dataslide = new Slideshow();
            return View();
        }
        public ActionResult EditSlide(int? id)
        {
            var dataslide = db.Slideshows.Where(x => x.id == id).FirstOrDefault();
            if (dataslide != null)
            {
                return View(dataslide);
            }
            else
            {
                return RedirectToAction("CreateSlide", "SlideShow");
            }
            
        }
        public ActionResult ListSlideShow()
        {
            var data = db.Slideshows.OrderBy(x=>x.displayorder).ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult DeleteSlide(int? id)
        {
            var data = db.Slideshows.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                db.Slideshows.Remove(data);
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        [HttpPost]
        public ActionResult SaveSlide(string imgurl, string url, int? displayorder, int? status, int? id,string video)
        {
            var dataslide = db.Slideshows.Where(x => x.id == id).FirstOrDefault();
            if (dataslide == null)
            {
                dataslide = new Slideshow();
                dataslide.imgurl = imgurl;
                dataslide.videoembed = video.Replace("watch?v=", "embed/");
                dataslide.url = url;
                dataslide.displayorder = displayorder;
                dataslide.status = status;
                db.Slideshows.Add(dataslide);
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                dataslide.imgurl = imgurl;
                dataslide.url = url;
                dataslide.displayorder = displayorder;
                dataslide.videoembed = video.Replace("watch?v=", "embed/");
                dataslide.status = status;
                db.SaveChanges();
                return Json(1);
            }
        }
    }
}