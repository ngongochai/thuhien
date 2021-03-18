using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace thuctaptotnghiep.Controllers
{
    public class SearchSlideController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: SearchSlide
        public ActionResult SearchSlide()
        {
            var slide = db.Slideshows.Where(x => x.status == 1).OrderBy(x => x.displayorder).ToList();
            ViewBag.City = db.cities.ToList();
            ViewBag.Work = db.NghanhNghes.Where(x=>x.Idnghanhcha!=0&&x.Tinhtrang==1).ToList();
            return View(slide);
        }
    }
}