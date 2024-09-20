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
        UdpClient client;
        IPEndPoint server_addr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void WaitForMessagesAsync()
        {
            UdpReceiveResult result;
            string message;
            while (true)
            {
                //Принятие данных от сервера
                result = await client.ReceiveAsync();
                message = Encoding.UTF8.GetString(result.Buffer);

                //Вывод принятых данных на экран
                lb_server.Items.Add(message);
            }
        }

        public async void SendMessageAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            //Отправка данных серверу
            await client.SendAsync(data, data.Length, server_addr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Создание канала для принятия данных
            client = new UdpClient(8887);

            //Формирование адреса для подключения к серверу
            server_addr = new IPEndPoint(IPAddress.Parse(tb_address.Text),8888);

            lb_client.Items.Add("Параметры соединения установлены");

            //Переход в режим ожидания сообщений
            WaitForMessagesAsync();
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

        //Нажатие на кнопку "Широковещательное подключение"
        private void button4_Click(object sender, EventArgs e)
        {
            //Создание канала для принятия данных
            client = new UdpClient(8887);

            //Формирование адреса для подключения к серверу
            //автоматическое определение широковещательного адреса
            server_addr = new IPEndPoint(IPAddress.Broadcast, 8888);

            lb_client.Items.Add("Установлены параметры для широковещательного подключения");
            lb_client.Items.Add($"Используется широковещательный адрес [{server_addr.ToString()}]");

            //Переход в режим ожидания сообщений
            WaitForMessagesAsync();
        }
    }
}
