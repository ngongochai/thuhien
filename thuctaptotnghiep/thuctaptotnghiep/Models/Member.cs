using Model.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;

namespace thuctaptotnghiep.Models
{
    public class Totalallow
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public void today()
        {
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
    }
    public class Member
    {
        public int MAUV { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string Hovatendem { get; set; }

        public string Ngaysinh { get; set; }

        public int? Gioitinh { get; set; }

        [StringLength(11)]
        public string Dienthoai1 { get; set; }

        [StringLength(11)]
        public string Dienthoai2 { get; set; }

        [StringLength(500)]
        public string EmailDN { get; set; }

        [StringLength(100)]
        public string EmailLH { get; set; }

        [StringLength(200)]
        public string Matkhau { get; set; }

        public string Hinhanh { get; set; }

        [StringLength(200)]
        public string Diachithuongtru { get; set; }

        [StringLength(200)]
        public string Diachihientai { get; set; }

        public int? Quocgia { get; set; }

        [StringLength(100)]
        public string Thanhpho { get; set; }

        [StringLength(100)]
        public string Quanhuyen { get; set; }
        public int? MaQH { get; set; }

        [StringLength(50)]
        public string SoCMND { get; set; }


        public int? Honnhan { get; set; }

        [StringLength(500)]
        public string FacebookUrl { get; set; }
        [AllowHtml]
        public string Muctieu { get; set; }

        public int? Tinhtrang { get; set; }

        public int? vaitro { get; set; }

        public int? Loaitaikhoan { get; set; }
        [StringLength(100)]
        public string Chucdanh { get; set; }
        public int? Namkinhnghiem { get; set; }
        public int? New { get; set; }
        [StringLength(200)]
        public string thanhpho2 { get; set; }

        [StringLength(200)]
        public string thanhpho3 { get; set; }

        public int? MaTP1 { get; set; }

        public int? MaTP2 { get; set; }

        public int? MaTP3 { get; set; }
        public IEnumerable<int> skills { get; set; }
        public int? language_0 { get; set; }
        public int? languagelevel_0 { get; set; }
        public int? language_1 { get; set; }
        public int? languagelevel_1 { get; set; }
        public int? language_2 { get; set; }
        public int? languagelevel_2 { get; set; }
        public int? language_3 { get; set; }
        public int? languagelevel_3 { get; set; }
        public int? resumeLanguageId { get; set; }
        public int? idnnuv { get; set; }
        public int entry_id { get; set; }
        public string position { get; set; }
        public string companyName { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int currentExp { get; set; }
        [AllowHtml]
        public string description { get; set; }
        public int? resumid { get; set; }
        public string school { get; set; }
        public int qualification { get; set; }
        public string subject { get; set; }
        public List<int> expectedLocation { get; set; }
        public List<int> expectedJobCategory { get; set; }
        public int expectedJobLevel { get; set; }
        public float expectedSalaryRange { get; set; }
    }
    public partial class data
    {
        public string action { get; set; }
        public string img_url { get; set; }
        public img_info img_info { get; set; }
    }
    public partial class img_info
    {
       public int h{get;set;}
        public int height{get;set;}
        public int w{get;set;}
        public int width { get; set; }
    }
    public class skill
    {
        public int skillId { get; set; }
        public string skillName{get;set;}
        public string status { get; set; }

    }
    public class expwork
    {
        public string description { get; set; }
        public string major { get; set; }
        public string schoolname { get; set; }
        public string companyname {get;set;}
        public DateTime? enddate { get; set; }
        public int entry_id { get; set; }
        public int experienceorder { get; set; }
        public int iscurrent { get; set; }
        public string jobtitle { get; set; }
        public DateTime lastdateupdated { get; set; }
        public DateTime? startdate { get; set; }
        public string workingCompanyName { get; set; }


    }
    public class expid
    {
        public int entryid { get; set; }
        public int educationorder { get; set; }
        public int experienceorder { get; set; }
    }
    public class complete
    {
        public string value { get; set; }
    }
    public class languagelv
    {
        public int? language { get; set; }
        public int? languagelve { get; set; }
    }
    public class Getvalue
    {
        internal static languagelv language1(int? l0, int? lv0, int? l1, int? lv1, int? l2, int? lv2, int? l3, int? lv3)
        {
            if (l0 != null)
            {
                var data = new languagelv{
                                 language=l0,
                                 languagelve=lv0
                             };
                return data;
            }
            else if (l1 != null)
            {
                var data = new languagelv
                {
                    language = l1,
                    languagelve = lv1
                };
                return data;
            }
            else if (l2 != null)
            {
                var data = new languagelv
                {
                    language = l2,
                    languagelve = lv2
                };
                return data;
            }
            else if (l3 != null)
            {
                var data = new languagelv
                {
                    language = l3,
                    languagelve = lv3
                };
                return data;
            }
            return null;
        }
    }
    public class checkresumid
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        public int check(int user)
        {
            Random rm = new Random();
            var resumid = rm.Next(1, 999999);
            if (db.ThongTinDaoTaos.Where(x => x.resumid == resumid && x.MaUV == user).FirstOrDefault() != null)
            {
                return check(user);
            }
            else
            {
                return resumid;
            }
        }
        public int check1(int user)
        {
            Random rm = new Random();
            var resumid = rm.Next(1, 999999);
            if (db.ThongTinNgheNghieps.Where(x => x.resumid == resumid && x.MaUV == user).FirstOrDefault() != null)
            {
                return check(user);
            }
            else
            {
                return resumid;
            }
        }
    }
}