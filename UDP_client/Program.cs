using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection.Emit;
using System.Data.Common;

namespace UDP_client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var udpClient = new UdpClient(5555);
            var brodcastAddress = IPAddress.Parse("224.0.0.0");// хост для отправки данных 
            string message = string.Empty;                                                     
            udpClient.JoinMulticastGroup(brodcastAddress);// присоединяемся к группе
            Console.WriteLine("Начало прослушивания сообщений");

            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                message = Encoding.UTF8.GetString(result.Buffer);
                // if (message == "END") break;

                Console.WriteLine(message);
            }
        }

    }
}
