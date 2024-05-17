package com.example.dataeditorwithbinding.webapi

import com.example.dataeditorwithbinding.data.entity.User
import retrofit2.Call
import retrofit2.http.*

interface RestAPI {
    @PUT("rest/person")
    fun CreatePerson(@Body person: User): Call<Void>

    @GET("rest/people")
    fun ListPeople(): Call<List<User>>

    @GET("rest/person")
    fun ReadPerson(@Query("id") id:Int):Call<User>

    @PATCH("rest/person")
    fun UpdatePerson(@Query("id")id:Int,@Body person: User):Call<Void>

    @DELETE("rest/person")
    fun DeletePerson(@Query("id")id:Int):Call<Void>
}