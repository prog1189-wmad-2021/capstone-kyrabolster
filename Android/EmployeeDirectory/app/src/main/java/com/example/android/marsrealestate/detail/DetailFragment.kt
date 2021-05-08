package com.example.android.marsrealestate.detail

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.MenuItem
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import com.example.android.marsrealestate.R
import com.example.android.marsrealestate.databinding.FragmentDetailBinding
import com.example.android.marsrealestate.network.Employee

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [DetailedFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class DetailFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {

        val binding = FragmentDetailBinding.inflate(inflater)
        binding.lifecycleOwner = this

        val employee = DetailFragmentArgs.fromBundle(arguments!!).employee
        binding.employee = employee
        Log.d("QQQQ", "employee ${employee.fullName}")

        //email
        binding.email.setOnClickListener(View.OnClickListener {
            val intent = Intent(Intent.ACTION_SENDTO).apply {
                data = Uri.parse("mailto:" + binding.email.getText())
            }
            try {
                startActivity(intent)
            } catch (e: Exception) {
                Toast.makeText(requireContext(), e.message, Toast.LENGTH_LONG).show()
            }
        })

        //phone
        binding.workphone.setOnClickListener(View.OnClickListener {
            val intent = Intent(Intent.ACTION_DIAL)
                intent.data = Uri.parse("tel:" + binding.workphone.getText())
            try {
                startActivity(intent)
            } catch (e: Exception) {
                Toast.makeText(requireContext(), e.message, Toast.LENGTH_LONG).show()
            }
        })

        return binding.root
    }
}