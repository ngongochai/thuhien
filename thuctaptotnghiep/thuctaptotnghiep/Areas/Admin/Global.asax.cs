using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
            var job = db.DangTinViecLams.Where(x => x.Ngayhethangjob != null && x.Tinhtrang != -2 && x.Ngayhethangjob.Value <= DateTime.Now).ToList();
            if (job.Count() > 0)
            {
                job.ForEach(x => x.Tinhtrang = -2);
                db.SaveChanges();
            }

        }
    }
}
