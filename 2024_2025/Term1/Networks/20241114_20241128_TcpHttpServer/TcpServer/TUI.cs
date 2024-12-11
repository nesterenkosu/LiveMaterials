using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class TUI
    {
        public static void PadWrite(string text)
        {
            Console.WriteLine(text.PadRight(Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static void SetColor(ConsoleColor bg, ConsoleColor fg)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
        }

        public static void VisualizeText(string title, string text)
        {
            SetColor(ConsoleColor.Blue, ConsoleColor.White);
            PadWrite(title);
            SetColor(ConsoleColor.Black, ConsoleColor.Green);            
            PadWrite(text);
            SetColor(ConsoleColor.Black, ConsoleColor.White);            
        }
        public static void VisualizeArray(string title, Dictionary<string, string> array)
        {
            int buffer_width= Console.BufferWidth;
            int title_width = 26;
            int value_width = buffer_width - title_width;

            

            SetColor(ConsoleColor.Red, ConsoleColor.White);
            PadWrite(title);  

            foreach (KeyValuePair<string, string> item in array)
            {
                SetColor(ConsoleColor.Gray, ConsoleColor.Black);
                Console.Write("{0,-"+ title_width + "}", item.Key);                

                SetColor(ConsoleColor.White, ConsoleColor.Blue);
                Console.WriteLine("{0,-"+ value_width + "}",item.Value);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }

            SetColor(ConsoleColor.White, ConsoleColor.Black);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
