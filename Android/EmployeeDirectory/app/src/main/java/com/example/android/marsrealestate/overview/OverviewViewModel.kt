/*
 * Copyright 2019, The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

package com.example.android.marsrealestate.overview

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.android.marsrealestate.network.Department
import com.example.android.marsrealestate.network.Employee
import com.example.android.marsrealestate.network.EmployeeApi
import kotlinx.coroutines.*

class OverviewViewModel : ViewModel() {

    private val _response = MutableLiveData<String>()

    val response: LiveData<String>
        get() = _response

    private val _employees = MutableLiveData<List<Employee>>()

    val properties: LiveData<List<Employee>>
        get() = _employees

    private val _navigateToSelectedEmployee = MutableLiveData<Employee>()
    val navigateToSelectedProperty: LiveData<Employee>
        get() = _navigateToSelectedEmployee

    private val _departmentListChanged = MutableLiveData<Boolean>()
    val departmentListChanged: LiveData<Boolean> get() = _departmentListChanged

    private val _departments = MutableLiveData<List<Department>>()
    val departments: LiveData<List<Department>>
        get() = _departments

    init {
        getEmployees(0)
        getDeptList()
    }

    public fun getEmployees(departmentId: Int) {
        viewModelScope.launch {
            try {
                _employees.value = EmployeeApi.retrofitService.getEmployees(departmentId)
            } catch (e: Exception) {
                _employees.value = ArrayList()
            }
        }
    }

    //get department list
    private fun getDeptList() {

        viewModelScope.launch {
            try {
                _departments.value = EmployeeApi.retrofitService.getDepartments()
            } catch (e: Exception) {
                _employees.value = ArrayList()
            }
        }
    }

    fun displayEmployeeDetails(employee: Employee) {
        _navigateToSelectedEmployee.value = employee
    }

    fun displayEmployeeDetailsComplete() {
        _navigateToSelectedEmployee.value = null
    }

    fun updateDepartmentListComplete() {
        _departmentListChanged.value = false
    }
}
