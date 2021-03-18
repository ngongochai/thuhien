using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using PagedList.Mvc;
using PagedList;
using Model.Setup;
using System.Globalization;

namespace Admin.Controllers
{
    [SessionExpire]
    public class ManagerJobAdminController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: ManagerJobAdmin
        public bool Sendmail2(int id)
        {
            var ativejob = db.DangTinViecLams.Where(x => x.MACV == id).FirstOrDefault();
            var configmail = db.ConfigMails.Where(x => x.smtpType == 3).FirstOrDefault();
            if (configmail == null)
            {
                configmail = new ConfigMail();
            }
            string smtpUserName = configmail.smtUserName;
            string smtpPassword = configmail.smtpPassword;
            string smtpHost = configmail.smtpHost;
            int smtpPort = int.Parse(configmail.smtpPort.ToString());

            string emailTo = ativejob.CongTy.Emaildangnhap;
            string subject = configmail.smtpSubject;
            string mailcontent = new EmailService().RenderPartialViewToString(this, "_SendActiveJob", ativejob);
            string body = mailcontent;
            if (configmail.EnableSSL == null)
                configmail.EnableSSL = false;
            bool ssl = bool.Parse(configmail.EnableSSL.ToString());
            EmailService service = new EmailService();

            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, subject, body, ssl);
            return kq;

        }
        public ActionResult ListAllJob()
        {
            return View();
        }

        public ActionResult ListAllJobGet(string sSearch, int sEcho, int iDisplayLength, int iDisplayStart, int iColumns, int iSortCol_0, string sSortDir_0, string minday, string maxday, int fiterjob)
        {
            string search = sSearch;
            int sortColumn = iSortCol_0;
            string sortDirection = sSortDir_0;
            Model.Setup.DataTable.DataTableData dataTableData = new Model.Setup.DataTable.DataTableData();
            dataTableData.draw = sEcho;
            dataTableData.recordsTotal = db.DangTinViecLams.Where(x => x.Tinhtrang != -1 && x.Type != 2).Count();
            int recordsFiltered = 0;
            dataTableData.data = new DataTable().FilterDataJob(ref recordsFiltered, iDisplayStart, iDisplayLength, search, sortColumn, sortDirection, minday, maxday,fiterjob);
            dataTableData.data = dataTableData.data.Where(x => x.Tinhtrang != -1 &&x.Tinhtrang!=-2&& x.type != 2).ToList();
            dataTableData.recordsFiltered = dataTableData.data.Count();
            // get just one page of data
            dataTableData.data = dataTableData.data.GetRange(iDisplayStart, Math.Min(iDisplayLength, dataTableData.data.Count - iDisplayStart));


            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListJobTrash(string sSearch, int sEcho, int iDisplayLength, int iDisplayStart, int iColumns, int iSortCol_0, string sSortDir_0, string minday, string maxday, int fiterjob)
        {
            string search = sSearch;
            int sortColumn = iSortCol_0;
            string sortDirection = sSortDir_0;
            Model.Setup.DataTable.DataTableData dataTableData = new Model.Setup.DataTable.DataTableData();
            dataTableData.draw = sEcho;
            dataTableData.recordsTotal = db.DangTinViecLams.Where(x => x.Tinhtrang == -1||x.Tinhtrang==-2).Count();
            int recordsFiltered = 0;
            dataTableData.data = new DataTable().FilterDataJob(ref recordsFiltered, iDisplayStart, iDisplayLength, search, sortColumn, sortDirection, minday, maxday, fiterjob);
            dataTableData.data = dataTableData.data.Where(x => x.Tinhtrang == -1 || x.Tinhtrang == -2).ToList();
            dataTableData.recordsFiltered = dataTableData.data.Count();
            dataTableData.data = dataTableData.data.GetRange(iDisplayStart, Math.Min(iDisplayLength, dataTableData.data.Count - iDisplayStart));
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Listjobadminget(string sSearch, int sEcho, int iDisplayLength, int iDisplayStart, int iColumns, int iSortCol_0, string sSortDir_0, string minday, string maxday, int fiterjob)
        {
            string search = sSearch;
            int sortColumn = iSortCol_0;
            string sortDirection = sSortDir_0;
            Model.Setup.DataTable.DataTableData dataTableData = new Model.Setup.DataTable.DataTableData();
            dataTableData.draw = sEcho;
            dataTableData.recordsTotal = db.DangTinViecLams.Where(x => x.Tinhtrang != -1 && x.Tinhtrang != -2 && x.Type == 2).Count();
            int recordsFiltered = 0;
            dataTableData.data = new DataTable().FilterDataJob(ref recordsFiltered, iDisplayStart, iDisplayLength, search, sortColumn, sortDirection, minday, maxday,fiterjob);
            dataTableData.data = dataTableData.data.Where(x => x.Tinhtrang != -1&&x.Tinhtrang!=-2 && x.type == 2).ToList();
            dataTableData.recordsFiltered = dataTableData.data.Count();
            dataTableData.data = dataTableData.data.GetRange(iDisplayStart, Math.Min(iDisplayLength, dataTableData.data.Count - iDisplayStart));
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Listjobemployerget(string sSearch, int sEcho, int iDisplayLength, int iDisplayStart, int iColumns, int iSortCol_0, string sSortDir_0, string minday, string maxday, int mact, int fiterjob)
        {
            string search = sSearch;
            int sortColumn = iSortCol_0;
            string sortDirection = sSortDir_0;
            Model.Setup.DataTable.DataTableData dataTableData = new Model.Setup.DataTable.DataTableData();
            dataTableData.draw = sEcho;
            dataTableData.recordsTotal = db.DangTinViecLams.Count();
            int recordsFiltered = 0;
            dataTableData.data = new DataTable().FilterDataJob(ref recordsFiltered, iDisplayStart, iDisplayLength, search, sortColumn, sortDirection, minday, maxday,fiterjob);
            dataTableData.data = dataTableData.data.Where(x => x.mact == mact).ToList();
            dataTableData.recordsFiltered = dataTableData.data.Count();
            dataTableData.data = dataTableData.data.GetRange(iDisplayStart, Math.Min(iDisplayLength, dataTableData.data.Count - iDisplayStart));
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListJobNotActive()
        {
            var result = db.DangTinViecLams.Where(x => x.Tinhtrang == 0).ToList();
            return View(result);
        }
        public ActionResult ListJobTrashAdmin(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 1000;
            var result = db.DangTinViecLams.Where(x => x.Tinhtrang == -1||x.Tinhtrang==-2);//-1 việc làm đã xóa, -2 việc làm đã hết hạn
            return View(result.OrderByDescending(x => x.MACV).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ChangeStatus(int macv, int value, int action)
        {
            var cheackcv = db.DangTinViecLams.Where(x => x.MACV == macv).FirstOrDefault();
            if (cheackcv == null)
            {
                return Json(new { status = 0 });
            }
            else
            {
                if (action == 1)
                {
                    cheackcv.Vieclamtotnhat = value;
                    db.SaveChanges();
                }
                if (action == 2)
                {
                    cheackcv.hot = value;
                    db.SaveChanges();
                }
                if (action == 3)
                {
                    cheackcv.Vieclamgoiy = value;
                    db.SaveChanges();
                } if (action == 4)
                {
                    cheackcv.Tinhtrang = value;
                    cheackcv.Trangthai = value;
                    if (cheackcv.Tinhtrang == 1)
                    {
                        var kq = Sendmail2(macv);
                        if (kq == true)
                        {
                            db.SaveChanges();
                        }
                        else
                        {
                            return Json(new { status = 0 });
                        }
                    }
                    else
                    {
                        db.SaveChanges();
                    }
                }
                if (action == 5)
                {
                    cheackcv.Trangthai = value;
                    db.SaveChanges();
                }
                return Json(new { status = 1 });
            }
        }
        public ActionResult EditJobAdmin(int? id)
        {
            var data = db.DangTinViecLams.Where(x => x.MACV == id).FirstOrDefault();
            if (data == null)
            {
                return Redirect("/Admin");
            }
            ViewBag.ListNghanh = db.NghanhNghes;
            List<string> language = new List<string>(new string[] { "Bất Kỳ", "Tiếng Anh", "Tiếng Việt", "Tiếng Trung Quốc", "Tiếng Nhật", "Tiếng Hàn Quốc", "Tiếng Pháp" ,
    "Tiếng Pháp","Tiếng Tây Ban Nha","Tiếng Ý"});
            ViewBag.Lag = language;
            ViewBag.City = db.cities;
            ViewBag.CB = db.CBmongmuons;
            return View(data);
        }
        [HttpPost]
        public ActionResult EditJobAdmin(DangTinViecLam model, int id, List<int> categories, List<int> city, string skill, string Dateex)
        {
            var dataedit = db.DangTinViecLams.Where(x => x.MACV == id).FirstOrDefault();
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
            dataedit.AdminTencongty = model.AdminTencongty;
            dataedit.AdminDienthoai = model.AdminDienthoai;
            dataedit.DiachiHS = model.DiachiHS;
            dataedit.TenCV = model.TenCV;
            dataedit.Luongthoathuan = model.Luongthoathuan;
            dataedit.Minluong = model.Minluong;
            dataedit.Maxluong = model.Maxluong;
            dataedit.MotaCV = model.MotaCV;
            dataedit.YeucauCV = model.YeucauCV;
            dataedit.NgonnguHS = model.NgonnguHS;
            dataedit.MACB = model.MACB;
            db.SaveChanges();
            return Json(1);
        }
        public class complete
        {
            public string value { get; set; }
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
        public ActionResult RemoveJobAdmin(List<int> val)
        {
            foreach (var id in val)
            {
                int macv = id;
                var data = db.DangTinViecLams.Where(x => x.MACV == macv).FirstOrDefault();
                if (data != null)
                {
                    data.Tinhtrang = -1;
                    db.SaveChanges();
                }
            }
            return Json(1);
        }
        public ActionResult DeleteJobAdmin(List<int> val)
        {
            
            foreach (var id in val)
            {
                int macv = id;
                var data = db.DangTinViecLams.Where(x => x.MACV == macv).FirstOrDefault();
                if (data != null)
                {
                    db.DangTinViecLams.Remove(data);
                    db.SaveChanges();
                }
                
            }
            return Json(1);
        }
        public ActionResult RestoreJobAdmin(List<string> val)
        {
            foreach (string id in val)
            {
                int macv = int.Parse(id);
                var data = db.DangTinViecLams.Where(x => x.MACV == macv).FirstOrDefault();
                if (data != null)
                {
                    data.Tinhtrang = 1;
                    db.SaveChanges();
                }
            }
            return Json(1);
        }

    }
}