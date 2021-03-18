using Model.Setup;
using Model.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thuctaptotnghiep.Models;

namespace thuctaptotnghiep.Controllers
{
    public class JobController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: Job
        public bool Sendmail(UVDangKy uvdk)
        {
            
            var checkemployer = db.CongTies.Where(x => x.MACT == uvdk.MACT).FirstOrDefault();
            var configmail = db.ConfigMails.Where(x => x.smtpType == 3).FirstOrDefault();
            if (configmail == null)
            {
                configmail = new ConfigMail();
            }
            string smtpUserName = configmail.smtUserName;
            string smtpPassword = configmail.smtpPassword;
            string smtpHost = configmail.smtpHost;
            if (configmail.EnableSSL == null)
                configmail.EnableSSL = false;
            bool ssl =  bool.Parse(configmail.EnableSSL.ToString());
            int smtpPort = int.Parse(configmail.smtpPort.ToString());

            string emailTo = uvdk.DangTinViecLam.Email;
            string subject = configmail.smtpSubject;
            string mailcontent = new EmailService().RenderPartialViewToString(this, "_SendMailEmployer", uvdk);
            string body = mailcontent;
            EmailService service = new EmailService();

            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, subject, body,ssl);
            return kq;

        }
        public bool Sendmail2(UVDangKy uvdk)
        {
            
            var checkmember = db.UngViens.Where(x => x.MAUV == uvdk.MAUV).FirstOrDefault();
            var configmail = db.ConfigMails.Where(x => x.smtpType == 3).FirstOrDefault();
            if (configmail == null)
            {
                configmail = new ConfigMail();
            }
            string smtpUserName = configmail.smtUserName;
            string smtpPassword = configmail.smtpPassword;
            string smtpHost = configmail.smtpHost;
            if (configmail.EnableSSL == null)
                configmail.EnableSSL = false;
            bool ssl = bool.Parse(configmail.EnableSSL.ToString());
            int smtpPort = int.Parse(configmail.smtpPort.ToString());

            string emailTo = checkmember.EmailLH;
            string subject = configmail.smtpSubject;
            string mailcontent = new EmailService().RenderPartialViewToString(this, "_SendMailMember", uvdk);
            string body = mailcontent;
            EmailService service = new EmailService();

            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, subject, body, ssl);
            return kq;

        }
        public ActionResult ShowJobHomePage()
        {
            return View();
        }
        public ActionResult TopJob()
        {
            var top = db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1 && x.Vieclamtotnhat == 1 &&x.hot!=1).OrderByDescending(x=>x.MACV).ToList();
            var tophot = db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1 && x.Vieclamtotnhat == 1 && x.hot==1).OrderByDescending(x=>x.MACV).ToList();
            foreach (var item in top) {
                if (DateTime.Now == item.NgayhethangTop)
                {
                    item.Vieclamtotnhat = 0;
                    db.SaveChanges();
                }
            }
            foreach (var item in tophot)
            {
                if (DateTime.Now == item.NgayhethangTop)
                {
                    item.Vieclamtotnhat = 0;
                    db.SaveChanges();
                }
            }
            tophot.AddRange(top);
            return View(tophot);
        }
        public string RenderPartialViewToString(Controller Job, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = Job.ControllerContext.RouteData.GetRequiredString("action");
            }

            Job.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Job.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(Job.ControllerContext, viewResult.View, Job.ViewData, Job.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult RecommandJob()
        {
            var re = db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1 && x.Vieclamgoiy == 1).OrderByDescending(x=>x.Ngaydang).ToList();
            foreach(var item in re)
             {
                if(DateTime.Now==item.NgayhethangRe){
                    item.Vieclamgoiy=0;
                }
            }
           string html = RenderPartialViewToString(this, "_RecommandJob", re);
           return Json(html);
        }
        public ActionResult AllJob()
        {
            var listjob = db.NghanhNghes.Where(x => x.Tinhtrang == 1).ToList();
            return View(listjob);
        }
        public ActionResult DetailJob(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            var datadetail = db.DangTinViecLams.Where(x => x.MACV == id && x.Trangthai == 1 && x.Tinhtrang == 1).FirstOrDefault();
            if (datadetail == null)
            {
                return Redirect("/");
            }
            if (datadetail.Luotxem == null)
            {
                datadetail.Luotxem = 1;
                db.SaveChanges();
            }
            else
            {
                datadetail.Luotxem = datadetail.Luotxem+1;
                db.SaveChanges();
            }
            return View(datadetail);
        }
        public ActionResult CheckUser(int id)
        {
            var user = Session["Member"] as Member;
            if (user == null)
            {
                return Json(new { status = 0 });
            }
            else
            {
                var checkapplyjob = db.UVDangKies.Where(x => x.MACV == id &&x.Tinhtrang==1 &&x.MAUV==user.MAUV).FirstOrDefault();
                if (checkapplyjob != null)
                {
                    return Json(new { status = 2 });
                }
                else
                {
                    var data = db.DangTinViecLams.Where(x => x.MACV == id&&x.Tinhtrang==1 &&x.Trangthai==1).FirstOrDefault();
                    if (data == null)
                    {
                        return Json(new { status = 3 });
                    }
                    else
                    {
                        var datauser = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
                        return Json(new { status = 1, phone = datauser.Dienthoai1, email = datauser.EmailLH, jobtitle = data.TenCV });
                    }
                }
            }
        }
        [SessionExpire]
        [HttpPost]
        public ActionResult ApplyJob(string email, string phone, int id)
        {
            var user = Session["Member"] as Member;
            if(user==null)
            {
                return RedirectToAction("Login","Member");
            }
            else if (db.DangTinViecLams.Where(x => x.MACV == id && x.Tinhtrang == 1&&x.Trangthai==1).FirstOrDefault() == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else{
                var update=db.UngViens.Where(x=>x.MAUV==user.MAUV).FirstOrDefault();
                update.Dienthoai1 = phone;
                update.EmailLH = email;
                db.SaveChanges();
                var checktinhtrang = db.UVDangKies.Where(x => x.MACV == id && x.MAUV == user.MAUV && x.Tinhtrang == 0).FirstOrDefault();
                if (checktinhtrang != null)
                {

                    checktinhtrang.Tinhtrang =1;
                    checktinhtrang.Trangthai = 0;
                    bool kq = Sendmail(checktinhtrang);
                    bool kq1 = Sendmail2(checktinhtrang);
                    Uri myUri = new Uri(Request.Url.ToString());
                    string host = myUri.Host; 
                    if (kq == true && kq1 == true)
                    {
                        bool sendsms = new SMS().SendSms(checktinhtrang.CongTy.Dienthoaiban, "chao " + new RemoveUnicodeModel().UnicodeNameModel(checktinhtrang.CongTy.Tencongty) + " Ban co mot ung vien da ung tuyen vao cong viec cua ban tren "+host);
                        bool sendms2 = new SMS().SendSms(checktinhtrang.UngVien.Dienthoai1, "chao " + checktinhtrang.UngVien.EmailDN + " ban vua ung tuyen vao vi tri " + new RemoveUnicodeModel().UnicodeNameModel(checktinhtrang.DangTinViecLam.TenCV) + ", vui long giu lien lac de lien he");
                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("ListApplyJob", "Member");
                }
                else
                {
                    UVDangKy dk = new UVDangKy();
                    dk.MAUV = user.MAUV;
                    dk.MACT = db.DangTinViecLams.Where(x => x.MACV == id).Select(x => x.MACT).FirstOrDefault();
                    dk.MACV = id;
                    dk.Tinhtrang = 0;
                    dk.Trangthai = 0;
                    dk.Ngaydangky = DateTime.Now;
                    db.UVDangKies.Add(dk);
                    db.SaveChanges();
                    var uvdk = db.UVDangKies.Where(x => x.MACV == id && x.MAUV == user.MAUV).FirstOrDefault();
                    bool kq = Sendmail(uvdk);
                    bool kq1 = Sendmail2(uvdk);
                    Uri myUri = new Uri(Request.Url.ToString());
                    string host = myUri.Host; 
                    if (kq == true&&kq1==true)
                    {
                        bool sendsms = new SMS().SendSms(uvdk.CongTy.Dienthoaiban, "xin chao "+new RemoveUnicodeModel().UnicodeNameModel(uvdk.CongTy.Tencongty) +" Ban co mot ung vien da ung tuyen vao cong viec cua ban tren "+host);
                        bool sendms2 = new SMS().SendSms(uvdk.UngVien.Dienthoai1, "xin chao "+uvdk.UngVien.EmailDN+" ban vua ung tuyen vao vi tri " + new RemoveUnicodeModel().UnicodeNameModel(uvdk.DangTinViecLam.TenCV) + ", vui long giu lien lac de lien he");
                        uvdk.Tinhtrang = 1;
                            db.SaveChanges();
                    }
                    return RedirectToAction("ListApplyJob", "Member");
                }
            }

        }
        [SessionExpire]
        public ActionResult SaveJob(int? macv)
        {
            var user=Session["Member"] as Member;
            if (macv == null)
            {
                return Redirect("/");
            }
            var datajob=db.DangTinViecLams.Where(x=>x.MACV==macv&&x.Tinhtrang==1&&x.Trangthai==1).FirstOrDefault();
            if (datajob == null)
            {
                return Redirect("/");
            }
            var checkdk = db.UVDangKies.Where(x => x.MACV == macv && x.MAUV == user.MAUV).FirstOrDefault();
            if (checkdk == null)
            {
                UVDangKy dk = new UVDangKy();
                dk.MAUV = user.MAUV;
                dk.MACV = macv;
                dk.MACT = datajob.CongTy.MACT;
                dk.Ngaydangky = DateTime.Now;
                dk.Tinhtrang = 0;
                dk.Trangthai = 0;
                db.UVDangKies.Add(dk);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}