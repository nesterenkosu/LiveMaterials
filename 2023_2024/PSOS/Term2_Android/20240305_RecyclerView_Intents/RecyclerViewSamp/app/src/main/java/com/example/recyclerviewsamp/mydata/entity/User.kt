package com.example.recyclerviewsamp.mydata.entity

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity
data class User(
    @PrimaryKey(autoGenerate = true)var UserID: Int,
    var Name: String,
    var Age: Int,
    var Email:String?
)
