package com.example.android.marsrealestate.network


import android.os.Parcelable
import com.squareup.moshi.Json
import com.squareup.moshi.JsonClass
import kotlinx.parcelize.Parcelize

@Parcelize
@JsonClass(generateAdapter = true)
data class Department(
        @Json(name = "DepartmentId")
        val departmentId: Int,
        @Json(name = "DepartmentName")
        val departmentName: String,
) : Parcelable