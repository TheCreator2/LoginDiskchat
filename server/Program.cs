using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the login thing");
            //create a http that listens on port 8080
            const int port = 8080;
            string prefix = $"http://localhost:{port}/";

            Console.WriteLine($"Listening {prefix}");
            HttpListener server = new HttpListener();
            server.Prefixes.Add(prefix);
            server.Start();
            

            
            bool running = true;
            int hitCount = 0;
            while (running)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
            
                string html = "";

                byte[] buffer = Encoding.UTF8.GetBytes(html);

                Console.WriteLine($"Request  '{request.RawUrl}'");

                if (request.HttpMethod == "POST")
                {
                    using(StreamReader r = new StreamReader(request.InputStream)){
                        string query = r.ReadToEnd();
                        Match m = Regex.match(query, "username=(.*)&password=(.*)");
                        if(m.sucess)
                        {
                            string username = m.Groups[1].value;
                            string username = m.Groups[2].value;

                            string html = $"";

                            if (username == "sam" && password == "password")
                            {
                                html = "login yes";
                            } else {
                                html = "login no";
                            }


                        }
                    }

                }

                string html = $"";
                byte[] buffer = Encoding.UTF8.GetBytes("");

                switch(request.RawUrl)
                {
                    case "/":
                        html = File.ReadAllText("../../static/index.html");
                         buffer = Encoding.UTF8.GetBytes(html);
                        response.ContentType = "text/html";
                        break;
                    default:
                        string path = "../../static" + request.RawUrl;
                        if(File.Exists(path))
                        {
                            buffer = File.ReadAllBytes(path);
                        } else
                        {
                            response.StatusCode = 404;
                            html = "sos m8 no file :(";
                            buffer = Encoding.UTF8.GetBytes(html);
                            Console.WriteLine($"Unkown URL: {request.RawUrl}");

                        }

                        Console.WriteLine($"Unknown URL: {request.RawUrl}");
                        break;
                }                                  

                Console.WriteLine($"Sending {buffer.Length} bytes");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0 , buffer.Length);
            }
            server.Stop();
        }
    }
}
