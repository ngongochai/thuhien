using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace Admin.Controllers
{
    [SessionExpire]
    public class QuangCaoController : Controller
    {
        // GET: QuangCao
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        [ValidateInput(false)]
        public ActionResult QCLayoutLeft(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertlayoutleft = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }
        [ValidateInput(false)]
        public ActionResult QCLayoutRight(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertlayoutright = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }

        //Quảng cáo trang chủ cột trái
        [ValidateInput(false)]
        public ActionResult QCIndexLeft(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertindexleft = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }

        //Quảng cáo trang chủ cột phải
        [ValidateInput(false)]
        public ActionResult QCIndexRight(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertindexright = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }

        //Quảng cáo tìm kiếm việc trang chủ đầu trang
        [ValidateInput(false)]
        public ActionResult QCIndexTop(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertindextop = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }

        //Quảng cáo tìm kiếm việc làm cột trái
        [ValidateInput(false)]
        public ActionResult QCSearchLeft(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertsearchjobleft = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }

        //Quảng cáo tìm kiếm việc làm ở dưới
        [ValidateInput(false)]
        public ActionResult QCSearchBottom(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertsearchjobbottom = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }

        //Quảng cáo danh sách tin tức
        [ValidateInput(false)]
        public ActionResult QCListNews(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertlistnews = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }
        //Quảng cáo chi tiết tin tức
        [ValidateInput(false)]
        public ActionResult QCDetailNews(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null )
            {
                ad.advertdetailnews = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }
        [ValidateInput(false)]
        public ActionResult QCDetailJobLeft(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null)
            {
                ad.advertdetailjobleft = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }
        [ValidateInput(false)]
        public ActionResult QCDetailJobRight(string html)
        {
            var ad = db.Adverts.FirstOrDefault();
            if (ad == null)
            {
                ad = new Advert();
                db.Adverts.Add(ad);
                db.SaveChanges();

            }
            if (html != null)
            {
                ad.advertdetailjobright = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return View(ad);
            }

        }
    }
}