window.onload = function load(){

    var icon = L.divIcon({
        className: "my-custom-pin",
        iconAnchor: [0, 24],
        labelAnchor: [-6, 0],
        popupAnchor: [0, -36]
    });


    var geocoder = L.esri.Geocoding.geocodeService();
    var display_pickup_markers;

    var table =  document.getElementById("orders_table_list");

    var order_list = L.map('order_list').setView([39.39870315600007, -99.41461918999994], 3);	
	L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
		maxZoom: 18,
		id: 'mapbox/streets-v11',
		tileSize: 512,
		zoomOffset: -1,
		accessToken: 'pk.eyJ1IjoibWFnZGFsZW5hMzE4IiwiYSI6ImNraGM5cGQ0bjAxMncycW0wbjNoNmdibjgifQ.3o366Xt1v3kTI8x_Q7vNJg'
	}).addTo(order_list);
	var markers_layer = L.layerGroup().addTo(order_list);
	

    var veteran_map = L.map('veteran_location').setView([39.39870315600007, -99.41461918999994], 3);	
	L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
		maxZoom: 18,
		id: 'mapbox/streets-v11',
		tileSize: 512,
		zoomOffset: -1,
		accessToken: 'pk.eyJ1IjoibWFnZGFsZW5hMzE4IiwiYSI6ImNraGM5cGQ0bjAxMncycW0wbjNoNmdibjgifQ.3o366Xt1v3kTI8x_Q7vNJg'
	}).addTo(veteran_map);
	var veteran_marker = L.layerGroup().addTo(veteran_map);
	var veteran_location;
	
    veteran_map.on('click', onveteranMapClick);
    
    var volunteer_map = L.map('volunteer_location').setView([39.39870315600007, -99.41461918999994], 3);	
	L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
		maxZoom: 18,
		id: 'mapbox/streets-v11',
		tileSize: 512,
		zoomOffset: -1,
		accessToken: 'pk.eyJ1IjoibWFnZGFsZW5hMzE4IiwiYSI6ImNraGM5cGQ0bjAxMncycW0wbjNoNmdibjgifQ.3o366Xt1v3kTI8x_Q7vNJg'
	}).addTo(volunteer_map);
	var volunteer_marker = L.layerGroup().addTo(volunteer_map);
	var volunteer_location;
	
	function onveteranMapClick(e){
		veteran_marker.clearLayers();
		
		var marker = L.marker(e.latlng).addTo(veteran_marker);
		veteran_location = e.latlng;
		veteran_map.setView(e.latlng, 13);
		
		geocoder.reverse().latlng(e.latlng).run(function (error, result) {
			if (error) {
				return;
			}
			document.getElementById("veteran_address").value = result.address.LongLabel;	
		});		
    }

    function onvolunteerMapClick(e){
		volunteer_marker.clearLayers();
		
		var marker = L.marker(e.latlng).addTo(volunteer_marker);
		volunteer_location = e.latlng;
		volunteer_map.setView(e.latlng, 13);
		
		geocoder.reverse().latlng(e.latlng).run(function (error, result) {
			if (error) {
				return;
			}
			document.getElementById("volunteer_address").value = result.address.LongLabel;	
		});		
    }
    
    volunteer_map.on('click', onvolunteerMapClick);

    document.getElementById("veteran").onclick = function(){
    document.getElementById("main_form").style.display = "none";
    document.getElementById("veteran_details").style.display = "block";
    veteran_map.invalidateSize();
    }

    document.getElementById("veteran_refresh").onclick = function(){
		var pickup_address = document.getElementById("veteran_address").value;

		if(pickup_address != null && pickup_address != ""){
			L.esri.Geocoding.geocode().text(pickup_address).run(function (error, response) {
				if (error) {
					console.log(err);
					window.alert("The provided address can not be found!");
					return;
				}
				
				var latlng = response.results[0].latlng;
				veteran_map.setView(latlng, 13)
				veteran_marker.clearLayers();
				var marker = L.marker(latlng).addTo(veteran_marker);
				veteran_location = latlng;
			});
			
        }
    }

    document.getElementById("volunteer_refresh").onclick = function(){
		var pickup_address = document.getElementById("volunteer_address").value;

		if(pickup_address != null && pickup_address != ""){
			L.esri.Geocoding.geocode().text(pickup_address).run(function (error, response) {
				if (error) {
					console.log(err);
					window.alert("The provided address can not be found!");
					return;
				}
				
				var latlng = response.results[0].latlng;
				volunteer_map.setView(latlng, 13)
				volunteer_marker.clearLayers();
				var marker = L.marker(latlng).addTo(volunteer_marker);
				volunteer_location = latlng;
			});
			
        }
    }

    document.getElementById("volunteer").onclick = function(){
        document.getElementById("main_form").style.display = "none";
        document.getElementById("volunteer_details").style.display = "block";
        volunteer_map.invalidateSize();
        }

        document.getElementById("veteren_login").onclick = function(){
            document.getElementById("main_form").style.display = "none";
            document.getElementById("veteran_sign_in").style.display = "block";
        }

        document.getElementById("volunteer_login").onclick = function(){
            document.getElementById("main_form").style.display = "none";
            document.getElementById("volunteer_sign_in").style.display = "block";
        }

        document.getElementById("next1").onclick = function store_veteran_value(){
            var mailformat = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if(!document.getElementById("veteran_mail_id").value.match(mailformat))
        { 
            alert("You have entered an invalid email address!")
         }
         else
            {
            console.log("assa");
            //Constructing JSON		
            const data = { 
                "name":document.getElementById("veteran_Full_name").value, 
                "age":document.getElementById("veteran_Age").value, 	
                "mail": document.getElementById("veteran_mail_id").value,
                "pass": document.getElementById("veteran_password").value,
                "location": {
                    "name": document.getElementById("veteran_Full_name").value,
                    "address": document.getElementById("veteran_address").value,
                    "latlng": {
                        "lat": veteran_location.lat,
                        "lng": veteran_location.lng
                    }
                }
            };
        console.log(JSON.stringify(data));
        fetch('https://localhost:44335/api/Veteran', {
			method: 'POST',
			headers: {
			'Content-Type': 'application/json',
			},
			body: JSON.stringify(data),
        })
        .then(response => response.json())
			.then(data => {
                if(data.toString() == "true")
                {
                console.log('Success:', data.toString());
                document.getElementById("veteran_details").style.display = "none";
                document.getElementById("main_form").style.display = "none";
                document.getElementById("register_done").style.display = "block";
                }
                else
                {
                    alert("THE MAIL ID ALREADY EXISTS IN DATABASE");
                }
            })
            .catch((error) => {
              console.error('Error:', error);
            });
    }
}

