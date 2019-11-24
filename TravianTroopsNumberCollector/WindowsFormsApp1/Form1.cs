using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser chrome;

        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            CefSettings settings = new CefSettings();
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.Initialize(settings);
            chrome = new ChromiumWebBrowser("http://google.com");
            //chrome.StatusMessage += Chrome_StatusMessage;
            //chrome.IsBrowserInitializedChanged += Chrome_IsBrowserInitializedChanged;
            //chrome.FrameLoadStart += Chrome_FrameLoadStart;
            //chrome.FrameLoadEnd += Chrome_FrameLoadEnd;
            //chrome.RegisterJsObject("object", new CallbackObjectForJs());
            this.panel1.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chrome.Load("www.kingdoms.com");

            //var script = string.Format("document.getElementsByTagName('body')");
            //if (color_index >= colors.Length)
            //{
            //    color_index = 0;
            //}

            //browser.GetMainFrame().ExecuteJavaScriptAsync(script);
        }
    }
}
