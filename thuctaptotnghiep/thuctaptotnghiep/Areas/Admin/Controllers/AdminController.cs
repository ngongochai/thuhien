using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace Admin.Controllers
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["Admin"] == null && HttpContext.Current.Session["Supper"] == null)
            {
                filterContext.Result = new RedirectResult("/dang-nhap-thanh-vien?ReturnUrl=" + HttpContext.Current.Request.RawUrl + "");
                return;
            }
            if (ctx.Session["Admin"] == null)
            {
                ctx.Session["Admin"] = ctx.Session["Supper"];
            }
            base.OnActionExecuting(filterContext);
        }
    }
    [SessionExpire]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult MainSlideBar()
        {
            return View();
        }
        public ActionResult Footer()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["Member"] = null;
            Session["Employer"] = null;
            Session["Admin"] = null;
            Session["SESSIONFILEPATH"] = null;
            Session["Supper"] = null;
            return Redirect("/dang-nhap-thanh-vien");
        }
    }
}