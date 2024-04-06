/*
В этом шаблоне замените:
имя_приложения - на реальное имя вашего приложения, заданное при создании приложения
НазваниеОбъектаДанных - осмысленное название того объекта данных, который предполагается выводить в RecyclerView
НазваниеКлассаДанных - название data-класса, созданного для данного RecyclerView 
названиеобъектаданных - название объекта данных, заданное в имени файла шаблона разметки
*/

package com.example.recyclerviewsamp

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.recyclerviewsamp.R
import com.example.recyclerviewsamp.RecyclerViewOnClickListener
import com.example.recyclerviewsamp.mydata.entity.User
import java.util.ArrayList

import com.example.recyclerviewsamp.mydata.entity.*

class RecyclerViewAdapter_User(
    private val rv_items: ArrayList<User>,
    private val onClickListener: RecyclerViewOnClickListener
    ) : RecyclerView.Adapter<RecyclerViewAdapter_User.MyViewHolder>() {

    inner class MyViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        //Связывание текстовых полей, объявленных в файле разметки (recycler_item_user.xml)
        //с переменными в данном коде
		
		//ЗДЕСЬ ДОБАВЬТЕ ПЕРЕМЕННЫЕ, СООТВЕТСТВУЮЩИЕ КАЖДОМУ ОТОБРАЖАЕМОМУ СТОЛБЦУ
        val tv_UserID: TextView = view.findViewById<View>(R.id.tv_UserID) as TextView
        val tv_Name: TextView = view.findViewById<View>(R.id.tv_Name) as TextView
        val tv_Age: TextView = view.findViewById<View>(R.id.tv_Age) as TextView
        val tv_Email: TextView = view.findViewById<View>(R.id.tv_Email) as TextView
    }

    //Подключение файла разметки (recycler_item_названиеобъектаданных.xml)
    //к данному файлу с кодом
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
                .inflate(R.layout.recycler_item_itemname, parent, false)
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
        holder.tv_UserID.text = rw_item.UserID.toString()
        holder.tv_Name.text = rw_item.Name
        holder.tv_Age.text = rw_item.Age.toString()
        holder.tv_Email.text = rw_item.Email

        //назначение текущему элементу списка
        //обработчика для события Click
        holder.itemView.setOnClickListener {
            onClickListener.onClicked(rw_item.UserID as Int)
        }
    }


    override fun getItemCount(): Int {
        //Получение количества элементов в списке
        return rv_items.size
    }
}


