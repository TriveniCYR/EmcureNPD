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

    UpdateProjectionCommercial();
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

function UpdateProjectionCommercial() {
    RenderCommercialPerPack();
   //RenderFinanceProjection();
}
function GetBatchSizeCostingTRValues() {
    var Arr_FinanceTable_tr = [];
    jQuery.each(Arr_FinanceTable_tr, function (index, item) {
        var trObj =
        {
            SKU: $(this).find("select.Skus option:selected").text(),
            PackSize: $(this).find("select.PakeSize option:selected").text(),

            hdnMSLow: $(this).find("#hdnMSLow").val(),
            hdnMSMid: $(this).find("#hdnMSMid").val(),
            hdnMSHigh: $(this).find("#hdnMSHigh").val(),

            marketInPacks: item.Marketinpacks,

            hdnNSPLow: $(this).find("#hdnNSPLow").val(),
            hdnNSPMid: $(this).find("#hdnNSPMid").val(),
            hdnNSPHigh: $(this).find("#hdnNSPHigh").val(),

            emcureCOGs_pack: item.EmcureCogsPack,

        }

        Arr_FinanceTable_tr.push(trObj);

    });
    return Arr_FinanceTable_tr;
}





