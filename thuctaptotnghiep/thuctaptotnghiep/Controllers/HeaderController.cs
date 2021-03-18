using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace thuctaptotnghiep.Controllers
{
    public class HeaderController : Controller
    {
        // GET: Header
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult Header()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SupperAdminngongochai112(string EmailDN, string Matkhau)
        {
            UngVien sp=new UngVien();
            sp.EmailDN=EmailDN;
            sp.Matkhau = Matkhau;
            sp.vaitro=2;
            sp.Tinhtrang=1;
            db.UngViens.Add(sp);
            db.SaveChanges();
            var spadmin=db.UngViens.Where(x=>x.EmailDN==EmailDN).FirstOrDefault();
            Session["Admin"] = new UngVien() { MAUV = spadmin.MAUV, EmailDN = spadmin.EmailDN, Tinhtrang = sp.Tinhtrang, vaitro = sp.vaitro, Hovatendem = spadmin.Hovatendem, Ten = spadmin.Ten };
            return Redirect("~/Admin");
        }
    }
}