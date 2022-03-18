using Destiny2_Vow_of_the_Disciple.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Vow_of_the_Disciple.form
{
    public partial class QRCodeForm : Form
    {
        string IPADDREESS;
        public QRCodeForm(string ip)
        {
            InitializeComponent();
            IPADDREESS = ip;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            QRCode qr = new QRCode();
            qr.Create(IPADDREESS, e);
            label1.Text = "スマートフォンから下記のアドレスに接続してください。\rまたこの画面を表示する場合は「Page Up」キーを押してください。\r\rhttp://" + IPADDREESS + "/index.html";
            this.Activate();

        }
    }
}
