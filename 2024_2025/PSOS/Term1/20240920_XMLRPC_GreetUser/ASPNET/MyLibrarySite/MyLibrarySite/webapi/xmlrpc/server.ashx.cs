using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookComputing.XmlRpc;


namespace MyLibrarySite.webapi.xmlrpc
{
    /// <summary>
    /// Сводное описание для server
    /// </summary>
    public class server : XmlRpcService
    {
        [XmlRpcMethod("mylibraryaspnet::greet_user", Description = "Метод, выводящий именное приветствие")]
        public string GreetUser(string username)
        {
            return "Здравствуй, " + username + "! Ты используешь версию на ASP.NET";
        }
    }
}