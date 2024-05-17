package com.example.dataeditorwithbinding

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import androidx.databinding.DataBindingUtil
import androidx.room.Room
import com.example.dataeditorwithbinding.data.AppDatabase
import com.example.dataeditorwithbinding.data.entity.User
import com.example.dataeditorwithbinding.databinding.ActivityEditUserBinding
import com.example.dataeditorwithbinding.webapi.RestAPI
import kotlinx.coroutines.*
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import kotlin.coroutines.CoroutineContext

class EditUser : AppCompatActivity(), CoroutineScope {

    //Активацния многопоточности
    private val rootJob = Job()
    override val coroutineContext: CoroutineContext
        get() = Dispatchers.Main + rootJob

    private lateinit var btn_save: Button
    private lateinit var binding: ActivityEditUserBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //setContentView(R.layout.activity_edit_user)

        binding = DataBindingUtil.setContentView(
            this, R.layout.activity_edit_user)
        binding.user= User("Василий","vasya@mail.ru",30)

        btn_save=findViewById(R.id.btn_save)
        btn_save.setOnClickListener {
            //Отладочный вывод введённого имени в консоль
            Log.d("myapp",binding.user!!.name)

            //Сохранение данных формы в базу данных
            var db = Room.databaseBuilder(
                applicationContext,
                AppDatabase::class.java,
                "my_binding_samp_db"
            ).allowMainThreadQueries().build()

            db.userDao().insertAll(binding.user!!)

            //Отправка данных на сервер по протоколу REST
            WorkWithRest()
        }
    }

    public fun WorkWithRest() = launch{

        TestCreatePerson()

    }

    public suspend fun TestCreatePerson(){
        try {
            //Подключение к серверу
            val retrofit = Retrofit.Builder().baseUrl("http://10.0.2.2:8888/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()

            //Создание прокси-класса
            //прокси-класс - это класс, методы которого проецируются
            //на методы веб-сервиса. Вызывая методы прокси-класса
            //мы вызываем методы веб-сервиса
            val service = retrofit.create(RestAPI::class.java)


            //Выполнение действия в другом потоке
            withContext(Dispatchers.IO) {
                //Вызов серверного метода CreatePerson
                //для сохранения данного пользователя в серверной базе данных
                service.CreatePerson(binding.user!!).execute()
            }

            Log.d("myapp","Пользоватлеь создан")
        }catch (ex:Exception) {
            Log.d("myapp","Ошибка: "+ex.toString())
        }
    }
}