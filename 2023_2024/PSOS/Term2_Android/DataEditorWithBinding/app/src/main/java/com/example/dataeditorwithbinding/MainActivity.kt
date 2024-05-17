package com.example.dataeditorwithbinding

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import androidx.databinding.DataBindingUtil
import com.example.dataeditorwithbinding.databinding.ActivityMainBinding
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.Job
import kotlinx.coroutines.launch
import kotlin.coroutines.CoroutineContext

class MainActivity : AppCompatActivity() {
    private lateinit var btn_edit_users: Button
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val binding: ActivityMainBinding = DataBindingUtil.setContentView(
            this, R.layout.activity_main)

        binding.handlers = this;
    }

    fun show_list_users(){
        var activity = Intent(this,ListUsers::class.java)
        startActivity(activity)
    }

    fun show_list_languages(){
        var activity = Intent(this,ListLanguages::class.java)
        startActivity(activity)
    }

    fun show_settings(){
        var activity = Intent(this,Settings::class.java)
        startActivity(activity)
    }

}