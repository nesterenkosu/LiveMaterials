using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			//Генерация прокси-объекта для доступа к REST API 
			//веб-сайта с акциями rbc.ru
            ChannelFactory<RBC_API> rbc_api = new ChannelFactory<RBC_API>("RBC_ENDPOINT");
            var proxy = rbc_api.CreateChannel();

			//Получение списка акций с веб-сайта
            Book[] books = proxy.GetBooks();
			
			//Вывод полученного списка акций в таблицу на форме
            foreach(Book book in books)
            {
                dataGridView1.Rows.Add(book.Author, book.Title, book.Year);
            }
        }
    }

    [ServiceContract]
    [DataContractFormat]
    public interface RBC_API
    {
		//Сопоставление метода GetBooks с методом REST API веб-сайта библиотеки
        [WebGet(
            UriTemplate = "/rest/books",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json
        )]
        Book[] GetBooks();
    }

	//Контракты данных для представления на языке C# данных, скачанных с веб-сайта
    [DataContract]
    public class Book
    {
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Year { get; set; }
    }
}
