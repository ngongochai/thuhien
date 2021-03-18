using Model.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model.Setup
{
    public class LoginMember
    {
        public thuctaptotnghiepDbContext Context = null;
        public LoginMember()
        {
            Context= new thuctaptotnghiepDbContext();
        }
        public int Login(string userName, string password)
        {
            Object[] sqlparams =
            {
                new SqlParameter("@username",userName),
                new SqlParameter("@password",password),
            };
            var result = Context.Database.SqlQuery<int>("exec sp_checkloginuser @username,@password", sqlparams).SingleOrDefault();
            return result;
        }
        public int LoginEmployer(string userName, string password)
        {
            Object[] sqlparams =
            {
                new SqlParameter("@username",userName),
                new SqlParameter("@password",password),
            };
            var result = Context.Database.SqlQuery<int>("exec sp_checkloginemployer @username,@password", sqlparams).SingleOrDefault();
            return result;
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        public static string trinhdo(int? matd)
        {
            if (matd == 1)
            {
                return "Sơ cấp";
            }
            if (matd == 2)
            {
                return "Trung cấp";

            }
            if (matd == 3)
            {
                return "Cao cấp";
            }
            if (matd == 4)
            {
                return "Bản ngữ";

            }
            else
            {
                return null;
            }
        }

    }
}
