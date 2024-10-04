using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CookComputing.XmlRpc;

namespace MyLibraryClient
{
    [XmlRpcUrl("http://mylibrary.loc/webapi/xmlrpc/server.php")]
    public interface ILibraryServiceProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("mylibrary::greet_user")]
        string GreetUser(string username);

        [XmlRpcMethod("mylibrary::log_in_user")]
        string LogInUser(string username, int age);

        [XmlRpcMethod("mylibrary::list_books")]
        Book[] ListBooks();

        [XmlRpcMethod("mylibrary::update_book")]
        int UpdateBook(int ID, string author,string title,int year);
    }

    public struct Book
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }

    public partial class Form1 : Form
    {
        ILibraryServiceProxy proxy;
        public Form1()
        {
            InitializeComponent();
            //Создание прокси-объекта
            proxy = XmlRpcProxyGen.Create<ILibraryServiceProxy>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            label2.Text = proxy.GreetUser(tb_name.Text);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = proxy.LogInUser(
                    tb_name.Text,
                    Convert.ToInt32(tb_age.Text)
                );
            }catch(XmlRpcFaultException ex)
            {
                MessageBox.Show("ОШИБКА ! "+ ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Загрузка списка книг с сайта и отображение его на форме
            this.bookBindingSource.DataSource = proxy.ListBooks();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            proxy.UpdateBook(
                Convert.ToInt32(tb_ID.Text),
                tb_Author.Text,
                tb_Title.Text,
                Convert.ToInt32(tb_Year.Text)
            );

            this.bookBindingSource.DataSource = proxy.ListBooks();
        }
    }
}
