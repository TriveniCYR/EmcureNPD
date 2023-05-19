var SumOfSales = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
var SumOfCOGS = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
var SumOfGC = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
var Expiries_Yearwise_Data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
var AnnualConfirmatoryRelease_Data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

$(document).ready(function () {
    $('#btnExportToPdf').click(() => {
        //$('#btnExportToPdf').css("display", "none");
        //$('aside.main-sidebar.sidebar-dark-primary.elevation-4').css("display", "none");
        //$("i.fas.fa-bars").css("display", "none");
        //$("span#NotificationNo.badge.badge-warning.navbar-badge").css("display", "none");
        ////document.getElementById("frmMA")
        //var pdf = new jsPDF('p', 'pt', 'a4');
        //pdf.addHTML(document.body, function () {
        //    pdf.save('PIDFManagementApproval.pdf');
        //});
        //$('#btnExportToPdf').css("display", "block");
        //$('aside.main-sidebar.sidebar-dark-primary.elevation-4').css("display", "block");
        //$("i.fas.fa-bars").css("display", "block");
        //$("span#NotificationNo.badge.badge-warning.navbar-badge").css("display", "block");

        /**/

        let width = $("#frmMA").width();
        let options = {
            pagesplit: true,
            'background': "#ffffff"
            /* 'width': width*/
            //'fontname':"Times-Roman",
            //'fontsize':"15"
        }

        let pdf = new jsPDF('p', 'pt', 'a4');
        //let pdf = new jsPDF('p', 'mm', 'a4');
        pdf.addHTML($("#frmMA"), options, function () {
            pdf.save('PIDFManagementApproval.pdf');
        });
    });

    $('#btnPrintPDF').click(function () {
        var ProjectName = $("#lblProjectName").text();
        var tdate = new Date();
        var dd = tdate.getDate(); //yields day
        var MM = tdate.getMonth(); //yields month
        var yyyy = tdate.getFullYear(); //yields year
        var hh = tdate.getHours();
        var mins = tdate.getMinutes();
        var currentDate = dd + "-" + (MM + 1) + "-" + yyyy + "-" + hh + "-" + mins;
        var fileName = 'ManagementApproval_' + (ProjectName == null || ProjectName == undefined ? "" : ProjectName.toString().trim()) + '_' + currentDate;
        printElement(fileName);
    });
    PBFtabClick();
    GetSUIMSVolumeYearWiseByPackSize();
});
function Project_ChartertabClick() {
    $('.PBFCharter').hide();
    $('#dvPBFCharter').addClass('active');

    $('.ProjectCharter').show();    
    $('#dvProjectCharter').removeClass('active');
}

function PBFtabClick() {
    $('.ProjectCharter').hide();
    $('#dvProjectCharter').addClass('active');

    $('.PBFCharter').show();
    $('#dvPBFCharter').removeClass('active');
}

function GetSUIMSVolumeYearWiseByPackSize() {

    let packSizeId = '0';
    let strengthId = '0';
    ajaxServiceMethod($('#hdnBaseURL').val() + `api/PidfFinance/GetSUIMSVolumeYearWiseByPackSize/${PidafId}/${encBuid}/${strengthId}/${packSizeId}`, 'GET', SUIMSVolumeYearWiseByPackSizeSuccess, SUIMSVolumeYearWiseByPackSizeError);
    function SUIMSVolumeYearWiseByPackSizeSuccess(data) {
        try {
            if (data.table1.length > 0)
                UpdateDynamicTextBoxValues(data.table1);

            //if (data.table2.length > 0)
            // UpdateSUM_of_Values(data.table2);
        }
        catch (e) {
            toastr.error('Error:' + e.message);
        }
    }
    function SUIMSVolumeYearWiseByPackSizeError() {
        toastr.error("Error");
    }
}

function UpdateDynamicTextBoxValues(table) {
    if (table.length == 10) {
        for (var i = 0; i < 10; i++) {
            Expiries_Yearwise_Data[i] = table[i].expiries;
            AnnualConfirmatoryRelease_Data[i] = table[i].annualConfirmatoryRelease;
        }
    }
}

