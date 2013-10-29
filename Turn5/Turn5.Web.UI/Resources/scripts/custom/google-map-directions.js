if (typeof Turn5 === "undefined") Turn5 = {};
if (typeof Turn5.Directions === "undefined") Turn5.Directions = {};
if (typeof Turn5.DirectionsMap === "undefined") Turn5.DirectionsMap = {};
if (typeof Turn5.DirectionsDisplay === "undefined") Turn5.DirectionsDisplay = {};
if (typeof Turn5.DirectionsService === "undefined") Turn5.DirectionsService = {};

/* Initialize maps */
Turn5.Directions.InitializeMapDirections = function(mapData, containerId) {
    
    if (typeof mapData != 'undefined') {
        Turn5.DirectionsDisplay = new google.maps.DirectionsRenderer();
        Turn5.DirectionsService = new google.maps.DirectionsService();

        var myLatLng = new google.maps.LatLng(mapData.Center.Latitude, mapData.Center.Longitude);
        var myOptions = {
            zoom: 14,
            center: myLatLng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        var map = new google.maps.Map($("#" + containerId)[0], myOptions);
        Turn5.DirectionsDisplay.setMap(map);

        centerMarker = new google.maps.Marker({
            position: myLatLng,
            map: map,
            icon: new google.maps.MarkerImage("../../images/v2/maps/map_markers.png",
                new google.maps.Size(38, 52),
                new google.maps.Point(0, 0),
                new google.maps.Point(19, 52))
        });
    }
    
};

Turn5.Directions.InitializeMapDirectionsPrint = function(mapData, containerId) {
    
    if (mapData.Markers != "undefined") {
        if (mapData.Markers.length > 0) {
            if (mapData.Markers[0].Address != null) {
                Turn5.Directions.DrawRoute(mapData, containerId, true);
            } else {
                Turn5.Directions.DrawCurrentLocation(mapData, containerId);
            }
        }
    }
    
};


/* ######################################################################################################################################################################## */


Turn5.Directions.DrawCurrentLocation = function(mapData, containerId) {

    var latLng = new google.maps.LatLng(mapData.Markers[1].Latitude, mapData.Markers[1].Longitude);
    var options = {
        zoom: 15,
        center: latLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: true,
        draggable: false
    };
    Turn5.DirectionsMap = new google.maps.Map($("#" + containerId)[0], options);
    centerMarker = new google.maps.Marker({
        position: latLng,
        map: Turn5.DirectionsMap,
        icon: new google.maps.MarkerImage("../../../images/v2/maps/map_markers.png",
                                                                    new google.maps.Size(38, 52),
                                                                    new google.maps.Point(0, 0),
                                                                    new google.maps.Point(19, 52))
    });
    Turn5.DirectionsDisplay.setMap(Turn5.DirectionsMap);

};

Turn5.Directions.DrawRoute = function(mapData, containerId, printMode) {

    Turn5.DirectionsDisplay = new google.maps.DirectionsRenderer();
    Turn5.DirectionsService = new google.maps.DirectionsService();   
    
    var myOptions = {
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: true,
        draggable: false
    };
    
    if (mapData.Center != "undefined") {
        var centerLatLng = new google.maps.LatLng(mapData.Center.Latitude, mapData.Center.Longitude);
        myOptions.center = centerLatLng;
    }
    
    Turn5.DirectionsMap = new google.maps.Map($("#" + containerId)[0], myOptions);
    Turn5.DirectionsDisplay.setMap(Turn5.DirectionsMap);
    
    if (!printMode) {
        
        eval(mapData.DirectionsLinkTrackingCode);
        
        // Kill previous directions display if present
        $('#divTripDirections ol').remove();

        var directionsLink = $(".buttons");
        if (directionsLink != null && directionsLink != undefined){
            directionsLink.removeClass( "buttons" );
            directionsLink.addClass( "buttons-alt" );
            $(".buttons-alt > #getDirectionsLink").val("Recalculate Directions");
            $(".buttons-alt > #getDirectionsLink").text("Recalculate Directions");
        }

        // Generate request
        var start = Turn5.Directions.getCurrentStart();
        var end = Turn5.Directions.getCurrentEnd();
        var saveLocation = $("#StartAddressTextBox").css("display") == "block"
                                ? start
                                : end;
        var trvlMode = $("#routeShortestDrivingTime").attr("checked")
                                ? google.maps.DirectionsTravelMode.DRIVING
                                : google.maps.DirectionsTravelMode.WALKING;
        var request = {
            origin: start,
            destination: end,
            travelMode: trvlMode
        };

        // Make request
        Turn5.DirectionsService.route(request, function (result, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                // Save location in cookies
                Turn5.Directions.saveLastLocation(saveLocation);
                // Display route
                Turn5.DirectionsDisplay.setDirections(result);
                // Remove default marker
                centerMarker.setMap(null);
                var route = result.routes[0];
                Turn5.Directions.clearRoute();

                for (var i = 0; i < route.legs.length; i++) {
                    var leg = route.legs[i];

                    $("#lblTripDistance").text(leg.distance.text);
                    $("#lblTripLength").text(leg.duration.text);

                    var dirList = document.createElement("ol");

                    $("#divTripDirections").append(dirList);

                    for (var j = 0; j < leg.steps.length; j++) {
                        var step = leg.steps[j];
                        dirList.appendChild(Turn5.Directions.BuildDirectionsStep(step.instructions, j + 1, step.distance.text));
                    }
                }
                
            }
        });

    } else {
        
        var start = mapData.Markers[0].Address;
        var end = mapData.Markers[1].Address;
        var trvlMode = "";
        if (mapData.Route == "0") {
            trvlMode = google.maps.DirectionsTravelMode.DRIVING;
        } else {
            trvlMode = google.maps.DirectionsTravelMode.WALKING;
        }

        var request = {
            origin: start,
            destination: end,
            travelMode: trvlMode
        };

        Turn5.DirectionsService.route(request, function(result, status) {

            if (status == google.maps.DirectionsStatus.OK) {

                Turn5.DirectionsDisplay.setDirections(result);

                var route = result.routes[0];

                for (var i = 0; i < route.legs.length; i++) {

                    var leg = route.legs[i];

                    $("#lblTripDistance").text(leg.distance.text);
                    $("#lblTripLength").text(leg.duration.text);

                    var directionsList = document.createElement("ol");
                    $("#divTripDirections").append(directionsList);

                    for (var j = 0; j < leg.steps.length; j++) {
                        var step = leg.steps[j];
                        directionsList.appendChild(Turn5.Directions.BuildDirectionsStep(step.instructions, j + 1, step.distance.text));
                    }
                }
                $("#divTrip").fadeIn("fast");
            }
        });
        
    }
};

