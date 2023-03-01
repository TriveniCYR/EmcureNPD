$(document).ready(function () { GetAllNotifications();  });
function GetAllNotifications() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllNotificationURL, 'POST', GetAllNotificationListSuccess, GetAllNotificationListError);
}
function GetAllNotificationListSuccess(data) {
    try {
        if (data != null) {
            $('#NotificationNo').html(data.recordsTotal);
            $('#NotificationCount').html(data.recordsTotal + " Notification/s");
        }   
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAllNotificationListError(x, y, z) {
    toastr.error(ErrorMessage);
}

var signalRServer = $.connection.signalRServer;

signalRServer.client.ShowAllNotification = function () { GetAllNotifications(); }

$.connection.hub.start();

GetAllNotifications();

//var connection = new signalR.HubConnectionBuilder().withUrl('signalRServer').configureLogging(signalR.LogLevel.Information).build();
////connection.start();
//connection.on('SendMessage', () => { GetAllNotifications(); });
////connection.invoke("SendNotification").catch(function (err) {
////    return console.error(err.toString());
////});

//async function start() {
//    try {
//        await connection.start();
//        console.log("SignalR Connected.");
//    } catch (err) {
//        console.log(err);
//        setTimeout(start, 5);
//    }
//};

//connection.onclose(async () => {
//    await start();
//});

//// Start the connection.
//start();
