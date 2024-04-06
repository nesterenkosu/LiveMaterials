package com.example.recyclerviewsamp.webapi.rest

import com.example.recyclerviewsamp.mydata.entity.User
import retrofit2.Call
import retrofit2.http.*

interface MyServiceAPI {

    @PUT("rest/person")
    fun CreatePerson(@Body item: REST_User): Call<Void>
    @GET("rest/people")
    fun ListPeople(): Call<List<REST_User>>
    @GET("rest/person")
    fun ReadPerson(@Query("id") id:Int):Call<REST_User>
    @PATCH("rest/person")
    fun UpdatePerson(@Query("id")id:Int,@Body item:
    REST_User): Call<Void>
    @DELETE("rest/person")
    fun DeletePerson(@Query("id")id:Int):Call<Void>

}