using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using System.IO;
using thuctaptotnghiep.Models;
using Model.Setup;
using System.Text.RegularExpressions;
using System.Text;


namespace thuctaptotnghiep.Controllers
{
    public class SearchJobController : Controller
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        // GET: SearchJob
        [HttpGet]
        public ActionResult GetValue(int? workid,int? cityid,string key)
        {
            string url="/SearchJob/ShowSearch";
            if (workid != null)
            {
                string workidstr = "?workid="+workid+"";
                url = url + workidstr;
            }
            if (cityid != null)
            {
                string cityidstr = "?cityid="+cityid+"&&";
                url = url + cityid;
            }
            if (key != null)
            {
                string keystr = "?key=" + key + "&&";
                url = url + keystr;
            }
            return Redirect(url);
        }
        public string RenderPartialViewToString(Controller SearchJob, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            }

            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        
        [HttpGet]
        public ActionResult ShowSearch(int? workid, int? cityid, string key)
        {
            ViewBag.City = db.cities.ToList();
            ViewBag.Work = db.NghanhNghes.Where(x=>x.Tinhtrang==1&&x.Idnghanhcha!=0).ToList();
            var result = db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1).ToList();
            if (workid != null && workid != -1)
            {

                result = result.Where(x => x.Manghanh1 == workid || x.Manghanh2 == workid || x.Manghanh3 == workid).ToList();
                var skill = db.SkillWorks.Where(x => x.MAN == workid).ToList();
                List<KyNang> listkn = new List<KyNang>();
                foreach (var item in skill)
                {
                    KyNang kn = new KyNang();
                    kn.TenKN = db.KyNangs.Where(x => x.MAKN == item.MAKN).Select(x => x.TenKN).FirstOrDefault();
                    listkn.Add(kn);
                }
                ViewBag.Skill = listkn;
            }
            else
            {
                ViewBag.Skill = db.KyNangs.Where(x => x.Manghanh != null).Take(10);
            }
            if (cityid != null && cityid != -1)
            {
                result = result.Where(x => x.MaTP1 == cityid || x.MaTP2 == cityid || x.MaTP3 == cityid).ToList();
            }
            if (key != null && key != "")
            {
                result = result.Where(x => (x.TenCV != null && x.TenCV.Contains(key)) || (x.Tags1 != null && x.Tags1.Contains(key)) || (x.Tags2 != null && x.Tags2.Contains(key)) || (x.Tags3 != null && x.Tags3.Contains(key)) || (x.Capbac != null && x.Capbac.Contains(key))).ToList();
            }
            List<JobCategory> listnghe = new List<JobCategory>();
            foreach (var item in result)
            {
                if (item.Manghanh1 != null)
                {
                   
                    if (!listnghe.Exists(x=>x.Id==item.Manghanh1))
                    {
                        JobCategory nn = new JobCategory();
                        nn.Id = int.Parse(item.Manghanh1.ToString());
                        nn.Count = result.Where(x => x.Manghanh1 == nn.Id || x.Manghanh2 == nn.Id || x.Manghanh3 == nn.Id).Count();
                        listnghe.Add(nn);
                    }
                }
                if (item.Manghanh2 != null)
                {
                    if (!listnghe.Exists(x=>x.Id==item.Manghanh2))
                    {
                        JobCategory nn = new JobCategory();
                        nn.Id = int.Parse(item.Manghanh2.ToString());
                        nn.Count = result.Where(x => x.Manghanh1 == nn.Id || x.Manghanh2 == nn.Id || x.Manghanh3 == nn.Id).Count();
                        listnghe.Add(nn);
                    }
                }
                if (item.Manghanh3 != null)
                {
                    if (!listnghe.Exists(x=>x.Id==item.Manghanh3))
                    {
                        JobCategory nn = new JobCategory();
                        nn.Id = int.Parse(item.Manghanh3.ToString());
                        nn.Count = result.Where(x => x.Manghanh1 == nn.Id || x.Manghanh2 == nn.Id || x.Manghanh3 == nn.Id).Count();
                        listnghe.Add(nn);
                    }
                }

            }

