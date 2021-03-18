using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using PagedList.Mvc;
using PagedList;

namespace thuctaptotnghiep.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult NewsHomePage()
        {
            return View(db.categorynews.Where(x=>x.showonhomepage==true&&x.trash!=true).OrderBy(x=>x.displayorder).ToList());
        }
        public ActionResult ListNewsOfCategory(int? categoryid,int? page)
        {
            if (categoryid == null)
            {
                return Redirect("/");
            }
            var data = db.categorynews.Where(x => x.categoryId == categoryid && x.trash != true && x.active == true).FirstOrDefault();
            if (data == null)
            {
                return Redirect("/");
            }
            else
            {
                var datanews = db.News.Where(x => x.categoryId == categoryid && x.active == true && x.trash != true);
                int pagenumber = (page ?? 1);
                int pagesize = 20;
                return View(datanews.OrderByDescending(x => x.newsid).ToPagedList(pagenumber, pagesize));
            }
        }
        public ActionResult DetailsNews(int? newsid)
        {
            if (newsid == null)
            {
                return Redirect("/");
            }
            var model = db.News.Where(x => x.newsid == newsid&&x.active==true&&x.trash!=true).FirstOrDefault();
            if (model == null)
            {
                return Redirect("/");
            }
            var list = db.News.Where(x => x.categoryId == model.categoryId && x.newsid != model.newsid).ToList().Take(8).ToList();
            ViewBag.data = list;
            model.ViewBest += 1;
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(model);
        }
    }
}