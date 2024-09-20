using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using ToByteFromByte;

namespace WindowsFormsApp_Server
{
    public struct Message
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string login;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string message;
    }
    public partial class Form1 : Form
    {
        StreamReader reader;
        StreamWriter writer;

        UdpClient udp_server;

        TcpListener tcpListenter;
        TcpClient client;

        List<TcpClient> clients = new List<TcpClient>();

        IPAddress server_addr, client_addr;

        public Form1()
        {
            InitializeComponent();

            udp_server = new UdpClient(7777);
            WaitForClientUdpRequest();

            tcpListenter = new TcpListener(IPAddress.Any, 8888);
            tcpListenter.Start();

            lb_server.Items.Add("Сервер запущен " + Dns.GetHostName());
            GetServerIpsAsync();

            WaitForClients();
        }

        public async void GetServerIpsAsync()
        {
            lb_server.Items.Add("Доступные адреса для подключений:");
            IPAddress[] ips = await Dns.GetHostAddressesAsync(Dns.GetHostName());
            foreach (var ip in ips.Where(i => i.AddressFamily == AddressFamily.InterNetwork))
                lb_server.Items.Add(ip);
        }

        public async void WaitForClients()
        {
            while (true)
            {
                client = await tcpListenter.AcceptTcpClientAsync();
                clients.Add(client);
                
                lb_client.Items.Add("Подключился ещё один клиент");
                WaitForMessagesAsync(client.GetStream());
            }
        }

        public async void WaitForMessagesAsync(NetworkStream stream)
        {
            byte[] bytes = new byte[512];
            Message msg;
            while (true)
            {
                await stream.ReadAsync(bytes, 0, Marshal.SizeOf(typeof(Message)));

                msg = Converter.fromBytes<Message>(bytes);

                lb_client.Items.Add($"Сообщение от клиента [{msg.login}]");
            }
        }

        public async void SendMessageAsync(string message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SendMessageAsync(tb_message.Text);
            lb_server.Items.Add(tb_message.Text);
        }

        public async void WaitForClientUdpRequest()
        {
            //Ожидание широковещательных UDP-запросов клиента, 
            //выполняющего поиск сервера
            UdpReceiveResult result;
            while (true)
            {
                result = await udp_server.ReceiveAsync();
                if (Encoding.UTF8.GetString(result.Buffer) == "HelloServer")
                    break;

            }
            client_addr = result.RemoteEndPoint.Address;

            lb_server.Items.Add("Получен запрос от клиента");

            //Отправка клиенту UDP-ответа, содержащего 
            //ip-адрес сервера
            SendServerIPToClient();
        }

        public async void SendServerIPToClient() {
            byte[] greet_server = Encoding.UTF8.GetBytes("HelloClient");
            await udp_server.SendAsync(
                greet_server,
                greet_server.Length,
                new IPEndPoint(client_addr,8887)
            );
            lb_server.Items.Add("Клиенту отправлен ip-адрес");
        }
    }
}
