using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the login thing");
            //create a http that listens on port 8080
            const int port = 8080;
            string prefix = $"http://localoast:{port}";

            Console.WriteLine($"Listening {prefix}");
            HttpListener server = new HttpListener();
            server.Prefixes.Add(prefix);
            server.Start();
            HttpListenerContext context = server.GetContext();

            
            bool running = true;
            int hitCount = 0;
            while (running)
            {
            
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
            
                string html = $"Ahhhhhhhhhhh you are request number {hitCount}".html;
                hitCount++;

                byte[] buffer = Encoding.UTF8.GetBytes(html);

                Console.WriteLine($"Request  '{request.RawUrl}'");

                switch(request.RawUrl)
                {
                    case "/":
                        html = File.ReadAllText("../../static/index.html");
                            break;
                    default:
                        string path = "../../static" + request.RawUrl;
                        if(File.Exists(path))
                        {
                            html = File.ReadAllBytes(path);
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
}

                Console.WriteLine($"Sending {buffer.Length} bytes");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0 , buffer.Length);
            }
            server.Stop();
        }
    }
}
