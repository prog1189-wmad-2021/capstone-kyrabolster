package com.example.android.marsrealestate.network


import android.os.Parcelable
import com.squareup.moshi.Json
import com.squareup.moshi.JsonClass
import kotlinx.android.parcel.Parcelize

@JsonClass(generateAdapter = true)
data class Employee(
    @Json(name = "CellPhone")
    val cellPhone: String,
    @Json(name = "City")
    val city: String,
    @Json(name = "Country")
    val country: String,
    @Json(name = "Email")
    val email: String,
    @Json(name = "EmpId")
    val empId: Int,
    @Json(name = "Errors")
    val errors: List<Any>,
    @Json(name = "FirstName")
    val firstName: String,
    @Json(name = "FullAddress")
    val fullAddress: String,
    @Json(name = "FullName")
    val fullName: String,
    @Json(name = "LastName")
    val lastName: String,
    @Json(name = "MiddleInitial")
    val middleInitial: String,
    @Json(name = "PostalCode")
    val postalCode: String,
    @Json(name = "Province")
    val province: String,
    @Json(name = "Street")
    val street: String,
    @Json(name = "WorkPhone")
    val workPhone: String
)