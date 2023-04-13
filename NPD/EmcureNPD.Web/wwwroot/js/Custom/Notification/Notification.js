$(document).ready(function () {
    GetAllNotifications();
    $(".notification").click(function () {
        ajaxServiceMethod($('#hdnBaseURL').val() + NotificationsClickedByUser, 'GET', GetNotificationClickedSuccess, GetNotificationClickedError);
    });
});
function GetAllNotifications() {
    let ColumnName = "CreatedDate", SortDir = "DESC", start = 0, length = 4;
    ajaxServiceMethod($('#hdnWebBaseURL').val() + GetWebFilteredNotifications + `?ColumnName=${ColumnName}&SortDir=${SortDir}&=start=${start}&length=${length}`, 'GET', GetAllNotificationListSuccess, GetAllNotificationListError);
}
function GetAllNotificationListSuccess(data) {
    try {
        let elehtml = '';
        let rowcount = 1;
        let result = JSON.parse(data)
        if (result != null) {
            if (result.data.length > 0) {
                if (result.data[0].pendingNotification > 0) {
                    $('#NotificationNo').html(result.data[0].pendingNotification);
                } else {
                    $('#NotificationNo').hide();
                }
                
            } else {
                $('#NotificationNo').hide();
            }
            $('#NotificationCount').html(result.recordsTotal + " Notifications");
            for (var i = 0; i < result.recordsFiltered; i++) {
                let notificationTitle = result.data[i].notificationTitle.length > 40 ? result.data[i].notificationTitle.slice(0, 39) + '...' : result.data[i].notificationTitle;
                /*<span class="badge badge-secondary"><i class="fas fa-envelope mr-1"></i>${rowcount} <b>${data.data[i].notificationTitle}</b></span>*/
                elehtml += `<a href="#" class="dropdown-item">
                    <span class="badge badge-secondary" title=${result.data[i].notificationTitle}>${rowcount}:<i class="fas fa-envelope mr-1"></i>${notificationTitle}</span>
                    <span class="float-right text-muted text-sm">${timeDiffrance(result.data[i].createdDate)}</span>
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

function GetNotificationClickedSuccess(data) {
    try {
        let result = JSON.parse(data)
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetNotificationClickedError(x, y, z) {
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
function timeDiffrance(_dateInput) {
    let _suffix = "";
    _dateInput = new Date(_dateInput);
    const _currentDate = new Date();
    let diffHour = (_currentDate - _dateInput) / 1000;
    const diffTime = Math.abs(_currentDate - _dateInput);
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    diffHour /= (60 * 60);
    diffHour = Math.abs(Math.round(diffHour));
    //millisecond to sec and min
    let diffMin = Math.floor((diffTime / 1000 / 60) << 0);
    let diffSec = Math.floor((diffTime / 1000) % 60);
    if (diffHour >= 24) {
        diffHour = diffDays;
        _suffix = "days";
    }
  else  if (diffHour < 24 && diffMin < 60) {
        diffHour = diffMin;
        _suffix = "min :" + diffSec + "sec";
    }
   else if (diffHour < 24) {

        _suffix = "hour";
    }
    return diffHour + "" + _suffix;
}