
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
            //Конечная точка (endpoint) - связка из ip-адреса и порта 
            IPEndPoint myendpoint = new IPEndPoint(IPAddress.Any, 8888);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(myendpoint);

            Console.WriteLine("UDP-сервер запущен. Ожидание подключения клиента...");

            //Получение данных по протоколу UDP
            //65535 - максимальный размер сообщения, которое можно передать по протоколу UDP
            byte[] data = new byte[255]; //Буфер для принимаемых данных
            EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0); //Переменная, в которую будет помещён адрес клиента
            int data_length=socket.ReceiveFrom(data, ref remoteIp);

            //Преобразование принятых данных из безтипового потока байт в тип string
            string message = Encoding.UTF8.GetString(data,0,data_length);

            Console.WriteLine($"Подключен клиент с адресом [{remoteIp}]");
            Console.WriteLine($"Принято сообщение [{message}]");

            Console.ReadLine();
        }
    }
}
