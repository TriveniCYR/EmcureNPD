var objMainForm = {};
$(document).ready(function () {    
    alert('ready called');
    GetPBFDropdown();
    //SetDivReadonly();
   // hideForms();
});
function GetPBFDropdown() {
    alert('GetPBFDropdown called');
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPBF, 'GET', GetPBFDropdownSuccess, GetPBFDropdownError);
}
function GetPBFDropdownSuccess(data) {
    try {
        if (data != null) {
            alert('MasterProductType called');
            $(data.MasterProductType).each(function (index, item) {
                $('#ProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            alert('MasterFormulationService called');
            $(data.MasterFormulationService).each(function (index, item) {
                $('#FormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            alert('MasterAnalyticalGLService called');
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#AnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });
           
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}
function hideForms() {
    var i, tabcontent;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
}
function SetDivReadonly() {
    $("#PIDFFormTemplate").find("input, submit, textarea, a, select").attr("disabled", "disabled");
    $("#PIDFFormTemplate").find("button, submit, a").hide();
    $("#PIDFFormTemplate").find("#collapseButton").show();
    $("#collapseButton").click();
}
$('#Save').click(function () {
    ValidateForm();
    $("#SaveType").val('Save');
});
$('#SaveDraft').click(function () {
    $("#IsModelValid").val('Valid')
    $("#SaveType").val('SaveDraft');
});
//function SaveClick() {
//    debugger;
//    //if (ValidateMainForm() && ValidateBU_Strength()) {
//       // objYears.length = 1;
//       // if (objYears.length > 0) {
//            $.extend(objMainForm, { 'SaveType': 'Sv' });
//            SaveCommertialPIDFForm();
//        //}
//        //else {
//            //toastr.error('No Year Data Added');
//        //}
//    //}
//}
//function SaveDraftClick() {
//    //if (ValidateBU_Strength()) {
//        $.extend(objMainForm, { 'SaveType': 'SvDrf' });
//        SaveCommertialPIDFForm();
//   // }
//}


//function SaveCommertialPIDFForm() {

//    $.extend(objMainForm, { 'ProjectName': $("#ProjectName").val() });
//    $.extend(objMainForm, { 'ProductStrength': $("#ProductStrength").val() });
//    $.extend(objMainForm, { 'SAPProjectProjectCode': $("#SAPProjectProjectCode").val() });
//    $.extend(objMainForm, { 'ImprintingEmbossingCodes': $("#ImprintingEmbossingCodes").val() });
//    $.extend(objMainForm, { 'TotalExpenses': $("#TotalExpenses").val() });
//    $.extend(objMainForm, { 'ProjectComplexity': $("#ProjectComplexity").val() });
//    $.extend(objMainForm, { 'ProductTypeId': $("#ProductTypeId").val() });
//    $.extend(objMainForm, { 'BudgetTimelineDate': $("#BudgetTimelineDate").val() });
//    $.extend(objMainForm, { 'PbfAnalFormulationId': $("#PbfAnalFormulationId").val() });
//    $.extend(objMainForm, { 'PbfAnalAnalyticalId': $("#PbfAnalAnalyticalId").val() });
//    alert($('#hdnBaseURL').val());
//    console.log(JSON.stringify(objMainForm));
//    ajaxServiceMethod($('#hdnBaseURL').val() + SavePBFAnatical, 'POST', SavePFBAnalyticalFormSuccess, SavePFBAnalyticalFormError, JSON.stringify(objMainForm));

//}
//function SavePFBAnalyticalFormSuccess(data) {
//    try {
//        $('#SavePIDFModel').modal('hide');
//        if (data._Success === true) {
//            window.location = "/PIDF/PIDFList";
//            toastr.success(RecordInsertUpdate);
//        }
//        else {
//            toastr.error(data._Message);
//        }
//    } catch (e) {
//        toastr.error('Error:' + e.message);
//    }
//}
//function SavePFBAnalyticalFormError(x, y, z) {
//    toastr.error(ErrorMessage);
//}

function ValidateMainForm() {
    var ArrofInvalid = []
    var MainFormFeilds = ['MarketSizeInUnit', 'ShelfLife']
    $.each(MainFormFeilds, function (_, kv) {
        if ($('#' + kv).val() == '') {
            $('#valmsg' + kv).text('Required');
            ArrofInvalid.push(kv);
        }
        else {
            $('#valmsg' + kv.name).text('');
        }
    });
    var status = (ArrofInvalid.length == 0) ? true : false;
    if (!status) { toastr.error('Some fields are missing !'); }
    return status;
}
