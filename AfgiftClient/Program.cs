using System;
using System.IO;
using System.Net.Sockets;

namespace AfgiftClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // client
            string Ip = "127.0.0.1";
            TcpClient clientSocket = new TcpClient(Ip, 7000);
            Console.WriteLine("Client ready");

            Stream ns = clientSocket.GetStream();  //provides a Stream
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Hvad koster din bil?");
                string bilPris = Console.ReadLine();
                sw.WriteLine(bilPris);
                string serverAnswer = sr.ReadLine();
                Console.WriteLine("Hvilken type bil har du? El eller benzin?");
                string bilType = Console.ReadLine();

                sw.WriteLine(bilType);
                sw.WriteLine();
                serverAnswer = sr.ReadLine();
                //serverAnswer = sr.ReadLine();
                Console.WriteLine("Bil afgift: " + serverAnswer + " kr.");

                Console.WriteLine("No more from server. Press Enter");
                Console.ReadLine();
            }
            ns.Close();
            clientSocket.Close();
        }
    }
}
