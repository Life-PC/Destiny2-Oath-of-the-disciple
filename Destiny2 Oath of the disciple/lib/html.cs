using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Oath_of_the_disciple.lib
{
    public class html
    {
        public string ipAddress;

        public html()
        {
            string hostname = Dns.GetHostName();
            IPAddress[] ips = Dns.GetHostAddresses(hostname);

            foreach (IPAddress a in ips)
            {
                if (a.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    ipAddress = a.ToString();
                    break;
                }
            }
        }

        public void create()
        {
            string htmlText = "";

            using (StreamReader sr = new StreamReader(
                "wwwroot\\temp.html", Encoding.GetEncoding("UTF-8")))
            {
                htmlText = sr.ReadToEnd();
            }

            htmlText = htmlText.Replace("{ipAddress}", ipAddress);

            System.Text.Encoding enc = new System.Text.UTF8Encoding(false);
            using (StreamWriter writer = new StreamWriter("wwwroot\\index.html", false, enc))
            {
                writer.WriteLine(htmlText);
            }
        }
    }
}
