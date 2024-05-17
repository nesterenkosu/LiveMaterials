package com.example.dataeditorwithbinding.data.entity

import androidx.databinding.BaseObservable
import androidx.databinding.Bindable
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.dataeditorwithbinding.BR

@Entity(tableName = "table_users")
class User: BaseObservable {
    constructor(name: String, email: String, age: Int):super() {
        this.name = name
        this.email = email
        this.age = age
    }

    @PrimaryKey(autoGenerate = true) var UserID: Int? = null

    @get:Bindable
    var name:String
        set(value) {
            field = value
            notifyPropertyChanged(BR.name)
        }
        get(){
            return field
        }

    @get:Bindable
    var email:String =""
        set(value) {
            field = value
            notifyPropertyChanged(BR.email)
        }
        get(){
            return field
        }

    @get:Bindable
    var age:Int = -1
        set(value) {
            field = value
            notifyPropertyChanged(BR.age)
        }
        get(){
            return field
        }
}
