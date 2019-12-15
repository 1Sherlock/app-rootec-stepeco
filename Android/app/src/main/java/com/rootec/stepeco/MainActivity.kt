package com.rootec.stepeco

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.NavigationUI
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        NavigationUI.setupWithNavController(
            bottom_navigation_view,
            (nav_host_fragment as NavHostFragment).navController
        )

        main_fab.setOnClickListener {
            (nav_host_fragment as NavHostFragment).navController.navigate(R.id.mainFragment)
        }
    }


}
