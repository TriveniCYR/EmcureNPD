$(document).ready(function () { GetAllNotifications(); });
function GetAllNotifications() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetFilteredNotifications + '/CreatedDate/DESC/0/4', 'GET', GetAllNotificationListSuccess, GetAllNotificationListError);
}
function GetAllNotificationListSuccess(data) {
    try {
        let elehtml = '';
        let rowcount = 1;
        if (data != null) {
            $('#NotificationNo').html(data.recordsTotal);
            $('#NotificationCount').html(data.recordsTotal + " Notifications");
            for (var i = 0; i < data.recordsFiltered; i++) {
                /*<span class="badge badge-secondary"><i class="fas fa-envelope mr-1"></i>${rowcount} <b>${data.data[i].notificationTitle}</b></span>*/
                elehtml += `<a href="#" class="dropdown-item">
                    <span class="badge badge-secondary">${rowcount}:<i class="fas fa-envelope mr-1"></i>${data.data[i].notificationTitleView}</span>
                    <span class="float-right text-muted text-sm">${timeDiffrance(data.data[i].createdDate)}</span>
                </a>`
                rowcount++;
            }
            $("#notificationCounter").html(elehtml);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAllNotificationListError(x, y, z) {
    toastr.error(ErrorMessage);
}


//var signalRServer = $.connection.signalRServer;

//signalRServer.client.ShowAllNotification = function () { GetAllNotifications(); }

//$.connection.hub.start();

//GetAllNotifications();

////var connection = new signalR.HubConnectionBuilder().withUrl('signalRServer').configureLogging(signalR.LogLevel.Information).build();
//////connection.start();
////connection.on('SendMessage', () => { GetAllNotifications(); });
//////connection.invoke("SendNotification").catch(function (err) {
//////    return console.error(err.toString());
//////});

////async function start() {
////    try {
////        await connection.start();
////        console.log("SignalR Connected.");
////    } catch (err) {
////        console.log(err);
////        setTimeout(start, 5);
////    }
////};

////connection.onclose(async () => {
////    await start();
////});

////// Start the connection.
////start();
//function timeDiffrance(_dateInput) {
//    let _suffix = "";
//    _dateInput = new Date(_dateInput);
//    const _currentDate = new Date();
//    let diffHour = (_currentDate - _dateInput) / 1000;
//    const diffTime = Math.abs(_currentDate - _dateInput);
//    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
//    diffHour /= (60 * 60);
//    diffHour = Math.abs(Math.round(diffHour));
//    console.log(diffTime + " milliseconds");
//    //millisecond to sec and min
//    let diffMin = Math.floor((diffTime / 1000 / 60) << 0);
//    let diffSec = Math.floor((diffTime / 1000) % 60);

//    console.log(diffDays + " days");
//    if (diffHour >= 24) {
//        diffHour = diffDays;
//        _suffix = "days";
//    }
//    if (diffHour < 24 && diffMin < 60) {
//        diffHour = diffMin;
//        _suffix = "min :" + diffSec + "sec";
//    }
//    else {

//        _suffix = "hour";
//    }
//    return diffHour + "" + _suffix;
//}