"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

$(function () {
	connection.start().then(function () {
		/*alert('Connected to NotificationHub');*/

		InvokePostLocation();


	}).catch(function (err) {
		return console.error(err.toString());
	});
});

// Product
function InvokePostLocation() {
	connection.invoke("GetLocation").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceiveLocation", function (latitude, longitude) {
	BindLocation(latitude, longitude);
});

function BindLocation(latitude, longitude) {
	//alert(latitude);
	//alert(ongitude);
	
}