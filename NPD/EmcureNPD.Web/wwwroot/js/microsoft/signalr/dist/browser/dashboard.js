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
	let count = 100;
	connection.invoke('GetNotification', count).catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceiveNotification", function (count) {
	BindLocation(count);
});

function BindLocation(count) {
	//alert(count);
	//alert(ongitude);
	
}