document.getElementById("next2").onclick = function store_volunteer_value(){
    console.log("assa");
    //Constructing JSON		
    var mailformat = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if(!document.getElementById("volunteer_mail_id").value.match(mailformat))
{ 
    alert("You have entered an invalid email address!")
 }
 else
    {
    const data = { 
        "name":document.getElementById("volunteer_Full_name").value, 
        "age":document.getElementById("volunteer_Age").value, 	
        "mail": document.getElementById("volunteer_mail_id").value,
        "pass": document.getElementById("volunteer_password").value,
        "location": {
            "name": document.getElementById("volunteer_Full_name").value,
            "address": document.getElementById("volunteer_address").value,
            "latlng": {
                "lat": volunteer_location.lat,
                "lng": volunteer_location.lng
            }
        }
                
};
console.log(JSON.stringify(data));
fetch('https://localhost:44335/api/Volunteer', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
})
.then(response => response.json())
    .then(data => {
        if(data.toString() == "true")
        {
        console.log('Success:', data.toString());
        document.getElementById("volunteer_details").style.display = "none";
        document.getElementById("main_form").style.display = "none";
        document.getElementById("register_done").style.display = "block";
        }
        else
        {
            alert("THE MAIL ID ALREADY EXISTS IN DATABASE");
        }
    })
    .catch((error) => {
      console.error('Error:', error);
    });

}
}


