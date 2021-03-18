using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [SessionExpire]
    public class FooterController : Controller
    {
        // GET: Footer
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult FooterLayout()
        {
            
            var footer = db.FooterLogoes.FirstOrDefault();
            return View(footer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FooterLayout(string html)
        {
            var footer = db.FooterLogoes.FirstOrDefault();
            if (footer == null)
            {
                footer = new FooterLogo();
                if (html != null && html != "")
                {
                    footer.footermember = html;
                    db.FooterLogoes.Add(footer);
                    db.SaveChanges();
                    return Json(1);
                }
            }
            else if (html != null && html != "")
            {
                footer.footermember = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return Json(0);
            }

            return Json(0);
        }
        public ActionResult FooterEmployer()
        {
            var footer = db.FooterLogoes.FirstOrDefault();
            return View(footer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FooterEmployer(string html)
        {
            var footer = db.FooterLogoes.FirstOrDefault();
            if (footer == null)
            {
                footer = new FooterLogo();
                if (html != null && html != "")
                {
                    footer.footeremployer = html;
                    db.FooterLogoes.Add(footer);
                    db.SaveChanges();
                    return Json(1);
                }
            }
            else if (html != null && html != "")
            {
                footer.footeremployer = html;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return Json(0);
            }

            return Json(0);
        }
        public ActionResult UpdateLogo()
        {
            var logo = db.FooterLogoes.FirstOrDefault();
            return View(logo);
        }
        [HttpPost]
        public ActionResult UpdateLogo(string logourl)
        {
            var footer = db.FooterLogoes.FirstOrDefault();
            if (footer == null)
            {
                footer = new FooterLogo();
                if (logourl != null && logourl != "")
                {
                    footer.logoimg = logourl;
                    db.FooterLogoes.Add(footer);
                    db.SaveChanges();
                    return Json(1);
                }
            }
            else if (logourl != null && logourl != "")
            {
                footer.logoimg = logourl;
                db.SaveChanges();
                return Json(1);
            }
            else
            {
                return Json(0);
            }

            return Json(0);
        }
    }
}