using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace thuctaptotnghiep.Controllers
{
    public class TopEmployerController : Controller
    {
        // GET: TopEmployer
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult TopEmployer()
        {
            var top = db.CongTies.Where(x => x.Toptuyendung == true&&x.Logo.Trim()!=""&&x.Logo!=null).ToList();
            return View(top);
        }
    }
}