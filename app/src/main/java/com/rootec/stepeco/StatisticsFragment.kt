package com.rootec.stepeco


import android.graphics.Color
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import kotlinx.android.synthetic.main.fragment_statistics.*
import com.jjoe64.graphview.series.LineGraphSeries
import com.jjoe64.graphview.GraphView
import com.jjoe64.graphview.series.DataPoint
import java.util.*


class StatisticsFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_statistics, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val series = LineGraphSeries<DataPoint>(
            arrayOf<DataPoint>(
                DataPoint(1.0, 2300.0),
                DataPoint(2.0, 3250.0),
                DataPoint(3.0, 4234.0),
                DataPoint(4.0, 3460.0),
                DataPoint(5.0, 3746.0),
                DataPoint(6.0, 3856.0),
                DataPoint(7.0, 2478.0),
                DataPoint(8.0, 2346.0),
                DataPoint(9.0, 3578.0),
                DataPoint(10.0, 3926.0)

            )
        )
        graph.addSeries(series)
        graph.title = "Статистика шагов за декабрь"

    }




}
