using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CSharp___Client1
{
    class Program
    {
        static int port = 3003;
        static string address = "127.0.0.1";

        static void Main(string[] args)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipEndPoint);

            Random rnd = new Random();

            while (true)
            {
                int number = rnd.Next(0, 100);
                byte[] data = Encoding.Unicode.GetBytes(number.ToString());
                int time = rnd.Next(1, 30);

                Console.WriteLine("Send: " + number);
                Console.WriteLine("Sleep: " + time + " sec.");

                socket.Send(data);

                Thread.Sleep(time * 1000);
            }
        }
    }
}
