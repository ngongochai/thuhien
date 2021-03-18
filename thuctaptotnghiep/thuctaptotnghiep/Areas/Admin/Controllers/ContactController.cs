using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Setup;
using PagedList.Mvc;
using PagedList;
namespace Admin.Controllers
{
    [SessionExpire]
    public class ContactController : Controller
    {
        // GET: Contact
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult ListContact(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 20;
            int pagenumber = (page ?? 1);
            var listcontact = db.LienHes;
            return View(listcontact.OrderByDescending(x => x.Id).ToPagedList(pagenumber, pagesize));
        }
        [HttpPost]
        public ActionResult deletecontact(List<int> selectedIds)
        {
            
            if (selectedIds.Count() > 0)
            {
                var listcontact = db.LienHes;
                foreach (int item in selectedIds)
                {
                    listcontact.Remove(listcontact.Where(x=>x.Id==item).FirstOrDefault());
                }
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}