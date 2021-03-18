using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thuctaptotnghiep.Models;
using Model.Framework;
using PagedList.Mvc;
using PagedList;
using Model.Setup;
using System.Text.RegularExpressions;
using System.Text;
namespace thuctaptotnghiep1.Controllers
{
    [SessionExpire]
    public class ManagerJobController : Controller
    {
        // GET: ManagerJob
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public bool Sendmail2(LuuHoso uvdk)
        {
           
            string email = uvdk.UngVien.EmailDN;
            var checkmember = db.UngViens.Where(x => x.EmailDN == email).FirstOrDefault();
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
            string mailcontent = new EmailService().RenderPartialViewToString(this, "_sendmessage", uvdk);
            string body = mailcontent;
            EmailService service = new EmailService();

            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, subject, body,ssl);
            return kq;

        }
        public bool Sendmail(UVDangKy uvdk)
        {
           
            string email = uvdk.UngVien.EmailDN;
            var checkmember = db.UngViens.Where(x => x.EmailDN == email).FirstOrDefault();
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
            string mailcontent = new EmailService().RenderPartialViewToString(this, "_Sendinterview", uvdk);
            string body = mailcontent;
            EmailService service = new EmailService();

            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, subject, body,ssl);
            return kq;

        }
        public ActionResult MemberForJob(string curentfiter, int? page)
        {
            var user = Session["Employer"] as CongTy;
            ViewBag.curentfiter = page;
            int pagesize = 20;
            int pagenumber = (page ?? 1);
            var datamember = db.UVDangKies.Where(x => x.MACT == user.MACT && x.Tinhtrang == 1).ToList();
            if (datamember.OrderByDescending(x => x.Id).ToPagedList(pagenumber, pagesize).Count() == 0)
            {
                return View(datamember.OrderByDescending(x => x.Id).ToPagedList(1, pagesize));
            }
            return View(datamember.OrderByDescending(x => x.Id).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult JobForMember(string curentfiter, int? page,int? macv)
        {
            if (macv == null)
            {
                return RedirectToAction("MemberForJob", "ManagerJob");
            }
            var user = Session["Employer"] as CongTy;
            ViewBag.curentfiter = page;
            int pagesize = 20;
            int pagenumber = (page ?? 1);
            var datajob = db.UVDangKies.Where(x=>x.MACV==macv &&x.Tinhtrang==1&&x.MACT==user.MACT).ToList();
            ViewData["MACV"] = macv;
            if (datajob.OrderByDescending(x => x.Id).ToPagedList(pagenumber, pagesize).Count() == 0)
            {
                return View(datajob.OrderByDescending(x => x.Id).ToPagedList(1, pagesize));
            }
            return View(datajob.OrderByDescending(x => x.Id).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult DetailMember(int id,int madk)
        {
            var user = Session["Employer"] as CongTy;
            var datamember = db.UngViens.Where(x => x.MAUV == id).FirstOrDefault();
            if (datamember == null)
            {
                return Redirect("/filenotfound.htm");
            }
            if (datamember.Luotxem == null)
            {
                datamember.Luotxem = 1;
                db.SaveChanges();
            }
            else
            {
                datamember.Luotxem += 1;
                db.SaveChanges();
            }
            var datadk = db.UVDangKies.Where(x => x.Id == madk && x.Tinhtrang == 1&&x.MACT==user.MACT).FirstOrDefault();
            if (datadk == null)
            {
                return Redirect("/ManagerJob/MemberForJob");
            }
            if (datadk.Trangthai == 0)
            {
                datadk.Trangthai = 1;
            }
            db.SaveChanges();
            ViewData["MADK"] = madk;
            return View(datamember);
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult ApplyUV(int ?madk,string notes)
        {
            if (madk == null)
            {
                return RedirectToAction("MemberForJob", "ManagerJob");
            }
            var datacv = db.UVDangKies.Where(x => x.Id == madk && x.Tinhtrang == 1).FirstOrDefault();
            if (datacv == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
            datacv.Trangthai=2;
            datacv.Ngayphanhoi = DateTime.Now;
            datacv.Tinnhan = notes;
            bool kq = Sendmail(datacv);
            Uri myUri = new Uri(Request.Url.ToString());
            string host = myUri.Host; 
            if (kq == true)
            {
                bool sendms2 = new SMS().SendSms(datacv.UngVien.Dienthoai1, "xin chao "+datacv.UngVien.EmailDN+" chung toi da gui cho ban mot email thong bao, vui long kiem tra email tren "+host);
                db.SaveChanges();
            }
            return RedirectToAction("ListAppyJob", "ManagerJob", new {macv=datacv.MACV });
            }
        }
        public ActionResult ListAppyJob(int? macv,string curentfiter, int? page)
        {
            if (macv == null)
            {
                    return RedirectToAction("MemberForJob", "ManagerJob");
            }
            var datajobmember = db.UVDangKies.Where(x => x.Trangthai == 2 && x.MACV == macv && x.Tinhtrang == 1).ToList();
            ViewBag.curentfiter = page;
            int pagesize = 20;
            int pagenumber = (page ?? 1);
                ViewData["MACV"] = macv;
                if (datajobmember.OrderByDescending(x => x.Ngayphanhoi).ToPagedList(pagenumber, pagesize).Count()==0)
                {
                    return View(datajobmember.OrderByDescending(x => x.Ngayphanhoi).ToPagedList(1, pagesize));
                }
            return View(datajobmember.OrderByDescending(x => x.Ngayphanhoi).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ChangeStatus(int madk,int status)
        {
            Uri myUri = new Uri(Request.Url.ToString());
            string host = myUri.Host; 
            var datadk = db.UVDangKies.Where(x => x.Id == madk && x.Tinhtrang == 1).FirstOrDefault();
            if (datadk == null)
            {
                return Json(0);
            }
            else
            {
                if (status == 5)
                {
                    datadk.Trangthai = 1;
                    db.SaveChanges();
                    return Json(3);
                }
                else if (status == 4)
                {
                    datadk.Trangthai = 4;
                    bool kq = Sendmail(datadk);
                    
                    if (kq == true)
                    {
                        bool sendms2 = new SMS().SendSms(datadk.UngVien.Dienthoai1, "xin chao "+datadk.UngVien.EmailDN+" chung toi da gui cho ban mot email, vui long kiem tra email tren"+host);
                        db.SaveChanges();
                    }
                }
                else if (status == 3)
                {
                    datadk.Trangthai = 3;
                    bool kq = Sendmail(datadk);

                    if (kq == true)
                    {
                        bool sendms2 = new SMS().SendSms(datadk.UngVien.Dienthoai1, "xin chao " + datadk.UngVien.EmailDN + " chung toi da gui cho ban mot email, vui long kiem tra email tren"+host);
                        db.SaveChanges();
                    }
                }
                else if (status == 2)
                {
                    datadk.Trangthai = 2;
                    datadk.Ngayphanhoi = DateTime.Now;
                    bool kq = Sendmail(datadk);
                    
                    if (kq == true)
                    {
                        bool sendms2 = new SMS().SendSms(datadk.UngVien.Dienthoai1, "xin chao " + datadk.UngVien.EmailDN + " chung toi da gui cho ban mot email, vui long kiem tra email tren"+host);
                        db.SaveChanges();
                    }
                }
                else if (status == 0)
                {
                    db.SaveChanges();
                    return Json(2);
                   
                }
                
                return Json(1);
            }
        }
        public ActionResult DeleteUV(List<string> listdelete,int macv)
        {
            var datacv = db.UVDangKies.Where(x => x.MACV == macv && x.Tinhtrang == 1).ToList();
            if (datacv.Count() == 0)
            {
                return Json(0);
            }
            foreach (string item in listdelete)
            {
                if (item != "on")
                {
                   int mauv= int.Parse(item);
                   var datadelete = db.UVDangKies.Where(x => x.MACV == macv && x.MAUV == mauv && x.Tinhtrang == 1).FirstOrDefault();
                   if (datadelete == null)
                   {
                       return Json(0);
                   }
                    db.UVDangKies.Remove(datadelete);
                    db.SaveChanges();
                }
            }
                return Json(1);
        }
        public ActionResult DeleteJob(List<string> listdelete)
        {
            foreach (string item in listdelete)
            {
                if (item != "on")
                {
                    int mauv = int.Parse(item);
                    var datadelete = db.UVDangKies.Where(x => x.MAUV == mauv && x.Tinhtrang == 1).FirstOrDefault();
                    db.UVDangKies.Remove(datadelete);
                    db.SaveChanges();
                }
            }
            return Json(1);
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult SaveMember(int? mauv, string savenote, int? status)
        {
            if (status != 1 && status != 2 && status != null && status != 3 && status != 4)
            {
                status = 1;
            }
            var user=Session["Employer"] as CongTy;
            var datamember = db.UngViens.Where(x => x.MAUV == mauv).FirstOrDefault();
            var checkmember = db.LuuHosoes.Where(x => x.MAUV == mauv&&x.MACT==user.MACT).FirstOrDefault();
            if (datamember == null)
            {
                return RedirectToAction("ListSaveMember", "ManagerJob");
            }
            if (checkmember != null)
            {
                if (savenote != null)
                {
                    checkmember.Ghichu = savenote;
                    db.SaveChanges();
                }
                if (status != null)
                {
                    if (status == 2)
                    {
                        checkmember.Tinhtrang = 1;
                    }
                        checkmember.Ngayphanhoi = DateTime.Now;
                    checkmember.Trangthai = status;
                    bool kq = Sendmail2(checkmember);
                    if(kq==true)
                    db.SaveChanges();
                }
                
                return RedirectToAction("ListSaveMember", "ManagerJob");
            }
            else
            {
                LuuHoso hs = new LuuHoso();
                hs.MAUV = datamember.MAUV;
                hs.MACT = user.MACT;
                hs.Ngayluu = DateTime.Now;
                hs.Trangthai = 1;
                hs.Tinhtrang = 1;
                hs.Ghichu = savenote;
                db.LuuHosoes.Add(hs);
                db.SaveChanges();
                return RedirectToAction("ListSaveMember", "ManagerJob");
            }
            
        }
        public ActionResult ListSaveMember(int? page)
        {
            var user=Session["Employer"] as CongTy;
            int pagesize = 20;
            int pagenumber = (page ?? 1);
            var datamember = db.LuuHosoes.Where(x => x.MACT == user.MACT).ToList();
            if (datamember.OrderByDescending(x => x.Ngayluu).ToPagedList(pagenumber, pagesize).Count() == 0)
            {
                return View(datamember.OrderByDescending(x => x.Ngayluu).ToPagedList(1, pagesize));
            }
            return View(datamember.OrderByDescending(x => x.Ngayluu).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult DeleteSaveMember(List<string> listdelete)
        {
            foreach (string item in listdelete)
            {
                if (item != "on")
                {
                    int mauv = int.Parse(item);
                    var datadelete = db.LuuHosoes.Where(x => x.MAUV == mauv).FirstOrDefault();
                    db.LuuHosoes.Remove(datadelete);
                    db.SaveChanges();
                }
            }
            return Json(1);
        }
        public ActionResult SearchMember()
        {
            return View();
        }
        public string RemoveUnicode2(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            string value = regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            value = rgx.Replace(value, " ");
            value = value.Trim();
            return value.ToLower();
        }
        [HttpGet]
        public ActionResult ShowSearchMember(string keywork, int? experience, int? workid, int? cityid, int? level,string skill,int? page)
        {
            var data = db.UngViens.Where(x => x.Tinhtrang != -1 && x.Tinhtrang == 1&&x.vaitro==1).OrderByDescending(x => x.Ngaycapnhat).ToList();
            if (keywork != null && keywork != "")
            {
                keywork = RemoveUnicode2(keywork);
                bool kq = "quan ly nha hang".Contains(keywork);
                string[] keylist = keywork.Split(new char[] { ' ', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in data)
                {
                    int count = 0;
                    foreach (string i in keylist)
                    {
                        if (item.Chucdanh!=null&&RemoveUnicode2(item.Chucdanh).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Loaitaikhoan = count;
                        }
                    }
                }
                int keylenght = 1;
                if (keylist.Count() >= 2)
                {
                    keylenght = 2;
                }
                data = (from re in data
                          where re.Loaitaikhoan >= keylenght
                        orderby re.Loaitaikhoan descending, re.Ngaycapnhat descending
                          select re).ToList();
            }
            if( skill !=null &&skill!="")
            {
                skill = RemoveUnicode2(skill);
                     data = (from a in data
                                  join b in db.KyNangUVs on a.MAUV equals b.MAUV
                             where b.TenKN != null && RemoveUnicode2(b.TenKN).Contains(skill)
                                  select a).ToList();

            }
            if (experience != null)
            {
                data = data.Where(x => x.Namkinhnghiem == experience).ToList();
            }
            if (workid != null)
            {
                data = (from a in data
                        join b in db.CVmongmuons on a.MAUV equals b.MaUV
                        where b.Nghanhnghe1==workid||b.Nghanhnghe2==workid||b.Nghanhnghe3==workid
                        select a).ToList();
            }
            if (cityid != null)
            {
                data = (from a in data
                        join b in db.CVmongmuons on a.MAUV equals b.MaUV
                        where b.Thanhpho1 == cityid || b.Thanhpho2 == cityid || b.Thanhpho3 == cityid
                        select a).ToList();
            }
            if (level != null)
            {
                data = (from a in data
                        join b in db.CVmongmuons on a.MAUV equals b.MaUV
                        where b.MaCB == level
                        select a).ToList();
            }
            int pagesize = 30;
            int pagenumber = (page ?? 1);
            ViewData["CountMember"] = data.Count();
            return View(data.ToPagedList(pagenumber,pagesize));
        }
        public ActionResult DetailMemberSearch(int? mauv)
        {
            if (mauv == null)
            {
                return Redirect("/filenotfound.htm");
            }
            var datauv = db.UngViens.Where(x => x.MAUV == mauv && x.Tinhtrang == 1).FirstOrDefault();
            if (datauv == null)
            {
                return Redirect("/filenotfound.htm");
            }
            if (datauv.Luotxem == null)
            {
                datauv.Luotxem = 1;
                db.SaveChanges();
            }
            else
            {
                datauv.Luotxem += 1;
                db.SaveChanges();
            }
            return View(datauv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMessages(int? mahs,string note)
        {
            Uri myUri = new Uri(Request.Url.ToString());
            string host = myUri.Host; 
            if (mahs == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            var datahs = db.LuuHosoes.Where(x => x.Id == mahs).FirstOrDefault();
            if (datahs == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            datahs.Ghichu = note;
            datahs.Trangthai = 2;
            bool kq = Sendmail2(datahs);
            
            if (kq == true)
            {
                bool sendms2 = new SMS().SendSms(datahs.UngVien.Dienthoai1, "xin chao " + datahs.UngVien.EmailDN + " chung toi da gui cho ban mot email, vui long kiem tra email tren"+host);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult ChangeStatusSave(int mahs, int status)
        {
            Uri myUri = new Uri(Request.Url.ToString());
            string host = myUri.Host; 
            var datadk = db.LuuHosoes.Where(x => x.Id == mahs && x.Tinhtrang == 1).FirstOrDefault();
            if (datadk == null)
            {
                return Json(0);
            }
            else
            {
                if (status == 5)
                {
                    datadk.Trangthai = 1;
                    db.SaveChanges();
                    return Json(3);
                }
                else if (status == 4)
                {
                    datadk.Trangthai = 4;
                    bool kq = Sendmail2(datadk);

                    if (kq == true)
                    {
                        bool sendms2 = new SMS().SendSms(datadk.UngVien.Dienthoai1, "Chuc mung ban da duoc moi phong van, vui long kiem tra email tren "+host);
                        db.SaveChanges();
                    }
                }
                else if (status == 3)
                {
                    datadk.Trangthai = 3;
                    bool kq = Sendmail2(datadk);
                    if (kq == true)
                    {
                        db.SaveChanges();
                    }
                }
                else if (status == 2)
                {
                    datadk.Trangthai = 2;
                    datadk.Ngayphanhoi = DateTime.Now;
                    bool kq = Sendmail2(datadk);
                   
                    if (kq == true)
                    {
                        bool sendms2 = new SMS().SendSms(datadk.UngVien.Dienthoai1, "Chuc mung ban da duoc moi phong van, vui long kiem tra email tren "+host);
                        db.SaveChanges();
                    }
                }
                else if (status == 0)
                {
                    db.SaveChanges();
                    return Json(2);
                }
                
                return Json(1);
            }
        }
    }
}