using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using PagedList.Mvc;
using PagedList;
using Model.Setup;

namespace Admin.Controllers
{
    [SessionExpire]
    public class ManagerNewsController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: ManagerNews
        public ActionResult ListCategoryNews()
        {
            return View(db.categorynews.Where(x => x.trash != true).ToList());
        }
        [HttpPost]
        public ActionResult CreateCategoryNews(string name)
        {
            categorynew ct = new categorynew();
            ct.categoryName = name.Trim();
            db.categorynews.Add(ct);
            db.SaveChanges();
            return Json(1);
        }
        [HttpPost]
        public ActionResult EditCategoryNews(string content, string name, int categoryid)
        {
            var datacategory = db.categorynews.Where(x => x.categoryId == categoryid && x.trash != true).FirstOrDefault();
            if (datacategory == null)
            {
                return Json(new { status = 0 });
            }
            if (name == "categoryName")
            {
                datacategory.categoryName = content.Trim();
                db.SaveChanges();
            }
            if (name == "showonhomepage")
            {
                var value = true;
                if (content == "1")
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
                datacategory.showonhomepage = value;
                db.SaveChanges();
            }
            if (name == "active")
            {
                var value = true;
                if (content == "1")
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
                datacategory.active = value;
                db.SaveChanges();
            }
            if (name == "position")
            {
                datacategory.position = string.IsNullOrEmpty(content) ? 1 : int.Parse(content);
                db.SaveChanges();
            }
            if (name == "displayorder")
            {
                datacategory.displayorder = string.IsNullOrEmpty(content) ? 1 : int.Parse(content);
                db.SaveChanges();
            }
            return Json(new { status = 1 });
        }
        [HttpPost]
        public ActionResult RemoveCategoryNews(int categoryid)
        {
            var data = db.categorynews.Where(x => x.categoryId == categoryid && x.trash != true).FirstOrDefault();
            if (data == null)
            {
                return Json(0);

            }
            else
            {
                var datanews = db.News.Where(x => x.categoryId == data.categoryId).ToList();
                datanews.ForEach(x => x.trash = true);
                data.trash = true;
                db.SaveChanges();
                return Json(new { status = 1 });
            }
        }
        public ActionResult CreateNews()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateNews(News model)
        {
            var user = Session["Admin"] as UngVien;
            model.datepost = DateTime.Now;
            model.namepost = user.EmailDN;
            model.titlenews = model.titlenews.Trim();
            model.ViewBest = 0;
            db.News.Add(model);
            db.SaveChanges();
            return Json(1);
        }
        public ActionResult EditNews(int? newsid)
        {
            if (newsid == null)
            {
                return Redirect("/Admin");
            }
            var datanews = db.News.Where(x => x.newsid == newsid && x.trash != true).FirstOrDefault();
            if (datanews == null)
            {
                return Redirect("/Admin");
            }
            else
            {
                return View(datanews);
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditNews(News model)
        {
            if (model.newsid == null)
            {
                return Json(0);
            }
            var datanews = db.News.Where(x => x.newsid == model.newsid && x.trash != true).FirstOrDefault();
            if (datanews == null)
            {
                return Json(0);
            }
            else
            {
                datanews.titlenews = model.titlenews.Trim();
                datanews.description = model.description;
                datanews.categoryId = model.categoryId;
                datanews.active = model.active;
                datanews.showonhomepage = model.showonhomepage;
                datanews.Featured = model.Featured;
                datanews.titleimages = model.titleimages;
                datanews.htmlcontent = model.htmlcontent;
                datanews.displayorder = model.displayorder;
                db.SaveChanges();
                return Json(1);
            }
        }
        [HttpPost]
        public ActionResult RemoveNewsAdmin(List<int> val)
        {
            if (val.Count == 0 || val == null)
            {
                return Json(0);
            }
            else
            {
                foreach (var item in val)
                {
                    int newsid = item;
                    var data = db.News.Where(x => x.newsid == newsid).FirstOrDefault();
                    if (data == null)
                    {
                        return Json(0);
                    }
                    else
                    {
                        data.trash = true;
                        db.SaveChanges();
                    }
                }
                return Json(1);
            }
        }
        //public ActionResult ListNews(int? page)
        //{
        //    var listnew = db.News.ToList();
        //    var listsearch = new List<News>();
        //    string search = Request.QueryString["search"];
        //    string status = Request.QueryString["status"];
        //    if (!string.IsNullOrEmpty(status))
        //    {
        //        if (status == "1")
        //            listnew.Where(x => x.active == true && x.trash != true).ToList();
        //        else if (status == "-1")
        //            listnew.Where(x => x.active == false && x.trash != true).ToList();
        //        else if (status == "2")
        //            listnew.Where(x => x.showonhomepage == true && x.trash != true).ToList();
        //        else if (status == "-2")
        //            listnew.Where(x => x.showonhomepage == false && x.trash != true).ToList();
        //        else if (status == "3")
        //            listnew.Where(x => x.Featured == true && x.trash != true).ToList();
        //        else
        //            listnew.Where(x => x.Featured == false && x.trash != true).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        search = new RemoveUnicodeModel().UnicodeNameModel(search.Trim());
        //        foreach (var item in listnew)
        //        {
        //            string title = new RemoveUnicodeModel().UnicodeNameModel(item.titlenews);
        //            string author = new RemoveUnicodeModel().UnicodeNameModel(item.namepost);
        //            string datecreate = new RemoveUnicodeModel().UnicodeNameModel(item.datepost.Value.ToString("dd/MM/yyyy"));
        //            string category = new RemoveUnicodeModel().UnicodeNameModel(item.categorynew.categoryName);
        //            if (item.trash != true && item.newsid.ToString() == search || author.Contains(search) || title.Contains(search) || datecreate.Contains(search) || category.Contains(search))
        //            {
        //                listsearch.Add(item);
        //            }
        //        }
        //    }
        //    int pagesize = 100;
        //    int pagenumber = (page ?? 1);
        //    return View(listsearch.Where(x => x.trash != true).OrderByDescending(x => x.newsid).ToPagedList(pagenumber, pagesize));
        //}

        public ActionResult ListNews(string search, string status, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagenumber = (page ?? 1);
            int pagesize = 100;
            if (search == null)
                search = "";
            search = new RemoveUnicodeModel().UnicodeNameModel(search.Trim());
            List<News> datanews = db.News.Where(x => x.trash != true).ToList();
            var listsearch = new List<News>();
            if (status != null)
            {
                if (status == "1")
                    datanews = datanews.Where(x => x.active == true).ToList();
                else if (status == "-1")
                    datanews = datanews.Where(x => x.active != true).ToList();
                else if (status == "2")
                    datanews = datanews.Where(x => x.showonhomepage == true).ToList();
                else if (status == "-2")
                    datanews = datanews.Where(x => x.showonhomepage != true).ToList();
                else if (status == "3")
                    datanews = datanews.Where(x => x.Featured == true).ToList();
                else if (status == "-3")
                    datanews = datanews.Where(x => x.Featured != true).ToList();
            }
            if (search != "")
            {
                foreach (var item in datanews)
                {
                    string title = new RemoveUnicodeModel().UnicodeNameModel(item.titlenews);
                    string author = new RemoveUnicodeModel().UnicodeNameModel(item.namepost);
                    string datecreate = new RemoveUnicodeModel().UnicodeNameModel(item.datepost.Value.ToString("dd/MM/yyyy"));
                    string category = new RemoveUnicodeModel().UnicodeNameModel(db.categorynews.Where(x => x.categoryId == item.categoryId).Select(x => x.categoryName).FirstOrDefault());
                    if (item.trash != true && item.newsid.ToString() == search || author.Contains(search) || title.Contains(search) || datecreate.Contains(search) || category.Contains(search))
                    {
                        listsearch.Add(item);
                    }
                }
            }
            else
            {
                listsearch = datanews;
            }
            return View(listsearch.OrderByDescending(x => x.newsid).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult ListNewsForCategory(int? page, int? categoryid)
        {
            if (categoryid == null)
            {
                return Redirect("/Admin");
            }
            var datacategory = db.categorynews.Where(x => x.categoryId == categoryid && x.trash != true).FirstOrDefault();
            if (datacategory == null)
            {
                return Redirect("/Admin");
            }
            int pagesize = 100;
            int pagenumber = (page ?? 1);
            return View(db.News.Where(x => x.trash != true && x.categoryId == categoryid).OrderByDescending(x => x.newsid).ToPagedList(pagenumber, pagesize));
        }

    }
}