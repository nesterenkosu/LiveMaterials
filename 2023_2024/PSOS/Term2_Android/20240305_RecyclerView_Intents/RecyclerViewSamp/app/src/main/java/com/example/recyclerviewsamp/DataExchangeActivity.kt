package com.example.recyclerviewsamp

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import androidx.room.Room
import com.example.recyclerviewsamp.mydata.AppDatabase
import com.example.recyclerviewsamp.mydata.entity.User
import com.example.recyclerviewsamp.webapi.rest.MyServiceAPI
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.Job
import kotlinx.coroutines.*
import org.apache.xmlrpc.client.XmlRpcClient
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl
import org.ksoap2.SoapEnvelope
import org.ksoap2.serialization.SoapObject
import org.ksoap2.serialization.SoapSerializationEnvelope
import org.ksoap2.transport.HttpTransportSE
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.net.URL
import kotlin.coroutines.CoroutineContext

class DataExchangeActivity : AppCompatActivity(), CoroutineScope {
    //Задействуем многопоточность
    private val rootJob = Job()
    override val coroutineContext: CoroutineContext
        get() = Dispatchers.Main + rootJob

    private lateinit var btn_go: Button
    private lateinit var btn_go_soap: Button
    private lateinit var btn_go_xmlrpc: Button
    private lateinit var db: AppDatabase;
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_data_exchange)

        btn_go=findViewById(R.id.btn_go)
        btn_go_soap=findViewById(R.id.btn_go_soap)
        btn_go_xmlrpc=findViewById(R.id.btn_go_xmlrpc)

        btn_go.setOnClickListener {
            WorkWithRest()
        }

        btn_go_soap.setOnClickListener {
            WorkWithSoap()
        }

        btn_go_xmlrpc.setOnClickListener {
            WorkWithXmlRpc()
        }
    }

    public fun WorkWithXmlRpc() = launch{
        //Подключение к локальной базе данных
        db = Room.databaseBuilder(
            applicationContext,
            AppDatabase::class.java, "users_db"
        ).allowMainThreadQueries().build()

        //Конфигурирование XML-RPC-клиента
        val config = XmlRpcClientConfigImpl()
        config.serverURL = URL("http://10.0.2.2:8888/xmlrpc.api.php")
        val client = XmlRpcClient()
        client.setConfig(config)

        withContext(Dispatchers.IO) {
            //Вызов метода веб-сервиса
            client.execute("myservice:ListPeople",arrayOf<Any>()) as Array<Object>
        }.forEach {

            db.userDao().insertAll(User(0,
                (it as HashMap<String,String>).get("Name") as String,
                (it as HashMap<String,Int>).get("Age") as Int,
                (it as HashMap<String,String>).get("Mail") as String
            ))
        }
    }

    public fun WorkWithSoap() = launch {
        db = Room.databaseBuilder(
            applicationContext,
            AppDatabase::class.java, "users_db"
        ).allowMainThreadQueries().build()

        //Формирование тела (тег Body) SOAP-запроса
        val request =
            SoapObject("http://localhost/","ListPeople_Request")

        //Формирование обёртки (тег Envelope)
        val env = SoapSerializationEnvelope(SoapEnvelope.VER12)
        env.dotNet=true;
        env.setOutputSoapObject(request)
        withContext(Dispatchers.IO){
            //Собственно вызов метода веб-сервиса
            val t = HttpTransportSE("http://10.0.2.2:8888/soap.api.php")
            t.call("",env)
            //Принятие возвращенного веб-сервисом ответа
            val response = env.bodyIn as SoapObject
            var user_id: String
            var user_name: String
            var user_age: String
            var user_mail: String
            for(i in 0..response.propertyCount-1) {
                user_id = (response.getProperty(i) as
                        SoapObject).getProperty("ID").toString()
                user_name = (response.getProperty(i) as
                        SoapObject).getProperty("Name").toString()
                user_age = (response.getProperty(i) as
                        SoapObject).getProperty("Age").toString()
                user_mail = (response.getProperty(i) as
                        SoapObject).getProperty("Mail").toString()

                Log.d("mysoap",user_name)

               // db.userDao().insertAll(User(0,user_name,user_age,user_mail))
            }
        }
    }


    //Объявление функции, запускаемой в отдельном потоке
    public fun WorkWithRest() = launch {
        db = Room.databaseBuilder(
            applicationContext,
            AppDatabase::class.java, "users_db"
        ).allowMainThreadQueries().build()

        val retrofit =
            Retrofit.Builder().baseUrl("http://10.0.2.2:8888/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        //Генерация прокси-класса на основе ранее объявленного
        //интерфейса MyServiceAPI
        val service = retrofit.create(MyServiceAPI::class.java)
        //Выбор вида создаваемого потока
        //IO - потоки, оптимизированные под ввод-вывод
        withContext(Dispatchers.IO) {
            service.ListPeople().execute().body()?.forEach {

                db.userDao().insertAll(User(0,it.Name,it.Age,it.Mail))
                //Log.d("mytag_new", it.ID.toString() + " " + it.Name)
            }
        }
    }

}