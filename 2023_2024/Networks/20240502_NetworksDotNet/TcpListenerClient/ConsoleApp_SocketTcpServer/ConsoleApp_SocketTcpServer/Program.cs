using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Подключить для работы с сетями
using System.Net;
using System.Net.Sockets;
//Подключить для работы с StreamReader и StreamWriter
using System.IO;

namespace ConsoleApp_SocketTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener;
            TcpClient client;

            //Задание ip-адреса, по которому сервер будет прослушивать подключения
            //IPAddress.Any - прослушивать все адреса, доступные на данном компьютере
            var ipaddress = IPAddress.Any;

            //5.79.226.239 - разрешить подключение только с указанного ip
            //var ipaddress = new IPAddress(new byte[] { 5, 79, 226, 239 });

            //Создание конечной точки, увязывающей ip-адрес и порт
            var endpoint = new IPEndPoint(ipaddress, 8888);

            listener = new TcpListener(endpoint);
            listener.Start();            

            Console.WriteLine("Сервер запущен. Ожидание подключений клиентов");

            //Ожидание подключения клиента
            client = listener.AcceptTcpClient();

            Console.WriteLine("Клиент подключен");

            //Связывание с клиентом потока - специального объекта
            //с помощью которого осуществляется принятие и отправка данных
            var stream = new StreamReader(client.GetStream());

            
            string message; //принятые данные, преобразованные в строку
            while (true)
            {
                message = stream.ReadLine();
                //печать принятых данных на консоль
                Console.WriteLine(message);

                if (message == "Q") break;
            }

            Console.WriteLine("Приём данных завершён");
            Console.ReadLine();
        }
    }
}
