using Skat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace AfgiftServer
{
    class AfgiftService
    {
        private TcpClient _connectionSocket;
        public AfgiftService(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;
        }

        public void Doit()
        {
            Stream ns = _connectionSocket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer;

            while (message != "")
            {
                int pris = Int32.Parse(message);
                Console.WriteLine("Bilens pris: " + pris + " kr.");
                sw.WriteLine(pris);

                string bilType = sr.ReadLine();
                Console.WriteLine("Bilens type: " + bilType);
                if (bilType == "benzin")
                {
                    answer = Convert.ToString(Afgift.BilAfgift(pris));
                    Console.WriteLine("Bilens Afgift " + answer + " kr.");
                    
                    sw.WriteLine(answer);

                }
                else if (bilType == "el")
                {
                    answer = Convert.ToString(Afgift.ElBilAfgift(pris));
                    Console.WriteLine("Bilens Afgift " + answer + " kr.");                    
                    sw.WriteLine(answer);
                    
                }
                else
                {
                    answer = "Fejl";
                    sw.WriteLine(answer);
                }

            }

            ns.Close();
            _connectionSocket.Close();
        }
    }
}
