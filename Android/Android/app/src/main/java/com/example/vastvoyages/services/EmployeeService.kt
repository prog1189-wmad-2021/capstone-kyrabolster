package com.example.vastvoyages.services

import com.example.vastvoyages.models.MyEmployee
import retrofit2.Call
import retrofit2.http.GET

interface EmployeeService {
    @GET("employees")
    fun getEmployeesList () : Call<List<MyEmployee>>
}