document.getElementById("volunteer_submit").onclick = function check_volunteer_value(){
    console.log("assa");
    //Constructing JSON		
    const data = { 	
        "mail": document.getElementById("volunteer_mail").value,
        "pass": document.getElementById("volunteer_pass").value,
        "type": 2      
};
console.log(JSON.stringify(data));
fetch('https://localhost:44335/api/Credentials', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
})
.then(response => response.json())
    .then(data => {
        console.log('Success:', data.toString());
        if(data.toString() == "true")
        {
        document.getElementById("volunteer_sign_in").style.display = "none";
        document.getElementById("select_orders").style.display = "block";
        order_list.invalidateSize();
        hello();
        }
        
        else
        {
            alert("INVALID CREDENTIALS");
        }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}

function hello()
{
        var address = 'https://localhost:44335/api/Items/';
		fetch(address, {
			method: 'GET',
        })
        .then(response => response.json())
			.then(data => {
                console.log(data);
                console.log("HIsasaassa");
                markers_layer.clearLayers();
				for(var i = 0;  i < data.length; i++){	
                    console.log("asss");
					//Closure function to display current data
					var cur_data = data[i];
					function display(cur_data){						
						return function(){
							updatemap(cur_data);
						}
                    };
                    var cur_pickup_lat = Number(cur_data.lat);
					var cur_pickup_lng = Number(cur_data.longitude);					
					var cur_pickup_latlng = L.latLng(cur_pickup_lat, cur_pickup_lng);
                    var cur_pickup_marker = L.marker(cur_pickup_latlng).addTo(markers_layer);
                    
                    
					cur_pickup_marker.on('click', display(cur_data));
        }
    })
    }

function updatemap(data){
    console.log("HI");
    document.getElementById("Order_name").innerText = data.orderno;
		document.getElementById("order_mail").innerText = data.name;
        document.getElementById("order_price").innerText = data.price;
		document.getElementById("Order_items").innerText = data.items;
    var display_pickup_lat = Number(data.lat);
    var display_pickup_long = Number(data.longitude);
        var display_pickup_latlng = L.latLng(display_pickup_lat, display_pickup_long);
        order_list.setView(display_pickup_latlng, 13);
        display_pickup_markers = L.layerGroup().addTo(order_list);
		var display_pickup_marker = L.marker(display_pickup_latlng).addTo(display_pickup_markers);
}


document.getElementById("veteran_submit").onclick = function check_veteran_value(){
    console.log("assa");
    //Constructing JSON		
    const data = { 	
        "mail": document.getElementById("veteran_mail").value,
        "pass": document.getElementById("veteran_pass").value,
        "type": 1       
};
console.log(JSON.stringify(data));
fetch('https://localhost:44335/api/Credentials', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
})
.then(response => response.json())
    .then(data => {
        console.log('Success:', data.toString());
        if(data.toString() == "true")
        {
            
        document.getElementById("veteran_sign_in").style.display = "none";
        document.getElementById("make_orders").style.display = "block";
        }
        else
        {
            alert("INVALID CREDENTIALS")
        }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}

    document.getElementById("home").onclick = function return_home(){
        document.getElementById("main_form").style.display = "block";
        document.getElementById("register_done").style.display = "none";
        document.getElementById("volunteer_pass").style.borderColor = "LightGray";
        document.getElementById("veteran_pass").style.borderColor = "LightGray";
        document.getElementById("volunteer_sign_in").reset();
        document.getElementById("veteran_sign_in").reset();
        document.getElementById("item_added_form").reset();
    }

    document.getElementById("home123").onclick = function return_home(){
        document.getElementById("main_form").style.display = "block";
        document.getElementById("item_added_form").style.display = "none";
        document.getElementById("volunteer_pass").style.borderColor = "LightGray";
        document.getElementById("veteran_pass").style.borderColor = "LightGray";
        document.getElementById("volunteer_sign_in").reset();
        document.getElementById("veteran_sign_in").reset();
        document.getElementById("item_added_form").reset();

    }

    document.getElementById("homego2").onclick = function return_home(){
        document.getElementById("veteran_sign_in").style.display = "none"
        document.getElementById("main_form").style.display = "block";
        document.getElementById("volunteer_sign_in").reset();
        document.getElementById("veteran_sign_in").reset();
        document.getElementById("item_added_form").reset();
    }

    document.getElementById("homego1").onclick = function return_home(){
        document.getElementById("volunteer_sign_in").style.display = "none"
        document.getElementById("main_form").style.display = "block";
        document.getElementById("volunteer_sign_in").reset();
        document.getElementById("veteran_sign_in").reset();
        document.getElementById("item_added_form").reset();
    }

    document.getElementById("returntohome1234").onclick = function return_home(){
        document.getElementById("orders_table").style.display = "none"
        document.getElementById("main_form").style.display = "block";
        document.getElementById("volunteer_sign_in").reset();
        document.getElementById("veteran_sign_in").reset();
        document.getElementById("item_added_form").reset();
    }

    document.getElementById("orders").onclick = function return_home(){
        document.getElementById("main_form").style.display = "none";
        document.getElementById("orders_table").style.display = "block";
        
        var address = 'https://localhost:44335/api/Veteran/';
		fetch(address, {
			method: 'GET',
        })
        .then(response => response.json())
			.then(data => {
                for(var i = 0;  i < data.length; i++){	
                populatetable(data[i]);
                }
            });
        }

        function populatetable(data)
        {
        let row = table.insertRow();
      let items = row.insertCell(0);
      let items_price = row.insertCell(1);
      let veteran = row.insertCell(2);
      let order_id = row.insertCell(3);
      let status = row.insertCell(4);
      let volunteer = row.insertCell(5);
      items.innerHTML = data.items;
      items_price.innerHTML = data.price;
      veteran.innerHTML = data.veteran_mail;
      order_id.innerHTML = data.orderid;
      status.innerHTML = data.Status;
      volunteer.innerHTML = data.volunteer_mail;
    }


    document.getElementById("takeup_order").onclick = function return_home(){
        console.log(document.getElementById("Order_name").textContent);
        const data = { 
            "name": document.getElementById("volunteer_mail").value,
            "orderno": document.getElementById("Order_name").textContent
        };
        console.log(data);
        fetch('https://localhost:44335/api/Acceptance', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
}).then(response => response.json())
.then(data => {
    document.getElementById("select_orders").style.display = "none";
    document.getElementById("select_orders").reset();
        document.getElementById("main_form").style.display = "block";
})
    }

    document.getElementById("sub_order").onclick = function take_data(){
        var arr1 = new Array(30);
        arr1.fill(0);
        var arr2 = new Array(30);
        arr2.fill(0);
        var flag = 0;
        for(var i=0;i<30;i++)
        {   
            var s = "quantity_item";
            s = s + (i+1);
            var t = document.getElementById(s).value;
            if(t>0)
            {
                arr1[i] = 1;
                arr2[i] = t;
                flag = 1;
            }
        }
        if(flag ==1)
        {
        const data = { itemlist : arr1 ,
                        quantity: arr2,
                        mail: document.getElementById("veteran_mail").value};
        fetch('https://localhost:44335/api/Items', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
})
.then(response => response.json())
    .then(data => {
        if(data[0] != 0)
        {
        var s = "The Order has been confirmed , Order number = " + data[1] + ", Price =$" + data[2];
        document.getElementById("item_added_form").style.display = "block";
        document.getElementById("make_orders").style.display = "none";
        document.getElementById("order_added").innerHTML = s;
        }
        else
        {
            alert("WAIT FOR PREVIOUS ORDER TO BE COMPLETED TO MAKE NEXT ORDER");
        }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}
else
{
    alert("NO ITEMS SELECTED");
}
    }
}
