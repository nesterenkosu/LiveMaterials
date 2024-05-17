using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp_SocketUdpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient(8887);

            string ip_as_string, message;
            byte[] data = new byte[255];

            IPEndPoint server_endpoint;
            
            Console.Write("Введите ip-адрес сервера: ");
            ip_as_string = Console.ReadLine();

            Console.Write("Введите сообщение для отправки: ");
            message = Console.ReadLine();

            server_endpoint = new IPEndPoint(IPAddress.Parse(ip_as_string),8888);

            //Преобразование сообщения в формате string в поток байт 
            //(преобразовали текстовые данные в двоичные)
            data = Encoding.UTF8.GetBytes(message);

            //Отправка данных по протоколу UDP
            client.Send(data, data.Length, server_endpoint);

            Console.WriteLine("Данные отправлены");

            Console.ReadLine();
        }
    }
}
