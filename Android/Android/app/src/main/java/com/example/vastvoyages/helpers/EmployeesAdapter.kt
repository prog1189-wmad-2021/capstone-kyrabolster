package com.example.vastvoyages.helpers

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.ListAdapter
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

        var employeeId = itemView.findViewById<TextView>(R.id.txtEmpId)

        fun bind(employee: MyEmployee) {
            employeeId.text = employee.EmpId.toString()
        }

    }
}

//class EmployeesAdapter(private val employeesList: List<MyEmployee>) :RecyclerView.Adapter<EmployeesAdapter.ViewHolder>() {
//
//    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
//        return holder.bind(employeesList[position])
//    }
//
//
//    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
//        return ViewHolder.from(parent)
//    }
//
//    class ViewHolder private constructor(val binding: EmployeeItemMyEmployeeBinding) : RecyclerView.ViewHolder(binding.root){
//
//        fun bind(item: MyEmployee) {
//            binding.emp = item
//            binding.executePendingBindings()
//        }
//
//        companion object {
//            fun from(parent: ViewGroup): ViewHolder {
//                val layoutInflater = LayoutInflater.from(parent.context)
//                val binding =
//                    EmployeeItemMyEmployeeBinding.inflate(layoutInflater, parent, false)
//                return ViewHolder(binding)
//            }
//        }
//    }
//}