using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Setup;

namespace thuctaptotnghiep.Controllers
{
    public class SendSMSController : Controller
    {
        // GET: SendSMS
        public ActionResult Send()
        {
            //string uboyt=new SMS().listsms();
            // uboyt = new SMS().listsms();
            return View();
           
        }
        [HttpPost]
        public ActionResult Send(string phonenumber, string mess)
        {
            bool kq = new SMS().SendSms(phonenumber, mess);
            //string input = new SMS().listsms();
            return View();
        }
    }
}