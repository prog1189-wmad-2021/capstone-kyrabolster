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
import com.example.android.marsrealestate.network.MarsApi
import kotlinx.coroutines.*


/**
 * The [ViewModel] that is attached to the [OverviewFragment].
 */
class OverviewViewModel : ViewModel() {

    private val _response = MutableLiveData<String>()

    val response: LiveData<String>
        get() = _response

    private val _properties = MutableLiveData<List<Employee>>()

    val properties: LiveData<List<Employee>>
        get() = _properties

    private val _navigateToSelectedProperty = MutableLiveData<Employee>()
    val navigateToSelectedProperty: LiveData<Employee>
        get() = _navigateToSelectedProperty

    //departments
    //coroutine for department
//    private var job = Job()
//    private val coroutineScope = CoroutineScope(job + Dispatchers.Main)

    private val _departmentListChanged = MutableLiveData<Boolean>()
    val departmentListChanged: LiveData<Boolean> get() = _departmentListChanged


//    var deptList = mutableListOf(Department(0, "All"))
//    public var deptList = MutableLiveData<List<Department>>()

    private val _departments = MutableLiveData<List<Department>>()
    val departments: LiveData<List<Department>>
        get() = _departments

    init {
        getMarsRealEstateProperties(0)
        getDeptList()
    }

    public fun getMarsRealEstateProperties(departmentId: Int) {
        viewModelScope.launch {
            try {
                _properties.value = MarsApi.retrofitService.getProperties(departmentId)
            } catch (e: Exception) {
                _properties.value = ArrayList()
            }
        }
    }

    //get department list
    private fun getDeptList() {

        viewModelScope.launch {
            try {
                _departments.value = MarsApi.retrofitService.getDepartments()
            } catch (e: Exception) {
                _properties.value = ArrayList()
            }
        }
    }

    fun displayPropertyDetails(employee: Employee) {
        _navigateToSelectedProperty.value = employee
    }

    fun displayPropertyDetailsComplete() {
        _navigateToSelectedProperty.value = null
    }

    fun updateDepartmentListComplete() {
        _departmentListChanged.value = false
    }
}
