using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace Admin.Controllers
{
    [SessionExpire]
    public class AdvertController : Controller
    {
        // GET: Advert
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult AdvertLayoutLeft(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
               
            }
            if (html != null && html != "")
            {
                ad.advertlayoutleft = html;
                return Json(1);
            }
            else
            {
                return View(ad);
            }
            
        }
    }
}