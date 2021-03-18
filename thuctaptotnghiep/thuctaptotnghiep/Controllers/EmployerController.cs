using Model.Framework;
using Model.Setup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thuctaptotnghiep.Models;

namespace thuctaptotnghiep1.Controllers
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["Employer"] == null)
            {
                filterContext.Result = new RedirectResult("~/Employer/Login?ReturnUrl=" + HttpContext.Current.Request.RawUrl + "");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class EmployerController : Controller
    {
        // GET: Employer
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public bool Sendmail(string email)
        {
            var checkemployer = db.CongTies.Where(x => x.Emaildangnhap == email).FirstOrDefault();
            var configmail = db.ConfigMails.Where(x => x.smtpType == 2).FirstOrDefault();
            if (configmail == null)
            {
                configmail = new ConfigMail();
            }
                Random pass = new Random();
                double pw = pass.Next(0, 999999);
                checkemployer.matkhaureset = LoginMember.MD5Hash(checkemployer.Emaildangnhap) + LoginMember.MD5Hash(pw.ToString());
                string smtpUserName = configmail.smtUserName;
                string smtpPassword = configmail.smtpPassword;
                string smtpHost = configmail.smtpHost;
                 int smtpPort=25;
                if (configmail.smtpPort!=null)
                {
                    smtpPort = int.Parse(configmail.smtpPort.ToString());
                }
                string emailTo = checkemployer.Emaildangnhap;
                string subject = configmail.smtpSubject;
                string mailcontent = new EmailService().RenderPartialViewToString(this, "_ActiveEmployer", checkemployer);
                string body = mailcontent;
                if (configmail.EnableSSL == null)
                    configmail.EnableSSL = false;
                bool ssl = bool.Parse(configmail.EnableSSL.ToString());
                EmailService service = new EmailService();

                bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                    emailTo, subject, body,ssl);
                db.SaveChanges();
                return kq;
            
        }
        public bool Sendmail2(DangTinViecLam ativejob)
        {
            
            var configmail = db.ConfigMails.Where(x => x.smtpType == 3).FirstOrDefault();
            var contact = db.ConfigMails.Where(x => x.smtpType == 4).FirstOrDefault();
            if (contact == null)
            {
                contact = new ConfigMail();
            }
            if (configmail == null)
            {
                configmail = new ConfigMail();
            }
            string smtpUserName = configmail.smtUserName;
            string smtpPassword = configmail.smtpPassword;
            string smtpHost = configmail.smtpHost;
            int smtpPort = int.Parse(configmail.smtpPort.ToString());

            string emailTo = contact.smtUserName;
            string subject = configmail.smtpSubject;
            string mailcontent = new EmailService().RenderPartialViewToString(this, "_SendNotice", ativejob);
            string body = mailcontent;
            if (configmail.EnableSSL == null)
                configmail.EnableSSL = false;
            bool ssl = bool.Parse(configmail.EnableSSL.ToString());
            EmailService service = new EmailService();

            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, subject, body, ssl);
            return kq;

        }
        [HttpPost]
        public ActionResult CheckMail(string email)
        {
            var check = db.CongTies.Where(x => x.Emaildangnhap == email).FirstOrDefault();
            if (check == null)
            {
                return Json(new {status=1 });
            }
            else
            {
                return Json(new { status = 0 });
            }
            
        }
        [SessionExpire]
        public ActionResult Profile()
        {
            if (Session["Employer"] == null)
            {
                return RedirectToAction("Login", "Employer");
            }
            return View();
        }
        public ActionResult ActiveEmployer(string id)
        {
            var checkemployer = db.CongTies.Where(x => x.matkhaureset == id).FirstOrDefault();
            if (checkemployer == null)
            {
                return Redirect("/filenotfound.htm");
            }
            else
            {
                checkemployer.Tinhtrang = 1;
                checkemployer.matkhaureset = null;
                db.SaveChanges();
                return RedirectToAction("Login", "Employer");
            }
        }
        public ActionResult Login()
        {
            if (Session["Employer"] != null)
            {
                return RedirectToAction("Index", "Employer");
            }
            ViewBag.Error = TempData["Error"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employer model,string returnurl)
        {
        
            var result = new LoginMember().LoginEmployer(model.Emaildangnhap,LoginMember.MD5Hash(model.Matkhau));
            if (result == 1)
            {
                var user=db.CongTies.Where(x=>x.Emaildangnhap==model.Emaildangnhap).FirstOrDefault();
                Session["Employer"] = new CongTy() { MACT = user.MACT, Emaildangnhap = user.Emaildangnhap, Tinhtrang = user.Tinhtrang };
                user.matkhaureset = null;
                db.SaveChanges();
                if (returnurl == null||returnurl=="")
                {
                    return RedirectToAction("Index","Employer");
                }
                else
                {
                    return Redirect(returnurl);
                }
            }
            else if (result == 2)
            {
                ModelState.AddModelError("", "Tài khoản Chưa được Kích hoạt");
                TempData["Error"] = "Tài khoản Chưa được Kích hoạt";
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {

                ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác");
                TempData["Error"]= "Email hoặc mật khẩu không chính xác";
                return Redirect(Request.UrlReferrer.ToString());
            }

        }
        public ActionResult SignUp()
        {
            if (Session["Employer"] != null)
            {
                return RedirectToAction("Index", "Employer");
            }
            var listnghe = db.NghanhNghes.Where(x=>x.Idnghanhcha!=0&&x.Tinhtrang==1);
            var listcity = db.cities;
            ViewBag.data = listnghe.Where(x=>x.Tinhtrang==1).ToList();
            ViewBag.city = listcity;
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(List<string> Manghanh, int matp, string Matkhau, string Tencongty,string Dienthoaiban,string Emaildangnhap)
        {
            Employer em = new Employer();
            CongTy ct = new CongTy();
            foreach (var item in Manghanh)
            {
                int mn = int.Parse(item);
                if (ct.Manghanh1 == null)
                {
                    ct.Manghanh1 = mn;
                }
                else if (ct.Manghanh2 == null)
                {
                    ct.Manghanh2 = mn;
                }
                else
                {
                    ct.Manghanh3 = mn;
                }
                
            }
            ct.Matkhau =LoginMember.MD5Hash(Matkhau) ;
            ct.MaTP1 = matp;           
            ct.Dienthoaiban = Dienthoaiban;
            ct.Tencongty = Tencongty;
            ct.Ngaytao = DateTime.Now;
            ct.Tinhtrang = 0;
            ct.Emaildangnhap = Emaildangnhap;
            db.CongTies.Add(ct);
            db.SaveChanges();
            bool kq = Sendmail(Emaildangnhap);
            if (kq == true)
            { 
                return Json(1);
            }
            else if (kq == false)
            {
                db.CongTies.Remove(db.CongTies.Where(x=>x.Emaildangnhap==Emaildangnhap).FirstOrDefault());
                db.SaveChanges();
                return Json(2);
            }
            else
            {
                return Json(2);
            }
        }
        public ActionResult Logout()
        {
            Session["Employer"]=null;
            return RedirectToAction("Login", "Employer");
        }
        [SessionExpire]
        public ActionResult Index()
        {
            return View();
        }
        [SessionExpire]
        public ActionResult NavBar()
        {
            return View();
        }
        [SessionExpire]
        public ActionResult InforAccount()
        {
            var user = Session["Employer"] as CongTy;
            var dataem = db.CongTies.Where(x => x.MACT == user.MACT).FirstOrDefault();
            return View(dataem);
        }
        [SessionExpire]
        [HttpPost]
        public ActionResult ChangePassword(string current_password,string password_first)
        {
            var user = Session["Employer"] as CongTy;
            current_password = LoginMember.MD5Hash(current_password);
            var checkpassword = db.CongTies.Where(x => x.Matkhau == current_password && x.MACT==user.MACT).FirstOrDefault();
            if(checkpassword!=null)
            {
                checkpassword.Matkhau = LoginMember.MD5Hash(password_first);
                db.SaveChanges();
                Session["Employer"] = null;
                return Json(new { status = "SUCCESS" });             
            }
            else
            {
                return Json(new { status = "ERROR_PASSWORD" });
            }
        }
        [SessionExpire]
        public ActionResult InforCompany()
        {
            var user = Session["Employer"] as CongTy;
            ViewBag.ListNghe = db.NghanhNghes.Where(x=>x.Tinhtrang==1&&x.Idnghanhcha!=0);
            ViewBag.QuyMo = db.QuyMoCTs;
            return View(db.CongTies.Where(x=>x.MACT==user.MACT).FirstOrDefault());
        }
        [SessionExpire]
        [HttpPost]
        public ActionResult InforCompany(Employer model)
        {
            var user = Session["Employer"] as CongTy;
            var dataem = db.CongTies.Where(x => x.MACT == user.MACT).FirstOrDefault();
            if (dataem != null)
            {
                dataem.Tencongty = model.Tencongty;
                dataem.Diachi = model.Diachi;
                dataem.Quymocongty = model.Quymocongty;
                dataem.Manghanh1 = null;
                dataem.Manghanh2 = null;
                dataem.Manghanh3 = null;
                dataem.Nguoilienhe = model.Nguoilienhe;
                dataem.Thongtin = model.Thongtin;
                dataem.Dienthoaiban = model.Dienthoaiban;
                dataem.Website = model.Website;
                Employer em = new Employer();
                foreach (var item in model.listnghanh)
                {
                    if (em.Manghanh1 == null)
                    {
                        em.Manghanh1 = item;
                        dataem.Manghanh1 = item;
                    }
                    else if (em.Manghanh2 == null)
                    {
                        em.Manghanh2 = item;
                        dataem.Manghanh2 = item;
                    }
                    else
                    {
                        em.Manghanh3 = item;
                        dataem.Manghanh3 = item;
                    }
                }
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else
            {
                return Json(new { status = "ERROR" });
            }
        }
        [SessionExpire]
        public ActionResult LogoCompany()
        {
            var user = Session["Employer"] as CongTy;
            ViewBag.Logo = db.CongTies.Where(x => x.MACT == user.MACT).Select(x=>x.Logo).FirstOrDefault();
            return View(db.HinhAnhCTs.Where(x=>x.MACT==user.MACT));
        }
        [SessionExpire]
        public void deleteimg(string imgname)
        {
            string deletepath = Server.MapPath("/Employer_upload/" +imgname);
            System.IO.File.Delete(deletepath);
        }
        [SessionExpire]
        public void Uploadlogo(HttpPostedFileBase logo,int id)
        {
            var user = Session["Employer"] as CongTy;
            string filename = "img"+id + user.Emaildangnhap;
            filename = LoginMember.MD5Hash(filename) + logo.FileName;
            string path = System.IO.Path.Combine(Server.MapPath("~/Employer_upload/"), filename);
            logo.SaveAs(path);
        }
        [SessionExpire]
        [HttpPost]
        public ActionResult UploadLogo(HttpPostedFileBase logo, HttpPostedFileBase logo1, int logoid1, HttpPostedFileBase logo2, int logoid2, HttpPostedFileBase logo3, int logoid3, int logodelete1, int logodelete2,int logodelete3, string video)
        {
            var user = Session["Employer"] as CongTy;
            var logoct = db.CongTies.Where(x => x.MACT == user.MACT).FirstOrDefault();
            if (logoct!=null)
            {
                logoct.VideoCT = video;
                db.SaveChanges();
            }
            var imgcompany = db.HinhAnhCTs.Where(x => x.MACT == user.MACT);
            HinhAnhCT img = new HinhAnhCT();
            if (logo != null)
            {
                if (logoct.Logo != null)
                {
                    deleteimg(logoct.Logo);
                }
                logoct.Logo = "logo" + user.Emaildangnhap + logo.FileName;
                logoct.Logo = LoginMember.MD5Hash(logoct.Logo)+logo.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Employer_upload/"), logoct.Logo);
                logo.SaveAs(path);
                db.SaveChanges();

            }
            if (imgcompany.Count() >0)
            {
                if (logoid1 == 0 && logoid2 == 0 && logoid3 == 0)
                {
                    foreach (var item in imgcompany)
                    {
                        deleteimg(item.AnhCT);
                    }
                    db.HinhAnhCTs.RemoveRange(imgcompany);
                    db.SaveChanges();
                }
                else
                {
                    if (logoid1 == 0)
                    {
                        var delete = db.HinhAnhCTs.Where(x => x.Id == logodelete1).FirstOrDefault();
                        if (delete != null)
                        {
                            deleteimg(delete.AnhCT);
                            db.HinhAnhCTs.Remove(delete);
                            db.SaveChanges();
                        }
                    }
                    if (logoid2 == 0)
                    {
                        var delete = db.HinhAnhCTs.Where(x => x.Id == logodelete2).FirstOrDefault();
                        if (delete != null)
                        {
                            deleteimg(delete.AnhCT);
                            db.HinhAnhCTs.Remove(delete);
                            db.SaveChanges();
                        }
                    }
                    if (logoid3 == 0)
                    {
                        var delete = db.HinhAnhCTs.Where(x => x.Id == logodelete3).FirstOrDefault();
                        if (delete != null)
                        {
                            deleteimg(delete.AnhCT);
                            db.HinhAnhCTs.Remove(delete);
                            db.SaveChanges();
                        }
                    }
                }
                    if (logoid1 > 0 && logo1 != null)
                    {
                        var logoimg1 = db.HinhAnhCTs.Where(x => x.Id == logoid1).FirstOrDefault();
                        deleteimg(logoimg1.AnhCT);
                        logoimg1.AnhCT = "img1" + user.Emaildangnhap ;
                        logoimg1.AnhCT = LoginMember.MD5Hash(logoimg1.AnhCT) + logo1.FileName;
                        db.SaveChanges();
                        Uploadlogo(logo1, 1);
                        deleteimg(logoimg1.AnhCT);
                    }
                    if (logoid2 > 0 && logo2 != null)
                    {
                        var logoimg1 = db.HinhAnhCTs.Where(x => x.Id == logoid1).FirstOrDefault();
                        deleteimg(logoimg1.AnhCT);
                        logoimg1.AnhCT = "img2" + user.Emaildangnhap ;
                        logoimg1.AnhCT = LoginMember.MD5Hash(logoimg1.AnhCT) + logo1.FileName;
                        db.SaveChanges();
                        Uploadlogo(logo1, 3);
                        
                    }
                    if (logoid3 > 0 && logo3 != null)
                    {
                        var logoimg1 = db.HinhAnhCTs.Where(x => x.Id == logoid1).FirstOrDefault();
                        deleteimg(logoimg1.AnhCT);
                        logoimg1.AnhCT = "img3" + user.Emaildangnhap ;
                        logoimg1.AnhCT = LoginMember.MD5Hash(logoimg1.AnhCT) + logo1.FileName;
                        db.SaveChanges();
                        Uploadlogo(logo1, 3);
                    }
                    if (logo1 != null)
                    {
                        var delete1 = db.HinhAnhCTs.Where(x => x.MACT == user.MACT).FirstOrDefault();
                        if (delete1 != null)
                        {
                            deleteimg(delete1.AnhCT);
                            db.HinhAnhCTs.Remove(delete1);
                            db.SaveChanges();
                        }
                        Uploadlogo(logo1, 1);
                        img.AnhCT = "img1" + user.Emaildangnhap ;
                        img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo1.FileName;
                        img.MACT = user.MACT;
                        db.HinhAnhCTs.Add(img);
                        db.SaveChanges();
                    }
                    if (logo2 != null)
                    {
                        var delete1 = db.HinhAnhCTs.Where(x => x.MACT == user.MACT).OrderBy(x=>x.Id).Skip(1).FirstOrDefault();
                        if (delete1 != null)
                        {
                            deleteimg(delete1.AnhCT);
                            db.HinhAnhCTs.Remove(delete1);
                            db.SaveChanges();
                        }
                        Uploadlogo(logo2, 2);
                        img.AnhCT = "img2" + user.Emaildangnhap ;
                        img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo2.FileName;
                        img.MACT = user.MACT;
                        db.HinhAnhCTs.Add(img);
                        db.SaveChanges();
                    }
                    if (logo3 != null)
                    {
                        var delete1 = db.HinhAnhCTs.Where(x => x.MACT == user.MACT).OrderBy(x=>x.Id).Skip(2).FirstOrDefault();
                        if (delete1 != null)
                        {
                            deleteimg(delete1.AnhCT);
                            db.HinhAnhCTs.Remove(delete1);
                            db.SaveChanges();
                        }
                        img.AnhCT = "img3" + user.Emaildangnhap ;
                        img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo3.FileName;
                        img.MACT = user.MACT;
                        db.HinhAnhCTs.Add(img);
                        db.SaveChanges();
                        Uploadlogo(logo3, 3);
                    }
            }
            else
            {
                if (logo1 != null)
                {
                    Uploadlogo(logo1,1);
                    img.AnhCT = "img1" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo1.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                }
                if (logo2 != null)
                {
                    Uploadlogo(logo2,2);
                    img.AnhCT = "img2" + user.Emaildangnhap ;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo2.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                }
                if (logo3 != null)
                {
                    img.AnhCT = "img3" + user.Emaildangnhap ;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo3.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                    Uploadlogo(logo3,3);
                }
            }
            return Json(new { status = "SUCCESS" });
        }
        
        [HttpPost]
        public ActionResult AutoCompleteSkill(string key)
        {
            var data = db.KyNangs.Where(x => x.TenKN.Contains(key));
            List<complete> dt = new List<complete>();
            complete data1 = new complete();
            List<string> list = new List<string>();
            foreach (var item in data)
            {
                list.Add(item.TenKN);
            }
            return Json(list);
        }
        [SessionExpire]
        public ActionResult CreateWork()
        {
            ViewBag.ListNghanh = db.NghanhNghes.Where(x => x.Tinhtrang == 1&&x.Idnghanhcha!=0);
            ViewBag.City = db.cities;
            ViewBag.CB = db.CBmongmuons;
            return View();
        }
        [SessionExpire]
        [HttpPost]
        public ActionResult CreateWork(DangTinViecLam model, List<int> categories, List<int> city, string skill, string Dateex)
        {
            var user = Session["Employer"] as CongTy;
            string[] reslts = skill.Split(new char[] { '`', '`' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in reslts)
            {
                if (model.Tags1 == null)
                {
                    model.Tags1 = item;
                }
                else if (model.Tags2 == null)
                {
                    model.Tags2 = item;
                }
                else  if (model.Tags3 == null)
                {
                    model.Tags3 = item;
                }
            }
            foreach(var item in categories )
            {
                if(model.Manghanh1==null)
                {
                    model.Manghanh1=item;
                    //model.NghanhNghe1 = db.NghanhNghes.Where(x => x.Id == model.Manghanh1).Select(x => x.Tennghanh).FirstOrDefault();
                }
                else if(model.Manghanh2==null)
                {
                    model.Manghanh2=item;
                    //model.Thanhpho2 = db.NghanhNghes.Where(x => x.Id == model.Manghanh2).Select(x => x.Tennghanh).FirstOrDefault();
                }
                else
                {
                    model.Manghanh3=item;
                    //model.Thanhpho3 = db.NghanhNghes.Where(x => x.Id == model.Manghanh3).Select(x => x.Tennghanh).FirstOrDefault();
                }
            }
            foreach(var item in city )
            {
                if(model.MaTP1==null){
                    model.MaTP1=item;
                    model.Thanhpho1 = db.cities.Where(x => x.Id == model.MaTP1).Select(x => x.City1).FirstOrDefault();
                }
                else if(model.MaTP2==null)
                {
                    model.MaTP2=item;
                    model.Thanhpho2 = db.cities.Where(x => x.Id == model.MaTP2).Select(x => x.City1).FirstOrDefault();
                }
                else
                {
                    model.MaTP3=item;
                    model.Thanhpho3 = db.cities.Where(x => x.Id == model.MaTP3).Select(x => x.City1).FirstOrDefault();
                }
            }
            Random rm = new Random();
            double start = rm.Next(0, 99999);
            while (db.DangTinViecLams.Where(x => x.AdminDienthoai == start.ToString()).FirstOrDefault() != null)
            {
                start = rm.Next(0, 99999);
            }
            DateTime endday;
            bool tryb = DateTime.TryParseExact(Dateex, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endday);
            model.Tinhtrang = 0;
            model.Trangthai = 0;
            model.Ngayhethangjob = endday;
            model.AdminDienthoai = start.ToString();
            model.MotaCV = model.MotaCV;
            model.YeucauCV = model.YeucauCV;
            model.Ngaydang = DateTime.Now;
            model.MACT = user.MACT;
            db.DangTinViecLams.Add(model);
            db.SaveChanges();
            var datasave = db.DangTinViecLams.Where(x => x.AdminDienthoai == start.ToString()).FirstOrDefault();
            datasave.AdminDienthoai = null;
            db.SaveChanges();
            bool kq = Sendmail2(datasave);
            if (kq == true)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
           
        }
        [SessionExpire]
        public ActionResult ListJob(int ?page)
        {
            var user = Session["Employer"] as CongTy;
            int pagenumber;
            if (page == null)
            {
                page = 1;
                pagenumber = 0;
            }
            else
            {
                string pagen = page.ToString();
                pagenumber = (Convert.ToInt32(pagen) * 20) - 20;
            }
            var products = db.DangTinViecLams.Where(x=>x.MACT==user.MACT && x.Tinhtrang==0).ToList();
            int c = products.Count();
            int pageajax = c / 20;
            if (c % 20 != 0)
            {
                pageajax++;
            }
            ViewData["page"] = pageajax;
            var listjob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 0).OrderByDescending(x=>x.MACV).Skip(pagenumber).Take(20).ToList();
            ViewBag.Active = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 1).Count();
            ViewBag.InActive = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 0).Count();
            ViewBag.HideJob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 0).Count();
            return View(listjob);
        }
        [SessionExpire]
        public ActionResult ActiveJob(int? page)
        {
            var user = Session["Employer"] as CongTy;
            int pagenumber;
            if (page == null)
            {
                page = 1;
                pagenumber = 0;
            }
            else
            {
                string pagen = page.ToString();
                pagenumber = (Convert.ToInt32(pagen) * 20) - 20;
            }
            var products = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Trangthai == 1 && x.Tinhtrang == 1).ToList();
            int c = products.Count();
            int pageajax = c / 20;
            if (c % 20 != 0)
            {
                pageajax++;
            }
            ViewData["page"] = pageajax;
            var listjob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 1).OrderByDescending(x => x.MACV).Skip(pagenumber).Take(20).ToList();
            ViewBag.Active = db.DangTinViecLams.Where(x=>x.MACT==user.MACT && x.Tinhtrang==1 && x.Trangthai==1).Count();
            ViewBag.InActive = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 0).Count();
            ViewBag.HideJob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 0).Count();
            return View(listjob);
        }
        [SessionExpire]
        public ActionResult Hidejob(int? page)
        {
            var user = Session["Employer"] as CongTy;
            int pagenumber;
            if (page == null)
            {
                page = 1;
                pagenumber = 0;
            }
            else
            {
                string pagen = page.ToString();
                pagenumber = (Convert.ToInt32(pagen) * 20) - 20;
            }
            var products = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Trangthai == 0 && x.Tinhtrang == 1).ToList();
            int c = products.Count();
            int pageajax = c / 20;
            if (c % 20 != 0)
            {
                pageajax++;
            }
            ViewData["page"] = pageajax;
            var listjob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 0).OrderByDescending(x => x.MACV).Skip(pagenumber).Take(20).ToList();
            ViewBag.Active = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 1).Count();
            ViewBag.InActive = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 0).Count();
            ViewBag.HideJob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 0).Count();
            return View(listjob);
        }
        [SessionExpire]
        public ActionResult Expired(int? page)
        {
            var user = Session["Employer"] as CongTy;
            int pagenumber;
            if (page == null)
            {
                page = 1;
                pagenumber = 0;
            }
            else
            {
                string pagen = page.ToString();
                pagenumber = (Convert.ToInt32(pagen) * 20) - 20;
            }
            var products = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Trangthai == -1 && x.Tinhtrang == 1).ToList();
            int c = products.Count();
            int pageajax = c / 20;
            if (c % 20 != 0)
            {
                pageajax++;
            }
            ViewData["page"] = pageajax;
            var listjob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == -1).OrderByDescending(x => x.MACV).Skip(pagenumber).Take(20).ToList();
            ViewBag.Active = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 1).Count();
            ViewBag.InActive = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 0).Count();
            ViewBag.HideJob = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1 && x.Trangthai == 0).Count();
            return View(listjob);
        }
        [SessionExpire]
        public ActionResult ActiveHidejob(int id)
        {
            var user = Session["Employer"] as CongTy;
            var datahide = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.MACV==id).FirstOrDefault();
            datahide.Trangthai = 0;
            db.SaveChanges();
            return Json(new { status = "SUCCESS" });
        }
        [SessionExpire]
        public ActionResult ActiveActivejob(int id)
        {
            var user = Session["Employer"] as CongTy;
            var datahide = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.MACV == id).FirstOrDefault();
            datahide.Trangthai = 1;
            db.SaveChanges();
            return Json(new { status = "SUCCESS" });
        }
        [SessionExpire]
        public ActionResult ActiveExpiredjob(int id)
        {
            var user = Session["Employer"] as CongTy;
            var datahide = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.MACV == id).FirstOrDefault();
            datahide.Trangthai = -1;
            db.SaveChanges();
            return Json(new { status = "SUCCESS" });
        }
        [SessionExpire]
        public ActionResult EditJob(int id)
        {
            var user = Session["Employer"] as CongTy;
            var dataedit = db.DangTinViecLams.Where(x => x.MACV == id && x.MACT == user.MACT&&x.Tinhtrang!=-1).FirstOrDefault();
            if (dataedit == null)
            {
                return Redirect("/filenotfound.htm");
            }
            ViewBag.ListNghanh = db.NghanhNghes.Where(x=>x.Tinhtrang==1&&x.Idnghanhcha!=0);
            List<string> language = new List<string>(new string[] { "Bất Kỳ", "Tiếng Anh", "Tiếng Việt", "Tiếng Trung Quốc", "Tiếng Nhật", "Tiếng Hàn Quốc", "Tiếng Pháp" ,
    "Tiếng Pháp","Tiếng Tây Ban Nha","Tiếng Ý"});
            ViewBag.Lag = language;
            ViewBag.City = db.cities;
            ViewBag.CB = db.CBmongmuons;
            return View(dataedit);
        }
        [HttpPost]
        public ActionResult EditJob(DangTinViecLam model, int id, List<int> categories, List<int> city, string skill, string Dateex)
        {
            var user = Session["Employer"] as CongTy;
            var dataedit = db.DangTinViecLams.Where(x => x.MACV == id && x.MACT==user.MACT&&x.Tinhtrang!=-1).FirstOrDefault();
            if (dataedit == null)
            {
                return Json(0);
            }
            if (skill == null)
            {
                return Json(0);
            }
            string[] reslts = skill.Split(new char[] { '`', '`' }, StringSplitOptions.RemoveEmptyEntries);
            dataedit.MaTP1 = null;
            dataedit.MaTP2 = null;
            dataedit.MaTP3 = null;
            dataedit.Tags1 = null;
            dataedit.Tags2 = null;
            dataedit.Tags3 = null;
            dataedit.Manghanh1 = null;
            dataedit.Manghanh2 = null;
            dataedit.Manghanh3 = null;
            foreach (string item in reslts)
            {
                if (model.Tags1 == null)
                {
                    dataedit.Tags1 = item;
                    model.Tags1 = item;
                }
                else if (model.Tags2 == null)
                {
                    dataedit.Tags2 = item;
                    model.Tags2 = item;
                }
                else if (model.Tags3 == null)
                {
                    dataedit.Tags3 = item;
                    model.Tags3 = item;
                }
            }
            foreach (var item in categories)
            {
                if (model.Manghanh1 == null)
                {
                    model.Manghanh1 = item;
                    dataedit.Manghanh1 = item;
                    dataedit.Thanhpho1 = db.NghanhNghes.Where(x => x.Id == model.Manghanh1).Select(x => x.Tennghanh).FirstOrDefault();
                }
                else if (model.Manghanh2 == null)
                {
                    model.Manghanh2 = item;
                    dataedit.Manghanh2 = item;
                    dataedit.Thanhpho2 = db.NghanhNghes.Where(x => x.Id == model.Manghanh2).Select(x => x.Tennghanh).FirstOrDefault();
                }
                else
                {
                    model.Manghanh3 = item;
                    dataedit.Manghanh3 = item;
                    dataedit.Thanhpho3 = db.NghanhNghes.Where(x => x.Id == model.Manghanh3).Select(x => x.Tennghanh).FirstOrDefault();
                }
            }
            foreach (var item in city)
            {
                if (dataedit.MaTP1 == null)
                {
                    model.MaTP1 = item;
                    dataedit.MaTP1 = item;
                    dataedit.Thanhpho1 = db.cities.Where(x => x.Id == model.MaTP1).Select(x => x.City1).FirstOrDefault();
                }
                else if (model.MaTP2 == null)
                {
                    model.MaTP2 = item;
                    dataedit.MaTP2 = item;
                    dataedit.Thanhpho2 = db.cities.Where(x => x.Id == model.MaTP2).Select(x => x.City1).FirstOrDefault();
                }
                else
                {
                    model.MaTP3 = item;
                    dataedit.MaTP3 = item;
                    dataedit.Thanhpho3 = db.cities.Where(x => x.Id == model.MaTP3).Select(x => x.City1).FirstOrDefault();
                }
            }
            DateTime endday;
            bool tryb = DateTime.TryParseExact(Dateex, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endday);
            dataedit.Ngayhethangjob = endday;
            dataedit.NguoiLH = model.NguoiLH;
            dataedit.Email = model.Email;
            dataedit.DiachiHS = model.DiachiHS;
            dataedit.TenCV = model.TenCV;
            dataedit.Luongthoathuan = model.Luongthoathuan;
            dataedit.Ngaydang = DateTime.Now;
            dataedit.Minluong = model.Minluong;
            dataedit.DienthoaiLH = model.DienthoaiLH;
            dataedit.Maxluong = model.Maxluong;
            dataedit.MotaCV = model.MotaCV;
            dataedit.YeucauCV = model.YeucauCV;
            dataedit.NgonnguHS = model.NgonnguHS;
            dataedit.MACB = model.MACB;
            db.SaveChanges();
            return Json(1);
        }
        public ActionResult CopyJob(int id)
        {
            var user = Session["Employer"] as CongTy;
            if (user == null)
            {
                return Json(new { status = "ERROR" });
            }
            else
            {
                var model = db.DangTinViecLams.Where(x => x.MACV == id && x.MACT == user.MACT).FirstOrDefault();
                Random rm = new Random();
                double start = rm.Next(0, 99999);
                while (db.DangTinViecLams.Where(x=>x.AdminDienthoai==start.ToString()).FirstOrDefault()!=null)
                {
                     start = rm.Next(0, 99999);
                }
                DangTinViecLam dataedit = new DangTinViecLam();
                
                dataedit.NguoiLH = model.NguoiLH;
                dataedit.Email = model.Email;
                dataedit.DiachiHS = model.DiachiHS;
                dataedit.MACT = user.MACT;
                dataedit.TenCV = model.TenCV;
                dataedit.Ngaydang = DateTime.Now;
                dataedit.Luongthoathuan = model.Luongthoathuan;
                dataedit.Minluong = model.Minluong;
                dataedit.Ngayhethangjob = model.Ngayhethangjob;
                dataedit.DienthoaiLH = model.DienthoaiLH;
                dataedit.Maxluong = model.Maxluong;
                dataedit.MotaCV = model.MotaCV;
                dataedit.YeucauCV = model.YeucauCV;
                dataedit.NgonnguHS = model.NgonnguHS;
                dataedit.Tags1 = model.Tags1;
                dataedit.Trangthai = 0;
                dataedit.Tinhtrang = 0;
                dataedit.Tags2 = model.Tags2;
                dataedit.Tags3 = model.Tags3;
                dataedit.MaTP1 = model.MaTP1;
                dataedit.MaTP2 = model.MaTP2;
                dataedit.AdminDienthoai = start.ToString();
                dataedit.MaTP3 = model.MaTP3;
                dataedit.Manghanh1 = model.Manghanh1;
                dataedit.Manghanh2 = model.Manghanh2;
                dataedit.Manghanh3 = model.Manghanh3;
                dataedit.MACB = model.MACB;
                db.DangTinViecLams.Add(dataedit);
                db.SaveChanges();
                var copyed = db.DangTinViecLams.Where(x => x.AdminDienthoai==start.ToString() && x.MACT==user.MACT).FirstOrDefault();
                copyed.AdminDienthoai = null;
                db.SaveChanges();
                var kq = Sendmail2(copyed);
                return Json(new { status = "SUCCESS", editUrl="/Employer/EditJob?id="+copyed.MACV+"" });
            }
            
        }
        public string RenderPartialViewToString(Controller Employer, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = Employer.ControllerContext.RouteData.GetRequiredString("action");
            }

            Employer.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Employer.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(Employer.ControllerContext, viewResult.View, Employer.ViewData, Employer.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        [SessionExpire]
        public ActionResult PageList(int page,int action)
        {
            var user=Session["Employer"] as CongTy;
            var model=new List<DangTinViecLam>();
            int pagenumber;
            if (page == null)
            {
                page = 1;
                pagenumber = 0;
            }
            else
            {
                string pagen = page.ToString();
                pagenumber = (Convert.ToInt32(pagen) * 20) - 20;
            }
            if (action == 0)
            {
                 model = db.DangTinViecLams.Where(x=>x.MACT==user.MACT && x.Tinhtrang==0).OrderByDescending(x=>x.MACV).Skip(pagenumber).Take(20).ToList();
            }
            else if (action == 1)
            {
                model = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Trangthai == 1 && x.Tinhtrang == 1).OrderByDescending(x => x.MACV).Skip(pagenumber).Take(20).ToList();
            }
            else if (action == 2)
            {
                model = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Trangthai == 0 && x.Tinhtrang == 1).OrderByDescending(x => x.MACV).Skip(pagenumber).Take(20).ToList();
            }
            else if (action == -1)
            {
                model = db.DangTinViecLams.Where(x => x.MACT == user.MACT && x.Trangthai == -1 && x.Tinhtrang == 1).OrderByDescending(x => x.MACV).Skip(pagenumber).Take(20).ToList();
            }
            string html = RenderPartialViewToString(this, "_PageList", model);
            return Json(html);
        }
        public ActionResult DeleteJob(int id)
        {
            var user = Session["Employer"] as CongTy;
            if (user == null)
            {
                return Json(new { status = "ERROR" });
            }
            else
            {
                var deletejob = db.DangTinViecLams.Where(x => x.MACV == id && x.MACT == user.MACT).FirstOrDefault();
                db.DangTinViecLams.Remove(deletejob);
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            } 
        }
       
    }
}