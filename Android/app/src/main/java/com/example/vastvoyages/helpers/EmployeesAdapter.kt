package com.example.vastvoyages.helpers

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.vastvoyages.R
import com.example.vastvoyages.models.MyEmployee

class EmployeesAdapter(private val employeesList: List<MyEmployee>) : RecyclerView.Adapter<EmployeesAdapter.ViewHolder>() {
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {

        val view  = LayoutInflater.from(parent.context).inflate(R.layout.employee_item,parent,false)
        return ViewHolder(view)
    }


    override fun getItemCount(): Int {

        return employeesList.size
    }


    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        Log.d("Response", "List Count :${employeesList.size} ")


        return holder.bind(employeesList[position])

    }
    class ViewHolder(itemView : View) :RecyclerView.ViewHolder(itemView) {


//        var imageView = itemView.findViewById<ImageView>(R.id.ivFlag)
        var employeeId = itemView.findViewById<TextView>(R.id.txtEmpId)
//        var tvCases = itemView.findViewById<TextView>(R.id.tvCases)
        fun bind(employee: MyEmployee) {

//            val name ="Cases :${country.cases.toString()}"
    employeeId.text = employee.EmpId.toString()
//            tvCases.text = name
//            Picasso.get().load(country.countryInfo.flag).into(imageView)
        }

    }
}