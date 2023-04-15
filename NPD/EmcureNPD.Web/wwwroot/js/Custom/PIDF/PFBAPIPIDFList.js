$(document).ready(function () {
});

function tabClick(action ,val, pidfidval) {
    if (action == 'PBFRnDForm')
        var url = "/PBF/PBFRnDForm?pidfid=" + pidfidval + "&bui=" + val;
    if (action == 'PBFAnalyticalForm')
        var url = "/PBF/PBFAnalyticalForm?pidfid=" + pidfidval + "&bui=" + val;
    if (action == 'PBFClinicalForm')
        var url = "/PBF/PBFClinicalForm?pidfid=" + pidfidval + "&bui=" + val;
    window.location.href = url;
}