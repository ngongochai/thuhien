using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace thuctaptotnghiep.Controllers
{
    public class NotfoundController : Controller
    {
        // GET: Notfound
        public ActionResult PageNotFound()
        {
       
            return View();
        }

        public ActionResult ServerError()
        {
            
            return View();
        }

        public ActionResult UnauthorisedRequest()
        {
           
            return View();
        }
        public ActionResult CatchAllUrls()
        {
            
            //throwing an exception here pushes the error through the Application_Error method for centralised handling/logging
            throw new HttpException(404, "The requested url " + Request.Url.ToString() + " was not found");
        }
    }
}