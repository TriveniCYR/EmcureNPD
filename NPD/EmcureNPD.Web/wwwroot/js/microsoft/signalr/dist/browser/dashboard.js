﻿"use strict";

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
	connection.invoke("GetNotification").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceiveNotification", function (count) {
	BindLocation(count);
});

function BindLocation(count) {
	alert(count);
	//alert(ongitude);
	
}