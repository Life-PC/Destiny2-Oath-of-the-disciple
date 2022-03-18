using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Destiny2_Vow_of_the_Disciple.lib
{
    public class Client
    {

        bool exitCount = true;
        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        public void Run()
        {

            int port = 8080;
            server = new TcpListener(IPAddress.Any, port);

            server.Start();

            try
            {
                client = server.AcceptTcpClient();
            }
            catch
            {
                return;
            }

            stream = client.GetStream();

            exitCount = true;

            while (exitCount)
            {
                try
                {
                    while (!stream.DataAvailable) ;
                    while (client.Available < 3) ;

                    byte[] bytes = new byte[client.Available];
                    stream.Read(bytes, 0, client.Available);
                    string s = Encoding.UTF8.GetString(bytes);

                    if (Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase))
                    {

                        string swk = Regex.Match(s, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                        string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                        byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
                        string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                        byte[] response = Encoding.UTF8.GetBytes(
                            "HTTP/1.1 101 Switching Protocols\r\n" +
                            "Connection: Upgrade\r\n" +
                            "Upgrade: websocket\r\n" +
                            "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

                        stream.Write(response, 0, response.Length);
                    }
                    else
                    {
                        bool fin = (bytes[0] & 0b10000000) != 0,
                            mask = (bytes[1] & 0b10000000) != 0;

                        int opcode = bytes[0] & 0b00001111,
                            msglen = bytes[1] - 128,
                            offset = 2;

                        if (msglen == 126)
                        {
                            msglen = BitConverter.ToUInt16(new byte[] { bytes[3], bytes[2] }, 0);
                            offset = 4;
                        }
                        else if (msglen == 127)
                        {
                            // msglen = BitConverter.ToUInt64(new byte[] { bytes[5], bytes[4], bytes[3], bytes[2], bytes[9], bytes[8], bytes[7], bytes[6] }, 0);
                            //offset = 10;
                        }

                        if (msglen == 0)
                        {

                        }
                        else if (mask)
                        {
                            byte[] decoded = new byte[msglen];
                            byte[] masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
                            offset += 4;

                            for (int i = 0; i < msglen; ++i)
                                decoded[i] = (byte)(bytes[offset + i] ^ masks[i % 4]);

                            string text = Encoding.UTF8.GetString(decoded);
                            text = text.Replace("\r\n", "");
                            text = text.Replace("\r", "");
                            text = text.Replace("\n", "");


                            TextCheck textCheck = new TextCheck();
                            textCheck.checkAndSend(text);

                        }
                    }
                }
                catch
                {

                }
            }

            server.Stop();
            client.Close();
            stream.Close();
        }

        public void Exit()
        {
            exitCount = false;

            if (client != null)
                client.Close();
            if (stream != null)
                stream.Close();
            if (server != null)
                server.Stop();
        }
    }
}
