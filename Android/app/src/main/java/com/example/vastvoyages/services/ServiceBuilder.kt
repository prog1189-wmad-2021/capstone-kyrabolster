package com.example.vastvoyages.services

import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object ServiceBuilder {
//    private const val URL ="https://localhost:44315/api/employees/"
//    private const val URL ="http://127.0.0.1/"
//private const val URL ="https://10.0.2.2/"

    //socket timeout : failed to connect
private const val URL ="http://10.0.2.2:56769/api/"

    //failed to connect
//    private const val URL ="https://127.0.0.1:44315/api/employees/"

    //CREATE HTTP CLIENT
    private val okHttp = OkHttpClient.Builder()

    //retrofit builder
    private val builder = Retrofit.Builder().baseUrl(URL)
        .addConverterFactory(GsonConverterFactory.create())
        .client(okHttp.build())

    //create retrofit Instance
    private val retrofit = builder.build()


    fun <T> buildService (serviceType :Class<T>):T{
        return retrofit.create(serviceType)
    }
}