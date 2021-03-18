using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace thuctaptotnghiep.Controllers
{
    public class FooterController : Controller
    {
        // GET: Footer
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult Footer()
        {
            var data = db.FooterLogoes.FirstOrDefault();
            return View(data);
        }
    }
}