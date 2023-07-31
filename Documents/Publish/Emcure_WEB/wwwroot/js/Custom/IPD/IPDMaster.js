var _userRegion = [];
var _IPDMode = 0;
var ExpirydateErrorMsg = 'Extension Expiry Date should greater than Original Expiry Date';
var PastdateErrorMsg = 'Can not select Past date';
var isValidIPDForm = true;
$(document).ready(function () {
    fnGetActiveBusinessUnit();
    GetRegionList();
   
    try {
        _PIDFId = parseInt($('#hdnPIDFId').val());
        _IPDMode = $('#hdnIPDIsView').val(); //parseInt($('#hdnPIDFId').val());
    } catch (e) {
        _IPDMode = getParameterByName("IsView");
    }
    if ($('#hdnIsPartial').val() != '1') {
        getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
    }
   
    $(document).on("change", "[id*='ExtensionExpiryDate']", function () {  
        var todaysDate = new Date();
        var _extensionExpiryDate = new Date($(this).val());
        var _originalExpiryDate = new Date($(this).closest('tr').find('.originalDate').val());
        if (_extensionExpiryDate <= _originalExpiryDate) {
            $(this).val('');
            $(this).css("border-color", "red");
            isValidIPDForm = false;
            toastr.error(ExpirydateErrorMsg);
        }
        else if (_extensionExpiryDate < new Date(todaysDate.getFullYear(), todaysDate.getMonth(), todaysDate.getDate())) {
            $(this).css("border-color", "red");
            $(this).val('');
            isValidIPDForm = false;
            toastr.error(PastdateErrorMsg);  
        }
        else {
            $(this).css("border-color", "");
        }
    });
    $(document).on("change", "[id*='OriginalExpiryDate']", function () {
        var todaysDate = new Date();
        var _originalExpiryDate  = new Date($(this).val());
        var _extensionExpiryDate = new Date($(this).closest('tr').find('.extendedDate').val());
        if (_extensionExpiryDate <= _originalExpiryDate) {
            $(this).val('');
            $(this).css("border-color", "red");
            isValidIPDForm = false;
            toastr.error(ExpirydateErrorMsg);
        }
        else if (_originalExpiryDate < new Date(todaysDate.getFullYear(), todaysDate.getMonth(), todaysDate.getDate())) {
            $(this).css("border-color", "red");
            $(this).val('');
            isValidIPDForm = false;
            toastr.error(PastdateErrorMsg);            
        }
        else {
            $(this).css("border-color", "");
        }
    });

    $(document).on("change", ".SelectPatentStrategyCommon", function () {
        if ($(this).val() == '6')
            $(this).next().next().removeClass('d-none');
        else {
            $(this).next().next().addClass('d-none');
        }
    });

    $(document).on("change", ".SelectCountryPD", function () {       
        if (!ValidateDuplicateCountryID(".SelectCountryPD"))
            $(this).val(undefined)
    });

    $(document).on("change", ".SelectCountryPDAPI", function () {
        if (!ValidateDuplicateCountryID(".SelectCountryPDAPI"))
            $(this).val(undefined)
    });
    
});
function Set_optionTextforAnyPatentstobeFiled () {
    $(".clsAnyPatentstobeFiled").each(function (ind, val) {
        val.options[0].text = '--Select--'
    });
}

function ValidateDuplicateCountryID(controlclass) {
    var arr = [];
    var IsValid = true;
    $(controlclass).each(function (ind, val) {
        var i = arr.indexOf(parseInt(this.value));
        if (i == -1)
            arr.push(parseInt(this.value));
        else {
            IsValid = false;            
        }
        
    });
    if (IsValid) {
        var IsValid = true;
    } else {        
        toastr.error("Duplicate Country not allowed", "Error:");
    }       
   
    return IsValid;
}


