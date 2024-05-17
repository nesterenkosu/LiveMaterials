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

namespace WindowsFormsApp_TcpServer
{
    public partial class Form1 : Form
    {
        TcpListener tcpListener;
        TcpClient tcpClient;

        StreamReader reader;
        StreamWriter writer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpListener = new TcpListener(new IPEndPoint(IPAddress.Any, 8888));
            tcpListener.Start();
            lb_server.Items.Add("Сервер запущен");

            WaitForClientsToConnectAsync();
        }

        public async void WaitForClientsToConnectAsync()
        {
            while (true)
            {
                tcpClient = await tcpListener.AcceptTcpClientAsync();
                lb_client.Items.Add("Клиент подключен");

                WaitForMessagesAsync();
            }
        }

        public async void WaitForMessagesAsync()
        {
            string message;
            while (true)
            {
                reader = new StreamReader(tcpClient.GetStream());
                message = await reader.ReadLineAsync();
                lb_client.Items.Add(message);
            }
        }

        public async void SendMessagesAsync(string message)
        {
            writer = new StreamWriter(tcpClient.GetStream());
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessagesAsync(tb_message.Text);
        }
    }
    }
