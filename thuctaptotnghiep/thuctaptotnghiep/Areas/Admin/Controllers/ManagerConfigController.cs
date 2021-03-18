using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
namespace Admin.Controllers
{
    [SessionExpire]
    public class ManagerConfigController : Controller
    {
        // GET: ManagerConfig
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult CategoryParent()
        {
            var data = db.NghanhNghes.Where(x => x.Tinhtrang == 1 && x.Idnghanhcha == 0);
            return View(data);
        }
        public ActionResult CategoryChild()
        {
            var data = db.NghanhNghes.Where(x => x.Tinhtrang == 1 && x.Idnghanhcha != 0);
            return View(data);
        }
        public ActionResult AddCategoryChild(string name,int parentid)
        {
            NghanhNghe nn = new NghanhNghe();
            nn.Tennghanh = name;
            nn.Idnghanhcha = parentid;
            nn.Tinhtrang = 1;
            db.NghanhNghes.Add(nn);
            db.SaveChanges();
            return Json(1);
        }
        public ActionResult AddCategoryParent(string name)
        {
            NghanhNghe nn = new NghanhNghe();
            nn.Tennghanh = name;
            nn.Idnghanhcha = 0;
            nn.Tinhtrang = 1;
            db.NghanhNghes.Add(nn);
            db.SaveChanges();
            return Json(1);
        }
        public ActionResult RemoveCategoryParent(int? id)
        {
            if (id == null)
            {
                return Redirect("/admin");
            }
            var data = db.NghanhNghes.Where(x => x.Id == id).FirstOrDefault();
            if (data == null)
            {
                return Redirect("/admin");
            }
            else
            {
                data.Tinhtrang = -1;
                db.SaveChanges();
                return Redirect("/Admin/ManagerConfig/Category");
            }
        }
        [HttpPost]
        public ActionResult EditCategoryParent(int id, string val)
        {
            if (id == null)
            {
                return Json(new {status=0 });
            }
            var data = db.NghanhNghes.Where(x => x.Tinhtrang == 1&&x.Id==id).FirstOrDefault();
            if (data == null)
            {
                return Json(new { status = 0 });
            }
            else
            {
                data.Tennghanh = val;
                db.SaveChanges();
                return Json(new { status = 1 });
            }
        }
        [HttpPost]
        public ActionResult EditCategoryChild(int id, string val,int parentid)
        {
            if (id == null)
            {
                return Json(new { status = 0 });
            }
            if (parentid != null)
            {
                var parent = db.NghanhNghes.Where(x => x.Id == parentid&&x.Tinhtrang==1).FirstOrDefault();
                if(parent==null)
                return Json(new { status = 0 });
            }
            var data = db.NghanhNghes.Where(x => x.Tinhtrang == 1&&x.Id==id).FirstOrDefault();
            if (data == null)
            {
               return Json(new {status=0 });
            }
            else
            {
                data.Tennghanh = val;
                data.Idnghanhcha = parentid;
                db.SaveChanges();
                return Json(new {status=1 });
            }
        }
        public ActionResult DeleteSkill(List<int>val)
        {

            foreach (var item in val)
            {
                int id = item;
                var data = db.KyNangs.Where(x => x.MAKN == id).FirstOrDefault();
                if (data != null)
                {
                    var knuv=db.KyNangUVs.RemoveRange(db.KyNangUVs.Where(x => x.MAKN == id).ToList());
                        db.KyNangUVs.RemoveRange(knuv);
                    db.KyNangs.Remove(data);
                    db.SaveChanges();
                    
                }
            }
            return Json(new { status = 1 });
        }
    }
}