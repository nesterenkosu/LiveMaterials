package com.example.myapplication.mydata.entity

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName="Users")
data class User (
    @PrimaryKey(autoGenerate = true)
    var id: Int?,
    var username: String,
    var age: Int
)