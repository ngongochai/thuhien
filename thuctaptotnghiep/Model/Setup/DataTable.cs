using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using System.Globalization;

namespace Model.Setup
{
    public class DataTable
    {
        thuctaptotnghiepDbContext db = new thuctaptotnghiepDbContext();
        RemoveUnicodeModel unicode = new RemoveUnicodeModel();

        public class ListJob
        {
            public string ID { get; set; }
            public string JobName { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Salary { get; set; }
            public string Company { get; set; }
            public string LogoCompany { get; set; }
            public string Datecreate { get; set; }
            public string TopJob { get; set; }
            public string Homepage { get; set; }
            public string Status { get; set; }
            public int MACV { get; set; }
            public string TenCV { get; set; }
            public int? Tinhtrang { get; set; }
            public int? type { get; set; }
            public string UVDK { get; set; }
            public int? mact { get; set; }
            public DateTime? Ngaydang { get; set; }
        }
        public class ListEmployer
        {
            public string ID { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string NameContact { get; set; }
            public string LogoCompany { get; set; }
            public string DateCreate { get; set; }
            public string NumberJob { get; set; }
            public string TopEmployer { get; set; }
            public string Status { get; set; }
            public DateTime? Ngaydangky { get; set; }
            public int? Sovieclam { get; set; }
            public string tencongty { get; set; }
            public int? tinhtrang { get; set; }
        }
        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<ListJob> data { get; set; }
          
        }
        public class DataTableData1
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<ListEmployer> data { get; set; }
        }
        public List<ListJob> FilterDataJob(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection, string minday, string maxday, int fiterjob)
        {
            List<DangTinViecLam> list = new List<DangTinViecLam>();
            var nghanhnghe = db.NghanhNghes.ToList();
            var city = db.cities.ToList();
            var company = db.CongTies.ToList();
            var listjob = db.DangTinViecLams.ToList();
            if (fiterjob <= 1 && fiterjob >= 0)
            {
                listjob = listjob.Where(x => x.Tinhtrang == fiterjob).ToList();
            }
            else if(fiterjob!=-1)
            {
                if (fiterjob == 2)
                {
                    listjob = listjob.Where(x => x.Trangthai == 0).ToList();
                }
                else if(fiterjob==3)
                {
                    listjob = listjob.Where(x => x.Trangthai == 1).ToList();
                }
                else if (fiterjob == 4)
                {
                    listjob=listjob.Where(x => x.Tinhtrang == -1).ToList();// việc làm đã xóa
                }
                else if (fiterjob == 5)
                {
                    listjob = listjob.Where(x => x.Tinhtrang == -2).ToList();//việc làm đã hết hạn
                }
            }
            

            DateTime startday;
            bool trya = DateTime.TryParseExact(minday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startday);
            DateTime endday;
            bool tryb = DateTime.TryParseExact(maxday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endday);
            if (!string.IsNullOrEmpty(minday) && !string.IsNullOrEmpty(maxday) && trya != null && tryb != null)
            {
                listjob = listjob.Where(x => x.Ngaydang.Value.Date >= startday.Date && x.Ngaydang.Value.Date <= endday.Date).ToList();
            }
            if (string.IsNullOrEmpty(minday) && !string.IsNullOrEmpty(maxday) && tryb == true)
            {
                listjob = listjob.Where(x => x.Ngaydang.Value.Date <= endday.Date).ToList();
            }
            if (!string.IsNullOrEmpty(minday) && string.IsNullOrEmpty(maxday) && trya == true)
            {
                listjob = listjob.Where(x => x.Ngaydang.Value.Date >= startday.Date).ToList();
            }
            var listdate = listjob;
            if (string.IsNullOrEmpty(search))
            {
                list = listdate;
            }
            else
            {
                search = unicode.UnicodeNameModel(search.Trim());
                foreach (var item in listdate)
                {
                    string thoathuan;
                    if (item.Luongthoathuan == 1)
                    {
                        thoathuan = "thoa thuan";
                    }
                    else
                    {
                        thoathuan = item.Minluong + " - " + item.Maxluong;
                        thoathuan = unicode.UnicodeNameModel(thoathuan);
                    }
                    string cat1 = nghanhnghe.Where(x => x.Id == item.Manghanh1).Select(x => x.Tennghanh).FirstOrDefault();
                    string cat2 = nghanhnghe.Where(x => x.Id == item.Manghanh2).Select(x => x.Tennghanh).FirstOrDefault();
                    string cat3 = nghanhnghe.Where(x => x.Id == item.Manghanh3).Select(x => x.Tennghanh).FirstOrDefault();
                    string city1 = city.Where(x => x.Id == item.MaTP1).Select(x => x.City1).FirstOrDefault();
                    string city2 = city.Where(x => x.Id == item.MaTP2).Select(x => x.City1).FirstOrDefault();
                    string city3 = city.Where(x => x.Id == item.MaTP3).Select(x => x.City1).FirstOrDefault();
                    string comp = item.CongTy.Tencongty;
                    if (unicode.UnicodeNameModel(item.TenCV).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.MACV.ToString()).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.Ngaydang.Value.ToString("dd/MM/yyyy")).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(comp).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(cat1).Contains(search) || unicode.UnicodeNameModel(cat2).Contains(search) || unicode.UnicodeNameModel(cat3).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(city1).Contains(search) || unicode.UnicodeNameModel(city2).Contains(search) || unicode.UnicodeNameModel(city3).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (thoathuan.Contains(search))
                    {
                        list.Add(item);
                    }
                }
            }