            List<JobCity> listcity = new List<JobCity>();
            foreach (var item in result)
            {
                if (item.MaTP1 != null)
                {

                    if (!listcity.Exists(x=>x.Id==item.MaTP1))
                    {
                        JobCity ct = new JobCity();
                        ct.Id = int.Parse(item.MaTP1.ToString());
                        ct.Count = result.Where(x => x.MaTP1 == ct.Id || x.MaTP2 == ct.Id || x.MaTP3 == ct.Id).Count();
                        listcity.Add(ct);
                    }
                }
                if (item.MaTP2 != null)
                {
                    if (!listcity.Exists(x => x.Id == item.MaTP2))
                    {
                        JobCity ct = new JobCity();
                        ct.Id = int.Parse(item.MaTP2.ToString());
                        ct.Count = result.Where(x => x.MaTP1 == ct.Id || x.MaTP2 == ct.Id || x.MaTP3 == ct.Id).Count();
                        listcity.Add(ct);
                    }
                }
                if (item.MaTP3 != null)
                {
                    if (!listcity.Exists(x => x.Id == item.MaTP3))
                    {
                        JobCity ct = new JobCity();
                        ct.Id = int.Parse(item.MaTP3.ToString());
                        ct.Count = result.Where(x => x.MaTP1 == ct.Id || x.MaTP2 == ct.Id || x.MaTP3 == ct.Id).Count();
                        listcity.Add(ct);
                    }
                }

            }
            if (workid == null)
            {
                ViewData["categoryid"] = 0;
            }
            else
            {
                ViewData["categoryid"] = workid;
            }
            if(cityid==null){
                ViewData["locationid"] = 0;
            }
            else
            {
                ViewData["locationid"] = cityid;
            }
            ViewBag.WorkId = listnghe.OrderByDescending(x=>x.Count).ToList();
            ViewBag.CityId = listcity.OrderByDescending(x=>x.Count).ToList();
            return View(result);
        }
        public string RemoveUnicode2(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            string value= regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            value = rgx.Replace(value, " ");
            return value.ToLower();
        }
        [HttpPost]
        public ActionResult BodySearch(int? workid, int? cityid, string key, List<string> selectcategory, List<string> selectlocation, List<string> selectkill, int? minsalary, int? maxsalary, int? level, int? page)
        {
            var result = db.DangTinViecLams.Where(x =>x.Tinhtrang == 1 && x.Trangthai == 1&& (x.Vieclamtotnhat == 1 || x.Vieclamgoiy == 1)).OrderByDescending(x => x.Ngaydang).ToList();
            var datasearch = db.DangTinViecLams.Where(x => x.Tinhtrang == 1 && x.Trangthai == 1&&x.Vieclamgoiy!=1&&x.Vieclamtotnhat!=1).OrderByDescending(x=>x.Ngaydang).ToList();
            result.AddRange(datasearch);
            if (workid != null &&workid!=-1)
            {
                result = result.Where(x => x.Manghanh1 == workid || x.Manghanh2 == workid || x.Manghanh3 == workid).ToList();
                    var skill= db.SkillWorks.Where(x => x.MAN == workid).ToList();
                     List<KyNang> listkn=new List<KyNang>();
                    foreach (var item in skill)
                    {
                        KyNang kn=new KyNang();
                        kn.TenKN=db.KyNangs.Where(x => x.MAKN == item.MAKN).Select(x => x.TenKN).FirstOrDefault();
                        listkn.Add(kn);
                    }
                    ViewBag.Skill = listkn;
                    ViewData["workid"] = workid;
            }
            if (cityid != null&&cityid!=-1)
            {
                result = result.Where(x => x.MaTP1 == cityid || x.MaTP2 == cityid || x.MaTP3 == cityid).ToList();
            }
            if (key != null &&key!="")
            {
                string keynot = RemoveUnicode2(key);
                keynot=keynot.Trim();
                string[] keylist = keynot.Split(new char[] {' ',' '}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in result)
                {
                    int count = 0;
                    foreach (string i in keylist)
                    {
                        if (RemoveUnicode2(item.TenCV).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Maxkinhnghiem = count;
                        }
                        if (item.Tags1!=null&&RemoveUnicode2(item.Tags1).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Maxkinhnghiem = count;
                        }
                        if (item.Tags2 != null&&RemoveUnicode2(item.Tags2).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Maxkinhnghiem = count;
                        }
                        if (item.Tags3 != null&&RemoveUnicode2(item.Tags3).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Maxkinhnghiem = count;
                        }
                        if (item.Capbac != null&&RemoveUnicode2(item.Capbac).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Maxkinhnghiem = count;
                        }
                        if (RemoveUnicode2(item.CongTy.Tencongty).IndexOf(i) > -1)
                        {
                            count += 1;
                            item.Maxkinhnghiem = count;
                        }
                    }
                }
                int keylenght = keylist.Count();
                if (keylist.Count() >= 4)
                {
                    keylenght = 4;
                }
                result = (from re in result
                          where re.Maxkinhnghiem >= keylenght
                          orderby re.Maxkinhnghiem descending, re.MACV descending
                          select re).ToList();
                //result = result.Where(x =>(x.TenCV!=null&&x.TenCV.IndexOf(key)>-1) || (x.Tags1 != null && x.Tags1.Contains(key)) || (x.Tags2 != null && x.Tags2.Contains(key)) || (x.Tags3!=null&&x.Tags3.Contains(key)) || (x.Capbac!=null&&x.Capbac.Contains(key))).ToList();
            }
            if (maxsalary != null && minsalary==null)
            {
                result = result.Where(x => x.Maxluong >= maxsalary).ToList();
            }
            else if(maxsalary!=null && minsalary!=null)
            {
                result = result.Where(x => x.Minluong >= minsalary && x.Maxluong <= maxsalary).ToList();
            }
            if (level != null)
            {
                result = result.Where(x => x.MACB == level).ToList();
            }
            if (selectcategory != null)
            {
                List<JobCategory> listcate = new List<JobCategory>();
                foreach (string item in selectcategory)
                {
                    JobCategory ca = new JobCategory();
                    int categoryid = Int32.Parse(item);
                    result = result.Where(x => x.Manghanh1 == categoryid || x.Manghanh2 == categoryid || x.Manghanh3 == categoryid).ToList();
                    ca.Id = categoryid;
                    listcate.Add(ca);
                }
                Session["Listcategory"] = listcate;

            }
            else
            {
                Session["Listcategory"] = null;
            }
            if (selectlocation != null)
            {
                List<JobCity> listcate = new List<JobCity>();
                foreach (string item in selectlocation)
                {
                    JobCity ca = new JobCity();
                    int categoryid = Int32.Parse(item);
                    result = result.Where(x => x.MaTP1 == categoryid || x.MaTP2 == categoryid || x.MaTP3 == categoryid).ToList();
                    ca.Id = categoryid;
                    listcate.Add(ca);
                }
                Session["Listcity"] = listcate;
            }
            else
            {
                Session["Listcity"] = null;
            }
            if (selectkill != null)
            {
                List<JobSkill> listskill = new List<JobSkill>();
                foreach (string item in selectkill)
                {
                    JobSkill sk = new JobSkill();
                    result = result.Where(x => x.Tags1 == item || x.Tags2 == item || x.Tags3 == item).ToList();
                    sk.Name = item;
                    listskill.Add(sk);
                }
                Session["Listskill"] = listskill;
            }
            else
            {
                Session["Listskill"] = null;
            }
            int pagenumber;
            string htmlleft = "";
            string htmlcity = "";
            string htmlskill = "";
            //duyet danh sach nghanh nghe , thanh pho , skill, cap bac
            if (page == null)
            {
                List<JobCategory> listnghe = new List<JobCategory>();
                List<JobCity> listcity = new List<JobCity>();
                List<JobSkill> listskill = new List<JobSkill>();
                foreach (var item in result)
                {
                    if (item.Manghanh1 != null)
                    {

                        if (!listnghe.Exists(x => x.Id == item.Manghanh1))
                        {
                            JobCategory nn = new JobCategory();
                            nn.Id = int.Parse(item.Manghanh1.ToString());
                            nn.Count = result.Where(x => x.Manghanh1 == nn.Id || x.Manghanh2 == nn.Id || x.Manghanh3 == nn.Id).Count();
                            listnghe.Add(nn);
                        }
                    }
                    if (item.Manghanh2 != null)
                    {
                        if (!listnghe.Exists(x => x.Id == item.Manghanh2))
                        {
                            JobCategory nn = new JobCategory();
                            nn.Id = int.Parse(item.Manghanh2.ToString());
                            nn.Count = result.Where(x => x.Manghanh1 == nn.Id || x.Manghanh2 == nn.Id || x.Manghanh3 == nn.Id).Count();
                            listnghe.Add(nn);
                        }
                    }
                    if (item.Manghanh3 != null)
                    {
                        if (!listnghe.Exists(x => x.Id == item.Manghanh3))
                        {
                            JobCategory nn = new JobCategory();
                            nn.Id = int.Parse(item.Manghanh3.ToString());
                            nn.Count = result.Where(x => x.Manghanh1 == nn.Id || x.Manghanh2 == nn.Id || x.Manghanh3 == nn.Id).Count();
                            listnghe.Add(nn);
                        }
                    }
                    if (item.MaTP1 != null)
                    {

                        if (!listcity.Exists(x => x.Id == item.MaTP1))
                        {
                            JobCity ct = new JobCity();
                            ct.Id = int.Parse(item.MaTP1.ToString());
                            ct.Count = result.Where(x => x.MaTP1 == ct.Id || x.MaTP2 == ct.Id || x.MaTP3 == ct.Id).Count();
                            listcity.Add(ct);
                        }
                    }
                    if (item.MaTP2 != null)
                    {
                        if (!listcity.Exists(x => x.Id == item.MaTP2))
                        {
                            JobCity ct = new JobCity();
                            ct.Id = int.Parse(item.MaTP2.ToString());
                            ct.Count = result.Where(x => x.MaTP1 == ct.Id || x.MaTP2 == ct.Id || x.MaTP3 == ct.Id).Count();
                            listcity.Add(ct);
                        }
                    }
                    if (item.MaTP3 != null)
                    {
                        if (!listcity.Exists(x => x.Id == item.MaTP3))
                        {
                            JobCity ct = new JobCity();
                            ct.Id = int.Parse(item.MaTP3.ToString());
                            ct.Count = result.Where(x => x.MaTP1 == ct.Id || x.MaTP2 == ct.Id || x.MaTP3 == ct.Id).Count();
                            listcity.Add(ct);
                        }
                    }
                    if (item.Tags1 != null)
                    {

                        if (!listskill.Exists(x => x.Name == item.Tags1))
                        {
                            JobSkill ct = new JobSkill();
                            ct.Name = item.Tags1;
                            ct.Count = result.Where(x => x.Tags1 == ct.Name || x.Tags2 == ct.Name || x.Tags3 == ct.Name).Count();
                            listskill.Add(ct);
                        }
                    }
                    if (item.Tags2 != null)
                    {
                        if (!listskill.Exists(x => x.Name == item.Tags2))
                        {
                            JobSkill ct = new JobSkill();
                            ct.Name = item.Tags2;
                            ct.Count = result.Where(x => x.Tags1 == ct.Name || x.Tags2 == ct.Name || x.Tags3 == ct.Name).Count();
                            listskill.Add(ct);
                        }
                    }
                    if (item.Tags3 != null)
                    {
                        if (!listskill.Exists(x => x.Name == item.Tags3))
                        {
                            JobSkill ct = new JobSkill();
                            ct.Name = item.Tags3;
                            ct.Count = result.Where(x => x.Tags1 == ct.Name || x.Tags2 == ct.Name || x.Tags3 == ct.Name).Count();
                            listskill.Add(ct);
                        }
                    }

                }
                ViewBag.ListSkill = listskill.OrderByDescending(x => x.Count).ToList();
                htmlskill = RenderPartialViewToString(this, "_Skill", result);
                ViewBag.CityId = listcity.OrderByDescending(x=>x.Count).ToList();
                ViewBag.WorkId = listnghe.OrderByDescending(x => x.Count).ToList() ;
                htmlcity = RenderPartialViewToString(this, "_Location", result);
                htmlleft = RenderPartialViewToString(this, "_Category", result);
                page = 1;
                pagenumber = 0;
            }
            else
            {
                string pagen = page.ToString();
                pagenumber = (Convert.ToInt32(pagen) * 40) - 40;
            }
            int c = result.Count();
            int pageajax = c / 40;
            if (c % 40 != 0)
            {
                pageajax++;
            }
            if (workid != null)
            {
                ViewData["workid"] = workid;
            }
            ViewData["page"] = pageajax;
            ViewData["curentpage"] = page;
            int totaljob = result.Count();
            result=result.Skip(pagenumber).Take(40).ToList();
            string html = RenderPartialViewToString(this, "_Searchpartial",result );
            return Json(new { html=html, htmlleft=htmlleft,htmlcity=htmlcity,total=totaljob,htmlskill=htmlskill });
        }
    }
}