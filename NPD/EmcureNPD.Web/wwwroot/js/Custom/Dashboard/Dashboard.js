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
        $("#PIDFContainer").append("<div class='col-lg-3 col-md-6 col - 12' style='color: "+data.PIDFList[i].statusColor+";'><div class='small-box bg-info'><div class='inner'> <h3 id='totalFinanceApproved'>" + data.PIDFList[i].statusCount +"</h3><p>" + data.PIDFList[i].pidfStatus +"</p></div><div class='icon'> <i class='far fa-save'></i></div></div></div >");
    }
     
    var xValues = ["Completed", "Final Approved", "Final Rejected", "Finance Approved", "Finance Pending Approval", "Finance Rejected", "IPD Created", "IPD/BD Approved", "IPD/BD Pending Approval", "IPD/BD Rejected", "PIDF Approved", "PIDF Created", "PIDF Pending Approval","PIDF Rejected"];
    var yValues = [];
    for (let j = 0; j < data.PIDFList.length;j++) {
        var FilteredList = data.PIDFList.filter(
            function (PIDFList) {
                return PIDFList.pidfStatus == xValues[j]}
        )
        console.log(FilteredList, 'filter')
        if (FilteredList.length > 0) {
            yValues.push(FilteredList[0].statusCount)
        }
    }
    console.log(yValues);
   
    var barColors = [
        "#b91d47",
        "#00aba9",
        "#2b5797",
        "#e8c3b9",
        "#1e7145",
        "#0d6efd",
        "#6610f2",
        "#6f42c1",
        "#d63384",
        "#dc3545",
        "#fd7e14",
        "#ffc107",
        "#198754",
        "#20c997"
        

    ];

    new Chart("chart", {
        type: "pie",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            title: {
                display: true,
                text: "Location Wise"
            }
        }
    });

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

