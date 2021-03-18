using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thuctaptotnghiep.Models
{
    public class CheckSession
    {
        public Member SessionMember()
        {
            if (HttpContext.Current.Session["Member"] == null)
            {
                return null;
            }
            else
            {
                var user = HttpContext.Current.Session["Member"] as Member;
                return user;

            }
        }
    }
}