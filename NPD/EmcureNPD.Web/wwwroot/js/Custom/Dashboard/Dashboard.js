$(document).ready(function () {
    GetDashboardDropdown();
    GetFinanacialYear();
});

function GetDashboardDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllDashboard, 'GET', GetDashboardDropdownSuccess);
}
function GetDashboardDropdownSuccess(data) {
    try {
        if (data != null) {            
            $(data.MasterBusinessUnits).each(function (index, item) {
                $('#BusinessUnitId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');
            });
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
$('#BusinessUnitId').change(
    function () {
        ajaxServiceMethod($('#hdnBaseURL').val() + GetPIDFList + "/" + $("#BusinessUnitId").val() + "/" + $("#years").val(), 'GET', GetPIDFListSuccess);
    }
)
function GetPIDFListSuccess(data) {
    console.log(data)
    $("#PIDFContainer").html('')
    for (var i = 0; i < data.PIDFList.length;i++) {
        console.log(i)
        $("#PIDFContainer").append("<div class='col-lg-3 col-md-6 col - 12'><div class='small-box bg-info'><div class='inner'> <h3 id='totalFinanceApproved'>" + data.PIDFList[i].statusCount +"</h3><p>" + data.PIDFList[i].pidfStatus +"</p></div><div class='icon'> <i class='far fa-save'></i></div></div></div >");
    }

}

function GetFinanacialYear() {
    var mySelect = $('#years');
    var startYear = 2024;
    var prevYear = 2023;
    for (var i = 0; i < 3; i++) {
        startYear = startYear - 1;
        prevYear = prevYear - 1;
        mySelect.append(
            $('<option></option>').val(prevYear + "-" + startYear).html(prevYear + "-" + startYear)
        );
    }
}

