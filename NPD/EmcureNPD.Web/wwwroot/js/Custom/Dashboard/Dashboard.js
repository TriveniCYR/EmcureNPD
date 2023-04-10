var markers = [];
var graph;
$(document).ready(function () {
    GetDashboardDropdown();
    GetFinanacialYear();
    /*GetPIDFReport();*/
    //$('#BusinessUnitId').change(
    //    function () {
    //        ajaxServiceMethod($('#hdnBaseURL').val() + GetPIDFList + "/" + $("#BusinessUnitId").val() + "/" + $("#years").val(), 'GET', GetPIDFListSuccess);
    //    }
    //);
    //$('#years').change(
    //    function () {
    //        ajaxServiceMethod($('#hdnBaseURL').val() + GetPIDFList + "/" + $("#BusinessUnitId").val() + "/" + $("#years").val(), 'GET', GetPIDFListSuccess);
    //    }
    //);
});
function GetPIDFReport() {
    let currentYear = new Date().getFullYear();
    let fromDate = `04-01-1970`;
    let toDate = `03-31-${currentYear}`;
    var _businessUnitId = ($("#BusinessUnitId").val() == null || $("#BusinessUnitId").val() == undefined ? "0" : $("#BusinessUnitId").val());
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPIDFList + "/" + _businessUnitId + "/" + fromDate + "/" + toDate, 'GET', GetPIDFListSuccess);
}
function GetDashboardDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllDashboard, 'GET', GetDashboardDropdownSuccess);
}
function GetDashboardDropdownSuccess(data) {
    try {
        $('#BusinessUnitId').append('<option value="0">-- All Business Unit --</option>');
        if (data != null) {            
            $(data.MasterBusinessUnits).each(function (index, item) {
                $('#BusinessUnitId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');

                var marker = {
                    latLng: [item.latitude, item.longitude],
                    name: item.businessUnitName,
                    bUnitId: item.businessUnitId
                };
                markers.push(marker);
            });
            getDashBoardData();
        }
        RenderVectorMap();
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPIDFListSuccess(data) {
    $("#PIDFContainer").html('');

    var xValues = [];
    var yValues = [];
    var barColors = [];
    
    for (var i = 0; i < data.PIDFList.length;i++) {
        $("#PIDFContainer").append('<div class="col-lg-2 text-center dashboard-counters"><div class="ibox"><div class="ibox-content" style="padding:10px 10px;">\
                <h2 class="no-margins counter" style="color:'+ data.PIDFList[i].statusColor + ';">' + data.PIDFList[i].statusCount + '</h2><h5>' + data.PIDFList[i].pidfStatus + '</h5></div></div></div>');

        xValues.push(data.PIDFList[i].pidfStatus);
        yValues.push(data.PIDFList[i].statusCount);
        barColors.push(data.PIDFList[i].statusColor);
    }

    if (graph != null && graph != undefined) {
        graph.destroy();
    }

    var ctx = $('#cPIDFChart');
    graph = new Chart(ctx, {
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
                text: "Status Wise"
            }
        }
    });

}
function GetFinanacialYear() {
    var mySelect = $('#years');
    var _currentDate = new Date();
    var minDate = new Date("1-1-1970");

    var startYear = _currentDate.getFullYear();
    var prevYear = _currentDate.getFullYear() - 1;
    if (_currentDate > new Date("" + _currentDate.getFullYear() +"/03/31")) {
        startYear = _currentDate.getFullYear() + 1;
        prevYear = _currentDate.getFullYear();
    }

    mySelect.append('<option value="' + minDate.getFullYear() + "-" + startYear+'">-- Select Financial Year --</option>');
    for (var i = 0; i < 3; i++) {
        mySelect.append(
            $('<option></option>').val((prevYear - i) + "-" + (startYear - i)).html((prevYear - i) + "-" + (startYear - i))
        );
    }
}
function RenderVectorMap() {
    $(function () {
        var map = $('#cPIDFMap').vectorMap({
            map: 'world_mill_en',
            scaleColors: ['#C8EEFF', '#0071A4'],
            normalizeFunction: 'polynomial',
            hoverOpacity: 0.7,
            hoverColor: false,
            regionStyle: {
                initial: {
                    fill: '#d2d6de',
                    stroke: '#000000',
                    "stroke-width": .5,
                    "stroke-opacity": 1
                },
                hover: {
                    fill: '#A0D1DC'
                }
            },
            backgroundColor: "transparent",
            markerStyle: {
                initial: {
                    fill: '#eb3434',
                    stroke: '#383f47'
                }
            },
            series: {
                markers: markers
            },
            onMarkerClick: function (event, index) {
                var markerData = markers[index];
                $('#BusinessUnitId').val(markerData.bUnitId).change();
                getDashBoardData();
                /*$('#BusinessUnitId').change();*/
            }
        }).vectorMap('get', 'mapObject');

        map.addMarkers(markers);
        // refresh the map
        map.updateSize();

    });
}

//date range
function dateRange() {
    //var dateRangeButton = document.getElementById("date-range-input");
    var yearsField = document.getElementById("years-field");
    var dateRangeFields = document.getElementById("date-range-fields");
    yearsField.style.display = "none";
    dateRangeFields.style.display = "block";
    $("#years").val("");
}
function closeDateRange() {
    var yearsField = document.getElementById("years-field");
    var dateRangeFields = document.getElementById("date-range-fields");
    yearsField.style.display = "block";
    dateRangeFields.style.display = "none";
    var currentYear = new Date().getFullYear();
    $("#years").val(`1970-${currentYear}`);
}
function getDashBoardData() {
    var dateRangeOrYear = "";
    var fromDate;
    var toDate;
    if ($("#years").val() != null) {
        dateRangeOrYear = $("#years").val();
        const [fromYear, toYear] = dateRangeOrYear.split("-");
        fromDate = `04-01-${fromYear}`;
        toDate = `03-31-${toYear}`;
    }
    else if ($("#years").val() == null) {
        var FromDate = $('#from-date').val();
        var ToDate = $('#to-date').val();
        var fromDate = new Date(FromDate).toLocaleDateString('en-US', {
            month: '2-digit',
            day: '2-digit',
            year: 'numeric'
        }).replace(/\//g, '-');
        var toDate = new Date(ToDate).toLocaleDateString('en-US', {
            month: '2-digit',
            day: '2-digit',
            year: 'numeric'
        }).replace(/\//g, '-');
    }
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPIDFList + "/" + $("#BusinessUnitId").val() + "/" + fromDate + "/" + toDate, 'GET', GetPIDFListSuccess);
}

