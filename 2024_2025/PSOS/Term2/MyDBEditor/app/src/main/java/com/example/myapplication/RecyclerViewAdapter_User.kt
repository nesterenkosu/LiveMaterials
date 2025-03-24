/*
В этом шаблоне замените:
имя_приложения - на реальное имя вашего приложения, заданное при создании приложения
НазваниеОбъектаДанных - осмысленное название того объекта данных, который предполагается выводить в RecyclerView
НазваниеКлассаДанных - название data-класса, созданного для данного RecyclerView 
названиеобъектаданных - название объекта данных, заданное в имени файла шаблона разметки
*/

package com.example.myapplication

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import java.util.ArrayList

import com.example.myapplication.mydata.entity.*

class RecyclerViewAdapter_User(
    private val rv_items: ArrayList<User>,
    private val onClickListener: RecyclerViewOnClickListener
) : RecyclerView.Adapter<RecyclerViewAdapter_User.MyViewHolder>() {

    inner class MyViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        //Связывание текстовых полей, объявленных в файле разметки (recycler_item_user.xml)
        //с переменными в данном коде

        //ЗДЕСЬ ДОБАВЬТЕ ПЕРЕМЕННЫЕ, СООТВЕТСТВУЮЩИЕ КАЖДОМУ ОТОБРАЖАЕМОМУ СТОЛБЦУ
        val tv_uid: TextView = view.findViewById<View>(R.id.tv_uid) as TextView
        val tv_user_name: TextView = view.findViewById<View>(R.id.tv_username) as TextView
        val tv_age: TextView = view.findViewById<View>(R.id.tv_age) as TextView
    }

    //Подключение файла разметки (recycler_item_названиеобъектаданных.xml)
    //к данному файлу с кодом
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.recycler_item_user, parent, false)
        return MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        //Эта функция вызывается последовательно
        //для каждого элемента списка RecyclerView
        //holder - экземпляр объекта разметки, созданный для
        //отображения текущего элемента
        //position - порядковый номер текущего элемента

        //получение данных для текущего элемента списка
        //из переданного в конструктор массива
        val rw_item = rv_items[position]

        //вывод свойств полученного объекта данных
        //в соответствующие текстовые поля разметки

        //ЗДЕСЬ ДОБАВЬТЕ ИНИЦИАЛИЗАЦИЮ ПЕРЕМЕННЫХ, СООТВЕТСТВУЮЩИХ КАЖДОМУ ОТОБРАЖАЕМОМУ СТОЛБЦУ
        holder.tv_uid.text = rw_item.id.toString()
        holder.tv_user_name.text = rw_item.username
        holder.tv_age.text = rw_item.age.toString()



        //назначение текущему элементу списка
        //обработчика для события Click
        holder.itemView.setOnClickListener {
            onClickListener.onClicked(rw_item.id as Int)
        }
    }


    override fun getItemCount(): Int {
        //Получение количества элементов в списке
        return rv_items.size
    }
}


