package com.example.myapplication.mydata

import androidx.room.Database
import androidx.room.RoomDatabase
import com.example.myapplication.mydata.dao.UserDao
import com.example.myapplication.mydata.entity.User

@Database(entities = [User::class], version = 2)
abstract class AppDatabase: RoomDatabase() {
    abstract fun UserDaoFactory(): UserDao
}