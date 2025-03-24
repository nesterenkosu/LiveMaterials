package com.example.myapplication.mydata.dao

import androidx.room.*
import com.example.myapplication.mydata.entity.User

@Dao
interface UserDao {
    //CRUD - Create Read Update Delete

    //Create
    @Insert
    fun insertAll(vararg item: User)

    //Read
    @Query("SELECT * FROM Users")
    fun getAll():List<User>

    @Query("SELECT * FROM Users WHERE id=:id")
    fun getById(id:Int?):User

    //Update
    @Update
    fun update(item:User)

    //Delete
    @Delete
    fun delete(item:User)
}
