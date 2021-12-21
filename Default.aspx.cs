using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TRANSLIT
{
    public partial class _Default : System.Web.UI.Page
    {
        private Multilang ml = new Multilang();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.Encoding cp = System.Text.Encoding.GetEncoding(1251);            
            string codepage = Request["codepage"];
            if(codepage != "")
            {
                if (codepage == "1251") cp = System.Text.Encoding.GetEncoding(1251);

                // DOS
                if (codepage == "866") cp = System.Text.Encoding.GetEncoding(866);
                if (codepage == "cp866") cp = System.Text.Encoding.GetEncoding(866);
                // UTF
                if (codepage == "utf") cp = System.Text.Encoding.UTF8;
                if (codepage == "unicode") cp = System.Text.Encoding.Unicode;
                // KOI8-R
                if (codepage == "koi8-r") cp = System.Text.Encoding.GetEncoding(20866);
                if (codepage == "cp20866") cp = System.Text.Encoding.GetEncoding(20866);
            };

            string url = Request["url"];
            if (url != "")
            {
                System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                System.Net.HttpWebResponse wres = (System.Net.HttpWebResponse)wr.GetResponse();
                System.IO.Stream rs = wres.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(rs,cp);
                Response.ContentType = wres.ContentType;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    line = ml.Translit(line);
                    Response.Write(line+"\r\n");
                };
            };
        }

        [Serializable]
        public class Multilang
        {
            private Dictionary<string, string> words = new Dictionary<string, string>();

            private string en = "";
            private string ru = "";

            public string EN
            {
                get { return en; }
                set
                {
                    en = System.Web.HttpUtility.HtmlEncode(value);
                }
            }

            public string RU
            {
                get { return ru; }
                set
                {
                    ru = System.Web.HttpUtility.HtmlEncode(value);
                    if ((en == null) || (en == String.Empty) || (en.Length == 0)) en = Translit(ru);
                }
            }

            private void InitDict()
            {
                words.Add("�", "a");
                words.Add("�", "b");
                words.Add("�", "v");
                words.Add("�", "g");
                words.Add("�", "d");
                words.Add("�", "e");
                words.Add("�", "yo");
                words.Add("�", "zh");
                words.Add("�", "z");
                words.Add("�", "i");
                words.Add("�", "j");
                words.Add("�", "k");
                words.Add("�", "l");
                words.Add("�", "m");
                words.Add("�", "n");
                words.Add("�", "o");
                words.Add("�", "p");
                words.Add("�", "r");
                words.Add("�", "s");
                words.Add("�", "t");
                words.Add("�", "u");
                words.Add("�", "f");
                words.Add("�", "h");
                words.Add("�", "c");
                words.Add("�", "ch");
                words.Add("�", "sh");
                words.Add("�", "sch");
                words.Add("�", "j");
                words.Add("�", "i");
                words.Add("�", "j");
                words.Add("�", "e");
                words.Add("�", "yu");
                words.Add("�", "ya");
                words.Add("�", "A");
                words.Add("�", "B");
                words.Add("�", "V");
                words.Add("�", "G");
                words.Add("�", "D");
                words.Add("�", "E");
                words.Add("�", "Yo");
                words.Add("�", "Zh");
                words.Add("�", "Z");
                words.Add("�", "I");
                words.Add("�", "J");
                words.Add("�", "K");
                words.Add("�", "L");
                words.Add("�", "M");
                words.Add("�", "N");
                words.Add("�", "O");
                words.Add("�", "P");
                words.Add("�", "R");
                words.Add("�", "S");
                words.Add("�", "T");
                words.Add("�", "U");
                words.Add("�", "F");
                words.Add("�", "H");
                words.Add("�", "C");
                words.Add("�", "Ch");
                words.Add("�", "Sh");
                words.Add("�", "Sch");
                words.Add("�", "J");
                words.Add("�", "I");
                words.Add("�", "J");
                words.Add("�", "E");
                words.Add("�", "Yu");
                words.Add("�", "Ya");

            }

            public string Translit(string RU)
            {
                string EN = RU;
                foreach (KeyValuePair<string, string> pair in words)
                    EN = EN.Replace(pair.Key, pair.Value);
                return EN;
            }

            public Multilang()
            {
                InitDict();
            }

            public Multilang(string EN, string RU)
            {
                InitDict();
                this.EN = EN;
                this.RU = RU;
            }
        }

    }
}
