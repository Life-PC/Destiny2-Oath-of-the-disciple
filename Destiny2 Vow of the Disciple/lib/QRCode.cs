using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Vow_of_the_Disciple.lib
{
    public class QRCode
    {
        public void Create(string textCode, PaintEventArgs e)
        {
            DotNetBarcode QR = new DotNetBarcode();
            QR.Type = DotNetBarcode.Types.QRCode;
            QR.PrintChar = true;

            textCode = "http://" + textCode + "/index.html";

            QR.WriteBar(textCode, 0, 0, 306, 274, e.Graphics);
        }
    }
}
