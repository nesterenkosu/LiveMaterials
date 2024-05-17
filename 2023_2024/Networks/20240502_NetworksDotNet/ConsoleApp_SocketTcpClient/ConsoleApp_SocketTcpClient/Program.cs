using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using System.Runtime.InteropServices;


namespace ConsoleApp_SocketTcpClient
{
    struct chess_move
    {
        public int x;
        public int y;
        public int figure;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string user_name;
    }

    class Program
    {
        static void Main(string[] args)
        {
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

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(ip, 8888));

            Console.WriteLine("Подключение выполнено.");

            chess_move my_move;

            Console.Write("Введите ваше имя:");
            my_move.user_name = Console.ReadLine();

            Console.WriteLine("Ваш ход. Укажите фигуру и координаты поля");            
            my_move.figure = Convert.ToInt32(Console.ReadLine());
            my_move.x = Convert.ToInt32(Console.ReadLine());
            my_move.y = Convert.ToInt32(Console.ReadLine());

            /*Console.Write("Введите сообщение для отправки: ");
            string message = Console.ReadLine();*/

            var stream = new NetworkStream(socket);

            byte[] my_move_bytes=getBytes<chess_move>(my_move);

            // отправляем массив байт на сервер 
            stream.Write(my_move_bytes, 0, my_move_bytes.Length);

            Console.WriteLine("Сообщение отправлено");
            

            Console.ReadLine();
        }

        static byte[] getBytes<T>(T str)
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(str, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return arr;
        }
    }
}
