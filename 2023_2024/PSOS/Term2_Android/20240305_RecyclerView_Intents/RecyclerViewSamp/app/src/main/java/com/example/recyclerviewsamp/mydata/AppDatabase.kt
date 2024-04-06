package com.example.recyclerviewsamp.mydata

import androidx.room.Database
import androidx.room.RoomDatabase
import com.example.recyclerviewsamp.mydata.dao.UserDao
import com.example.recyclerviewsamp.mydata.entity.User

@Database(entities = [User::class], version = 2)
abstract class AppDatabase: RoomDatabase() {
    abstract fun userDao():UserDao
}