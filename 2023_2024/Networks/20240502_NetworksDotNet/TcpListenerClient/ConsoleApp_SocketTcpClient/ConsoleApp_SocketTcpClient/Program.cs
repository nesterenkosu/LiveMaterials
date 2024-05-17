using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using System.IO;

namespace ConsoleApp_SocketTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client;

            string ip_as_string;
            IPAddress ip;

            Console.WriteLine("Введите ip-адрес сервера");
            ip_as_string = Console.ReadLine();

            try
            {
                ip = IPAddress.Parse(ip_as_string);
            }catch(Exception ex)
            {
                Console.WriteLine("Ошибка в формате ip-адреса");
                Console.ReadLine();
                return;
            }

            client = new TcpClient();
            client.Connect(new IPEndPoint(ip, 8888));

            Console.WriteLine("Подключение выполнено.");
            Console.Write("Введите сообщение для отправки: ");
            string message = Console.ReadLine();

            var stream = new StreamWriter(client.GetStream());

            // отправляем массив байт на сервер 
            stream.WriteLine(message);
            stream.Flush();

            Console.WriteLine("Сообщение отправлено");
            

            Console.ReadLine();
        }
    }
}
