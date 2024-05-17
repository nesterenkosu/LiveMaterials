package com.example.dataeditorwithbinding

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.dataeditorwithbinding.data.entity.User
import com.example.dataeditorwithbinding.recyclerviews.RecyclerViewOnClickListener
import com.example.dataeditorwithbinding.recyclerviews.Users.RecyclerViewAdapter_Users

class ListUsers : AppCompatActivity() {
    private lateinit var rv_users: RecyclerView
    private lateinit var rv_users_items: ArrayList<User>
    private lateinit var adapter: RecyclerViewAdapter_Users
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_list_users)

        rv_users=findViewById(R.id.rv_users)

        //задание в качестве компоновочного контейнера для
        //элементов списка компонента LinearLayoutManager
        //с вертикальным расположением элементов
        val linearLayoutManager = LinearLayoutManager(applicationContext)
        linearLayoutManager.orientation = LinearLayoutManager.VERTICAL
        rv_users.layoutManager = linearLayoutManager

        //Создание массива элементов, через который в RecyclerView
        //будут поступать элементы списка
        rv_users_items = ArrayList<User>()
        //Создание экземпляра объекта RecyclerViewAdapter_Users,
        //определяющего логику работа элемента RecyclerView

        adapter = RecyclerViewAdapter_Users(
            rv_users_items,
            object : RecyclerViewOnClickListener {
                //Обработчик щелчка на элементе списка
                override fun onClicked(idx: Int) {
                    //Вывод выбранного элемента
                    Log.d("myapp",
                        "Вы выбрали: "
                                +rv_users_items[idx].name+" "
                                +rv_users_items[idx].age+" "
                                +rv_users_items[idx].email
                    )
                }
            }
        )

        
        //Вспомогательная переменная adapter служит для более короткого
        //вызова notifyDataSetChanged() в коде далее
        //(чтобы не писать (rv_users.adapter as RecyclerViewAdapter_Users).notifyDataSetChanged())
        rv_users.adapter=adapter
    }
}