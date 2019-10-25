using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AfgiftServer
{
    class Program
    {
        static void Main(string[] args)
        {

            // server
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 7000);
            serverSocket.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated");
                AfgiftService service = new AfgiftService(connectionSocket);

                Task.Factory.StartNew((() => service.Doit()));

            }
            serverSocket.Stop();

        }
    }
}
