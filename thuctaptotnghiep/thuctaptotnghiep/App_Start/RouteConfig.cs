using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using thuctaptotnghiep.Models;

namespace thuctaptotnghiep
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                  routes.MapRoute(
                "ChangePassWordEmployer",
                "doanh-nghiep/quen-mat-khau/{id}",
                new { controller = "ResetPassWord", action = "ChangePasswordEmployer" },
                new[] { "thuctaptotnghiep.Controllers" });

                   routes.MapRoute(
                "ForgotPassowordEmployer",
                "doanh-nghiep/quen-mat-khau",
                new { controller = "ResetPassWord", action = "ForgotPassWordEmployer" },
                new[] { "thuctaptotnghiep.Controllers" });

                   routes.MapRoute(
                "ForgotPassoword",
                "quen-mat-khau",
                new { controller = "ResetPassWord", action = "ForGotPassWordMember" },
                new[] { "thuctaptotnghiep.Controllers" });

                 routes.MapRoute(
                "ChangePassword",
                "quen-mat-khau/thay-doi-mat-khau/{id}",
                new { controller = "ResetPassWord", action = "ChangePassword" },
                new[] { "thuctaptotnghiep.Controllers" });

                  routes.MapRoute(
                "Profile",
                "ho-so",
                new { controller = "Member", action = "Profile" },
                new[] { "thuctaptotnghiep.Controllers" });

                  routes.MapRoute(
                "ListApplyJob",
                "viec-lam-cua-ban",
                new { controller = "Member", action = "ListApplyJob" },
                new[] { "thuctaptotnghiep.Controllers" });

                  routes.MapRoute(
                "ListCompany",
                "cong-ty-luu-ho-so",
                new { controller = "Member", action = "ListEmployerSaved" },
                new[] { "thuctaptotnghiep.Controllers" });

                    routes.MapRoute(
                "Login",
                "dang-nhap-thanh-vien",
                new { controller = "Member", action = "Login" },
                new[] { "thuctaptotnghiep.Controllers" });

                   routes.MapRoute(
                "Logout",
                "dang-xuat",
                new { controller = "Member", action = "Logout" },
                new[] { "thuctaptotnghiep.Controllers" });

                    routes.MapRoute(
                "Register",
                "dang-ky-thanh-vien",
                new { controller = "Member", action = "Register" },
                new[] { "thuctaptotnghiep.Controllers" });

                    routes.MapRoute(
                 name: "Search",
                url: "tim-viec-lam",
                 defaults: new { controller = "SearchJob", action = "ShowSearch" },
                 namespaces: new[] { "thuctaptotnghiep.Controllers" }
            );

               
                    routes.MapRoute(
                        "JobDetail",
                        "viec-lam-{TenCV}-{id}",
                         new { controller = "Job", action = "DetailJob" },
                        new[] { "thuctaptotnghiep.Controllers" });

                    routes.MapRoute(
                            "Category",
                            "ban-tin-{categoryName}-{categoryid}",
                            new { controller = "News", action = "ListNewsOfCategory", categoryid = UrlParameter.Optional },
                        new[] { "thuctaptotnghiep.Controllers" });

               routes.MapRoute(
             "DetailNews",
             "{categoryName}/{titlenews}-{newsid}",
             new { controller = "News", action = "DetailsNews", id = UrlParameter.Optional },
             new[] { "thuctaptotnghiep.Controllers" });



                routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "thuctaptotnghiep.Controllers" }
    );
                routes.MapRoute(
       "CatchAllUrls",
       "{*url}",
       new { controller = "Notfound", action = "CatchAllUrls" }
    );
        }
    }
}