            var listdatatable = new List<ListJob>();
            foreach (var item in list)
            {
                string thoathuan;
                if (item.Luongthoathuan == 1)
                {
                    thoathuan = "Thỏa Thuận";
                }
                else
                {
                    thoathuan = item.Minluong + " - " + item.Maxluong;
                }
                string img;
                if (item.CongTy.Logo == null)
                {
                    img = "<img style='width:100%' src='/Employer_upload/noimgcompany.png' />";
                }
                else
                {
                    img = "<a href='/admin/ManagerMemberAdmin/EditEmployer?mact=" + item.MACT + "'><img style='width:100%' src='/Employer_upload/" + item.CongTy.Logo + "' /></a>";
                }
                string topjob;
                if (item.Vieclamtotnhat == 1)
                {
                    topjob = "<label class='custom-control custom-checkbox'>Show<input hidden type='checkbox' checked value='1' id='" + item.MACV + "' class='topjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                else
                {
                    topjob = "<label class='custom-control custom-checkbox'>Show<input type='checkbox' hidden value='1' id='" + item.MACV + "' class='topjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                string hot;
                if (item.hot == 1)
                {
                    hot = "<label class='custom-control custom-checkbox'>Hot<input hidden type='checkbox' checked value='1' id='" + item.MACV + "' class='hotjob custom-control-input'><span class='custom-control-indicator-hot'></span></label>";
                }
                else
                {
                    hot = "<label class='custom-control custom-checkbox'>Hot<input hidden type='checkbox' value='1' id='" + item.MACV + "' class='hotjob custom-control-input'><span class='custom-control-indicator-hot'></span></label>";
                }
                string homepage;
                if (item.Vieclamgoiy == 1)
                {
                    homepage = "<label class='custom-control custom-checkbox'>Show<input checked type='checkbox' hidden value='1' id='" + item.MACV + "' class='showjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                else
                {
                    homepage = "<label class='custom-control custom-checkbox'>Show<input type='checkbox' hidden value='1' id='" + item.MACV + "' class='showjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                string status;
                if (item.Tinhtrang == 1)
                {
                    status = "<label class='custom-control custom-checkbox'>Kích hoạt<input checked type='checkbox' hidden value='1' id='active_" + item.MACV + "' class='activejob custom-control-input'><span class='custom-control-indicator-active'></span></label>";
                }
                else
                {
                    status = "<label class='custom-control custom-checkbox'>Kích hoạt<input type='checkbox' hidden value='1' id='active_" + item.MACV + "' class='activejob custom-control-input'><span class='custom-control-indicator-active'></span></label>";
                }
                string status1;
                if (item.Trangthai == 1)
                {
                    status1 = "<label class='custom-control custom-checkbox'>Ẩn<input checked type='checkbox' hidden value='1' id='status_" + item.MACV + "' class='statusjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                else
                {
                    status1 = "<label class='custom-control custom-checkbox'>Ẩn<input type='checkbox' hidden value='1' id='status_" + item.MACV + "' class='statusjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                string cat1 = nghanhnghe.Where(x => x.Id == item.Manghanh1).Select(x => x.Tennghanh).FirstOrDefault();
                string cat2 = nghanhnghe.Where(x => x.Id == item.Manghanh2).Select(x => x.Tennghanh).FirstOrDefault();
                string cat3 = nghanhnghe.Where(x => x.Id == item.Manghanh3).Select(x => x.Tennghanh).FirstOrDefault();
                string city1 = city.Where(x => x.Id == item.MaTP1).Select(x => x.City1).FirstOrDefault();
                string city2 = city.Where(x => x.Id == item.MaTP2).Select(x => x.City1).FirstOrDefault();
                string city3 = city.Where(x => x.Id == item.MaTP3).Select(x => x.City1).FirstOrDefault();
                string comp = item.CongTy.Tencongty;
                ListJob data = new ListJob();
                data.ID = item.MACV + "<br /><input type='checkbox' class='checkchild' value='" + item.MACV + "' />";
                data.JobName = "<a href='/Admin/ManagerJobAdmin/EditJobAdmin/" + item.MACV + "'>" + item.TenCV + "</a>";
                data.Category = cat1 + " " + cat2 + " " + cat3;
                data.City = city1 + " " + city2 + " " + city3;
                data.Company = "<a href='/admin/ManagerMemberAdmin/EditEmployer?mact=" + item.MACT + "'>" + comp + "</a>";
                data.Salary = "<span>" + thoathuan + "</span>";
                data.LogoCompany = img;
                data.Datecreate = item.Ngaydang.Value.ToString("dd/MM/yyyy");
                data.TopJob = topjob + hot;
                data.Homepage = homepage;
                data.Status = status + status1;
                data.UVDK = "<a href='/admin/ManagerMemberAdmin/ACEmployer?mact=" + item.MACT + "'><h3>" + db.UVDangKies.Where(x=>x.MACV==item.MACV&&x.Tinhtrang==1).Count() + "</h3></a>";
                data.Tinhtrang = item.Tinhtrang;
                data.mact = item.MACT;
                data.MACV = item.MACV;
                data.TenCV = item.TenCV;
                data.type = item.Type;
                data.Ngaydang = item.Ngaydang;
                listdatatable.Add(data);
            }

            // simulate sort
            if (sortColumn == 0)
            {// sort ID
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.MACV).ToList() : listdatatable.OrderByDescending(x => x.MACV).ToList();

            }
            else if (sortColumn == 1)
            {// sort TÊN CÔNG VIỆC
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.TenCV).ToList() : listdatatable.OrderByDescending(x => x.TenCV).ToList();

            }
            else if (sortColumn == 7)
            {// NGày đăng
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.Ngaydang).ToList() : listdatatable.OrderByDescending(x => x.Ngaydang).ToList();
            }

            //recordFiltered = listdatatable.Count;

            //// get just one page of data
            //listdatatable = listdatatable.GetRange(start, Math.Min(length, listdatatable.Count - start));
            return listdatatable;
        }
        public List<ListEmployer> FilterDataEmployer(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection, string minday, string maxday)
        {
            List<CongTy> list = new List<CongTy>();
            var listemployer = db.CongTies.ToList();

            DateTime startday;
            bool trya = DateTime.TryParseExact(minday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startday);
            DateTime endday;
            bool tryb = DateTime.TryParseExact(maxday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endday);
            if (!string.IsNullOrEmpty(minday) && !string.IsNullOrEmpty(maxday) && trya != null && tryb != null)
            {
                listemployer = listemployer.Where(x => x.Ngaytao.Value.Date >= startday.Date && x.Ngaytao.Value.Date <= endday.Date).ToList();
            }
            if (string.IsNullOrEmpty(minday) && !string.IsNullOrEmpty(maxday) && tryb == true)
            {
                listemployer = listemployer.Where(x => x.Ngaytao.Value.Date <= endday.Date).ToList();
            }
            if (!string.IsNullOrEmpty(minday) && string.IsNullOrEmpty(maxday) && trya == true)
            {
                listemployer = listemployer.Where(x => x.Ngaytao.Value.Date >= startday.Date).ToList();
            }
            var listdate = listemployer;
            if (string.IsNullOrEmpty(search))
            {
                list = listdate;
            }
            else
            {
                search = unicode.UnicodeNameModel(search.Trim());
                foreach (var item in listdate)
                {
                    if (unicode.UnicodeNameModel(item.Tencongty).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.MACT.ToString()).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (item.Ngaytao!=null&&unicode.UnicodeNameModel(item.Ngaytao.Value.ToString("dd/MM/yyyy")).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.Diachi).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.Dienthoaiban).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.Emaildangnhap).Contains(search))
                    {
                        list.Add(item);
                    }
                    else if (unicode.UnicodeNameModel(item.Nguoilienhe).Contains(search))
                    {
                        list.Add(item);
                    }
                }
            }

