using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Setup;

namespace thuctaptotnghiep.Controllers
{
    public class ResetPassWordController : Controller
    {
        // GET: ResetPassWord
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public bool Sendmail(string email,int action)
        {
            var checkmember = db.UngViens.Where(x => x.EmailDN == email).FirstOrDefault();
            var checkemployer = db.CongTies.Where(x => x.Emaildangnhap == email).FirstOrDefault();
            var configmail = db.ConfigMails.Where(x => x.smtpType == 1).FirstOrDefault();
            if (configmail == null)
            {
                configmail = new ConfigMail();
            }
            if (action == 1)
            {
                Random pass = new Random();
                double pw = pass.Next(0, 999999);
                checkmember.matkhaureset = LoginMember.MD5Hash(checkmember.EmailDN) + LoginMember.MD5Hash(pw.ToString());
                string smtpUserName = configmail.smtUserName;
                string smtpPassword = configmail.smtpPassword;
                string smtpHost = configmail.smtpHost;
                int smtpPort = int.Parse(configmail.smtpPort.ToString());

                string emailTo = checkmember.EmailDN;
                if (configmail.EnableSSL == null)
                    configmail.EnableSSL = false;
                bool ssl = bool.Parse(configmail.EnableSSL.ToString());
                string subject = configmail.smtpSubject;
                string mailcontent = new EmailService().RenderPartialViewToString(this, "_MailForgotPassword", checkmember);
                string body = mailcontent;
                EmailService service = new EmailService();

                bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                    emailTo, subject, body,ssl);
                db.SaveChanges();
                return kq;
            }
            else
            {
                Random pass = new Random();
                double pw = pass.Next(0, 999999);
                checkemployer.matkhaureset = LoginMember.MD5Hash(checkemployer.Emaildangnhap) + LoginMember.MD5Hash(pw.ToString());
                string smtpUserName = configmail.smtUserName;
                string smtpPassword = configmail.smtpPassword;
                string smtpHost = configmail.smtpHost;
                int smtpPort = int.Parse(configmail.smtpPort.ToString());
                if (configmail.EnableSSL == null)
                    configmail.EnableSSL = false;
                bool ssl = bool.Parse(configmail.EnableSSL.ToString());
                string emailTo = checkemployer.Emaildangnhap;
                string subject = configmail.smtpSubject;
                string mailcontent = new EmailService().RenderPartialViewToString(this, "_MailForgotPasswordEmployer", checkemployer);
                string body = mailcontent;
                EmailService service = new EmailService();

                bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                    emailTo, subject, body,ssl);
                db.SaveChanges();
                return kq;
            }
        }
        public ActionResult ForGotPassWordMember()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForGotPassWordMember(string email)
        {
            var checkmember = db.UngViens.Where(x => x.EmailDN == email).FirstOrDefault();
            if (checkmember == null)
            {
                return Json(0);
            }
            else
            {
                bool kq=Sendmail(email,1);
                if (kq == false)
                {
                    return Json(2);
                }
                else
                {
                    return Json(1);
                }
                
            }
        }
        public ActionResult ChangePassword(string id)
        {
            var checkmember = db.UngViens.Where(x => x.matkhaureset == id.ToString()).FirstOrDefault();
            if (checkmember == null)
            {
                return Redirect("/quen-mat-khau");
            }
            else
            {
                return View(checkmember);
            }
        }
        [HttpPost]
        public ActionResult ChangePassword(string password,string email)
        {
            var checkmember = db.UngViens.Where(x => x.EmailDN == email).FirstOrDefault();
            if (checkmember == null)
            {
                return Json(0);
            }
            else
            {
                checkmember.Matkhau = LoginMember.MD5Hash(password);
                checkmember.matkhaureset = null;
                db.SaveChanges();
                return Json(1);
            }
        }
        public ActionResult ForgotPassWordEmployer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassWordEmployer(string Emaildangnhap)
        {
            var checkemployer = db.CongTies.Where(x => x.Emaildangnhap == Emaildangnhap).FirstOrDefault();
            if (checkemployer == null)
            {
                return Json(0);
            }
            else
            {
                bool kq = Sendmail(Emaildangnhap, 2);
                if (kq == false)
                {
                    return Json(2);
                }
                else
                {
                    return Json(1);
                }
            }
        }
        public ActionResult ChangePasswordEmployer(string id)
        {
            var checkemployer = db.CongTies.Where(x => x.matkhaureset == id).FirstOrDefault();
            if (checkemployer == null)
            {
                return RedirectToAction("ForgotPassWordEmployer", "ResetPassWord");
            }
            else
            {
                return View(checkemployer);
            }
        }
        [HttpPost]
        public ActionResult ChangePasswordEmployer(string Matkhau, string Emaildangnhap)
        {
            var checkemployer = db.CongTies.Where(x => x.Emaildangnhap == Emaildangnhap).FirstOrDefault();
            if (checkemployer == null)
            {
                return Json(0);
            }
            else
            {
                checkemployer.Matkhau = LoginMember.MD5Hash(Matkhau);
                checkemployer.matkhaureset = null;
                db.SaveChanges();
                return Json(1);
            }
        }
    }
}