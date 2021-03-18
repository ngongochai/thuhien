using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Model.Framework;

namespace thuctaptotnghiep
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //var job = db.DangTinViecLams.Where(x =>x.Ngayhethangjob!=null&& x.Ngayhethangjob.Value.Date == DateTime.Now.Date).ToList();
            //job.ForEach(x => x.Tinhtrang = -1);
            //db.SaveChanges();
        }
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
            var job = db.DangTinViecLams.Where(x => x.Ngayhethangjob != null &&x.Tinhtrang!=-2&& x.Ngayhethangjob.Value <= DateTime.Now).ToList();
            if (job.Count() > 0)
            {
                job.ForEach(x => x.Tinhtrang = -2);
                db.SaveChanges();
            }
          
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
            var allow = db.LuotTruyCaps.FirstOrDefault();
            if (allow == null)
            {
                allow = new LuotTruyCap();
                allow.today = 0;
                allow.total = 0;
                db.LuotTruyCaps.Add(allow);
            }
            if (allow.datetoday == null)
            {
                allow.datetoday = DateTime.Now;
                if (allow.today == null)
                {
                    allow.today = 1;
                    allow.total += 1;
                }
                else
                {
                    allow.today += 1;
                    allow.total += 1;
                }
            }
            else if (allow.datetoday.Value.Date == DateTime.Now.Date)
            {

                allow.today += 1;
                allow.total += 1;
            }
            else
            {
                allow.datetoday = DateTime.Now;
                allow.today = 1;
                allow.total += 1;
            }
            db.SaveChanges();
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            //Error logging omitted

            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            IController errorController = new Controllers.NotfoundController();
            routeData.Values.Add("controller", "Notfound");
            routeData.Values.Add("area", "");
            routeData.Values.Add("ex", exception);

            if (httpException != null)
            {
                //this is a basic exampe of how you can choose to handle your errors based on http status codes.
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        Response.Clear();

                        // page not found
                        routeData.Values.Add("action", "PageNotFound");

                        Server.ClearError();
                        // Call the controller with the route
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

                        break;
                    case 500:
                        // server error
                        routeData.Values.Add("action", "ServerError");

                        Server.ClearError();
                        // Call the controller with the route
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                        break;
                    case 403:
                        // server error
                        routeData.Values.Add("action", "UnauthorisedRequest");

                        Server.ClearError();
                        // Call the controller with the route
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                        break;
                    //add cases for other http errors you want to handle, otherwise HTTP500 will be returned as the default.
                    default:
                        // server error
                        routeData.Values.Add("action", "ServerError");

                        Server.ClearError();
                        // Call the controller with the route
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                        break;
                }
            }
            //All other exceptions should result in a 500 error as they are issues with unhandled exceptions in the code
            else
            {
                routeData.Values.Add("action", "UnauthorisedRequest");
                Server.ClearError();
                // Call the controller with the route
                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }
    }
}
