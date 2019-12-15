var marker = null; 
var MyMap;
var center;
var heatmap;
var list = [];

function initMap() {
    center = {lat: 41.31251115, lng: 69.25528505};
    MyMap = new google.maps.Map(document.getElementById('map'), {
        zoom: 6.5,
        center: center
    });
}

function CreateMarker(){
    if(marker !== null){
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        map: MyMap,
        draggable: true,
        animation: google.maps.Animation.DROP,
        position: center,
        icon: "./MarkerIcon.png"
    });
    marker.addListener('click', toggleBounce);
    google.maps.event.addListener(marker, 'dragend', function (event) {
        var lat = this.getPosition().lat();
        var lng = this.getPosition().lng();
        console.log(lat);
        console.log(lng);
        $.ajax({ 
            type: 'GET', 
            url: 'http://signum-001-site1.gtempurl.com/api/Signal/GetPoint', 
            data: { 
                longitude: lng,
                latitude: lat
            }, 
            dataType: 'json',
            success: function (data) { 
                $("#ModalBody tr").remove();
                $('table thead').append("<tr><td align = 'center'><strong>Операторы</strong></td><td align = 'center'><strong>Уровень сигнала</strong></td></tr>");                                      
                $.each(data, function(index, element) {
                    $('table tbody').append("<tr><td>" + element.operator + "</td><td>" + (element.powerAverage).toFixed(2) + " dBm</td></tr>");                      
                });    
            }
        });
    document.getElementById("ModalBtn").click();    
    });
}

function toggleBounce() {
    if (marker.getAnimation() !== null) {
        marker.setAnimation(null);
    } else {
        marker.setAnimation(google.maps.Animation.BOUNCE);
    }
}




$( "#SbmBtn" ).click(function() {
    var operatorName = $('#SelOperator').val();
    if(operatorName == "Все операторы"){
        operatorName = null;
    }
    if(operatorName == "Beeline"){
        operatorName = "Beeline UZ";
    }
    var yearFrom = $('#SelDateFrom').val();
    var yearTo = $('#SelDateTo').val();
    var signalType = $('#SelType').val();
    $.ajax({ 
        type: 'GET', 
        url: 'http://signum-001-site1.gtempurl.com/api/Signal/Get', 
        data: { 
            signalType: signalType,
            operatorName: operatorName,
            yearFrom: yearFrom,
            yearTo: yearTo
        }, 
        dataType: 'json',
        success: function (ResData) { 
            list = [];
            MyMap = new google.maps.Map(document.getElementById('map'), {
                zoom: 10,
                center: center
            });
            $.each(ResData, function(index, element) {
               list.push({location: new google.maps.LatLng(element.latitude, element.longitude), weight: (115 + element.power)})
            });
            var heatmap = new google.maps.visualization.HeatmapLayer({
                data: list
              });
            heatmap.setMap(MyMap);
        }
    });    
});