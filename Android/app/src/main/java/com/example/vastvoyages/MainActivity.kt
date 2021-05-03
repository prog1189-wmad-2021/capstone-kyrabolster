package com.example.vastvoyages

import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.vastvoyages.helpers.EmployeesAdapter
import com.example.vastvoyages.models.MyEmployee
import com.example.vastvoyages.services.EmployeeService
import com.example.vastvoyages.services.ServiceBuilder
import kotlinx.android.synthetic.main.activity_main.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        loadEmployees()
    }

    private fun loadEmployees() {
        //initiate the service
        val destinationService  = ServiceBuilder.buildService(EmployeeService::class.java)
        val requestCall = destinationService.getEmployeesList()
        //make network call asynchronously
        requestCall.enqueue(object : Callback<List<MyEmployee>> {
            override fun onResponse(call: Call<List<MyEmployee>>, response: Response<List<MyEmployee>>) {
                Log.d("Response", "onResponse: ${response.body()}")
                if (response.isSuccessful) {
                    val employeeList = response.body()!!
                    Log.d("Response", "employeeList size : ${employeeList.size}")
                    val adp = EmployeesAdapter(response.body()!!)
                    employee_recycler.apply {
                        setHasFixedSize(true)
                        layoutManager = GridLayoutManager(this@MainActivity, 2)
                        adapter =  adp
                    }

                    //test
//                    val mRecycler = employee_recycler as RecyclerView
//                    mRecycler.layoutManager = GridLayoutManager(this@MainActivity, 2)
//                    mRecycler.adapter = adp
                } else {
                    Toast.makeText(this@MainActivity, "Something went wrong ${response.message()}", Toast.LENGTH_LONG).show()
                }
            }

            override fun onFailure(call: Call<List<MyEmployee>>, t: Throwable) {
                Toast.makeText(this@MainActivity, "Something went wrong $t", Toast.LENGTH_LONG).show()
            }
        })
    }
}