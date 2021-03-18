using Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using thuctaptotnghiep.Models;
using Model.Framework;
using Facebook;
using System.Configuration;
using Owin;
using System.IO;
using System.Globalization;
using PagedList.Mvc;
using PagedList;

namespace thuctaptotnghiep.Controllers
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["Member"] == null)
            {
                filterContext.Result = new RedirectResult("/dang-nhap-thanh-vien?ReturnUrl="+HttpContext.Current.Request.RawUrl+"");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class MemberController : Controller
    {
        // GET: Member
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        [SessionExpire]
        public ActionResult UploadResume(HttpPostedFileBase resume)
        {
            var user = Session["Member"] as Member;
            
            if( Request.Files.Count>0)
            { 
                var hosoupload=db.UngViens.Where(x=>x.EmailDN==user.EmailDN).FirstOrDefault();
                var fileupload = Request.Files[0];
                string extension = Path.GetExtension(fileupload.FileName);
                string filename = fileupload.FileName.Replace(extension,"")+"_"+LoginMember.MD5Hash(user.EmailDN) + extension;;
                double filesize = fileupload.ContentLength;
                try
                {
                    if (extension != ".doc" && extension != ".docx" && extension != ".xlsx" && extension != ".xls" && extension != ".pdf" && extension != ".txt")
                    {
                        return Json(0);
                    }
                    if (hosoupload.Hosoupload == null)
                    {
                       
                        string path = Server.MapPath("/Profile_upload/" + filename);
                        fileupload.SaveAs(path);
                        hosoupload.Hosoupload = filename;
                        db.SaveChanges();
                        return Json(1);
                    }
                    else
                    {
                        string path = Server.MapPath("/Profile_upload/" + hosoupload.Hosoupload);
                         System.IO.File.Delete(path);
                        
                         string pathsave = Server.MapPath("/Profile_upload/" + filename);
                         fileupload.SaveAs(pathsave);
                         hosoupload.Hosoupload = filename;
                         db.SaveChanges();
                         return Json(1);
                    }
                }
                catch
                {
                    return Json(-1);
                }
            }
            return Json(-1);
        }
        [SessionExpire]
        public ActionResult Profile()
        {
            var user = Session["Member"] as Member;
            if (Session["Member"] == null)
            {
                return Redirect("~/Member/Login");
            }
            else
            {
                var data = db.UngViens.Where(x => x.EmailDN == user.EmailDN).FirstOrDefault();
                var datamember = from uv in db.UngViens
                                 where uv.MAUV == data.MAUV 
                                 select uv;
                return View(datamember.FirstOrDefault());
            }
         
        }
        [HttpPost]
        public ActionResult Profile(Member model,string action)
        {
            var user = Session["Member"] as Member;
            var dateupdate = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
            if (dateupdate != null)
            {
                dateupdate.Ngaycapnhat = DateTime.Now;
                db.SaveChanges();
            }
            if(action=="save-header-info")
            {
                var data = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
                data.Ten = model.Ten;
                data.Hovatendem = model.Hovatendem;
                data.Namkinhnghiem = model.Namkinhnghiem;
                data.Chucdanh = model.Chucdanh;
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else if (action == "save-profile")
            {
                var data = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
                data.Muctieu = model.Muctieu;
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else if (action == "save-contact-info")
            {
                var data = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
                data.EmailLH = model.EmailLH;
                data.Dienthoai1 = model.Dienthoai1;
                data.Gioitinh = model.Gioitinh;
                data.Honnhan = model.Honnhan;
                data.Ngaysinh = DateTime.ParseExact(model.Ngaysinh, "dd/MM/yyyy",CultureInfo.InvariantCulture);
                data.Diachihientai = model.Diachihientai;
                data.Quocgia = model.Quocgia;
                data.MaQH = model.MaQH;
                data.Quanhuyen = model.Quanhuyen;
                data.Thanhpho = model.Thanhpho;
                data.MaTP1 = model.MaTP1;
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else if(action=="load-district")
            {
                var data = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
                var district = db.Quanhuyens.Where(x => x.Matp == model.MaTP1);
                string option = "";
                foreach (var item in district)
                {
                    option += "<option value='"+item.Id+"'>"+item.Tenquanhuyen+"</option>";
                }
                return Json(new { status = "SUCCESS", option,maqh=data.MaQH });
            }
            else if (action == "add-skill-jobseeker")
            {
                var skilljob = db.KyNangUVs.Where(x => x.MAUV == user.MAUV);
                db.KyNangUVs.RemoveRange(skilljob);
                db.SaveChanges();
                KyNangUV kn = new KyNangUV();
                foreach (int item in model.skills)
                {
                    kn.MAKN = item;
                    kn.MAUV = user.MAUV;
                    kn.TenKN = db.KyNangs.Where(x => x.MAKN == item).Select(x=>x.TenKN).FirstOrDefault();
                    db.KyNangUVs.Add(kn);
                    db.SaveChanges();
                }
                return Json(new { status = "SUCCESS" });
            }
            else if (action == "save-language-info")
            {
                //if (model.language_0 != null)
                //{
                //    var dt1 = db.NgonNguUVs.Where(x => x.MaNN == model.language_0 && x.MAUV==user.MAUV).FirstOrDefault();
                //    if (dt1 == null)
                //    {
                //        NgonNguUV nn = new NgonNguUV();
                //        nn.MAUV = user.MAUV;
                //        nn.MaNN = model.language_0;
                //        nn.Tentrinhdo = LoginMember.trinhdo(model.languagelevel_0);
                //        nn.trinhdo = model.languagelevel_0;
                //        nn.Ten = db.NgonNgus.Where(x => x.Id == model.language_0).Select(x => x.Ten).FirstOrDefault();
                //        db.NgonNguUVs.Add(nn);
                //        db.SaveChanges();
                //        int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                //        var dt12 = db.NgonNguUVs.Where(x => x.MaNN == model.language_0 && x.MAUV == user.MAUV).FirstOrDefault();
                //        dt12.count = dt;
                //        db.SaveChanges();
                //        return Json(new { status = "SUCCESS", resumeLanguageId = dt,idnnuv=dt12.id });
                //    }
                //    else
                //    {
                //        return Json(new { status = "ERROR" });
                //    }

                //}
                //else
                //{
                //    if (model.language_1 != null)
                //    {
                //        var c1 = db.NgonNguUVs.Where(x => x.MaNN == model.language_1 && x.MAUV == user.MAUV).FirstOrDefault();
                //        if (c1 != null)
                //        {
                //            return Json(new { status = "ERROR" });
                //        }
                //        var l1 = db.NgonNguUVs.Where(x => x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //        l1.MaNN = model.language_1;
                //        l1.trinhdo = model.languagelevel_1;
                //        l1.Tentrinhdo = LoginMember.trinhdo(model.languagelevel_1);
                //        l1.Ten = db.NgonNgus.Where(x => x.Id == model.language_1).Select(x => x.Ten).FirstOrDefault();
                //        db.SaveChanges();
                //        int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                //        return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = model.idnnuv });
                //    }
                //    if (model.language_2 != null)
                //    {
                //        var c2 = db.NgonNguUVs.Where(x => x.MaNN == model.language_2 && x.MAUV == user.MAUV).FirstOrDefault();
                //        var c55 = db.NgonNguUVs.Where(x => x.MaNN == model.language_2 && x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //        if (c55 != null)
                //        {
                //            var l1 = db.NgonNguUVs.Where(x => x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //            l1.MaNN = model.language_2;
                //            l1.Tentrinhdo = LoginMember.trinhdo(model.languagelevel_2);
                //            l1.trinhdo = model.languagelevel_2;
                //            l1.Ten = db.NgonNgus.Where(x => x.Id == model.language_2).Select(x => x.Ten).FirstOrDefault();
                //            db.SaveChanges();
                //            int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                //            return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = model.idnnuv });
                //        }
                //        if (c2 != null)
                //        {
                //            return Json(new { status = "ERROR" });
                //        }
                //        else
                //        {
                //            var l1 = db.NgonNguUVs.Where(x => x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //            l1.MaNN = model.language_2;
                //            l1.Tentrinhdo = LoginMember.trinhdo(model.languagelevel_2);
                //            l1.trinhdo = model.languagelevel_2;
                //            l1.Ten = db.NgonNgus.Where(x => x.Id == model.language_2).Select(x => x.Ten).FirstOrDefault();
                //            db.SaveChanges();
                //            int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                //            return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = model.idnnuv });
                //        }

                //    }
                //    if (model.language_3 != null)
                //    {
                //        var c3 = db.NgonNguUVs.Where(x => x.MaNN == model.language_3 && x.MAUV == user.MAUV).FirstOrDefault();
                //        var c55 = db.NgonNguUVs.Where(x => x.MaNN == model.language_3 && x.id==model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //        if (c55 != null)
                //        {
                //            var l1 = db.NgonNguUVs.Where(x => x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //            l1.MaNN = model.language_3;
                //            l1.trinhdo = model.languagelevel_3;
                //            l1.Tentrinhdo = LoginMember.trinhdo(model.languagelevel_3);
                //            l1.Ten = db.NgonNgus.Where(x => x.Id == model.language_3).Select(x => x.Ten).FirstOrDefault();
                //            db.SaveChanges();
                //            int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                //            return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = model.idnnuv });
                //        }
                //        if (c3 != null)
                //        {
                //            return Json(new { status = "ERROR" });
                //        }
                //        else
                //        {
                //            var l1 = db.NgonNguUVs.Where(x => x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                //            l1.MaNN = model.language_3;
                //            l1.trinhdo = model.languagelevel_3;
                //            l1.Tentrinhdo = LoginMember.trinhdo(model.languagelevel_3);
                //            l1.Ten = db.NgonNgus.Where(x => x.Id == model.language_3).Select(x => x.Ten).FirstOrDefault();
                //            db.SaveChanges();
                //            int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                //            return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = model.idnnuv });
                //        }
                //    }
                //}
                languagelv getdata = Getvalue.language1(model.language_0, model.languagelevel_0, model.language_1, model.languagelevel_1, model.language_2, model.languagelevel_2, model.language_3, model.languagelevel_3);
                var datalaguage = db.NgonNguUVs.Where(x => x.MaNN == getdata.language && x.MAUV == user.MAUV).FirstOrDefault(); ;
                if (datalaguage == null)
                {
                    NgonNguUV nn = new NgonNguUV();
                    nn.MAUV = user.MAUV;
                    nn.MaNN = getdata.language;
                    nn.Tentrinhdo = LoginMember.trinhdo(getdata.languagelve);
                    nn.trinhdo = getdata.languagelve;
                    nn.Ten = db.NgonNgus.Where(x => x.Id == getdata.language).Select(x => x.Ten).FirstOrDefault();
                    db.NgonNguUVs.Add(nn);
                    db.SaveChanges();
                    int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                    var dt12 = db.NgonNguUVs.Where(x => x.MaNN == getdata.language && x.MAUV == user.MAUV).FirstOrDefault();
                    dt12.count = dt;
                    db.SaveChanges();
                    return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = dt12.id });
                }
                if (model.idnnuv != null)
                {
                    var update1 = db.NgonNguUVs.Where(x => x.MaNN == getdata.language && x.id == model.idnnuv).FirstOrDefault();
                    var update2 = db.NgonNguUVs.Where(x => x.MaNN == getdata.language && x.MAUV == user.MAUV).FirstOrDefault();
                    if (update1 != null)
                    {
                        update1.trinhdo = getdata.languagelve;
                        update2.Tentrinhdo = LoginMember.trinhdo(getdata.languagelve);
                        db.SaveChanges();
                        int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                        return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv =model.idnnuv });
                    }
                    else if (update2 == null)
                    {
                        update2.MaNN = getdata.language;
                        update2.Ten = db.NgonNgus.Where(x => x.Id == getdata.language).Select(x => x.Ten).FirstOrDefault();
                        update2.Tentrinhdo = LoginMember.trinhdo(getdata.languagelve);
                        update2.trinhdo = getdata.languagelve;
                        db.SaveChanges();
                        int dt = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV).Count();
                        return Json(new { status = "SUCCESS", resumeLanguageId = dt, idnnuv = model.idnnuv });
                    }
                    else
                    {
                        return Json(new { status = "ERROR" });
                    }
                }
                return Json(new { status = "ERROR" });
                
            }
            else if (action == "remove-language-info")
            {
                var rm = db.NgonNguUVs.Where(x => x.id == model.idnnuv && x.MAUV == user.MAUV).FirstOrDefault();
                db.NgonNguUVs.Remove(rm);
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else if (action == "save-employment-history")
            {
                var checkwork = db.ThongTinNgheNghieps.Where(x => x.Id == model.entry_id && x.MaUV == user.MAUV).FirstOrDefault();
                if (checkwork == null)
                {
                    ThongTinNgheNghiep tw =new ThongTinNgheNghiep();
                    tw.MaUV = user.MAUV;
                    tw.Mucdoquantam = model.description;
                    tw.Capbachientai = model.position;
                    Random rm = new Random();
                    tw.resumid = new checkresumid().check1(user.MAUV);
                    tw.Congtylamviecganday = model.companyName;
                    if (model.fromDate.Year < 1753)
                    {
                        tw.Fromday = null;
                    }
                    else
                    {
                        tw.Fromday = model.fromDate;
                    }
                    tw.Hiennay = model.currentExp;
                    if (model.currentExp>0)
                    {
                        tw.Today = null;
                        TimeSpan expn = DateTime.Now.Date - model.fromDate.Date;
                        tw.Namkinhnghiem = expn.Days;
                    }
                    else
                    {
                        if (model.toDate.Year < 1753)
                        {
                            tw.Today = null;
                        }
                        else
                        {
                            tw.Today = model.toDate;
                            TimeSpan expn = model.toDate.Date - model.fromDate.Date;
                            tw.Namkinhnghiem = expn.Days;
                        }
                    }
                    db.ThongTinNgheNghieps.Add(tw);
                    db.SaveChanges();
                    var returnexpwork = db.ThongTinNgheNghieps.Where(x => x.MaUV == user.MAUV);
                    List<expid>orders = new List<expid>();
                    
                    int count=10;
                    foreach (var item in returnexpwork)
                    {
                        expid exp = new expid();
                        exp.entryid = item.Id;
                        exp.experienceorder = count;
                        count = count + 10;
                        orders.Add(exp);
                    }
                    var datajson = db.ThongTinNgheNghieps.Where(x => x.resumid == tw.resumid && x.MaUV == user.MAUV).FirstOrDefault();
                    expwork data =new expwork();
                    data.companyname = datajson.Congtylamviecganday;
                    data.description = datajson.Mucdoquantam;
                    if (datajson.Hiennay == null)
                    {
                        data.iscurrent = 1;
                    }
                    else
                    {
                        data.enddate = datajson.Today;
                    }
                    data.jobtitle = datajson.Capbachientai;
                    data.entry_id = datajson.Id;
                    data.workingCompanyName = datajson.Congtylamviecganday;
                    return Json(new { status = "SUCCESS", lastdateupdated = DateTime.Now, mostrecentemployer = model.companyName, mostrecentposition=model.position,orders,data });

                }
                else
                {
                    var update = db.ThongTinNgheNghieps.Where(x => x.Id == model.entry_id).FirstOrDefault();
                    if (model.fromDate.Year < 1753)
                    {
                        update.Fromday = null;
                    }
                    else
                    {
                        update.Fromday = model.fromDate;
                    }
                    update.Hiennay = model.currentExp;
                    if (model.currentExp > 0)
                    {
                        update.Today = null;
                        TimeSpan expn = DateTime.Now.Date - model.fromDate.Date;
                        update.Namkinhnghiem = expn.Days;
                    }
                    else
                    {
                        if (model.toDate.Year < 1753)
                        {
                            update.Today = null;
                        }
                        else
                        {
                            update.Today = model.toDate;
                            TimeSpan expn = model.toDate.Date - model.fromDate.Date;
                            update.Namkinhnghiem = expn.Days;
                        }
                    }
                    update.Mucdoquantam = model.description;
                    update.Capbachientai = model.position;
                    update.Congtylamviecganday = model.companyName;
                    db.SaveChanges();
                    var returnexpwork = db.ThongTinNgheNghieps.Where(x => x.MaUV == user.MAUV);
                    List<expid> orders = new List<expid>();

                    int count = 10;
                    foreach (var item in returnexpwork)
                    {
                        expid exp = new expid();
                        exp.entryid = item.Id;
                        exp.experienceorder = count;
                        count = count + 10;
                        orders.Add(exp);
                    }
                    var datajson = db.ThongTinNgheNghieps.Where(x => x.Id == model.entry_id && x.MaUV == user.MAUV).FirstOrDefault();
                    expwork data = new expwork();
                    data.companyname = datajson.Congtylamviecganday;
                    data.description = datajson.Mucdoquantam;
                    if (datajson.Hiennay == null)
                    {
                        data.iscurrent = 1;
                    }
                    else
                    {
                        data.enddate = datajson.Today;
                    }
                    data.jobtitle = datajson.Capbachientai;
                    data.entry_id = datajson.Id;
                    data.workingCompanyName = datajson.Congtylamviecganday;
                    return Json(new { status = "SUCCESS", lastdateupdated = DateTime.Now, mostrecentemployer = model.companyName, mostrecentposition = model.position, orders, data });
                    
                }
            }
            else if (action == "delete-employment-history")
            {
                var deleteeh = db.ThongTinNgheNghieps.Where(x => x.Id == model.entry_id).FirstOrDefault();
                db.ThongTinNgheNghieps.Remove(deleteeh);
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else if (action == "save-education-history")
            {
                var dataedu = db.ThongTinDaoTaos.Where(x => x.Id == model.entry_id).FirstOrDefault();
                if (dataedu == null)
                {
                    ThongTinDaoTao eduinfor = new ThongTinDaoTao();
                    eduinfor.MaUV = user.MAUV;
                    eduinfor.Chuyennghanh = model.subject;
                    eduinfor.Tentruong = model.school;
                    eduinfor.MaHV = model.qualification;
                    eduinfor.Hocvan = db.CapBacs.Where(x => x.MACB == model.qualification).Select(x=>x.Tencapbac).FirstOrDefault();
                    if (model.fromDate.Year < 1753)
                    {
                        eduinfor.Tuthang = null;
                        eduinfor.Denthang = null;
                    }
                    else
                    {
                        eduinfor.Tuthang = model.fromDate;
                        eduinfor.Denthang = model.toDate;
                    }
                    eduinfor.Thanhtuu = model.description;
                    eduinfor.resumid = new checkresumid().check(user.MAUV);
                    db.ThongTinDaoTaos.Add(eduinfor);
                    db.SaveChanges();
                    var returnexpwork = db.ThongTinNgheNghieps.Where(x => x.MaUV == user.MAUV);
                    List<expid> orders = new List<expid>();

                    int count = 10;
                    foreach (var item in returnexpwork)
                    {
                        expid exp = new expid();
                        exp.entryid = item.Id;
                        exp.educationorder = count;
                        count = count + 10;
                        orders.Add(exp);
                    }
                    var datajson = db.ThongTinDaoTaos.Where(x => x.resumid == eduinfor.resumid && x.MaUV == user.MAUV).FirstOrDefault();
                    expwork data = new expwork();
                    data.schoolname = datajson.Tentruong;
                    data.description = datajson.Thanhtuu;
                        data.enddate = datajson.Denthang;
                        data.startdate = datajson.Tuthang;
                    data.entry_id = datajson.Id;
                    return Json(new { status = "SUCCESS", orders, data });
                }
                else
                {
                    var updatedu = db.ThongTinDaoTaos.Where(x => x.Id == model.entry_id).FirstOrDefault();
                    updatedu.MaUV = user.MAUV;
                    updatedu.Chuyennghanh = model.subject;
                    updatedu.Tentruong = model.school;
                    updatedu.MaHV = model.qualification;
                    updatedu.Hocvan = db.CapBacs.Where(x => x.MACB == model.qualification).Select(x=>x.Tencapbac).FirstOrDefault();
                    if (model.fromDate.Year < 1753)
                    {
                        updatedu.Tuthang = null;
                        updatedu.Denthang = null;
                    }
                    else
                    {
                        updatedu.Tuthang = model.fromDate;
                        updatedu.Denthang = model.toDate;
                    }
                    updatedu.Thanhtuu = model.description;
                    db.SaveChanges();
                    var returnexpwork = db.ThongTinNgheNghieps.Where(x => x.MaUV == user.MAUV);
                    List<expid> orders = new List<expid>();
                    int count = 10;
                    foreach (var item in returnexpwork)
                    {
                        expid exp = new expid();
                        exp.entryid = item.Id;
                        exp.educationorder = count;
                        count = count + 10;
                        orders.Add(exp);
                    }
                    var datajson = db.ThongTinDaoTaos.Where(x => x.resumid == updatedu.resumid && x.MaUV == user.MAUV).FirstOrDefault();
                    expwork data = new expwork();
                    data.schoolname = datajson.Tentruong;
                    data.description = datajson.Thanhtuu;
                    data.enddate = datajson.Denthang;
                    data.startdate = datajson.Tuthang;
                    data.entry_id = datajson.Id;
                    return Json(new { status = "SUCCESS", orders, data });

                }
            }
            else if (action == "delete-education-history")
            {
                var deletedu = db.ThongTinDaoTaos.Where(x => x.Id == model.entry_id).FirstOrDefault();
                db.ThongTinDaoTaos.Remove(deletedu);
                db.SaveChanges();
                return Json(new { status = "SUCCESS" });
            }
            else if (action == "save-summary-info")
            {
                var sumary = db.CVmongmuons.Where(x => x.MaUV == user.MAUV).FirstOrDefault();
                if (sumary == null)
                {
                    CVmongmuon cv = new CVmongmuon();
                    cv.MaUV = user.MAUV;
                    cv.USD = model.expectedSalaryRange;
                    cv.MaCB = model.expectedJobLevel;
                    foreach (var item in model.expectedLocation)
                    {
                        if (cv.Thanhpho1 == null)
                        {
                            cv.Thanhpho1 = item;
                        }
                        else if(cv.Thanhpho2==null)
                        {
                            cv.Thanhpho2 = item;
                        }
                        else
                        {
                            cv.Thanhpho3 = item;
                        }
                    }
                    foreach (var item in model.expectedJobCategory)
                    {
                        if (cv.Nghanhnghe1 == null)
                        {
                            cv.Nghanhnghe1 = item;
                        }
                        else if (cv.Nghanhnghe2 == null)
                        {
                            cv.Nghanhnghe2 = item;
                        }
                        else
                        {
                            cv.Nghanhnghe3 = item;
                        }
                    }
                    db.CVmongmuons.Add(cv);
                    db.SaveChanges();
                    return Json(new { status = "SUCCESS" });
                    
                }
                else
                {
                    sumary.MaUV = user.MAUV;
                    sumary.USD = model.expectedSalaryRange;
                    sumary.MaCB = model.expectedJobLevel;
                    CVmongmuon cv=new CVmongmuon();
                    foreach (var item in model.expectedLocation)
                    {
                        if (cv.Thanhpho1 == null)
                        {
                            cv.Thanhpho1 = item;
                        }
                        else if (cv.Thanhpho2 == null)
                        {
                            cv.Thanhpho2 = item;
                        }
                        else
                        {
                            cv.Thanhpho3 = item;
                        }
                    }
                    foreach (var item in model.expectedJobCategory)
                    {
                        if (cv.Nghanhnghe1 == null)
                        {
                            cv.Nghanhnghe1 = item;
                        }
                        else if (sumary.Nghanhnghe2 == null)
                        {
                            cv.Nghanhnghe2 = item;
                        }
                        else
                        {
                            cv.Nghanhnghe3 = item;
                        }
                    }
                    sumary.Thanhpho1 = cv.Thanhpho1;
                    sumary.Thanhpho2 = cv.Thanhpho2;
                    sumary.Thanhpho3 = cv.Thanhpho3;
                    sumary.Nghanhnghe1 = cv.Nghanhnghe1;
                    sumary.Nghanhnghe2 = cv.Nghanhnghe2;
                    sumary.Nghanhnghe3=cv.Nghanhnghe3;
                    db.SaveChanges();
                    return Json(new { status = "SUCCESS" });
                }
            }
            return Json(new { status = "ERROR" });
        }
        [SessionExpire]
        public ActionResult ListApplyJob( int? page)
        {
            var user = Session["Member"] as Member;
            if(user==null)
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 20;
            int pagenumber = (page ?? 1);
            var listapplyjob = db.UVDangKies.Where(x => x.MAUV == user.MAUV);
            return View(listapplyjob.OrderByDescending(x => x.Id).ToPagedList(pagenumber, pagesize));
        }
        [SessionExpire]
        public ActionResult ListEmployerSaved(int? page)
        {
            var user = Session["Member"] as Member;
            int pagesize = 20;
            int pagenumber = (page ?? 1);
            var listsaved = db.LuuHosoes.Where(x => x.MAUV == user.MAUV&&x.Tinhtrang==1);
            return View(listsaved.OrderByDescending(x => x.Ngayluu).ToPagedList(pagenumber, pagesize));
        }
       [SessionExpire]
        [HttpPost]
        public ActionResult ChangePassword(string password)
        {
            var user = Session["Member"] as Member;
            var data = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
            data.Matkhau = LoginMember.MD5Hash(password);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult DeleteUVDK(List<string> listdelete)
        {
            foreach (string item in listdelete)
            {
                if (item != "on")
                {
                    int madk = int.Parse(item);
                    var datadelete = db.UVDangKies.Where(x => x.Id==madk).FirstOrDefault();
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
        [SessionExpire]
        public ActionResult DeleteSaved(List<string> listdelete)
        {
            foreach (string item in listdelete)
            {
                if (item != "on")
                {
                    int madk = int.Parse(item);
                    var datadelete = db.LuuHosoes.Where(x => x.Id == madk).FirstOrDefault();
                    if (datadelete == null)
                    {
                        return Json(0);
                    }
                    datadelete.Tinhtrang = 0;
                    db.SaveChanges();
                }
            }
            return Json(1);
        }
        [SessionExpire]
        [HttpPost]
        public ActionResult Uploadimage(HttpPostedFileBase avatar, string action)
        {
            
            var user=new Member();
            if (Session["Member"] != null)
            {
                user = Session["Member"] as Member;
            }
            else{
                return RedirectToAction("Login","Member");
            }
                  var data1 = db.UngViens.Where(x => x.MAUV == user.MAUV).FirstOrDefault();
                if (avatar != null && action == "upload-avatar")
                {
                string filename = System.IO.Path.GetFileName(avatar.FileName);
                string extname = System.IO.Path.GetExtension(avatar.FileName);
                if (extname != ".jpg" && extname != ".png" && extname != ".gif")
                {
                    return Json(new { status = "error" });
                }
                else
                {
                    string savenamedata = data1.MAUV.ToString() + extname;
                    string path = System.IO.Path.Combine(Server.MapPath("~/Profile_upload/"), savenamedata);
                    avatar.SaveAs(path);
              
                    data1.Hinhanh = savenamedata;
                    db.SaveChanges();
                    var data = new data
                    {
                        action = "crop",
                        img_url = "/profile_upload/"+savenamedata+"",
                        img_info = new img_info
                        {
                            h = 100,
                             w= 100,
                            height = 300,
                            width=300
                        }
                    };
                    return Json(new {status="SUCCESS",data});
                }
                }
                else if (action == "remove-avatar")
                {
                    string deletepath = Server.MapPath("/profile_upload/" + data1.Hinhanh);
                    System.IO.File.Delete(deletepath);
                    data1.Hinhanh = null;
                    db.SaveChanges();
                }
                else if (action == "crop-avatar")
                {
                    var data2 = new data
                    {
                        img_url = "/profile_upload/" + data1.Hinhanh + "",
                    };
                    return Json(new { status = "SUCCESS", data = data2 });
                }
                return Json(new { status = "SUCCESS" });
        }
        [HttpPost]

        public ActionResult SkillCompelete(string str)
        {
            var data = db.KyNangs.Where(x => x.TenKN.Contains(str));
            List<complete> dt = new List<complete>();
            complete data1=new complete();
            List<string> list = new List<string>();
            foreach(var item in data)
            {
                list.Add(item.TenKN);
            }
            return Json(list);
        }
        [HttpPost]
        [SessionExpire]
        public ActionResult UpdateSkill(string action,string skillName)
        {
            var skill = db.KyNangs.Where(x => x.TenKN == skillName).FirstOrDefault();
            if (skill == null)
            {
                KyNang kn = new KyNang();
                kn.TenKN = skillName;
                kn.tinhtrang = 2;
                db.KyNangs.Add(kn);
                db.SaveChanges();
            }
            var skill1 = db.KyNangs.Where(x => x.TenKN == skillName).FirstOrDefault();
            var data = new skill()
            {
                skillId=skill1.MAKN,
                skillName=skill1.TenKN,
                status="new"
                
            };
            return Json(new{status="SUCCESS",data});
        }
        [SessionExpire]
        public ActionResult InforGeneral()
        {
            Member result = new CheckSession().SessionMember();
            if (result==null)
            {
                return RedirectToAction("Login", "Member");
            }
            else
            {
                var data = db.UngViens.Where(x => x.MAUV == result.MAUV).FirstOrDefault();
                return View(data);
            }
            
        }
        [SessionExpire]
        public ActionResult Skill()
        {
            var user = Session["Member"] as Member;
            var data = db.KyNangUVs.Where(x => x.MAUV == user.MAUV);
            return View(data);
        }
        [SessionExpire]
        public ActionResult Language()
        {
            var user = Session["Member"] as Member;
            var nn = db.NgonNguUVs.Where(x => x.MAUV == user.MAUV);
            ViewBag.Listl = db.NgonNgus;
            return View(nn);
        }
        [SessionExpire]
        public ActionResult ExpWork()
        {
            var user = Session["Member"] as Member;
            return View(db.ThongTinNgheNghieps.Where(x=>x.MaUV==user.MAUV));
        }
        [SessionExpire]
        public ActionResult Education()
        {
            var user = Session["Member"] as Member;
            var cb = db.CapBacs;
            ViewBag.BangCap = cb;
            return View(db.ThongTinDaoTaos.Where(x=>x.MaUV==user.MAUV));
        }
        [SessionExpire]
        public ActionResult InforReference()
        {
            return View();
        }
        [SessionExpire]
        public ActionResult Benefit()
        {
            var user = Session["Member"] as Member;
            ViewBag.Address = db.cities;
            ViewBag.Career = db.NghanhNghes.Where(x=>x.Idnghanhcha!=0&&x.Tinhtrang==1);
            ViewBag.Position = db.CBmongmuons;
            CVmongmuon datacv = db.CVmongmuons.Where(x => x.MaUV == user.MAUV).FirstOrDefault();
                return View(datacv);
        }
        [SessionExpire]
        public ActionResult ColRight()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Session["Member"] != null)
            {

              return RedirectToAction("Profile","Member");
            }
            if (Session["Admin"] != null)
            {

                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Error = TempData["Error"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(Member model,string callback)
        {
           
            if (model.EmailDN == null || model.Matkhau == null)
            {
                ModelState.AddModelError("", "Vui lòng nhập email và mật khẩu");
                return View();
            }
            var result = new LoginMember().Login(model.EmailDN,LoginMember.MD5Hash(model.Matkhau));
            if (result == 1)
            {
                var user = db.UngViens.Where(x => x.EmailDN == model.EmailDN).FirstOrDefault();
                Session["Member"] = new Member() { MAUV = user.MAUV, EmailDN = user.EmailDN, Hovatendem = user.Hovatendem, Ten = user.Ten, Loaitaikhoan = user.Loaitaikhoan };
                user.matkhaureset = null;
                db.SaveChanges();
                if (callback == null||callback=="")
                {
                    return RedirectToAction("Profile", "Member");
                }
                else
                {
                    return Redirect(callback);
                }
            }
            else if(result==2)
            {
                var user = db.UngViens.Where(x => x.EmailDN == model.EmailDN).FirstOrDefault();
                Session["Admin"] = new UngVien() { MAUV = user.MAUV, EmailDN = user.EmailDN, Hovatendem = user.Hovatendem, Ten = user.Ten, Loaitaikhoan = user.Loaitaikhoan, vaitro = user.vaitro };
                user.matkhaureset = null;
                db.SaveChanges();
                Session["SESSIONFILEPATH"] = "~/Content/Uploaded";
                if (callback != null && callback != "")
                {
                    return Redirect(callback);
                }
                else
                {
                    return Redirect("~/Admin");
                }
                
            }
            else if (result == 3)
            {
                var user = db.UngViens.Where(x => x.EmailDN == model.EmailDN).FirstOrDefault();
                Session["Supper"] = new UngVien() { MAUV = user.MAUV, EmailDN = user.EmailDN, Hovatendem = user.Hovatendem, Ten = user.Ten, Loaitaikhoan = user.Loaitaikhoan, vaitro = 3 };
                user.matkhaureset = null;
                db.SaveChanges();
                Session["SESSIONFILEPATH"] = "~/Content/Uploaded";
                if (callback != null && callback != "")
                {
                    return Redirect(callback);
                }
                else
                {
                    return Redirect("~/Admin");
                }
            }
            else if (result == -1)
            {
                TempData["Error"] = "Tài khoản của bạn hiện không đăng nhập được, vui lòng liên hệ với chúng tôi";
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                TempData["Error"] = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Member model)
        {
            
            var check = db.UngViens.Where(x => x.EmailDN == model.EmailDN).FirstOrDefault();
            if (check == null)
            {
                UngVien uv = new UngVien();
                uv.Hovatendem = model.Hovatendem;
                uv.Ten = model.Ten;
                uv.EmailDN = model.EmailDN;
                uv.EmailLH = model.EmailDN;
                uv.Matkhau = LoginMember.MD5Hash(model.Matkhau);
                uv.Loaitaikhoan = 1;
                uv.Tinhtrang = 1;
                uv.Ngaydangky = DateTime.Now;
                uv.Ngaycapnhat = DateTime.Now;
                uv.vaitro = 1;
                uv.Namkinhnghiem = 0;
                db.UngViens.Add(uv);
                db.SaveChanges();
                var user = db.UngViens.Where(x => x.EmailDN == model.EmailDN).FirstOrDefault();
                //Directory.CreateDirectory("~/Profile_upload/"+user.MAUV+"");
                return RedirectToAction("Profile","Member");
            }
            else
            {
                ModelState.AddModelError("", "Email đã tồn tại trong hệ thống");
                return View();
            }
       
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code, string callback)
        {
            callback = HttpContext.Request.UrlReferrer.Query;
            callback = callback.Replace("?ReturnUrl=", "");
            if (code == null)
            {
                return RedirectToAction("Login", "Member");
            }
            var fb = new FacebookClient();

            var exist = Session["facebook"] as Reload;
            if (Session["facebook"] != null)
            {
                if (exist.curent == code)
                {
                    return View();
                }
            }
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                    string userName = me.id;
                    string email = me.email;
                string emaildn;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                if (email == null)
                {
                    emaildn = userName;
                }
                else
                {

                    emaildn = email;
                }
                var checklogin = db.UngViens.Where(x => x.EmailDN == emaildn).FirstOrDefault();
                if (checklogin == null)
                {
                    UngVien uv = new UngVien();
                    uv.Hovatendem = "" + lastname + " " + middlename + ""; 
                    uv.Ten =firstname;
                    uv.EmailDN = emaildn;
                    uv.EmailLH = email;
                    uv.Loaitaikhoan = 2;
                    uv.Tinhtrang = 1;
                    uv.Ngaydangky = DateTime.Now;
                    uv.Ngaycapnhat = DateTime.Now;
                    uv.vaitro = 1;
                    uv.Namkinhnghiem = 0;
                    db.UngViens.Add(uv);
                    db.SaveChanges();
                    var user = db.UngViens.Where(x => x.EmailDN == uv.EmailDN).FirstOrDefault();
                    Session["Member"] = new Member() { MAUV=user.MAUV,EmailDN=user.EmailDN, Loaitaikhoan = uv.Loaitaikhoan, Hovatendem = uv.Hovatendem, Ten = uv.Ten };
                    //Directory.CreateDirectory("~/Profile_upload/" + user.MAUV + "");
                    if (callback == null||callback=="")
                    {
                        return RedirectToAction("Profile", "Member");
                    }
                    else
                    {
                        return Redirect(callback);
                    }
                }
                else if (checklogin.Tinhtrang == 0)
                {
                    TempData["Error"] = "Tài khoản của bạn đang tạm khóa vui lòng liên hệ với chúng tôi";
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    if (checklogin.Tinhtrang == -1)
                    {
                        TempData["Error"] = "Tài khoản của bạn hiện không đăng nhập được, vui lòng liên hệ với chúng tôi";
                        return Redirect(Request.UrlReferrer.ToString());

                    }
                    if (checklogin.vaitro == 1&&checklogin.Tinhtrang==1)
                    {
                        Session["Member"] = new Member() { MAUV = checklogin.MAUV, EmailDN = checklogin.EmailDN, Loaitaikhoan = checklogin.Loaitaikhoan, Hovatendem = checklogin.Hovatendem, Ten = checklogin.Ten };
                        if (callback == null||callback=="")
                        {
                            return RedirectToAction("Profile", "Member");
                        }
                        else
                        {
                            return Redirect(callback);
                        }
                    }
                    else if (checklogin.vaitro == 2 && checklogin.Tinhtrang == 1)
                    {
                        Session["Admin"] = new UngVien() { MAUV = checklogin.MAUV, EmailDN = checklogin.EmailDN, Hovatendem = checklogin.Hovatendem, Ten = checklogin.Ten, Loaitaikhoan = checklogin.Loaitaikhoan, vaitro = checklogin.vaitro };
                        Session["SESSIONFILEPATH"] = "~/Content/Uploaded";
                        if (callback != null && callback != "")
                        {
                            return Redirect(callback);
                        }
                        else
                        {
                            return Redirect("~/Admin");
                        }
                    }
                    else if (checklogin.vaitro == 3)
                    {
                        Session["Supper"] = new UngVien() { MAUV = checklogin.MAUV, EmailDN = checklogin.EmailDN, Hovatendem = checklogin.Hovatendem, Ten = checklogin.Ten, Loaitaikhoan = checklogin.Loaitaikhoan, vaitro = checklogin.vaitro };
                        Session["SESSIONFILEPATH"] = "~/Content/Uploaded";
                        if (callback != null && callback != "")
                        {
                            return Redirect(callback);
                        }
                        else
                        {
                            return Redirect("~/Admin");
                        }
                    }
                }

            }
            return RedirectToAction("Login", "Member");
        }
        public ActionResult JobForMember()
        {
            if (Session["Member"] != null)
            {
                var user = Session["Member"] as Member;
                DangTinViecLam dt=new DangTinViecLam();
                var cvmm = db.CVmongmuons.Where(x => x.MaUV == user.MAUV).FirstOrDefault();
                if (cvmm != null)
                {
                    dt.Manghanh1 = cvmm.Nghanhnghe1;
                    dt.Manghanh2 = cvmm.Nghanhnghe2;
                    dt.Manghanh3 = cvmm.Nghanhnghe3;
                    var result = db.DangTinViecLams.Where(x => x.Manghanh1 == dt.Manghanh1 || x.Manghanh2 == dt.Manghanh2 || x.Manghanh3 == dt.Manghanh3).OrderByDescending(x => x.MACV).Take(15).ToList();
                    result = result.Where(x => x.Trangthai == 1 && x.Tinhtrang == 1).ToList();
                    if (result.Count == 0)
                    {
                        return View(db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1).OrderByDescending(x => x.MACV).Take(15).ToList());
                    }
                    return View(result);
                }
                else
                {
                    return View(db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1).OrderByDescending(x => x.MACV).Take(15).ToList());
                }

            }
            return View(db.DangTinViecLams.Where(x=>x.Tinhtrang==1&&x.Trangthai==1).OrderByDescending(x => x.MACV).Take(15).ToList());
        }
        public ActionResult Logout()
        {
            Session["Member"] = null;
            return RedirectToAction("Login", "Member");
        }
        public ActionResult ACMember(int mauv)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Member", "Login");
            }
            var user = db.UngViens.Where(x => x.MAUV == mauv).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("NotFound", "PageNotFound");
            }
            Session["Member"] = new Member() { MAUV = user.MAUV, EmailDN = user.EmailDN, Hovatendem = user.Hovatendem, Ten = user.Ten, Loaitaikhoan = user.Loaitaikhoan };
            return Redirect("/viec-lam-cua-ban");
        } 

    }
}