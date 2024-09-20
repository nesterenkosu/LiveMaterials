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

namespace WindowsFormsApp_Client
{
    public partial class Form1 : Form
    {
        StreamWriter writer;
        StreamReader reader;

        UdpClient udp_client;
        TcpClient client;

        IPAddress server_address;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            udp_client = new UdpClient(8887);
        }

        private async void WaitForMessagesAsync()
        {
            string message;
            while (true)
            {
                message = await reader.ReadLineAsync();
                lb_server.Items.Add(message);
            }
        }

        public async void SendMessageAsync(string message)
        {
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToTcpServer(IPAddress.Parse(tb_address.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessageAsync(tb_message.Text);
            lb_client.Items.Add(tb_message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.Close();
            lb_client.Items.Add("Подключение разорвано");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BroadcastQueryForServer();
        }

        public async void BroadcastQueryForServer()
        {
            //Отправка широковещательного запроса для поиска сервера
            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast,7777);
            byte[] greet_server = Encoding.UTF8.GetBytes("HelloServer");
            await udp_client.SendAsync(
                greet_server,
                greet_server.Length,
                broadcast
            );

            lb_server.Items.Add("Отправка запроса на поиск сервера ...");

            //Ожидание ответа сервера на широковещательный запрос
            WaitForServerUdpResponse();
        }

        public async void WaitForServerUdpResponse()
        {
            UdpReceiveResult result;

            //Ожидание UDP-ответа сервера
            while (true)
            {
                result = await udp_client.ReceiveAsync();
                if (Encoding.UTF8.GetString(result.Buffer) == "HelloClient")
                    break;
            }

            server_address = result.RemoteEndPoint.Address;

            lb_server.Items.Add("Ответ сервера получен.");
            lb_server.Items.Add($"Ip-адрес сервера: [{server_address}]");

            //Подключение к серверу по протоколу TCP
            ConnectToTcpServer(server_address);
        }

        public void ConnectToTcpServer(IPAddress server_address)
        {
            client = new TcpClient();
            client.Connect(server_address, 8888);
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
            lb_client.Items.Add("Подключение к серверу установлено");

            WaitForMessagesAsync();
        }
    }
}