function fnGetActiveBusinessUnit() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetActiveBusinessUnit, 'GET', GetActiveBusinessUnitSuccess, GetActiveBusinessUnitError);
}
function GetActiveBusinessUnitSuccess(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data._object, function (index, item) {
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + item.businessUnitId + '" data-toggle="pill" aria-selected="true" onclick="LoadIPDForm(' + _PIDFID + ', ' + item.businessUnitId +')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
        businessUnitPanel += '<div class="tab-pane ' + ((item.businessUnitId == _selectBusinessUnit ? "fade show active" : "")) + '" id="custom-tabs-' + item.businessUnitId + '" role="tabpanel" aria-labelledby="custom-tabs-two-' + item.businessUnitId + '-tab"></div>';
    });
    $('#custom-tabs-two-tab').html(businessUnitHTML);
    $('#custom-tabs-two-tabContent').html(businessUnitPanel);
   
    LoadIPDForm(_PIDFID, _selectBusinessUnit);
    
}
function GetActiveBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
function LoadIPDForm(pidfId, BusinessUnitId) {  
    _selectBusinessUnit = BusinessUnitId;
    if ($("#custom-tabs-" + BusinessUnitId).html() == "") {
        $.get(_IPDPartialURL, { pidfid: pidfId, bui: BusinessUnitId }, function (content) {
            $("#custom-tabs-" + BusinessUnitId).html(content);
            $('#SelectedTabBusinessUnit').val(_selectBusinessUnit);
            SetDisableForOtherUserBU(_selectBusinessUnit);
            GetCountryList_PatentDetailsFormulation();
            GetPatentStrategyDropdownList();
            Set_optionTextforAnyPatentstobeFiled();
        });
    }
   
}
// #region Get Region List
function GetRegionList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllRegion + "/" + $('#LogInId').val(), 'GET', GetRegionListSuccess, GetRegionListError);
}
function GetRegionListSuccess(data) {
    try {
        //alert(_userRegion.length);
        _userRegion = data._object;
        //setRegion();
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetRegionListError(x, y, z) {
    toastr.error(ErrorMessage);
}

function setRegion() {
    var selRegion = getParentFormId().find('#RegionIds').val();
    $.each(_userRegion, function (index, object) {
        getParentFormId().find('.regionCombo').append($('<option>').text(object.regionName).attr('value', object.regionId));
    });
    getParentFormId().find('.regionCombo').select2({ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter') });
    if (selRegion != undefined && selRegion != "") {
        let arr = selRegion.split(',');
        getParentFormId().find('.regionCombo').val(arr).trigger('change');
    }
    else {
        getParentFormId().find('.regionCombo').val("-").trigger('change');
    }
}

// #region Get Country List
function GetCountryList() {
    if (getParentFormId().find('.regionCombo').val() != null && getParentFormId().find('.regionCombo').val() != "") {
        ajaxServiceMethod($('#hdnBaseURL').val() + AllRegionCountry + "/" + getParentFormId().find('.regionCombo').val(), 'GET', GetCountryListSuccess, GetCountryListError);
    }
}
function GetCountryListSuccess(data) {
    try {
        var selCountry = getParentFormId().find('#CountryIds').val();
        getParentFormId().find('#CountryId').empty();

        $.each(data._object, function (index, object) {
            getParentFormId().find('#CountryId').append($('<option>').text(object.countryName).attr('value', object.countryId));
            if (selCountry != undefined && selCountry != "") {
                let arr = selCountry.split(',');
                getParentFormId().find('#CountryId').val(arr).trigger('change');
            }
        });
        getParentFormId().find(('.countryCombo')).select2({ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter') });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryListError(x, y, z) {
    toastr.error(ErrorMessage);
}


// # Patent Details Formulation- Get Country List
//'.SelectCountryPD'
function FillPatendDetailsDropDown_CountryId(controlClass,data) {
    getParentFormId().find(controlClass).empty();
    getParentFormId().find(controlClass).each(function () {
        var selectControl = $(this);
        selectControl.append('<option value="">--select--</option>');
        $.each(data._object, function (index, object) {
            selectControl.append('<option value="' + object.countryId + '">' + object.countryName + '</option>');
            // let strhtml = '<option value="' + object.countryId + '">' + object.countryName + '</option>'
            // selectControl.append(strhtml);
        });
        selectControl.val(selectControl.next().val());
    });
}

function GetCountryList_PatentDetailsFormulation() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetCountryForPatentDetails + "/" + _selectBusinessUnit, 'GET', GetCountryList_PatentDetailsFormulationSuccess, GetCountryList_PatentDetailsFormulationError);
    
}
function GetCountryList_PatentDetailsFormulationSuccess(data) {
    try {
        FillPatendDetailsDropDown_CountryId('.SelectCountryPD', data);
        FillPatendDetailsDropDown_CountryId('.SelectCountryPDAPI', data);
       // getParentFormId().find('.SelectCountryPD').val(arr).trigger('change');
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryList_PatentDetailsFormulationError(x, y, z) {
    toastr.error(ErrorMessage);
}
//patent details Patent Strategy dropdown List

function FillPatendDetailsDropDown_PatentStrategy(controllclass, data){
    getParentFormId().find(controllclass).empty();
    getParentFormId().find(controllclass).each(function () {
        var selectControl = $(this);
        selectControl.append('<option value="">--select--</option>');
        $.each(data._object, function (index, object) {
            selectControl.append('<option value="' + object.patentStrategyId + '">' + object.patentStrategyName + '</option>');
            // let strhtml = '<option value="' + object.countryId + '">' + object.countryName + '</option>'
            // selectControl.append(strhtml);
        });
        selectControl.val(selectControl.next().val()).trigger('change');;
    });
}


function GetPatentStrategyDropdownList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPatentStrategyList, 'GET', GetPatentStrategyListSuccess, GetPatentStrategyListError);

}
function GetPatentStrategyListSuccess(data) {
    try {
        FillPatendDetailsDropDown_PatentStrategy('.SelectPatentStrategyPD', data);
        FillPatendDetailsDropDown_PatentStrategy('.SelectPatentStrategyPDAPI', data);

    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPatentStrategyListError(x, y, z) {
    toastr.error(ErrorMessage);
}


function SaveIPDClick(type) {
    isValidIPDForm = true;
    $('.originalDate').trigger("change");
    $('.extendedDate').trigger("change");
   
    if (type == 'Drf') {
        $('#fIPDForm_' + _selectBusinessUnit).validate().settings.ignore = "*";

    } else {
        isValidIPDForm = validatePatentDetails();
        isValidIPDForm = validateDynamicControldDetailsIPD();
    }
    getParentFormId().find('#SaveType').val(type);
    getParentFormId().find('#RegionIds').val(getParentFormId().find('.regionCombo').val());
    getParentFormId().find('#CountryIds').val(getParentFormId().find('#CountryId').val());
    SetIPDChildRows();
    SetIPDChildRowsAPI();
    if (isValidIPDForm) {
        return true;
    } else {
        return false;
    }
    
}

function addRowParent(j) {
    var table = getParentFormId().find('#parentBody');
    var node = getParentFormId().find('#parentRow_0').clone(true);
    table.find('tr:last').after((node.length > 1 ? node[0] : node));
    table.find('tr:last').find("input").val("");
    IPDSetChildRowDeleteIcon();
}
function removeRowParent(j, element) {
    $(element).closest("tr").remove();
    IPDSetChildRowDeleteIcon();
}
function tabClick(val, pidfidval) {
    //let urlPieces = [location.protocol, '//', location.host]
    //var url = urlPieces.join('') + "/PIDForm/PIDForm?pidfid=" + pidfidval + "&bui=" + val;
    var url = "/PIDForm/PIDForm?pidfid=" + pidfidval + "&bui=" + val;
    window.location.href = url;
}
function IPDSetChildRowDeleteIcon() {
    if (getParentFormId().find('#PIDFTable tbody tr').length > 1 && _IPDMode != 1) {
        getParentFormId().find('.apiDeleteIcon').show();
    } else {
        getParentFormId().find('.apiDeleteIcon').hide();
    }
    IPDSetChildRowDeleteIconAPI();
}
function SaveIPDForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod(_IPDSaveUpdateURL, 'POST', SaveIPDFormSuccess, SaveIPDFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveIPDFormSuccess(data) {
    try {
        if (data._Success === true) {
            toastr.success(RecordInsertUpdate);
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveIPDFormError(data) {
    toastr.error(ErrorMessage);
}
function SetIPDChildRows() {
    $.each(getParentFormId().find('#PIDFTable tbody tr'), function (index, value) {

        $(this).find("td:first select").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].CountryId");

        $(this).find("td:eq(1) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].PatentNumber");
        $(this).find("td:eq(2) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Type");
        $(this).find("td:eq(3) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].OriginalExpiryDate");
        $(this).find("td:eq(4) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].ExtensionExpiryDate");
        $(this).find("td:eq(5) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Comments");
        $(this).find("td:eq(6) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Strategy");

        //$(this).find("td:eq(6) select").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].CountryId");
        $(this).find("td:eq(7) select[id$=PatentStrategy]").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].PatentStrategy");
        $(this).find("td:eq(7) input[id$=PatentStrategyOther]").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].PatentStrategyOther");
        $(this).find("td:eq(8) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].BasicPatentExpiry");
        $(this).find("td:eq(9) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].OtherLmitingPatentDate1");
        $(this).find("td:eq(10) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].OtherLmitingPatentDate2");
        $(this).find("td:eq(11) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].EarliestLaunchDate");

        $(this).find("td:eq(12) select").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].AnyPatentstobeFiled");
        $(this).find("td:eq(13) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].EarliestMarketEntry");
        $(this).find("td:eq(14) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].StimatedNumberofgenericsinthe");
        $(this).find("td:eq(15) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Lawfirmbeingused");

    });
}
$(document).on("change", ".regionCombo", function () {
    GetCountryList();
});
function getParentFormId() {
    return $('#fIPDForm_' + _selectBusinessUnit);
}
function readOnlyIPDForm() {
    $('#dvIPDContainer').find(getParentFormId()).find('input').attr('readonly', true).attr('disabled', true);
    $('#dvIPDContainer').find(getParentFormId()).find('textarea').attr('readonly', true).attr('disabled', true);
    $('#dvIPDContainer').find(getParentFormId()).find('select').attr('readonly', true).attr('disabled', true).trigger("change");
    $('#dvIPDContainer').find(getParentFormId()).find('.operationButton').hide();
}
function SetDisableForOtherUserBU(_selectBusinessUnit) {
    var UserwiseBusinessUnit = $('#BusinessUnitsByUser').val().split(',');
    var BUval = _selectBusinessUnit.toString();
    var status = UserwiseBusinessUnit.indexOf(BUval);
    // var IsViewInMode = ($("#IsView").val() == '1')
    if (status == -1) {
        readOnlyIPDForm();
    }
}

function readOnlyIPDFormForOtherBU(flag) {
    $('#ProjectName').attr('readonly', flag);
    $('#dvIPDContainer').find('input').attr('readonly', flag).attr('disabled', flag);
    $('#dvIPDContainer').find('textarea').attr('readonly', flag).attr('disabled', flag);
    //$('button').attr('readonly', true).attr('disabled', true);
    $('#dvIPDContainer').find('select').attr('readonly', flag).attr('disabled', flag);
    if(flag)
        $('#dvIPDContainer').find('.operationButton').hide();
    else
        $('#dvIPDContainer').find('.operationButton').show();
}
function checkRadioCheckOrNot() {
    if ($(".IPDIsComment").is(":checked")) { $(".IPDCommentText").attr("readonly", false); $(".IPDCommentText").attr("disabled", false  ) }
    else {
        $(".IPDCommentText").val("");
        $(".IPDCommentText").attr("disabled", true);
        $(".IPDCommentText").attr("readonly", true)
    }
}

// Validation For Paten Details

function validatePatentDetails() {
    var flag = true;
    $('#dvIPDContainer').find('.validateChildDetails').each(function () {     
        if ($(this).val().trim() == '') {
            $(this).css("border-color", "red");
            $(this).focus();
            flag = false;
        }
        else {
            $(this).css("border-color", "");
        }
    });  
    if (!flag) {
        toastr.error("Some fields are missing values,Fill all Business Unit Tab details !");  
    }
    return flag;
}
function validateDynamicControldDetailsIPD() {
    var flag = true;
    $('#dvIPDContainer').find('.customvalidateformcontrol').each(function () {
        if ($(this).val().trim() == '') {
            $(this).css("border-color", "red");
            $(this).focus();
            flag = false;
        } else {
            $(this).css("border-color", "");
        }
    });
    return flag;
   
}
function validatecontrols(control) {
    if ($(control).val().trim() == '') {
        $(control).css("border-color", "red");
        $(control).focus();
        isValidIPDForm = false;
    }
    else {
        $(control).css("border-color", "");
    }
}

//-----------------patent Details API---------------------------------------
function addRowParentAPI(j) {
    var table = getParentFormId().find('#parentBodyAPI');
    var node = getParentFormId().find('#parentRowAPI_0').clone(true);
    table.find('tr:last').after((node.length > 1 ? node[0] : node));
    table.find('tr:last').find("input").val("");
    IPDSetChildRowDeleteIconAPI();
}
function removeRowParentAPI(j, element) {
    $(element).closest("tr").remove();
    IPDSetChildRowDeleteIconAPI();
}
function IPDSetChildRowDeleteIconAPI() {
    if (getParentFormId().find('#PIDFTableAPI tbody tr').length > 1 && _IPDMode != 1) {
        getParentFormId().find('.apiDeleteIconAPI').show();
    } else {
        getParentFormId().find('.apiDeleteIconAPI').hide();
    }
}
function SetIPDChildRowsAPI() {
    $.each(getParentFormId().find('#PIDFTableAPI tbody tr'), function (index, value) {
        $(this).find("td:first select").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].CountryId");
        $(this).find("td:eq(1) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].PatentNumber");
        $(this).find("td:eq(2) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].Type");
        $(this).find("td:eq(3) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].OriginalExpiryDate");
        $(this).find("td:eq(4) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].ExtensionExpiryDate");
        $(this).find("td:eq(5) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].Comments");
        $(this).find("td:eq(6) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].Strategy");


       // $(this).find("td:eq(6) select").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].CountryId");
        $(this).find("td:eq(7) select[id$=PatentStrategy]").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].PatentStrategy");
        $(this).find("td:eq(7) input[id$=PatentStrategyOther]").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].PatentStrategyOther");
        $(this).find("td:eq(8) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].BasicPatentExpiry");
        $(this).find("td:eq(9) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].OtherLmitingPatentDate1");
        $(this).find("td:eq(10) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].OtherLmitingPatentDate2");
        $(this).find("td:eq(11) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].EarliestLaunchDate");

        $(this).find("td:eq(12) select").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].AnyPatentstobeFiled");
        $(this).find("td:eq(13) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].EarliestMarketEntry");
        $(this).find("td:eq(14) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].StimatedNumberofgenericsinthe");
        $(this).find("td:eq(15) input").attr("name", "pidf_IPD_PatentDetailsEntitiesAPI[" + index.toString() + "].Lawfirmbeingused");


    });
}


