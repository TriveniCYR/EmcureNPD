"use strict";
//SignalR code
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
connection.on("sendToUser", (articleHeading, articleContent, articleTime) => {
    var heading = document.createElement("h3");
    heading.textContent = articleHeading;

    var p = document.createElement("p");
    p.innerText = articleContent;

    var div = document.createElement("div");
    div.appendChild(heading);
    div.appendChild(p);

    var txtmessage = articleHeading + "<br/>" + articleContent + "<br/>" + articleTime;    
    toastr.options.positionClass = 'toast-bottom-right';
    toastr['info'](txtmessage, 'Information');    
    ////Call realtime notification data
    //getrealtimeNotificationdata();
    //toastr['info']('Here is my client-side Success message', 'Information');
    //document.getElementById("articleList").appendChild(div);
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});