function UpdateSUM_of_Values(table) {
    if (table.length == 10) {
        for (var i = 0; i < 10; i++) {
            SumOfSales[i] = table[i].sumOfSales;
            SumOfCOGS[i] = table[i].sumOfCOGS;
            SumOfGC[i] = table[i].sumOfGC;
        }
    }
}

function RenderFinanceProjection() {
    $('#tblFinanceProjection').html('');

    let html = "";
    let _startDate = $('#ProjectStartDate').val();
    let _launchDate = $('#ProductLaunchDate').val();
    if (_startDate != "" && _launchDate != "") {
        let _ProjectStartDate = new Date(_startDate);
        let _ProjectLaunchDate = new Date(_launchDate);
        let tentativeStartDate = _ProjectStartDate;

        tentativeStartDate.setMonth(_ProjectStartDate.getMonth() - 3);

        let _FinancialYearStartDate = new Date(tentativeStartDate.getFullYear() + "/03/31");
        let _BeginYear = tentativeStartDate.getFullYear();
        if (_FinancialYearStartDate > tentativeStartDate) {
            _BeginYear = tentativeStartDate.getFullYear() - 1;
        }

        var _financialYearLaunchDate = new Date(_ProjectLaunchDate.getFullYear() + "/03/31");
        var _SalesBeginYear = _financialYearLaunchDate.getFullYear();
        if (_financialYearLaunchDate < _ProjectLaunchDate) {
            _SalesBeginYear = _ProjectLaunchDate.getFullYear() + 1;
        }


        html += '<thead class="bg-light">';
        html += '<tr><th colspan="3">Finance Projection</th>';
        var counter = 0;
        for (var i = 0; i < 10; i++) {
            if ((_BeginYear + i) >= _SalesBeginYear) {
                counter++;
            }
            html += "<th class='thYearCounter'>" + (counter > 0 ? counter : "-") + "</th>";
        }
        html += "</tr>";
        var Projection_Year_data = [];
        html += "<tr><th colspan='3' >NPV calculations CAD</th>";
        for (var i = 0; i < 10; i++) {
            html += "<th>" + "Mar-" + (_BeginYear + i).toString().substr(-2) + "</th>";
            Global_Projection_Year_data.push((_BeginYear + i).toString());
            Projection_Year_data.push((_BeginYear + i).toString(), '02', '31');
        }

        html += "</tr>";
        html += "</thead><tbody>";

        html += '<tr><td colspan="3" >#Operating Months</td>';
        var Operating_Months_Projection_data = [];
        for (var i = 0; i < 10; i++) {
            var _projectionDate = new Date((_BeginYear + i) + "/03/31");
            const diffTime = Math.abs(_projectionDate - _ProjectLaunchDate);
            const diffMonths = Math.round(diffTime / (1000 * 60 * 60 * 24 * 30));
            let result = ((_projectionDate < _ProjectLaunchDate) ? "0" : (diffMonths > 12 ? 12 : diffMonths));
            Operating_Months_Projection_data.push(result);
            html += "<td class='trRevenueMonths'>" + result + "</td>";
        }
        html += "</tr>";
        $('#tblFinanceProjection').html(html);
        //    var SumOfGrossSales = SumOfSales;
        //    /*---------------Gross Sales--------------------------*/
        //    html += "<tr class='bg-light'><td colspan='3'>Gross Sales</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = SumOfGrossSales[i];
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*---------------Net Sales--------------------------*/
        //    html += "<tr class='bg-light'><td colspan='3' >Net Sales</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = SumOfSales[i];
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    /*------------[COGS]-Cost of goods sold--------------------------*/
        //    html += "<tr class=''><td colspan='3' >Cost of goods sold</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = SumOfCOGS[i];
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*---------------Expiries-----------------------*/
        //    html += "<tr class='Expiries'><td colspan='3' >Expiries</td>";
        //    for (var i = 0; i < 10; i++) {
        //        html += "<td> <input onchange='ExpiriesValueChange(this," + i + ");' type='number' name='" + Global_Projection_Year_data[i] + "' value='" + Expiries_Yearwise_Data[i] + "' class='form-control Expiriestxtbox UpdateProjectionCommercial' id='ProjectionExpiries_" + i + "' > </td>";
        //    }
        //    html += "</tr>";
        //    /*-----------[GC]-Gross margin--------------------------*/
        //    var GC_Projection = [];
        //    html += "<tr class='lblHeading'><td colspan='3' >Gross margin</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = SumOfSales[i] - (SumOfCOGS[i] + formatToNumber(Expiries_Yearwise_Data[i], true));
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //        GC_Projection.push(result);
        //    }
        //    html += "</tr>";
        //    /*-----------[GC%]-Gross margin%--------------------------*/
        //    var CGPercentage = [];
        //    html += "<tr class='lblHeading percentage'><td colspan='3' >Gross margin%</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = GC_Projection[i] / SumOfSales[i];
        //        result = (Math.abs(result) == Infinity || isNaN(result) || result == -Infinity) ? 0 : result;
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //        CGPercentage.push(result);
        //    }
        //    html += "</tr>";
        //    html += "<tr class='lblHeading bg-light'><td colspan='13' >Expenses as defined as per profit share agreement</td>";

        //    /*-----------MA Annual fees--------------------------*/
        //    var Marketing_Allowance_Value = $('#MarketingAllowance').val(); // G20
        //    html += "<tr class='lblHeading'><td colspan='3' >MA Annual fees</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Marketing_Allowance_Value * SumOfGrossSales[i];
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*---------------Annual confirmatory release testing-----------------------*/
        //    html += "<tr class='Expiries'><td colspan='3' >Annual confirmatory release testing</td>";
        //    for (var i = 0; i < 10; i++) {
        //        html += "<td> <input type='number' onchange='AnnualconfirmatoryValueChange(this," + i + ");' name='" + Global_Projection_Year_data[i] + "' value='" + AnnualConfirmatoryRelease_Data[i] + "' class='form-control AnnualConfirmatoryRtxtbox UpdateProjectionCommercial' id='ProjectionAnnualConfirmatoryRelease_" + i + "' > </td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Opex--------------------------*/
        //    var Opexasapercenttosale = $('#Opexasapercenttosale').val(); // G21
        //    html += "<tr class='lblHeading'><td colspan='3' >Opex</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Opexasapercenttosale * SumOfGrossSales[i];
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*----------- EBITDA - before Profit share--------------------------*/
        //    var EBITDA_projection = [];
        //    html += "<tr class='lblHeading'><td colspan='3' > EBITDA - before Profit share</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = GC_Projection[i] - ((Marketing_Allowance_Value * SumOfGrossSales[i]) + formatToNumber(AnnualConfirmatoryRelease_Data[i], true) + (Opexasapercenttosale * SumOfGrossSales[i]))
        //        EBITDA_projection.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";

        //    }
        //    html += "</tr>";
        //    /*-----------EBITDA % (before PS)--------------------------*/
        //    html += "<tr class='lblHeading percentage'><td colspan='3' > EBITDA % (before PS)</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = EBITDA_projection[i] / SumOfSales[i];
        //        result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Profit Share--------------------------*/
        //    var Profit_Share_Value = $('#ExternalProfitSharepercent').val(); // G16
        //    html += "<tr class='lblHeading'><td colspan='3' > Profit Share</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = EBITDA_projection[i] * Profit_Share_Value;
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' ></td>";
        //    /*-----------MA Annual fees--------------------------*/
        //    var Marketing_Allowance_Value = $('#MarketingAllowance').val(); // G20
        //    html += "<tr class='lblHeading'><td colspan='3' >MA Annual fees</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Marketing_Allowance_Value * SumOfGrossSales[i];
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Net Income - PBT--------------------------*/
        //    var Net_Income_PBT_projection_data = [];
        //    html += "<tr class='lblHeading'><td colspan='3' > Net Income - PBT</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = EBITDA_projection[i] - (EBITDA_projection[i] * Profit_Share_Value) + (Marketing_Allowance_Value * SumOfGrossSales[i]);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //        Net_Income_PBT_projection_data.push(result);
        //    }
        //    html += "</tr>";
        //    /*-----------% Margin--------------------------*/
        //    html += "<tr class='lblHeading percentage'><td colspan='3' >% Margin</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Net_Income_PBT_projection_data[i] / SumOfSales[i];
        //        result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' ></td>";
        //    /*-----------Tax--------------------------*/
        //    var Incometaxrate_Value = $('#Incometaxrate').val(); // G14
        //    html += "<tr class='lblHeading'><td colspan='3' >Tax</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Net_Income_PBT_projection_data[i] / Incometaxrate_Value;
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' ></td>";

        //    /*-----------Net Income - PAT--------------------------*/
        //    var Net_Income_PAT_projection_data = [];
        //    html += "<tr class='lblHeading'><td colspan='3' >Net Income - PAT</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Net_Income_PBT_projection_data[i] - (Net_Income_PBT_projection_data[i] / Incometaxrate_Value);
        //        Net_Income_PAT_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------% PAT Margin--------------------------*/
        //    html += "<tr class='lblHeading percentage'><td colspan='3' >% PAT Margin</td>";
        //    for (var i = 0; i < 10; i++) {
        //        let result = Net_Income_PAT_projection_data[i] / SumOfSales[i];
        //        result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' ></td></tr>";
        //    html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' ></td></tr>";

        //    /*-----------Receivables--------------------------*/
        //    var Temp_WCT_html = "";
        //    Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Receivables </td>";
        //    var CollectioninDays_Value = $('#CollectioninDays').val();
        //    var Receivables_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = SumOfSales[i] * CollectioninDays_Value / 365;
        //        Receivables_Projection.push(result);
        //        Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    Temp_WCT_html += "</tr>";
        //    /*-----------Inventory--------------------------*/
        //    Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Inventory </td>";
        //    var InventoryinDays_Value = $('#InventoryinDays').val();
        //    var Inventory_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = SumOfCOGS[i] * InventoryinDays_Value / 365;
        //        Inventory_Projection.push(result);
        //        Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    Temp_WCT_html += "</tr>";
        //    /*-----------Creditors--------------------------*/
        //    Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Creditors </td>";
        //    var CreditorinDays_Value = $('#CreditorinDays').val();
        //    var Creditors_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = -(SumOfCOGS[i] + (EBITDA_projection[i] * Profit_Share_Value)) * CreditorinDays_Value / 365
        //        Creditors_Projection.push(result);
        //        Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    Temp_WCT_html += "</tr>";
        //    /*-----------Working capital Total--------------------------*/
        //    Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Working capital Total  </td>";
        //    var Working_capital_Total = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = Receivables_Projection[i] + Inventory_Projection[i] + Creditors_Projection[i];
        //        Working_capital_Total.push(result);
        //        Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    Temp_WCT_html += "</tr>";
        //    /*-----------Incremental working capital--------------------------*/
        //    html += "<tr class='lblHeading'><td colspan='3' > Incremental working capital  </td>";
        //    var Incremental_working_capital_projection_data = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i > 0) {
        //            result = Working_capital_Total[i - 1] - Working_capital_Total[i];
        //        }
        //        Incremental_working_capital_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";


        //    html += "<tr class='lblHeading bg-light emptyRow'><td>Deal Terms</td><td colspan='12' ></td></tr>";
        //    /*-----------R&D analytical cost--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >R&D analytical cost</td>";
        //    var RnD_analytical_cost_projection_data = [];
        //    var On_Agreement_Signing_Value = 1000; // this need to evaluate;

        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            if (_ProjectStartDate <= Projection_Year_data[i] && _ProjectStartDate > Projection_Year_data[i - 1]) {
        //                result = -On_Agreement_Signing_Value;
        //            }
        //            else {
        //                result = 0;
        //            }
        //        }
        //        RnD_analytical_cost_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------RLD sample cost--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >RLD sample cost</td>";
        //    var RLD_sample_projection_data = [];
        //    var cost_Value = $('#Rldsamplecost').val();
        //    var compareDate_string = $('#RldsamplecostPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        RLD_sample_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Batch manufacturing cost--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >Batch manufacturing cost</td>";
        //    var Batch_manufacturing_projection_data = [];
        //    var cost_Value = $('#BatchmanufacturingcostOrApiactualsEst').val();
        //    var compareDate_string = $('#BatchmanufacturingcostOrApiactualsEstPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        Batch_manufacturing_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------6 months stability cost--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >6 months stability cost</td>";
        //    var Sixmonths_stability_costprojection_data = [];
        //    var cost_Value = $('#Sixmonthsstabilitycost').val();
        //    var compareDate_string = $('#SixmonthsstabilitycostPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        Sixmonths_stability_costprojection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Tech Transfer--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >Tech Transfer</td>";
        //    var Tech_Transfer_projection_data = [];
        //    var cost_Value = $('#TechTransfer').val();
        //    var compareDate_string = $('#TechTransferPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        Tech_Transfer_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------BE studies--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >  BE studies  </td>";
        //    var BE_studies_projection_data = [];
        //    var cost_Value = $('#Bestudies').val();
        //    var compareDate_string = $('#BestudiesPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        BE_studies_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Filing fees--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >  Filing fees  </td>";
        //    var Filing_fees_projection_data = [];
        //    var cost_Value = $('#Filingfees').val();
        //    var compareDate_string = $('#FilingfeesPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        Filing_fees_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Other fees--------------------------*/
        //    html += "<tr class='lblHeading '><td colspan='3' >  Other fees </td>";
        //    var Other_fees_projection_data = [];
        //    var cost_Value = $('#ToolingAndChangeParts').val();
        //    var compareDate_string = $('#ToolingAndChangePartsPhaseEndDate').val();
        //    var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i == 0) {
        //            result = 0;
        //        } else {
        //            result = (compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1]) ? -cost_Value : 0;
        //        }
        //        result = result - (1 - (Incometaxrate_Value / 100))
        //        Other_fees_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";

        //    /*-----------Total investment--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Total investment </td>";
        //    var TotalInvestment_projection_data = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = Other_fees_projection_data[i] +
        //            Filing_fees_projection_data[i] +
        //            BE_studies_projection_data[i] +
        //            Tech_Transfer_projection_data[i] +
        //            Sixmonths_stability_costprojection_data[i] +
        //            Batch_manufacturing_projection_data[i] +
        //            RLD_sample_projection_data[i] +
        //            RnD_analytical_cost_projection_data[i];

        //        TotalInvestment_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Net Cash Flow--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Net Cash Flow </td>";
        //    var NetCashFlow_projection_data = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = Incremental_working_capital_projection_data[i] +
        //            Net_Income_PAT_projection_data[i] +
        //            TotalInvestment_projection_data[i] +

        //            NetCashFlow_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Cumulative cash flow -Combined--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Cumulative cash flow -Combined </td>";
        //    var Cumulative_cash_flow_projection_data = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i > 0) {
        //            result = NetCashFlow_projection_data[i] + Cumulative_cash_flow_projection_data[i - 1];
        //        }
        //        Cumulative_cash_flow_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Discount Factor Projection--------------------------*/
        //    html += "<tr class='lblHeading percentage'><td colspan='3' > Discount Factor Projection </td>";
        //    var DiscountRate_Value = $('#DiscountRate').val();
        //    var Discount_Factor_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        var Row_Projection_th_Index = $('#tblFinanceProjection').find('.thYearCounter:eq(' + i + ')').text();
        //        var trProjectionYearIndex = formatToNumber((Row_Projection_th_Index == '-') ? '0' : Row_Projection_th_Index);
        //        let result = 0;
        //        result = 1 / (Math.pow((1 + DiscountRate_Value / 100), trProjectionYearIndex));
        //        Discount_Factor_Projection.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Discount Cash Flow--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Discount Cash Flow </td>";
        //    var Discount_Cash_Flow_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        result = Discount_Factor_Projection[i] * NetCashFlow_projection_data[i];
        //        Discount_Cash_Flow_Projection.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Cumulative Discount Cash Flow--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Cumulative Discount cash flow </td>";
        //    var Cumulative_Discount_cash_flow_projection_data = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i > 0) {
        //            result = Discount_Cash_Flow_Projection[i] + Cumulative_Discount_cash_flow_projection_data[i - 1];
        //        }
        //        Cumulative_Discount_cash_flow_projection_data.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Simple payback in # of months--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Simple payback in # of months </td>";
        //    var Simple_payback_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i > 0) {
        //            if (Cumulative_cash_flow_projection_data[i - 1] > 0) {
        //                result = 0;
        //            }
        //            else {
        //                if (Cumulative_cash_flow_projection_data[i] > 0) {
        //                    result = -Cumulative_cash_flow_projection_data[i - 1] / NetCashFlow_projection_data[i] * 12;
        //                }
        //                else {
        //                    Operating_Months_Projection_data[i];
        //                }
        //            }
        //        }

        //        Simple_payback_Projection.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    /*-----------Discounted Payback in # of months--------------------------*/
        //    html += "<tr class='lblHeading bg-light'><td colspan='3' > Discounted payback in # of months </td>";
        //    var Discounted_payback_Projection = [];
        //    for (var i = 0; i < 10; i++) {
        //        let result = 0;
        //        if (i > 0) {
        //            if (Cumulative_Discount_cash_flow_projection_data[i - 1] > 0) {
        //                result = 0;
        //            }
        //            else {
        //                if (Cumulative_Discount_cash_flow_projection_data[i] > 0) {
        //                    result = -Cumulative_Discount_cash_flow_projection_data[i - 1] / Discount_Cash_Flow_Projection[i] * 12;
        //                }
        //                else {
        //                    Operating_Months_Projection_data[i];
        //                }
        //            }
        //        }
        //        Discounted_payback_Projection.push(result);
        //        html += "<td>" + result.toFixed(3) + "</td>";
        //    }
        //    html += "</tr>";
        //    html += "<tr class='lblHeading bg-light emptyRow'><td>Working capital</td><td colspan='12' ></td></tr>";

        //    html += Temp_WCT_html;

        //    html += "</tbody>";

        //    /*-------------------------->>Start Output grid--------------------------------*/
        //    var TempHtml = "";
        //    var GestationPeriodinYears_value = formatToNumber($("#GestationPeriodinYears").val(), true);
        //    var DCFP = Discount_Cash_Flow_Projection;
        //    TempHtml += '<thead class="bg-light">';
        //    TempHtml += '<tr><th>Output grid</th> <th></th> <th>From launch</th></tr>';
        //    TempHtml += "</thead><tbody>";
        //    TempHtml += "<tr> <td>10-Year NPV to Emcure</td> <td> " + eval(Discount_Cash_Flow_Projection.join('+')).toFixed(3) + " </td><td></td></tr>";
        //    let Year5Result = DCFP[0] + DCFP[1] + DCFP[2] + DCFP[3] + DCFP[4];
        //    TempHtml += "<tr> <td>5-Year NPV to Emcure</td> <td> " + Year5Result.toFixed(3) + " </td><td></td></tr>";
        //    TempHtml += "<tr> <td>Discounted payback in yrs</td> <td> " + (eval(Discounted_payback_Projection.join('+')) / 12).toFixed(3) + " </td>";
        //    TempHtml += "<td> " + ((eval(Discounted_payback_Projection.join('+')) / 12) - GestationPeriodinYears_value).toFixed(3) + "</td></tr>";
        //    TempHtml += "<tr> <td>Total investment</td> <td> " + eval(TotalInvestment_projection_data.join('+')).toFixed(3) + " </td><td></td></tr>";
        //    TempHtml += "</tbody>";
        //    $('#tblOutputGridFinanceProjection').html(TempHtml);
        //    /*-------------------------->>End Output grid--------------------------------*/

        //}
    }
    $('#tblFinanceProjection').html(html);

}
