using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using PagedList.Mvc;
using PagedList;
using Model.Setup;
using Admin.Models;
using System.Globalization;

namespace Admin.Controllers
{
    [SessionExpire]
    public class ManagerMemberAdminController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: ManagerMemberAdmin
        public ActionResult ListMemberAdmin(int ?page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            var datamember = db.UngViens.Where(x => x.Tinhtrang != -1 &&x.vaitro==1).ToList();
            return View(datamember.OrderByDescending(x=>x.MAUV).ToPagedList(pagenumber,pagesize));
        }
        [HttpGet]
        public ActionResult ListMemberAdmin(string search, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            if (search == null)
                search = "";
            search = new RemoveUnicodeModel().UnicodeNameModel(search.Trim());
            var datamember = db.UngViens.ToList();
            var listsearch = new List<UngVien>();
            if (search != "")
            {
                foreach (var item in datamember)
                {
                    string hovaten = new RemoveUnicodeModel().UnicodeNameModel(item.Hovatendem + " " + item.Ten);
                    string emaildn = new RemoveUnicodeModel().UnicodeNameModel(item.EmailDN);
                    string emaillh = new RemoveUnicodeModel().UnicodeNameModel(item.EmailLH);
                    string phone = new RemoveUnicodeModel().UnicodeNameModel(item.Dienthoai1);
                    if (item.MAUV.ToString() == search || hovaten.Contains(search) || emaildn.Contains(search) || emaillh.Contains(search) || phone.Contains(search))
                    {
                        listsearch.Add(item);
                    }
                }
            }
            else
            {
                listsearch = datamember.Where(x => x.Tinhtrang != -1).ToList();
            }
            return View(listsearch.OrderByDescending(x => x.MAUV).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ListMemberTrashAdmin(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            var datamember = db.UngViens.Where(x => x.Tinhtrang == -1).ToList();
            return View(datamember.OrderByDescending(x => x.MAUV).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ChangeStatus(int? mauv, int? val, int? action)
        {
            var datamember=db.UngViens.Where(x=>x.MAUV==mauv).FirstOrDefault();
            if (mauv == null || datamember==null)
            {
                return Json(0);
            }
            else
            {
                if (action == 1) 
                {
                    if (val == 1)
                        datamember.vaitro = 2;
                    else
                        datamember.vaitro = 1;
                    db.SaveChanges();
                }
                if (action == 2)
                {
                    datamember.Tinhtrang = val;
                    db.SaveChanges();
                }
                if (action == 3)
                {
                    if (val == 1)
                        datamember.Tinhtrang = -1;
                    else
                        datamember.Tinhtrang = 1;
                    db.SaveChanges();
                }
                return Json(1);
            }
        }
        public ActionResult DetailMemberAdmin(int? mauv)
        {
            
            if (mauv == null)
            {
                return Redirect("/Amin");
            }
                var datamember=db.UngViens.Where(x=>x.MAUV==mauv).FirstOrDefault();
            if (datamember == null)
             {
                 return Redirect("/Admin");
             }
            else
            {
                return View(datamember);
            }
        }
        public ActionResult ListEmployerAdmin(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            var datamember = db.CongTies.Where(x => x.Tinhtrang != -1).ToList();
            return View(datamember.OrderByDescending(x => x.MACT).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ListEmployerAdminGet(string sSearch, int sEcho, int iDisplayLength, int iDisplayStart, int iColumns, int iSortCol_0, string sSortDir_0, string minday, string maxday)
        {
            string search = sSearch;
            int sortColumn = iSortCol_0;
            string sortDirection = sSortDir_0;
            Model.Setup.DataTable.DataTableData1 dataTableData = new Model.Setup.DataTable.DataTableData1();
            dataTableData.draw = sEcho;
            dataTableData.recordsTotal = db.DangTinViecLams.Where(x => x.Tinhtrang == 1).Count();
            int recordsFiltered = 0;
            dataTableData.data = new DataTable().FilterDataEmployer(ref recordsFiltered, iDisplayStart, iDisplayLength, search, sortColumn, sortDirection, minday, maxday);
            dataTableData.data = dataTableData.data.Where(x => x.tinhtrang != -1).ToList();
            dataTableData.recordsFiltered = dataTableData.data.Count();
            dataTableData.recordsTotal = dataTableData.data.Count();
            // get just one page of data
            dataTableData.data = dataTableData.data.GetRange(iDisplayStart, Math.Min(iDisplayLength, dataTableData.data.Count - iDisplayStart));
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListEmployerTrashGet(string sSearch, int sEcho, int iDisplayLength, int iDisplayStart, int iColumns, int iSortCol_0, string sSortDir_0, string minday, string maxday)
        {
            string search = sSearch;
            int sortColumn = iSortCol_0;
            string sortDirection = sSortDir_0;
            Model.Setup.DataTable.DataTableData1 dataTableData = new Model.Setup.DataTable.DataTableData1();
            dataTableData.draw = sEcho;
            dataTableData.recordsTotal = db.DangTinViecLams.Where(x => x.Tinhtrang == 1).Count();
            int recordsFiltered = 0;
            dataTableData.data = new DataTable().FilterDataEmployer(ref recordsFiltered, iDisplayStart, iDisplayLength, search, sortColumn, sortDirection, minday, maxday);
            dataTableData.data = dataTableData.data.Where(x => x.tinhtrang == -1).ToList();
            dataTableData.recordsFiltered = dataTableData.data.Count();
            dataTableData.recordsTotal = dataTableData.data.Count();
            // get just one page of data
            dataTableData.data = dataTableData.data.GetRange(iDisplayStart, Math.Min(iDisplayLength, dataTableData.data.Count - iDisplayStart));
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListEmployerTrashAdmin(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            var datamember = db.CongTies.Where(x => x.Tinhtrang == -1).ToList();
            return View(datamember.OrderByDescending(x => x.MACT).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ChangeStatusEmployer(int? mact, int? val, int? action)
        {
            var datamember = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            var listjobemployer = db.DangTinViecLams.Where(x => x.MACT == mact).ToList();
            if (mact == null || datamember == null)
            {
                return Json(0);
            }
            else
            {
                if (action == 1)
                {
                    if (val == 1)
                    {
                         datamember.Tinhtrang = 1;
                         listjobemployer.ForEach(x => { x.Tinhtrang = 1; x.Trangthai = 1; });
                    }
                    else
                    {
                        datamember.Tinhtrang = 0;
                        listjobemployer.ForEach(x => { x.Tinhtrang = 0; x.Trangthai = 0; });
                    }
                        
                    db.SaveChanges();
                }
                if (action == 2)
                {
                    if (val == 0)
                    {
                        datamember.Tinhtrang = 1;
                        listjobemployer.ForEach(x => { x.Tinhtrang = 1; x.Trangthai = 1; });
                    }
                    else
                    {
                        datamember.Tinhtrang = -1;
                        listjobemployer.ForEach(x => { x.Tinhtrang = 0; x.Trangthai = 0; });
                    }
                    db.SaveChanges();
                }
                if(action==3)
                {
                    if(val==1)
                    datamember.Toptuyendung = true;
                    else
                        datamember.Toptuyendung = false;
                    db.SaveChanges();
                    
                }
                return Json(1);
            }
        }
        public ActionResult ListJobForEmployer(int? page,int? id)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 200;
            ViewData["Name"] = db.CongTies.Where(x => x.MACT == id).Select(x => x.Tencongty).FirstOrDefault();
            ViewBag.MACT = id;
            var result = db.DangTinViecLams.Where(x => x.Tinhtrang != -1 &&x.MACT==id).ToList();
            return View(result.OrderByDescending(x => x.MACV).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult CreateEmployer(int mact)
        {
            return View();
        }
        public ActionResult EditEmployer(int? mact)
        {
            var dataemployer = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            if (dataemployer == null)
            {
                return Redirect("/Admin");
            }
            else
            {
                return View(dataemployer);
            }
            
        }
        public ActionResult InforAccount(int mact)
        {
            var dataem = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            return View(dataem);
        }
        [HttpPost]
        public ActionResult ChangePassword(string password_first,int mact)
        {
            if (mact == null)
            {
                return Json(new { status = "ERROR_PASSWORD" });
            }
            var checkpassword = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
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
        public ActionResult InforCompany(int mact)
        {
            ViewBag.ListNghe = db.NghanhNghes;
            ViewBag.QuyMo = db.QuyMoCTs;
            return View(db.CongTies.Where(x => x.MACT == mact).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult InforCompany(Employer model,int mact)
        {
            var dataem = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            if (dataem != null)
            {
                dataem.Tencongty = model.Tencongty;
                dataem.Diachi = model.Diachi;
                dataem.Quymocongty = model.Quymocongty;
                dataem.Nguoilienhe = model.Nguoilienhe;
                dataem.Manghanh1 = null;
                dataem.Manghanh2 = null;
                dataem.Manghanh3 = null;
                dataem.Website = model.Website;
                dataem.Thongtin = model.Thongtin;
                dataem.Dienthoaiban = model.Dienthoaiban;
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
        public ActionResult LogoCompany(int mact)
        {
            ViewBag.Logo = db.CongTies.Where(x => x.MACT == mact).Select(x => x.Logo).FirstOrDefault();
            return View(db.HinhAnhCTs.Where(x => x.MACT == mact));
        }
        public void deleteimg(string imgname)
        {
            string deletepath = Server.MapPath("/Employer_upload/" + imgname);
            System.IO.File.Delete(deletepath);
        }
        public void Uploadlogo(HttpPostedFileBase logo, int id,int mact)
        {
            var user = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            string filename = "img" + id + user.Emaildangnhap ;
            filename = LoginMember.MD5Hash(filename) + logo.FileName;
            string path = System.IO.Path.Combine(Server.MapPath("~/Employer_upload/"), filename);
            logo.SaveAs(path);
        }
        [HttpPost]
        public ActionResult UploadLogo(HttpPostedFileBase logo, HttpPostedFileBase logo1, int logoid1, HttpPostedFileBase logo2, int logoid2, HttpPostedFileBase logo3, int logoid3, int logodelete1, int logodelete2, int logodelete3, string video,int mact)
        {
            var user = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            var logoct = db.CongTies.Where(x => x.MACT == user.MACT).FirstOrDefault();
            if (logoct != null)
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
                logoct.Logo = LoginMember.MD5Hash(logoct.Logo) + logo.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Employer_upload/"), logoct.Logo);
                logo.SaveAs(path);
                db.SaveChanges();

            }
            if (imgcompany.Count() > 0)
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
                    logoimg1.AnhCT = "img1" + user.Emaildangnhap;
                    logoimg1.AnhCT = LoginMember.MD5Hash(logoimg1.AnhCT) + logo1.FileName;
                    db.SaveChanges();
                    Uploadlogo(logo1, 1,mact);
                    deleteimg(logoimg1.AnhCT);
                }
                if (logoid2 > 0 && logo2 != null)
                {
                    var logoimg1 = db.HinhAnhCTs.Where(x => x.Id == logoid1).FirstOrDefault();
                    deleteimg(logoimg1.AnhCT);
                    logoimg1.AnhCT = "img2" + user.Emaildangnhap ;
                    logoimg1.AnhCT = LoginMember.MD5Hash(logoimg1.AnhCT) + logo1.FileName;
                    db.SaveChanges();
                    Uploadlogo(logo1, 3, mact);

                }
                if (logoid3 > 0 && logo3 != null)
                {
                    var logoimg1 = db.HinhAnhCTs.Where(x => x.Id == logoid1).FirstOrDefault();
                    deleteimg(logoimg1.AnhCT);
                    logoimg1.AnhCT = "img3" + user.Emaildangnhap;
                    logoimg1.AnhCT = LoginMember.MD5Hash(logoimg1.AnhCT) + logo1.FileName;
                    db.SaveChanges();
                    Uploadlogo(logo1, 3, mact);
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
                    Uploadlogo(logo1, 1, mact);
                    img.AnhCT = "img1" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo1.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                }
                if (logo2 != null)
                {
                    var delete1 = db.HinhAnhCTs.Where(x => x.MACT == user.MACT).OrderBy(x => x.Id).Skip(1).FirstOrDefault();
                    if (delete1 != null)
                    {
                        deleteimg(delete1.AnhCT);
                        db.HinhAnhCTs.Remove(delete1);
                        db.SaveChanges();
                    }
                    Uploadlogo(logo2, 2, mact);
                    img.AnhCT = "img2" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo2.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                }
                if (logo3 != null)
                {
                    var delete1 = db.HinhAnhCTs.Where(x => x.MACT == user.MACT).OrderBy(x => x.Id).Skip(2).FirstOrDefault();
                    if (delete1 != null)
                    {
                        deleteimg(delete1.AnhCT);
                        db.HinhAnhCTs.Remove(delete1);
                        db.SaveChanges();
                    }
                    img.AnhCT = "img3" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo3.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                    Uploadlogo(logo3, 3, mact);
                }
            }
            else
            {
                if (logo1 != null)
                {
                    Uploadlogo(logo1, 1, mact);
                    img.AnhCT = "img1" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo1.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                }
                if (logo2 != null)
                {
                    Uploadlogo(logo2, 2, mact);
                    img.AnhCT = "img2" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo2.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                }
                if (logo3 != null)
                {
                    img.AnhCT = "img3" + user.Emaildangnhap;
                    img.AnhCT = LoginMember.MD5Hash(img.AnhCT) + logo3.FileName;
                    img.MACT = user.MACT;
                    db.HinhAnhCTs.Add(img);
                    db.SaveChanges();
                    Uploadlogo(logo3, 3, mact);
                }
            }
            return Json(new { status = "SUCCESS" });
        }
        public ActionResult ACEmployer(int mact)
        {
            var dataem = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            Session["Employer"] = dataem;
            return Redirect("/ManagerJob/MemberForJob");
        }
        public ActionResult AddNewJob(int mact)
        {
            ViewBag.ListNghanh = db.NghanhNghes.Where(x => x.Tinhtrang == 1);
            ViewBag.City = db.cities;
            ViewBag.CB = db.CBmongmuons;
            var datact = db.CongTies.Where(x => x.MACT == mact).FirstOrDefault();
            if (datact == null)
            {
                return Redirect("/notfound/PageNotFound");
            }
            return View(datact);
        }
        [HttpPost]
        public ActionResult AddNewJob(DangTinViecLam model, List<int> categories, List<int> city, string skill, string Dateex,int mact)
        {
            var user = Session["Admin"] as UngVien;
            var dataamdin = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
            var datacompany = db.CongTies.Where(x => x.MACT == model.MACT).FirstOrDefault();
            if (skill != null || skill != "")
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
            return Json(mact);
        }
    }
}