using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
namespace Admin.Controllers
{
    [SessionExpire]
    public class ConfigEmailController : Controller
    {
        // GET: ConfigEmail
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
       
   
        public ActionResult ListMail()
        {
            if (Session["Supper"] == null)
            {
                return Redirect("/Admin");
            }
            var mail = db.ConfigMails.ToList();
            return View(mail);
        }

        public ActionResult AddNew()
        {
            if (Session["Supper"] == null)
            {
                return Redirect("/Admin");
            }
            ConfigMail cm = new ConfigMail();
            return View(cm);
        }
        public ActionResult EditMail(int? id)
        {
            if (Session["Supper"] == null)
            {
                return Redirect("/Admin");
            }
            var cm=db.ConfigMails.Where(x=>x.id==id).FirstOrDefault();
            if (cm == null)
            {
                return RedirectToAction("ListMail", "ConfigEmail");
            }
            else
            {
                return View(cm);
            }
           
        }
        [HttpPost]
        public ActionResult EditMail(ConfigMail config)
        {
            if (Session["Supper"] == null)
            {
                return Redirect("/Admin");
            }
            
            if (config.id==0)
            {
                config.smtUserName = config.smtUserName.Trim();
                db.ConfigMails.Add(config);
                db.SaveChanges();
                return RedirectToAction("ListMail", "ConfigEmail");
            }
            else
            {
                var cm=db.ConfigMails.Where(x=>x.id==config.id).FirstOrDefault();
                if(cm!=null)
                {
                    cm.Name = config.Name;
                    cm.smtpHost = config.smtpHost;
                    cm.smtpPassword = config.smtpPassword;
                    cm.smtpPort = config.smtpPort;
                    cm.smtUserName = config.smtUserName.Trim();
                    cm.EnableSSL = config.EnableSSL ;
                    cm.smtpSubject = config.smtpSubject;
                    cm.smtpType = config.smtpType;
                    db.SaveChanges();
                }
                return RedirectToAction("ListMail", "ConfigEmail");
            }
        }
        public ActionResult DeleteMail(int? id)
        {
            if (Session["Supper"] == null)
            {
                return Redirect("/Admin");
            }
            var cm = db.ConfigMails.Where(x => x.id == id).FirstOrDefault();
            if (cm == null)
            {
                return RedirectToAction("ListMail", "ConfigEmail");
            }
            else
            {
                db.ConfigMails.Remove(cm);
                db.SaveChanges();
                return RedirectToAction("ListMail", "ConfigEmail");
            }

        }
    }
}