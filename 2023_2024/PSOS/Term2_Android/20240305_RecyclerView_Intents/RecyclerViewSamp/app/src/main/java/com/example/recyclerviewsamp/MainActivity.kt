package com.example.recyclerviewsamp

import android.app.Activity
import android.app.AlertDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.room.Room
import com.example.recyclerviewsamp.mydata.AppDatabase
import com.example.recyclerviewsamp.mydata.entity.User

class MainActivity : AppCompatActivity() {
    private lateinit var tv_selection: TextView
    private lateinit var rv_users: RecyclerView
    private lateinit var items: ArrayList<User>
    private lateinit var btn_add: Button
    private lateinit var btn_edit: Button
    private lateinit var btn_del: Button
    private lateinit var db: AppDatabase;

    //id элемента, выбранного в RecyclerView при последнем клике
    private var data_id: Int = -1
    //Флаг, определяющий, является ли текущий режим редактированием
    private var is_edit: Boolean = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        btn_add = findViewById(R.id.btn_add)
        btn_edit = findViewById(R.id.btn_edit)
        btn_del = findViewById(R.id.btn_del)

        tv_selection = findViewById(R.id.tv_selection)

        //Получение доступа к элементу управления RecyclerView,
        //объявленному в файле разметки
        rv_users = findViewById(R.id.rv_users)

        //Объявление массива - источника данных (его элементы
        //будут отображаться в RecyclerView)
        items = ArrayList<User>()

        //Задание формата отображения RecyclerView в виде вертикального списка
        rv_users.layoutManager = LinearLayoutManager(
            applicationContext,LinearLayoutManager.VERTICAL,
            false)

        //Задание адаптера RecyclerView - специального объекта, связывающего
        //RecyclerView с отображаемыми в нём данными, а также задающего
        //обработчики событий
        rv_users.adapter = RecyclerViewAdapter_User (
            items,//ранее объявленный массив-источник данных
            object : RecyclerViewOnClickListener {
                //Обработчик щелчка на элементе списка
                override fun onClicked(item_id: Int) {
                    data_id = item_id
                    //tv_selection.setText("Вы выбрали: "+items[idx].Name)
                }
            }
        )

        //добавление элементов в массив - источник данных вручную
        /*var i: Int
        for(i in 1..100) {
            items.add(User(i, "Пользователь " + i.toString(), 30, "andrew@mail.ru"))
        }*/

        //Установить соединение с БД
        db = Room.databaseBuilder(
            applicationContext,
            AppDatabase::class.java, "users_db"
        ).allowMainThreadQueries().build()





        /*items.add(User(0,"Василий",50,"vasya@mail.ru"))
        items.add(User(1,"Николай",20,"nic@mail.ru"))
        items.add(User(2,"Сергей",27,"serge@mail.ru"))*/

        //Уведомление RecyclerView о том, что данные
        //в массиве-источнике изменились (это нужно
        //для обновления отображаемых в RecyclerView данных)
        UpdateRecyclerView()

        btn_add.setOnClickListener {
            var edit_user = Intent(this,ActivityEditUser::class.java)
            startActivityForResult(edit_user,1)
        }

        btn_edit.setOnClickListener {
            if(data_id > 0) {
                //Установка флага режима редактирования
                is_edit = true;

                //Извлечение из базы данных элемента, выбранного в RecyclerView
                var user_to_edit = db.userDao().getById(data_id)

                //Подготовка окна редактирования к запуску
                var edit_user = Intent(this, ActivityEditUser::class.java)

                //Передача данных редактируемого пользователя
                edit_user.putExtra("user_id",user_to_edit.UserID)
                edit_user.putExtra("user_name",user_to_edit.Name)
                edit_user.putExtra("user_age",user_to_edit.Age)
                edit_user.putExtra("user_email",user_to_edit.Email)

                //Открытие окна
                startActivityForResult(edit_user, 1)
            }
        }

        //Удаление элемента, выделенного в RecyclerView
        btn_del.setOnClickListener {
            if(data_id > 0) {
                val builder = AlertDialog.Builder(this)
                //задание текста предупреждения
                builder.setMessage("Вы действительно хотите удалить выбранного пользователя?")
                //создание подтверждающей кнопки и задание действий
                //выполняемых при нажатии на неё
                builder.setPositiveButton("Да") { _,_ ->
                    db.userDao().delete(User(data_id,"",0,""))
                    UpdateRecyclerView()
                }
                //создание отрицающей кнопки, при нажатии на
                //которой не будет выполнено никаких действий
                builder.setNegativeButton("Нет") {_,_->}
                //отображение созданного всплывающего окна
                builder.show()
            }
        }
    }

    //Обработчик возвратов из дочерних окон
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if(requestCode == 1) { //MainActivity2
            if(resultCode == Activity.RESULT_OK) {
                //Получение данных из дочернего окна
                var name = data!!.getStringExtra("name")
                var age = data!!.getStringExtra("age")
                var email = data!!.getStringExtra("email")

               // var id = items.size+1
               // items.add(User(id,name.toString(),age!!.toInt(),email.toString()))
                if(!is_edit)
                    db.userDao().insertAll(User(0,name.toString(),age!!.toInt(),email.toString()))
                else
                    db.userDao().update(User(data_id,name.toString(),age!!.toInt(),email.toString()))

                UpdateRecyclerView()
            }
        }
    }

    fun UpdateRecyclerView(){
        //Очистка списка
        items.clear()

        //Заполнение списка новыми данными
        var user: User
        for(user in db.userDao().getAll())
            items.add(user)
        
        (rv_users.adapter)?.notifyDataSetChanged()
    }
}