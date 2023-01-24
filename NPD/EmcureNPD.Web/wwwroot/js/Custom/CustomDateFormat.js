window.CustomDateFormat = function (current_datetime, formatStyle) {
    const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    current_datetime = new Date(current_datetime);
    let retFormat = "";
    if (formatStyle == 1) {
        let formatted_date = current_datetime.getDate() + "-" + months[parseInt(current_datetime.getMonth())] + "-" + current_datetime.getFullYear()
        retFormat = formatted_date;
    } else if (formatStyle == 2) {
        let formatted_date = current_datetime.getDate() + "-" + months[parseInt(current_datetime.getMonth())] + "-" + current_datetime.getFullYear() + " " + ((String(current_datetime.getHours())).length > 1 ? current_datetime.getHours() : '0' + current_datetime.getHours()) + ":" + ((String(current_datetime.getMinutes())).length > 1 ? current_datetime.getMinutes() : '0' + current_datetime.getMinutes())
        retFormat = formatted_date;
    } else {
        let formatted_date = current_datetime.getDate() + "-" + months[parseInt(current_datetime.getMonth())] + "/" + current_datetime.getFullYear()
        retFormat = formatted_date;
    }
    return scrapeNumbers(retFormat);
}

function arrIndex(fnd, arr) {
    for (var len = arr.length, i = 0; i < len; i++) {
        if (i in arr && arr[i] === fnd) {
            return i;
        }
    }
    return -1;
}

function scrapeNumbers(str) {
    var arr = str.replace(/-+/g, "-").replace(/^-/, "").replace(/-$/, "").split("-");
    for (var i = 0, len = arr.length, rtn = []; i < len; i++) {
        if (i in arr && arrIndex(arr[i], rtn) == -1) {
            rtn.push(arr[i]);
        }
    }
    return rtn.join("-");
}