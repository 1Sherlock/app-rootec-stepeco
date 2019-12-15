package com.rootec.stepeco


import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.yandex.mapkit.Animation
import com.yandex.mapkit.MapKitFactory
import com.yandex.mapkit.geometry.Point
import com.yandex.mapkit.map.CameraPosition
import kotlinx.android.synthetic.main.fragment_main.*

import androidx.databinding.adapters.TextViewBindingAdapter.setText
import android.widget.TextView
import com.yandex.mapkit.layers.ObjectEvent
import com.yandex.mapkit.user_location.UserLocationObjectListener
import com.yandex.mapkit.user_location.UserLocationView
import com.yandex.mapkit.MapKit
import com.yandex.mapkit.user_location.UserLocationLayer
import com.yandex.mapkit.map.RotationType
import android.graphics.PointF
import com.yandex.mapkit.map.IconStyle

import android.graphics.Color
import com.yandex.runtime.image.ImageProvider




class MainFragment : Fragment(), UserLocationObjectListener {

    private lateinit var userLocationLayer: UserLocationLayer

    override fun onObjectUpdated(p0: UserLocationView, p1: ObjectEvent) {

    }

    override fun onObjectRemoved(p0: UserLocationView) {

    }

    override fun onObjectAdded(p0: UserLocationView) {
        userLocationLayer.setAnchor(
            PointF((map.width * 0.5f), (map.height * 0.5f)),
            PointF((map.width * 0.5f), (map.height * 0.83f))
        )

        val pinIcon = p0.pin.useCompositeIcon()

        pinIcon.setIcon(
            "pin",
            ImageProvider.fromResource(requireContext(), R.drawable.search_result),
            IconStyle().setAnchor(PointF(0.5f, 0.5f))
                .setRotationType(RotationType.ROTATE)
                .setZIndex(1f)
                .setScale(0.5f)
        )

//        p0.accuracyCircle.fillColor = Color.BLUE
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        MapKitFactory.setApiKey("11159977-5dee-4b7f-a199-9b58fc6b84bb")
        MapKitFactory.initialize(activity)



        return inflater.inflate(R.layout.fragment_main, container, false)
    }



    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        map.map.isRotateGesturesEnabled = false
        map.map.move(CameraPosition(Point(0.0, 0.0), 14f, 0f, 0f))

        val mapKit = MapKitFactory.getInstance()
        userLocationLayer = mapKit.createUserLocationLayer(map.mapWindow)
        userLocationLayer.isVisible = true
        userLocationLayer.isHeadingEnabled = true

        userLocationLayer.setObjectListener(this)


    }

    override fun onStart() {
        super.onStart()
        map.onStart()
        MapKitFactory.getInstance().onStart()
    }

    override fun onStop() {
        super.onStop()
        map.onStop()
        MapKitFactory.getInstance().onStop()
    }


}
