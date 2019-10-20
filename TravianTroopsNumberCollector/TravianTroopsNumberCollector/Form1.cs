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
using CefSharp;
using CefSharp.WinForms;

namespace TravianTroopsNumberCollector
{
    public partial class Form1 : Form
    {
        List<User> Users;
        string operation = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CefSharp.WinForms.WebView wb = new WebView();           
            wb.Dock = DockStyle.Fill;
            wb.Address = "www.google.com";
            this.Controls.Add(wb);

            Users = new List<User> { new User() { UserName = "umityilmaz1099@gmail.com", Password = "23yKn3Ps168" } };
            //Users listesi verilerini txt dosyasından alacak. Şu anda geçici olarak veri atandı.

            foreach (var item in Users)
            {
                OpenKingdoms();
                //LoginKingdoms();
            }

        }

        void OpenKingdoms()
        {
            operation = "OpenPage";
            wb.Navigate("http://www.kingdoms.com");
        }

        void LoginKingdoms()
        {
            operation = "LoginPage";
            wb_DocumentCompleted(wb, null);
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (operation == "OpenPage")
            //{
            //    HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");

            //    foreach (HtmlElement button in buttons)
            //    {
            //        string text = button.InnerText;

            //        if (text == "Giriş")
            //        {
            //            button.InvokeMember("Click");
            //            break;
            //        }
            //    }
            //}

            //if (operation == "LoginPage")
            //{
            //    HtmlElementCollection inputs = wb.Document.GetElementsByTagName("input");

            //    foreach (HtmlElement input in inputs)
            //    {
            //        string text = button.InnerText;

            //        if (text == "Giriş")
            //        {
            //            button.InvokeMember("Click");
            //            break;
            //        }
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (operation == "OpenPage")
            {
                HtmlElementCollection buttons = wb.Document.GetElementsByTagName("a");

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (operation == "OpenPage")
            {
                HtmlElementCollection inputs = wb.Document.GetElementsByTagName("input");

                foreach (HtmlElement input in inputs)
                {
                    string text = input.GetAttribute("name");

                    if (text == "email")
                    {
                        input.SetAttribute("value", "deneme@deneme.com");
                        break;
                    }
                }
            }
        }



        //private void btnGo_Click(object sender, EventArgs e)
        //{
        //    firstLogin = true;
        //}

        //private void btnGetHtmlData_Click(object sender, EventArgs e)
        //{
        //    string content = wb.Document.Body.InnerHtml;
        //    Clipboard.SetText(content);
        //}

        //bool firstLogin = false;
        //bool epostaBuldu = false;
        //bool sifreBuldu = false;
        //HtmlElement loginButton = null;

        //bool profileLogin = false;
        //HtmlElement profileButton = null;

        //private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    if (firstLogin == true && e.Url.ToString().Contains("www.facebook.com"))
        //    {
        //        HtmlElementCollection inputs = wb.Document.Body.GetElementsByTagName("input");
        //        foreach (HtmlElement input in inputs)
        //        {
        //            string attribute = input.GetAttribute("id");
        //            if (!string.IsNullOrEmpty(attribute) && attribute == "email")
        //            {
        //                input.SetAttribute("value", "umityilmaz9999@gmail.com");
        //                epostaBuldu = true;
        //            }

        //            if (!string.IsNullOrEmpty(attribute) && attribute == "pass")
        //            {
        //                input.SetAttribute("value", "3m3l2014");
        //                sifreBuldu = true;
        //            }

        //            attribute = input.GetAttribute("value");
        //            if (!string.IsNullOrEmpty(attribute) && attribute == "Giriş Yap")
        //            {
        //                //input.InvokeMember("Click");
        //                loginButton = input;
        //            }
        //        }
        //    }
        //    else if (firstLogin && epostaBuldu && sifreBuldu && loginButton != null)
        //    {
        //        //sıradaki işlem buraya yazılabilir.
        //    }
        //    else if (e.Url.ToString().Contains("www.facebook.com/kullaniciAdi"))
        //    {
        //        //profiline gitmiştir.
        //    }

        //    if (profileLogin && e.Url.ToString().Contains("www.facebook.com"))
        //    {
        //        HtmlElementCollection spans = wb.Document.GetElementsByTagName("span");

        //        foreach (HtmlElement span in spans)
        //        {
        //            if (span.InnerText == "Ümit")
        //            {
        //                profileButton = span;
        //                break;
        //            }
        //        }
        //    }
        //}

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    if (epostaBuldu && sifreBuldu && loginButton != null)
        //    {
        //        loginButton.InvokeMember("Click");
        //    }
        //}

        //private void btnProfile_Click(object sender, EventArgs e)
        //{
        //    wb_DocumentCompleted(wb, null);

        //    if (profileButton != null)
        //    {
        //        profileButton.InvokeMember("Click");
        //    }
        //    profileLogin = true;
        //}

    }
}
