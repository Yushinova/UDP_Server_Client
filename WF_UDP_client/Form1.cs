using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_UDP_client
{
    public partial class Form1 : Form
    {
        UdpClient udpClient = new UdpClient(2222);//порт для подключения
        IPAddress brodcastAddress = IPAddress.Parse("224.0.0.0");// хост для отправки данных 
        string message = string.Empty;
        public Form1()
        {
            InitializeComponent();
            udpClient.JoinMulticastGroup(brodcastAddress);
            Task.Run(Go);
        }
        private async Task Go()
        {
            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                message = Encoding.UTF8.GetString(result.Buffer);
                Invoke(new Action(()=>LabelTime.Text = message));
            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            udpClient.DropMulticastGroup(brodcastAddress);
        }
    }
}
