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
                words.Add("à", "a");
                words.Add("á", "b");
                words.Add("â", "v");
                words.Add("ã", "g");
                words.Add("ä", "d");
                words.Add("å", "e");
                words.Add("¸", "yo");
                words.Add("æ", "zh");
                words.Add("ç", "z");
                words.Add("è", "i");
                words.Add("é", "j");
                words.Add("ê", "k");
                words.Add("ë", "l");
                words.Add("ì", "m");
                words.Add("í", "n");
                words.Add("î", "o");
                words.Add("ï", "p");
                words.Add("ð", "r");
                words.Add("ñ", "s");
                words.Add("ò", "t");
                words.Add("ó", "u");
                words.Add("ô", "f");
                words.Add("õ", "h");
                words.Add("ö", "c");
                words.Add("÷", "ch");
                words.Add("ø", "sh");
                words.Add("ù", "sch");
                words.Add("ú", "j");
                words.Add("û", "i");
                words.Add("ü", "j");
                words.Add("ý", "e");
                words.Add("þ", "yu");
                words.Add("ÿ", "ya");
                words.Add("À", "A");
                words.Add("Á", "B");
                words.Add("Â", "V");
                words.Add("Ã", "G");
                words.Add("Ä", "D");
                words.Add("Å", "E");
                words.Add("¨", "Yo");
                words.Add("Æ", "Zh");
                words.Add("Ç", "Z");
                words.Add("È", "I");
                words.Add("É", "J");
                words.Add("Ê", "K");
                words.Add("Ë", "L");
                words.Add("Ì", "M");
                words.Add("Í", "N");
                words.Add("Î", "O");
                words.Add("Ï", "P");
                words.Add("Ð", "R");
                words.Add("Ñ", "S");
                words.Add("Ò", "T");
                words.Add("Ó", "U");
                words.Add("Ô", "F");
                words.Add("Õ", "H");
                words.Add("Ö", "C");
                words.Add("×", "Ch");
                words.Add("Ø", "Sh");
                words.Add("Ù", "Sch");
                words.Add("Ú", "J");
                words.Add("Û", "I");
                words.Add("Ü", "J");
                words.Add("Ý", "E");
                words.Add("Þ", "Yu");
                words.Add("ß", "Ya");

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
