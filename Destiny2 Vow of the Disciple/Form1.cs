using Destiny2_Vow_of_the_Disciple.lib;
using Destiny2_Vow_of_the_Disciple.lib.hook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Destiny2_Vow_of_the_Disciple
{
    public partial class Form1 : Form
    {
        private bool _bScreenMode;
        Thread thread,httpserver;
        Client client;
        httpServer http;
        private MyClipboardViewer viewer;

        public Form1()
        {
            viewer = new MyClipboardViewer(this);
            viewer.ClipboardHandler += this.OnClipBoardChanged;

            InitializeComponent();
        }


        // クリップボードにテキストがコピーされると呼び出される
        private void OnClipBoardChanged(object sender, ClipboardEventArgs args)
        {
            Console.WriteLine(args.Text);
        }

        private void keyboardHook1_KeyboardHooked(object sender, KeyboardHookedEventArgs e)
        {
            if (Keys.PageUp != e.KeyCode)
                return;

            if (e.UpDown == KeyboardUpDown.Down)
                return;

            if (_bScreenMode)
            {
                this.WindowState = FormWindowState.Maximized;

                _bScreenMode = false;

                this.Activate();
                this.TopMost = true;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.TopMost = false;

                _bScreenMode = true;
            }

        }

        private void icon_Click(object sender, EventArgs e)
        {
            Button iconButton = (Button)((Control)sender);

            this.WindowState = FormWindowState.Minimized;
            this.TopMost = false;

            _bScreenMode = true;

            SendKeys.Send("{ENTER}");
            SendKeys.Send(iconButton.Text);
            SendKeys.Send("{ENTER}");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _bScreenMode = false;

            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            _bScreenMode = false;

            this.Activate();
            this.TopMost = true;

            client = new Client();
            http = new httpServer();
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Exit();
            http.exit();
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Exit();
            http.exit();
        }

        private void スマートフォンサーバー起動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.TopMost = false;

            _bScreenMode = true;

            cmd cmd = new cmd();
            cmd.Start();

            client.Exit();
            http.exit();

            thread = new Thread(client.Run);
            thread.Start();

            httpserver = new Thread(http.start);
            httpserver.Start();

            html html = new html();
            html.create();

            form.QRCodeForm qr = new form.QRCodeForm(html.ipAddress);
            qr.ShowDialog();


        }
    }
}
