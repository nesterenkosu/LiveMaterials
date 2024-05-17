using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Подключить для работы с сетями
using System.Net;
using System.Net.Sockets;
using ToByteFromByte;

using System.Runtime.InteropServices;

namespace ConsoleApp_SocketTcpServer
{
    class Program
    {
        struct chess_move
        {
            public int x;
            public int y;
            public int figure;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string user_name;
        }

        static void Main(string[] args)
        {
            //Создание сокета - объекта, реализующего сетевое соединение
            Socket mysocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Задание ip-адреса, по которому сервер будет прослушивать подключения
            //IPAddress.Any - прослушивать все адреса, доступные на данном компьютере
            var ipaddress = IPAddress.Any;

            //5.79.226.239 - разрешить подключение только с указанного ip
            //var ipaddress = new IPAddress(new byte[] { 5, 79, 226, 239 });

            //Создание конечной точки, увязывающей ip-адрес и порт
            var endpoint = new IPEndPoint(ipaddress, 8888);

            //Привязка конечной точки к сокету
            mysocket.Bind(endpoint);

            //Запуск сервера
            mysocket.Listen(1000);

            Console.WriteLine("Сервер запущен. Ожидание подключений клиентов");

            //Ожидание подключения клиента
            var client = mysocket.Accept();

            Console.WriteLine("Клиент подключен");

            //Связывание с клиентом потока - специального объекта
            //с помощью которого осуществляется принятие и отправка данных
            var stream = new NetworkStream(client);

            int bytes; //Количество принятых из сети байт
            var responseData = new byte[512]; //собственно принятые данные

            chess_move user_move;

            //принятие из сети двоичных данных
            bytes = stream.Read(responseData, 0, Marshal.SizeOf(typeof(chess_move)));
            
            //преобразование данных из двоичной формы в структуру chess_move
            user_move = Converter.fromBytes<chess_move>(responseData);

            //печать принятых данных на консоль
            Console.WriteLine("Игрок сделал следующий ход:");
            Console.WriteLine($"Имя пользователя=[{user_move.user_name}] Фигура=[{user_move.figure}] x=[{user_move.x}] y=[{user_move.y}]");


            Console.WriteLine("Приём данных завершён");
            Console.ReadLine();
        }

        
    }
}
