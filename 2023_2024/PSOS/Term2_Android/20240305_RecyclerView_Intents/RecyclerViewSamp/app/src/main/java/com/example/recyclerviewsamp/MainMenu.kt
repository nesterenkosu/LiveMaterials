package com.example.recyclerviewsamp

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button

class MainMenu : AppCompatActivity() {
    private lateinit var btn_users_editor: Button
    private lateinit var btn_data_exchange: Button
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main_menu)

        btn_users_editor = findViewById(R.id.users_editor)
        btn_data_exchange = findViewById(R.id.data_exchange)

        btn_users_editor.setOnClickListener {
            var myintent = Intent(this, MainActivity::class.java)
            startActivityForResult(myintent,1)
        }

        btn_data_exchange.setOnClickListener {
            var myintent = Intent(this, DataExchangeActivity::class.java)
            startActivityForResult(myintent,1)
        }
    }
}