Turn5.Directions.BuildDirectionsStep = function(instructions, sequence, distance) {

    var row = document.createElement("li");

    var instr = document.createElement("div");
    instr.setAttribute("class", "dirStepText");
    instr.innerHTML = instructions;

    var dist = document.createElement("div");
    dist.setAttribute("class", "dirStepDist");
    dist.innerHTML = distance;

    var clearRow = document.createElement("br");
    clearRow.setAttribute("class", "clear");

    row.appendChild(instr);
    row.appendChild(dist);
    row.appendChild(clearRow);
    return row;

};



/* Helper functions */

Turn5.Directions.ValidateRoute = function(startTextBoxId, endTextBoxId) {

    if (!Turn5.Directions.IsFormValid(startTextBoxId, endTextBoxId)) {
        if (!Turn5.Directions.IsReverse(endTextBoxId)) {
            $("ul.error li.error-message").html("Sorry, but we need an address in the <strong>START</strong> box so we can search for directions.  Please try your search again.");
        } else {
            $("ul.error li.error-message").html("Sorry, but we need an address in the <strong>END</strong> box so we can search for directions.  Please try your search again.");
        }
        $(".error-messages").show();
        return false;
    } else {
        $(".error-messages").hide();
    }
    return true;
    
};

Turn5.Directions.IsFormValid = function(startTextBoxId, endTextBoxId) {
    
    if (Turn5.Directions.IsReverse(endTextBoxId)) {
        return $("#" + endTextBoxId).val().length > 0;
    } else {
        return $("#" + startTextBoxId).val().length > 0;
    }
    
};

