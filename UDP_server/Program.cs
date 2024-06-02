using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UDP_server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var brodcastAddress = IPAddress.Parse("224.0.0.0");//локальный хост для отправки данных 
            var udpSender = new UdpClient();
            Console.WriteLine("Начало отправки сообщений...");

            TimerCallback tm = new TimerCallback(UpdateTime);
            Timer timer = new Timer(tm, null, 0, 1000);
            Console.ReadKey();

            async void UpdateTime(object obj)
            {         
                string str = DateTime.Now.ToLongTimeString();
                await udpSender.SendAsync(Encoding.Default.GetBytes(str), 8, new IPEndPoint(brodcastAddress, 5555));//отправка UDP_client консоль
                await udpSender.SendAsync(Encoding.Default.GetBytes(str), 8, new IPEndPoint(brodcastAddress, 2222));//отправка WF_UDP_client
                Console.WriteLine(str);
            }
        }


    }
}
