using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny2_Vow_of_the_Disciple.lib
{
    public class cmd
    {
        public void Start()
        {
            if (File.Exists("root.bat"))
            {
                string bat = "";

                using (StreamReader sr = new StreamReader(
                    "root.bat", Encoding.GetEncoding("UTF-8")))
                {
                    bat = sr.ReadToEnd();
                }

                bat = bat.Replace("{AppDir}", System.Reflection.Assembly.GetEntryAssembly().Location);

                System.Text.Encoding enc = new System.Text.UTF8Encoding(false);
                using (StreamWriter writer = new StreamWriter("root.bat", false, enc))
                {
                    writer.WriteLine(bat);
                }


                Process proc = new Process();
                proc.StartInfo.FileName = @"root.bat";

                proc.StartInfo.Verb = "RunAs";

                proc.Start();
                proc.WaitForExit();
                proc.Close();

                File.Delete("root.bat");
            }
        }
    }
}