Turn5.Directions.IsReverse = function(endTextBoxId) {
    
    return $("#" + endTextBoxId).css("display") == "block";
    
};

Turn5.Directions.getCurrentStart = function() {
    
    if ($("#StartAddressLabel").css("display") != "none")
        return $("#StartAddressLabel").text();
    else
        return $("#StartAddressTextBox").val();
    
};

Turn5.Directions.getCurrentEnd = function() {
    
    if ($("#EndAddressLabel").css("display") != "none")
        return $("#EndAddressLabel").text();
    else
        return $("#EndAddressTextBox").val();
    
};

Turn5.Directions.saveLastLocation = function(value) {
    
    var values = jQuery.cookie_get("userPreference", "BusinessWhere");
    var a = new Array();
    a.push(escape(value).toLowerCase());

    if (values != null) {
        for (i = 0; i < values.length; i++) {
            var oldVal = Turn5.Directions.TrimForComparison(unescape(values[i])).toLowerCase();
            var newVal = Turn5.Directions.TrimForComparison(unescape(a[0]));
            if (oldVal != newVal && values[i].toLowerCase().length > 0) {
                if (value != unescape(values[i]).replace(/\+/g, " ")) {
                    a.push(values[i]);
                }
            }
        }
    }

    while (a.length > 10)
        a.pop();

    jQuery.cookieValue_set("userPreference", "BusinessWhere", a.join(escape("|").toLowerCase()));
    
};

Turn5.Directions.TrimForComparison = function(value) {
    
    var re = /[\.,\-\s\+]/gi;
    return value.toString().replace(re, "");
    
};

Turn5.Directions.printDirections = function(printLink) {
    
    var route = $("input[name='route']");
    var routeVal = "0";
    for (var i = 0; i < route.length; i++) {
        if (route[i].checked == true) {
            routeVal = route[i].value;
            break;
        }
    }

    var start = Turn5.Directions.IsReverse() ? $("#EndAddressTextBox").val() : $("#StartAddressTextBox").val()
    var end = Turn5.Directions.IsReverse() ? $("#StartAddressLabel").text() : $("#EndAddressLabel").text();

    var printUrl = printLink.href + "&from=" + start + "&to=" + end + "&route=" + routeVal; // + "&reverse=" + (IsReverse() ? "1" : "0");
    window.open(printUrl, "_blank", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=yes, width=900, height=800");
    return false;
    
};

Turn5.Directions.clearRoute = function () {
    
    $("#lblTripDistance").val("");
    $("#lblTripLength").val("");
    $("#divTripDirections").val("");
    
};

Turn5.Directions.omnitureUpdateStep = function(step) {
    if (typeof(s) != "undefined") {
        s.pageName = "directions:" + step.toString();
        s.prop1 = s.pageName;
        s.eVar1 = s.pageName;
        s.eVar20 = s.pageName;
        s.eVar29 = s.pageName;

        s_code = s.t();
        if (s_code) {
            document.write(s_code);
        }
    }
};

Turn5.Directions.ShowDirections = function() {
    $("#divTrip").show();
    $("#map_canvas").hide();

    Turn5.Directions.MakeInActive($("#printMapAndDirections")[0]);
    Turn5.Directions.MakeInActive($("#printMap")[0]);
    Turn5.Directions.MakeActive($("#printDirections")[0]);
};

Turn5.Directions.ShowMap = function() {
    $("#divTrip").hide();
    $("#map_canvas").show();

    Turn5.Directions.MakeInActive($("#printMapAndDirections")[0]);
    Turn5.Directions.MakeActive($("#printMap")[0]);
    Turn5.Directions.MakeInActive($("#printDirections")[0]);
};

Turn5.Directions.ShowMapAndDirections = function() {
    $("#divTrip").show();
    $("#map_canvas").show();

    Turn5.Directions.MakeActive($("#printMapAndDirections")[0]);
    Turn5.Directions.MakeInActive($("#printMap")[0]);
    Turn5.Directions.MakeInActive($("#printDirections")[0]);
};

Turn5.Directions.MakeActive = function(element) {
    $(element).removeClass("print-options-in-active").addClass("print-options-active");
};

Turn5.Directions.MakeInActive = function(element) {
    $(element).removeClass("print-options-active").addClass("print-options-in-active");
};