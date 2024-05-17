package com.example.dataeditorwithbinding.data

import androidx.room.Database
import androidx.room.RoomDatabase
import com.example.dataeditorwithbinding.data.dao.UserDao
import com.example.dataeditorwithbinding.data.entity.User

@Database(entities = [
    User::class
],version = 2)
abstract class AppDatabase: RoomDatabase() {
    abstract fun userDao(): UserDao
}