using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Destiny2_Oath_of_the_disciple.lib
{
    public class httpServer
    {
        bool exitCount = true;
        HttpListener listener;
        public void start()
        {
            string root = @"wwwroot\";
            string prefix = "http://+:80/";

            listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();

            exitCount = true;

            while (exitCount)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest req = context.Request;
                    HttpListenerResponse res = context.Response;

                    if (req.HttpMethod != "GET")
                    {
                        res.Close();
                        continue;
                    }

                    string path = root + req.RawUrl.Replace("/", "\\");

                    if (File.Exists(path))
                    {
                        byte[] content = File.ReadAllBytes(path);
                        res.OutputStream.Write(content, 0, content.Length);
                    }
                    res.Close();
                }
                catch
                {

                }
            }
        }

        public void exit()
        {
            exitCount = false;

            if (listener != null)
                listener.Stop();
        }
    }
}
