﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            firstLogin = true;
            wb.Navigate(txtUrl.Text);
        }

        private void btnGetHtmlData_Click(object sender, EventArgs e)
        {
            string content = wb.Document.Body.InnerHtml;
            Clipboard.SetText(content);
        }

        bool firstLogin = false;
        bool epostaBuldu = false;
        bool sifreBuldu = false;
        HtmlElement loginButton = null;

        bool profileLogin = false;
        HtmlElement profileButton = null;

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (firstLogin == true && e.Url.ToString().Contains("www.facebook.com"))
            {
                HtmlElementCollection inputs = wb.Document.Body.GetElementsByTagName("input");
                foreach (HtmlElement input in inputs)
                {
                    string attribute = input.GetAttribute("id");
                    if (!string.IsNullOrEmpty(attribute) && attribute == "email")
                    {
                        input.SetAttribute("value", "umityilmaz9999@gmail.com");
                        epostaBuldu = true;
                    }

                    if (!string.IsNullOrEmpty(attribute) && attribute == "pass")
                    {
                        input.SetAttribute("value", "3m3l2014");
                        sifreBuldu = true;
                    }

                    attribute = input.GetAttribute("value");
                    if (!string.IsNullOrEmpty(attribute) && attribute == "Giriş Yap")
                    {
                        //input.InvokeMember("Click");
                        loginButton = input;
                    }
                }
            }
            else if (firstLogin && epostaBuldu && sifreBuldu && loginButton != null)
            {
                //sıradaki işlem buraya yazılabilir.
            }
            else if (e.Url.ToString().Contains("www.facebook.com/kullaniciAdi"))
            {
                //profiline gitmiştir.
            }

            if (profileLogin && e.Url.ToString().Contains("www.facebook.com"))
            {
                HtmlElementCollection spans = wb.Document.GetElementsByTagName("span");

                foreach (HtmlElement span in spans)
                {
                    if (span.InnerText == "Ümit")
                    {
                        profileButton = span;
                        break;
                    }
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (epostaBuldu && sifreBuldu && loginButton != null)
            {
                loginButton.InvokeMember("Click");
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            wb_DocumentCompleted(wb, null);

            if (profileButton != null)
            {
                profileButton.InvokeMember("Click");
            }
            profileLogin = true;
        }
    }
}
