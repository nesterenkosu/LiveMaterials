using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace TcpServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            Dictionary<string,string> postParams = new Dictionary<string, string>();
            Dictionary<string, string> getParams = new Dictionary<string, string>();

            string url = "";

            byte[] RequestBuffer = new byte[1024];
            byte[] ResponceBuffer = new byte[1024];

            string responce_headers, responce_body, responce, _start_line="";
            string request, _body, __get_params;
            string[] _headers_body, _headers, _key_val, _post_params, _get_params;

            bool first_line = true;

            

            //Запуск сервера
            TcpListener server = new TcpListener(IPAddress.Any, 8787);
            server.Start();

            for (; ; )
            {
                first_line = true;

                //Ожидание подключения клиентов
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("Ожидание подключения...");

                Console.ResetColor();

                //Ожидание подключения клиентов
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Клиент подключен");

                requestHeaders.Clear();

                //Принятие запроса от клиента
                client.GetStream().Read(RequestBuffer, 0, RequestBuffer.Length);
                request = Encoding.ASCII.GetString(RequestBuffer);

                //Console.WriteLine(request);
                TUI.VisualizeText("Содержимое запроса",request);

                if (request.Trim().Trim('\0') == "") {
                    client.Close();
                    continue; 
                }

                //Console.ReadLine();

                //Парсинг заголовков запроса, и их помещение в массив
                _headers_body= request.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);                
                _headers = _headers_body[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach(string _header in _headers)
                {
                    //Пропуск стартовой строки
                    if (first_line) { 
                        first_line = false;
                        _start_line = _header;
                        continue; 
                    }
                    
                    if (_header.Trim() == "") continue;

                    //Разбивка заголовка по символу ":" на имя заголовка и значение заголовка
                    _key_val = _header.Split(new string[] { ":" }, StringSplitOptions.None);

                    //if (_key_val[0].Trim() == "" || _key_val[1].Trim() == "") continue;

                    //Добавление извлечённого заголовка в ассоциативный массив
                    requestHeaders.Add(_key_val[0],_key_val[1]);
                }

                //Парсинг полей формы (POST-параметров)
                if(_headers_body[1].Trim().Trim('\0')!="")
                { //если тело запроса не пустое
                    _body = _headers_body[1];
                    _post_params = _body.Split(new string[] { "&" }, StringSplitOptions.None);
                    foreach(string _post_param in _post_params)
                    {
                        _key_val = _post_param.Split(new string[] { "=" }, StringSplitOptions.None);
                        postParams.Add(_key_val[0], _key_val[1]);
                    }
                }

                //Парсинг GET-параметров и адресной строки
                Match ReqMatch = Regex.Match(_start_line, @"^\w+\s+([^\s\?]+)\??([^\s]*)\s+HTTP/.*|");
                url = ReqMatch.Groups[1].Value;
                if (ReqMatch.Groups.Count >= 2 && ReqMatch.Groups[2].Value.Trim()!="") {
                    __get_params = ReqMatch.Groups[2].Value;
                    _get_params = __get_params.Split(new string[] { "&" }, StringSplitOptions.None);
                    foreach (string _get_param in _get_params)
                    {
                        _key_val = _get_param.Split(new string[] { "=" }, StringSplitOptions.None);
                        getParams.Add(_key_val[0], _key_val[1]);
                    }
                }

                TUI.VisualizeArray("Результат парсинга заголовков", requestHeaders);
                TUI.VisualizeArray("Результат парсинга POST-параметров", postParams);
                TUI.VisualizeArray("Результат парсинга GET-параметров", getParams);
                TUI.VisualizeText("Запрошен адрес",url);

                //Формирование ответа клиенту
                responce_body = "<h1>Hello from Server!</h1>";

                responce_headers = "HTTP/1.1 200 OK\n" +
                                   "Content-type: text/html\n" +
                                   "Content-Length: " + responce_body.Length + "\n\n";

                responce = responce_headers + responce_body;

                ResponceBuffer = Encoding.ASCII.GetBytes(responce);

                //Отправка ответа клиенту
                client.GetStream().Write(ResponceBuffer, 0, ResponceBuffer.Length);

                Thread.Sleep(500);

                //Отсоединение клиента
                client.Close();

                Console.WriteLine("Клиент отсоединён");
                Console.Write(new string('-',Console.BufferWidth));
            }

            server.Stop();

            Console.ReadLine();
        }

        
    }
}
