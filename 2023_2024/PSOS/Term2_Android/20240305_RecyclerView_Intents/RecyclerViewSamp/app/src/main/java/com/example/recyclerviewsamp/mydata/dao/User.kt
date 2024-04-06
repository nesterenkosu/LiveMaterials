package com.example.recyclerviewsamp.mydata.dao

import androidx.room.*
import com.example.recyclerviewsamp.mydata.entity.User

@Dao
interface UserDao {
    @Insert
    fun insertAll(vararg item: User)

    @Query("SELECT * FROM user")
    fun getAll():List<User>

    @Query("SELECT * FROM user WHERE UserID=:id ")
    fun getById(id:Int):User

    @Update
    fun update(item:User)

    @Delete
    fun delete(item:User)
}
