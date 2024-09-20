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

namespace WindowsFormsApp_Server
{
    public partial class Form1 : Form
    {
       
        //Центральный объект для осуществления передачи данных
        //по протоколу UDP
        UdpClient server;

        //Связка из ip-адреса и порта
        IPEndPoint client_endpoint;

        public Form1()
        {
            InitializeComponent();

            //Создание серверного объекта UdpClient
            //и связывание с ним порта 8888
            server = new UdpClient(8888);           

            lb_server.Items.Add("Сервер запущен "+Dns.GetHostName());

            GetServerIpsAsync();
            WaitForMessagesAsync();
        }

        public async void GetServerIpsAsync()
        {
            lb_server.Items.Add("Доступные адреса для подключений:");
            IPAddress[] ips = await Dns.GetHostAddressesAsync(Dns.GetHostName());
            foreach (var ip in ips.Where(i => i.AddressFamily == AddressFamily.InterNetwork))
                lb_server.Items.Add(ip);
        }

        /*public async void WaitForClients()
        {
            while(true)
            {
                client=await tcpListenter.AcceptTcpClientAsync();
                reader=new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                lb_client.Items.Add("Клиент подключился");
                WaitForMessagesAsync();
            }
        }*/

        public async void WaitForMessagesAsync()
        {//Ожидание данных от клиентов
            
            UdpReceiveResult result;
            string message;
            while (true)
            {
                //ожидание данных
                result = await server.ReceiveAsync();
                
                lb_client.Items.Add($"Сообщение от клиента с адресом [{result.RemoteEndPoint}]");

                //получение ip-адреса клиента
                client_endpoint = result.RemoteEndPoint;

                //получение и раскодирование отправленной
                //клиентом строки
                message = Encoding.UTF8.GetString(result.Buffer);

                //вывод принятой строки на экран
                lb_client.Items.Add(message);


            }
        }

        public async void SendMessageAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            //Отправка данных клиенту
            await server.SendAsync(data, data.Length, client_endpoint);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SendMessageAsync(tb_message.Text);
            lb_server.Items.Add(tb_message.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
