using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using System.Net;
using System.Web;
using System.IO;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        List<User> Users = new List<User> { new User() { UserName = "umityilmaz1099@gmail.com", Password = "23yKn3Ps168" } };
        string operation = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var appName = Process.GetCurrentProcess().ProcessName + ".exe";
            //SetIE11KeyforWebBrowserControl(appName);

            //Users listesi verilerini txt dosyasından alacak. Şu anda geçici olarak veri atandı.
        }

        private void SetIE11KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {
                // For 64 bit machine
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                else  //For 32 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                // If the path is not correct or
                // if the user haven't priviledges to access the registry
                if (Regkey == null)
                {
                    MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                // Check if key is already present
                if (FindAppkey == "11001")
                {
                    MessageBox.Show("Required Application Settings Present");
                    Regkey.Close();
                    return;
                }

                // If a key is not present add the key, Key value 8000 (decimal)
                if (string.IsNullOrEmpty(FindAppkey))
                    Regkey.SetValue(appName, unchecked((int)0x2AF9), RegistryValueKind.DWord);

                // Check for the key after adding
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                if (FindAppkey == "11001")
                    MessageBox.Show("Application Settings Applied Successfully");
                else
                    MessageBox.Show("Application Settings Failed, Ref: " + FindAppkey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Settings Failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the Registry
                if (Regkey != null)
                    Regkey.Close();
            }
        }

        void OpenKingdomsWebPage()
        {
            operation = "OpenKingdomsWebPage";
            wb.Navigate("https://mellon-t5.traviangames.com/easyXDM/proxy.html?timestamp=1571834743179&amp;urn=%2Fauthentication%2Flogin%2FapplicationDomain%2Fwww.kingdoms.com%2FapplicationPath%2F%252F%2FapplicationInGame%2F0%2FapplicationId%2Ftravian-ks%2FapplicationCountryId%2Ftr%2FapplicationInstanceId%2Fportal-tr%2FapplicationLanguageId%2Ftr_TR%2FapplicationCookieEnabled%2F1%3Fmsname%3Dmsid%26msid%3Dner39fdp3b4jv6ff4fe0qdfsc7&amp;xdm_e=https%3A%2F%2Fwww.kingdoms.com&amp;xdm_c=default6769&amp;xdm_p=1");
        }

        void LoginKingdoms()
        {
            operation = "LoginKingdoms";
            wb_DocumentCompleted(wb, null);
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (operation == "OpenKingdomsWebPage")
            {
                HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");

                foreach (HtmlElement button in buttons)
                {
                    string text = button.InnerText;

                    if (text == "Giriş")
                    {
                        button.InvokeMember("Click");
                        break;
                    }
                }
            }

            if (operation == "LoginKingdoms")
            {
                //string jCode = "var iframe = document.getElementsByTagName('iframe')[3]; var innerDoc = iframe.contentWindow.document; innerDoc.documentElement.innerHTML";
                //object html = wb.Document.InvokeScript("eval", new object[] { jCode });

                //mshtml.HTMLDocument doc = (mshtml.HTMLDocument)wb.Document.DomDocument;
                //object index = 2;
                //mshtml.IHTMLWindow2 frame = (mshtml.IHTMLWindow2)doc.frames.item(ref index);
                //doc = (mshtml.HTMLDocument)frame.document;
                //string a = doc.documentElement.innerHTML;

                //HttpWebResponse deneme = SendDataToService();

                //var stream = deneme.GetResponseStream();
                //HtmlDocument doc = new HtmlDocument();

                //doc.Load(stream);

                //using (var client = new WebClient())
                //{
                //    string html = client.DownloadString("http://www.kingdoms.com");
                //}

                HtmlElement div = wb.Document.GetElementById("mellonModal").Children[0].Children[0].Document.Body;

                //HtmlWindowCollection iframes = wb.Document.Window.Frames;

                //HtmlWindow iframe = wb.Document.Window.Frames[2];

                //HtmlElement element = iframe.Document.GetElementsByTagName("input")[0];

                //var frame = wb.Document.Window.Frames[2];



                //string htmlElement = wb.Document.Window.Frames[0].Document.Body.OuterHtml;

                //HtmlElement div2 = div.Children[0].Children[0];

                //HTMLFrameBase deneme = div.DomElement as HTMLFrameBase;

                //HtmlElement element = deneme[2].Document.Body;



                //foreach (HtmlElement input in inputs)
                //{
                //    string text = input.InnerText;

                //    if (text == "Giriş")
                //    {
                //        button.InvokeMember("Click");
                //        break;
                //    }
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenKingdomsWebPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginKingdoms();
        }

        protected void AppendParameter(StringBuilder sb, string name, string value)
        {
            string encodedValue = HttpUtility.UrlEncode(value);
            sb.AppendFormat("{0}={1}&", name, encodedValue);
        }

        private HttpWebResponse SendDataToService()
        {
            StringBuilder sb = new StringBuilder();
            AppendParameter(sb, "email", "umityilmaz1099@gmail.com");
            AppendParameter(sb, "password", "23yKn3Ps168");


            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());

            string url = "http://kingdoms.com/"; //or: check where the form goes

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.Credentials = CredentialCache.DefaultNetworkCredentials; // ??

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // do something with response

            return response;
        }
    }
}
