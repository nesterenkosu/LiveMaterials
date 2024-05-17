
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp_SocketUdpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient server = new UdpClient(8888);       

            Console.WriteLine("UDP-сервер запущен. Ожидание подключения клиента...");

            //Получение данных по протоколу UDP
            //65535 - максимальный размер сообщения, которое можно передать по протоколу UDP
            byte[] data = new byte[255]; //Буфер для принимаемых данных
            IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0); //Переменная, в которую будет помещён адрес клиента
            data = server.Receive(ref remoteIp);

            //Преобразование принятых данных из безтипового потока байт в тип string
            string message = Encoding.UTF8.GetString(data,0,data.Length);

            Console.WriteLine($"Подключен клиент с адресом [{remoteIp}]");
            Console.WriteLine($"Принято сообщение [{message}]");

            Console.ReadLine();
        }
    }
}
