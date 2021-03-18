using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Setup;

namespace thuctaptotnghiep.Controllers
{
    public class ContactController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: Contact
        public ActionResult addcontact(LienHe model)
        {
            model.DateCreate = DateTime.Now;
            db.LienHes.Add(model);
            db.SaveChanges();
            return View();
        }
    }
}