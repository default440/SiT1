using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CSharp___Server
{
    class Program
    {
        static int port = 3003;
        static string address = "127.0.0.1";

        static Socket sListener;

        static Queue<int> buffer1 = new Queue<int>();
        static Queue<int> buffer2 = new Queue<int>();

        static void Main(string[] args)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);

            sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sListener.Bind(ipEndPoint);
            sListener.Listen(10);

            Thread receiver1 = new Thread(new ThreadStart(Receiver1));
            Thread receiver2 = new Thread(new ThreadStart(Receiver2));

            receiver1.Start();
            receiver2.Start();

            while (true)
            {
                if (buffer1.Count > 0 && buffer2.Count > 0)
                {
                    Console.WriteLine(buffer1.Dequeue() + buffer2.Dequeue());
                }

                Thread.Sleep(10);
            }
        }

        static void Receiver1()
        {
            Socket handler = sListener.Accept();

            Console.WriteLine("RCV1 - connected");

            while (true)
            {
                byte[] message = new byte[4];
                int length = handler.Receive(message);
                int number = Convert.ToInt32(Encoding.Unicode.GetString(message, 0, length));

                Console.WriteLine("RCV1 - Receive: " + number);

                buffer1.Enqueue(number);

                Thread.Sleep(10);
            }
        }

        static void Receiver2()
        {
            Socket handler = sListener.Accept();

            Console.WriteLine("RCV2 - connected");

            while (true)
            {
                byte[] message = new byte[4];
                int length = handler.Receive(message);
                int number = Convert.ToInt32(Encoding.Unicode.GetString(message, 0, length));

                Console.WriteLine("RCV2 - Receive: " + number);

                buffer2.Enqueue(number);

                Thread.Sleep(10);
            }
        }
    }
}
