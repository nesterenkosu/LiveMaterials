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
        [XmlRpcMethod("mylibraryservice::greet_user")]
        string GreetUser(string username);
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ILibraryServiceProxy proxy = XmlRpcProxyGen.Create<ILibraryServiceProxy>();

            label2.Text = proxy.GreetUser(textBox1.Text);


        }
    }
}
