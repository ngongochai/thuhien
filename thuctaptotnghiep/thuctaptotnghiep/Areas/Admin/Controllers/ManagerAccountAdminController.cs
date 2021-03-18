using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Setup;
using PagedList.Mvc;
using PagedList;
using System.Globalization;

namespace Admin.Controllers
{
    [SessionExpire]
    public class ManagerAccountAdminController : Controller
    {
        // GET: ManagerAccountAdmin
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult ListAccountAdmin(int? page)
        {
            int pagesize = 100;
            int pagenumber = (page ?? 1);
            return View(db.UngViens.Where(x=>x.vaitro==2&&x.Tinhtrang!=-1).OrderByDescending(x=>x.MAUV).ToPagedList(pagenumber,pagesize));
        }
        public ActionResult ProfileAdmin(int? mauv)
        {
            if (mauv == null)
            {
                return Redirect("/Admin");
            }
            var data = db.UngViens.Where(x => x.MAUV == mauv&&x.vaitro==2).FirstOrDefault();
            if(data==null)
            {
                return Redirect("/Admin");
            }
            return View(data);
        }
        public ActionResult InforAccountAdmin(int mauv)
        {
            var dataem = db.UngViens.Where(x => x.MAUV == mauv && x.vaitro == 2).FirstOrDefault();
            return View(dataem);
        }
        [HttpPost]
        public ActionResult ChangePasswordAdmin(string password_first,int mauv)
        {
            
            var checkpassword = db.UngViens.Where(x => x.MAUV == mauv&&x.vaitro==2).FirstOrDefault();
            if (checkpassword != null)
            {
                checkpassword.Matkhau = LoginMember.MD5Hash(password_first);
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else
            {
                return Json(new { status = "ERROR_PASSWORD" });
            }
        }
        public ActionResult InforAdmin(int mauv)
        {
            
            ViewBag.ListNghe = db.NghanhNghes;
            ViewBag.QuyMo = db.QuyMoCTs;
            var data = db.UngViens.Where(x => x.MAUV == mauv).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult InforAdmin(UngVien model)
        {
            
            var dataem = db.UngViens.Where(x => x.MAUV == model.MAUV&&x.vaitro==2).FirstOrDefault();
            if (dataem != null)
            {
                dataem.Ngaysinh = model.Ngaysinh;
                dataem.Ten = model.Ten;
                dataem.Hovatendem = model.Hovatendem;
                dataem.Chucdanh = model.Chucdanh;
                dataem.Dienthoai1 = model.Dienthoai1;
                dataem.Diachihientai = model.Diachihientai;
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else
            {
                return Json(new { status = "ERROR" });
            }
        }
        public ActionResult LogoAdmin(int mauv)
        {
       
            var logo = db.UngViens.Where(x => x.MAUV == mauv).FirstOrDefault();
            return View(logo);
        }
        public void deleteimg(string imgname)
        {
            string deletepath = Server.MapPath("/Profile_upload/" + imgname);
            System.IO.File.Delete(deletepath);
        }
        [HttpPost]
        public ActionResult UploadLogo(HttpPostedFileBase logo,int mauv)
        {
           
            var logoct = db.UngViens.Where(x => x.MAUV == mauv&&x.vaitro==2).FirstOrDefault();
            if (logoct != null)
            {
                if (logo != null)
                {
                    if (logoct.Hinhanh != null)
                    {
                        deleteimg(logoct.Hinhanh);
                    }
                    logoct.Hinhanh = "logoadmin" + logoct.EmailDN + logo.FileName;
                    string path = System.IO.Path.Combine(Server.MapPath("~/Profile_upload/"), logoct.Hinhanh);
                    logo.SaveAs(path);
                    db.SaveChanges();
                }
            }
            return Json(new { status = "SUCCESS" });
        }
        public ActionResult CreateJob()
        {
            ViewBag.ListNghanh = db.NghanhNghes.Where(x=>x.Tinhtrang==1);
            ViewBag.City = db.cities;
            ViewBag.CB = db.CBmongmuons;
            return View();
        }
        [HttpPost]
        public ActionResult CreateJob(DangTinViecLam model, List<int> categories, List<int> city, string skill, string Dateex)
        {
            var user = Session["Admin"] as UngVien;
            var dataamdin = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
            var checkemployer = db.CongTies.Where(x => x.Emaildangnhap == model.AdminEmail).FirstOrDefault();
            if (checkemployer == null)
            {
                CongTy ct = new CongTy();
                ct.Emaildangnhap = model.AdminEmail;
                ct.Diachi = model.AdminDiachi;
                ct.Dienthoaiban = model.AdminDienthoai;
                ct.Nguoilienhe = model.NguoiLH;
                ct.Ngaytao = DateTime.Now;
                ct.Tinhtrang = 1;
                ct.Tencongty = model.AdminTencongty;
                ct.Matkhau = model.AdminEmail;
                db.CongTies.Add(ct);
                db.SaveChanges();
            }
            var datacompany = db.CongTies.Where(x => x.Emaildangnhap == model.AdminEmail).FirstOrDefault();
            if (skill != null || skill!="")
            {
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
                    else if (model.Tags3 == null)
                    {
                        model.Tags3 = item;
                    }
                }
            }
            foreach (var item in categories)
            {
                if (model.Manghanh1 == null)
                {
                    model.Manghanh1 = item;
                    //model.NghanhNghe1 = db.NghanhNghes.Where(x => x.Id == model.Manghanh1).Select(x => x.Tennghanh).FirstOrDefault();
                }
                else if (model.Manghanh2 == null)
                {
                    model.Manghanh2 = item;
                    //model.Thanhpho2 = db.NghanhNghes.Where(x => x.Id == model.Manghanh2).Select(x => x.Tennghanh).FirstOrDefault();
                }
                else
                {
                    model.Manghanh3 = item;
                    //model.Thanhpho3 = db.NghanhNghes.Where(x => x.Id == model.Manghanh3).Select(x => x.Tennghanh).FirstOrDefault();
                }
            }
            foreach (var item in city)
            {
                if (model.MaTP1 == null)
                {
                    model.MaTP1 = item;
                    model.Thanhpho1 = db.cities.Where(x => x.Id == model.MaTP1).Select(x => x.City1).FirstOrDefault();
                }
                else if (model.MaTP2 == null)
                {
                    model.MaTP2 = item;
                    model.Thanhpho2 = db.cities.Where(x => x.Id == model.MaTP2).Select(x => x.City1).FirstOrDefault();
                }
                else
                {
                    model.MaTP3 = item;
                    model.Thanhpho3 = db.cities.Where(x => x.Id == model.MaTP3).Select(x => x.City1).FirstOrDefault();
                }
            }
            DateTime endday;
            bool tryb = DateTime.TryParseExact(Dateex, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endday);
            model.Ngayhethangjob = endday;
            model.Tinhtrang = 1;
            model.AdminDienthoai = "";
            model.AdminDiachi = "";
            model.Type = 2;
            model.AdminEmail = user.EmailDN;
            model.MACT = datacompany.MACT;
            model.Trangthai = 1;
            model.MotaCV = model.MotaCV;
            model.YeucauCV = model.YeucauCV;
            model.Ngaydang = DateTime.Now;
            db.DangTinViecLams.Add(model);
            db.SaveChanges();
            return Json(1);
        }
        public ActionResult ListJobOfAdmin(int ?page)
        {
            var user=Session["Admin"] as UngVien;
            int pagesize = 200;
            int pagenumber = (page ?? 1);
            var data = db.DangTinViecLams.Where(x => x.Type == 2&&x.Tinhtrang==1);
            return View(data.OrderByDescending(x => x.MACV).ToPagedList(pagenumber, pagesize));

        }
        public ActionResult AddMemberToAdmin(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            var datamember = db.UngViens.Where(x => x.Tinhtrang != -1 && x.vaitro == 1).ToList();
            return View(datamember.OrderByDescending(x => x.MAUV).ToPagedList(pagenumber, pagesize));
        }
    }
}