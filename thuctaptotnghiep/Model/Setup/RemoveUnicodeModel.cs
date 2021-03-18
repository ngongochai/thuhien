using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.Setup
{
    public class RemoveUnicodeModel
    {
        public string RemoveUnicodeRemoveUnicode(string str)
        {
            if (str == null)
            {
                str = "";
            }
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public string UnicodeNameModel(string name)
        {
            if (name == null)
                name = "";
            string phrase = name;
            string str = RemoveUnicodeRemoveUnicode(phrase).ToLower();
            str = RemoveAccent(str).ToLower();
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            str = rgx.Replace(str, " ");
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 200 ? str.Length : 200).Trim();
            str = Regex.Replace(str, @"\s", " "); // hyphens   
            return str;
        }
        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