            var listdatatable = new List<ListEmployer>();
            foreach (var item in list)
            {
                ListEmployer data = new ListEmployer();
                data.ID = item.MACT.ToString();
                data.Company = "<a href='/admin/ManagerMemberAdmin/EditEmployer?mact=" + item.MACT + "'>" + item.Tencongty + "</a>";
                data.Address = "<span>" + item.Diachi + "</span>";
                data.Phone = "<span>" + item.Dienthoaiban + "</span>";
                data.Email = "<span>" + item.Emaildangnhap + "</span>";
                data.NameContact = "<span>" + item.Nguoilienhe + "</span>";
                if (item.Logo == null)
                {
                    data.LogoCompany = "<img style='width:60%' src='/Employer_upload/noimgcompany.png' />";
                }
                else
                {
                    data.LogoCompany = "<a href='/admin/ManagerMemberAdmin/EditEmployer?mact=" + item.MACT + "'><img style='width:100%' src='/Employer_upload/" + item.Logo + "' /></a>";
                }
                if (item.Ngaytao != null)
                {
                    data.DateCreate = " <span>" + item.Ngaytao.Value.ToString("dd/MM/yyyy HH:mm") + "</span> ";
                }
                data.Sovieclam = db.DangTinViecLams.Where(x => x.MACT == item.MACT).Count();
                data.NumberJob = "<span style='font-size:25px;color:blue;font-weight:bold;'><a href='/admin/ManagerMemberAdmin/ListJobForEmployer/" + item.MACT + "'>" + data.Sovieclam + "</a></span>";
                if (item.Toptuyendung == true)
                {
                    data.TopEmployer = "<label class='custom-control custom-checkbox'><span>Show</span><br /><input checked value='-1' hidden type='checkbox' id='top_" + item.MACT + "' class='toptd custom-control-input'><span class='custom-control-indicator-hot'></span></label>";
                }
                else
                {
                    data.TopEmployer = "<label class='custom-control custom-checkbox'><span>Show</span><br /><input value='-1' hidden type='checkbox' id='top_" + item.MACT + "' class='toptd custom-control-input'><span class='custom-control-indicator-hot'></span></label>";
                }
                string status;
                string delete;
                if (item.Tinhtrang == 1)
                {
                    status = "<label class='custom-control custom-checkbox'><span>Kích Hoạt</span><br /><input checked value='0' hidden type='checkbox' id='status_" + item.MACT + "' class='statusjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                else
                {
                    status = "<label class='custom-control custom-checkbox'><span>Kích Hoạt</span><br /><input value='1' hidden type='checkbox' id='status_" + item.MACT + "' class='statusjob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                if (item.Tinhtrang != -1)
                {
                    delete = "<label class='custom-control custom-checkbox'><span>Xóa</span><br /><input value='-1' hidden type='checkbox' id='delete_" + item.MACT + "' class='deletejob custom-control-input'><span class='custom-control-indicator'></span></label>";
                }
                else
                {
                    delete = "";
                }
                data.Status = status + delete;
                data.Ngaydangky = item.Ngaytao;
                data.tencongty = item.Tencongty;
                data.tinhtrang = item.Tinhtrang;
                listdatatable.Add(data);
            }

            // simulate sort
            if (sortColumn == 0)
            {// sort ID
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.ID).ToList() : listdatatable.OrderByDescending(x => x.ID).ToList();

            }
            else if (sortColumn == 1)
            {// sort TÊN CÔNG ty
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.tencongty).ToList() : listdatatable.OrderByDescending(x => x.tencongty).ToList();

            }
            else if (sortColumn == 7)
            {// NGày đăng
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.Ngaydangky).ToList() : listdatatable.OrderByDescending(x => x.Ngaydangky).ToList();
            }
            else if (sortColumn == 8)
            {// số việc làm
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.Sovieclam).ToList() : listdatatable.OrderByDescending(x => x.Sovieclam).ToList();
            }
            else if (sortColumn == 9)
            {
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.TopEmployer).ToList() : listdatatable.OrderByDescending(x => x.TopEmployer).ToList();
            }
            else if (sortColumn == 10)
            {
                listdatatable = (sortDirection == "asc") ? listdatatable.OrderBy(x => x.tinhtrang).ToList() : listdatatable.OrderByDescending(x => x.tinhtrang).ToList();
            }
            //recordFiltered = listdatatable.Count;

            //// get just one page of data
            //listdatatable = listdatatable.GetRange(start, Math.Min(length, listdatatable.Count - start));
            return listdatatable;
        }
    }
}
