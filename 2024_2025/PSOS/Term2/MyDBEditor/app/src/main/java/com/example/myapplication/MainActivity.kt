package com.example.myapplication

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.room.Room
import androidx.room.RoomDatabase
import com.example.myapplication.mydata.AppDatabase
import com.example.myapplication.mydata.entity.User

class MainActivity : AppCompatActivity() {
    //Объявление переменных для каждого элемента интерфейса
    //с которым предполагается работать в коде
    private lateinit var et_username: EditText
    private lateinit var et_age: EditText
    private lateinit var btn_go: Button
    private lateinit var btn_delete: Button
    private lateinit var rv_users: RecyclerView
    private lateinit var adapter: RecyclerViewAdapter_User

    //private lateinit var db: RoomDatabase.Builder<AppDatabase>

    //Массив для хранения данных, отображаемых в RecyclerView
    private lateinit var items: ArrayList<User>

    private var uid: Int? = null

    fun UpdateRecyclerView(){
        //СИНХРОНИЗАЦИЯ RECYCLERVIEW С БАЗОЙ ДАННЫХ

        //Очистка RecyclerView
        items.clear()

        //Соединение с базой данных
        val db = Room.databaseBuilder(
            applicationContext,
            AppDatabase::class.java, "UsersDb20250317"
        ).allowMainThreadQueries().build()

        //Заполнение RecyclerView данными из базы
        items.addAll(db.UserDaoFactory().getAll())

        //Уведомить RecyclerView что данные в списке изменились
        adapter.notifyDataSetChanged()
    }

    fun ClearForm(){
        et_username.setText("");
        et_age.setText("")
    }

    fun ResetDatabaseUi(){
        ClearForm()
        UpdateRecyclerView()
        uid=null
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        //Связвание переменных с элементами интерфейса
        et_username = findViewById(R.id.et_username)
        et_age = findViewById(R.id.et_age)
        btn_go = findViewById(R.id.btn_go)
        btn_delete = findViewById(R.id.btn_delete)
        rv_users = findViewById(R.id.rv_users)

        //Установить соединение с БД
        val db = Room.databaseBuilder(
            applicationContext,
            AppDatabase::class.java, "UsersDb20250317"
        ).allowMainThreadQueries().build()

        items = ArrayList<User>()

        //Задание формата отображения RecyclerView в виде вертикального списка
        rv_users.layoutManager = LinearLayoutManager(
            applicationContext,LinearLayoutManager.VERTICAL,
            false)

        //Инициализация RecyclerView
        adapter = RecyclerViewAdapter_User (
                items,//ранее объявленный массив-источник данных
                object : RecyclerViewOnClickListener {
                    //Обработчик щелчка на элементе списка
                    override fun onClicked(idx: Int) {
                        val user = db.UserDaoFactory().getById(idx)
                        et_username.setText(user.username);
                        et_age.setText(user.age.toString());

                        uid = user.id
                    }
                }
        )

        rv_users.adapter = adapter

        UpdateRecyclerView()

        //Обработка нажатия на кнопку "Сохранить"
        btn_go.setOnClickListener {
            var username = et_username.getText().toString();
            var age = et_age.getText().toString().toInt()

            if(uid==null) {
                //Сохранение нового пользователя в базу данных
                db.UserDaoFactory().insertAll(User(null, username, age))
            }else{
                //Обновление существующего пользователя
                db.UserDaoFactory().update(User(uid, username, age))
            }

            ResetDatabaseUi()

        }

        //Обработка нажатия на кнопку "Удалить"
        btn_delete.setOnClickListener {
            db.UserDaoFactory().delete(User(uid,"",0))
            ResetDatabaseUi()
        }
    }
}