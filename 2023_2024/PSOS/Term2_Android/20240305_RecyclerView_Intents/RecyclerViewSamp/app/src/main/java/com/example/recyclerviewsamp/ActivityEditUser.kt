package com.example.recyclerviewsamp

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.TextView

class ActivityEditUser : AppCompatActivity() {
    private lateinit var tv_ID: TextView
    private lateinit var tv_Name: TextView
    private lateinit var tv_Age: TextView
    private lateinit var tv_Email: TextView
    private lateinit var btn_Save: Button
    private lateinit var btn_Cancel: Button
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_edit_user)

        tv_ID = findViewById(R.id.tv_ID)
        tv_Name = findViewById(R.id.tv_Name)
        tv_Age = findViewById(R.id.tv_Age)
        tv_Email = findViewById(R.id.tv_Email)
        btn_Save = findViewById(R.id.btn_Save)
        btn_Cancel = findViewById(R.id.btn_Cancel)

        tv_ID.setText(intent.getIntExtra("user_id",0).toString())
        tv_Name.setText(intent.getStringExtra("user_name"))
        tv_Age.setText(intent.getIntExtra("user_age",0).toString())
        tv_Email.setText(intent.getStringExtra("user_email"))

        btn_Save.setOnClickListener {
            //Возврат данных в родительское окно
            //создание объекта Intent
            var result_intent = Intent()

            //Передача данных в родительское окно
            result_intent.putExtra("name",tv_Name.text.toString())
            result_intent.putExtra("age",tv_Age.text.toString())
            result_intent.putExtra("email",tv_Email.text.toString())

            //собственно возврат в родительское окно
            setResult(Activity.RESULT_OK,result_intent)

            //закрытие текущего окна
            finish()
        }

        btn_Cancel.setOnClickListener {
            //Возврат данных в родительское окно
            //создание объекта Intent
            var result_intent = Intent()

            //собственно возврат в родительское окно
            setResult(Activity.RESULT_CANCELED,result_intent)

            //закрытие текущего окна
            finish()
        }
    }
}