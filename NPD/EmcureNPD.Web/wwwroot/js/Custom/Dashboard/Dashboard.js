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
                <h2 class="no-margins counter" style="color:'+ data.PIDFList[i].statusColor + '">' + data.PIDFList[i].statusCount + '</h2><h5>' + data.PIDFList[i].pidfStatus + '</h5></div></div></div>');

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
function hexToRgbA(hex) {
    var c;
    if (/^#([A-Fa-f0-9]{3}){1,2}$/.test(hex)) {
        c = hex.substring(1).split('');
        if (c.length == 3) {
            c = [c[0], c[0], c[1], c[1], c[2], c[2]];
        }
        c = '0x' + c.join('');
        return 'rgba(' + [(c >> 16) & 255, (c >> 8) & 255, c & 255].join(',') + ',1)';
    }
    throw new Error('Bad Hex');
}
// Version 4.0
const pSBC = (p, c0, c1, l) => {
    let r, g, b, P, f, t, h, i = parseInt, m = Math.round, a = typeof (c1) == "string";
    if (typeof (p) != "number" || p < -1 || p > 1 || typeof (c0) != "string" || (c0[0] != 'r' && c0[0] != '#') || (c1 && !a)) return null;
    if (!this.pSBCr) this.pSBCr = (d) => {
        let n = d.length, x = {};
        if (n > 9) {
            [r, g, b, a] = d = d.split(","), n = d.length;
            if (n < 3 || n > 4) return null;
            x.r = i(r[3] == "a" ? r.slice(5) : r.slice(4)), x.g = i(g), x.b = i(b), x.a = a ? parseFloat(a) : -1
        } else {
            if (n == 8 || n == 6 || n < 4) return null;
            if (n < 6) d = "#" + d[1] + d[1] + d[2] + d[2] + d[3] + d[3] + (n > 4 ? d[4] + d[4] : "");
            d = i(d.slice(1), 16);
            if (n == 9 || n == 5) x.r = d >> 24 & 255, x.g = d >> 16 & 255, x.b = d >> 8 & 255, x.a = m((d & 255) / 0.255) / 1000;
            else x.r = d >> 16, x.g = d >> 8 & 255, x.b = d & 255, x.a = -1
        } return x
    };
    h = c0.length > 9, h = a ? c1.length > 9 ? true : c1 == "c" ? !h : false : h, f = this.pSBCr(c0), P = p < 0, t = c1 && c1 != "c" ? this.pSBCr(c1) : P ? { r: 0, g: 0, b: 0, a: -1 } : { r: 255, g: 255, b: 255, a: -1 }, p = P ? p * -1 : p, P = 1 - p;
    if (!f || !t) return null;
    if (l) r = m(P * f.r + p * t.r), g = m(P * f.g + p * t.g), b = m(P * f.b + p * t.b);
    else r = m((P * f.r ** 2 + p * t.r ** 2) ** 0.5), g = m((P * f.g ** 2 + p * t.g ** 2) ** 0.5), b = m((P * f.b ** 2 + p * t.b ** 2) ** 0.5);
    a = f.a, t = t.a, f = a >= 0 || t >= 0, a = f ? a < 0 ? t : t < 0 ? a : a * P + t * p : 0;
    if (h) return "rgb" + (f ? "a(" : "(") + r + "," + g + "," + b + (f ? "," + m(a * 1000) / 1000 : "") + ")";
    else return "#" + (4294967296 + r * 16777216 + g * 65536 + b * 256 + (f ? m(a * 255) : 0)).toString(16).slice(1, f ? undefined : -2)
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