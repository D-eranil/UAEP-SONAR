if (document.body.contains(document.getElementById('contact_map'))) {
    var marker;
    var map;
    var image = '/assets/images/map-marker.png';
    function initialize() {
        var Lat = $(lat).val();
        var Lon = $(lon).val();
        var Title = $(title).val(); 
        var URL = "https://www.google.com/maps/place/" + Title + "/" + Lat + "/" + Lon + ",17z/data=!4m5!3m4!1s0x3e5e688571a9ea6f:0x9a408a4eb584a94c!8m2!3d24.4519877!4d54.3812232?hl=en";
        var map;
        var bounds = new google.maps.LatLngBounds();
        var mapOptions = {
            mapTypeId: 'roadmap',
            disableDefaultUI: true,
            gestureHandling: 'cooperative',
        };

        // Display a map on the page
        map = new google.maps.Map(document.getElementById("contact_map"), mapOptions);
        map.setTilt(45);
        // Multiple Markers
        var markers = [
            [Title , Lat , Lon]
        ];
        var marker, i;
        for (i = 0; i < markers.length; i++) {
            var position = new google.maps.LatLng(markers[i][1], markers[i][2]);
            bounds.extend(position);
            marker = new google.maps.Marker({
                position: position,
                url: URL,
                map: map,
                icon: image,
                draggable: false,
                animation: google.maps.Animation.DROP,
                title: markers[i][0]
            });
            google.maps.event.addListener(marker, 'click', function() {
                window.open(
                    marker.url,
                    '_blank' // <- This is what makes it open in a new window.
                );


            });
            map.fitBounds(bounds);
        }
        var boundsListener = google.maps.event.addListener((map), 'idle', function(event) {
            this.setZoom(15);
            google.maps.event.removeListener(boundsListener);
        });
    }
    google.maps.event.addDomListener(window, 'load', initialize);
}

//$('#direction').click(function () {
    //var uaepPark = new google.maps.LatLng($(lat).val(), $(lon).val());
    //var UserLoca = 
//});





