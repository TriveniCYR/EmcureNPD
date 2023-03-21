﻿var _userRegion = [];
var _IPDMode = 0;
$(document).ready(function () {
    debugger;
    fnGetActiveBusinessUnit();
    GetRegionList();
    SetDisableForOtherUserBU();
    try {
        _PIDFId = parseInt($('#hdnPIDFId').val());
        _IPDMode = $('#hdnIPDIsView').val(); //parseInt($('#hdnPIDFId').val());
    } catch (e) {
        _IPDMode = getParameterByName("IsView");
    }
    getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
});
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
  
    SetDisableForOtherUserBU(_selectBusinessUnit);
}
function GetActiveBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
function LoadIPDForm(pidfId, BusinessUnitId) {
    _selectBusinessUnit = BusinessUnitId;
    if ($("#custom-tabs-" + BusinessUnitId).html() == "") {
        $.get(_IPDPartialURL, { pidfid: pidfId, bui: BusinessUnitId }, function (content) {
            $("#custom-tabs-" + BusinessUnitId).html(content);
        });
    }
}
// #region Get Region List
function GetRegionList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllRegion + "/" + $('#LogInId').val(), 'GET', GetRegionListSuccess, GetRegionListError);
}
function GetRegionListSuccess(data) {
    try {
        _userRegion = data._object;
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
    $('.regionCombo').select2();
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
        $('.countryCombo').select2();
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryListError(x, y, z) {
    toastr.error(ErrorMessage);
}
function SaveIPDClick(type) {
    getParentFormId().find('#SaveType').val(type);
    getParentFormId().find('#RegionIds').val(getParentFormId().find('.regionCombo').val());
    getParentFormId().find('#CountryIds').val(getParentFormId().find('#CountryId').val());
    SetIPDChildRows();
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
        $(this).find("td:first input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].PatentNumber");
        $(this).find("td:eq(1) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Type");
        $(this).find("td:eq(2) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].OriginalExpiryDate");
        $(this).find("td:eq(3) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].ExtensionExpiryDate");
        $(this).find("td:eq(4) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Comments");
        $(this).find("td:eq(5) input").attr("name", "pidf_IPD_PatentDetailsEntities[" + index.toString() + "].Strategy");
    });
}
$(document).on("change", ".regionCombo", function () {
    GetCountryList();
});
function getParentFormId() {
    return $('#fIPDForm_' + _selectBusinessUnit);
}
function readOnlyIPDForm() {
    $('#dvIPDContainer').find('input').attr('readonly', true).attr('disabled', true);
    $('#dvIPDContainer').find('textarea').attr('readonly', true).attr('disabled', true);
    //$('button').attr('readonly', true).attr('disabled', true);
    $('#dvIPDContainer').find('select').attr('readonly', true).attr('disabled', true).trigger("change");
    $('#dvIPDContainer').find('.operationButton').hide();
}
function SetDisableForOtherUserBU(_selectBusinessUnit) {
   var UserwiseBusinessUnit = $('#BusinessUnitsByUser').val().split(',');    
    var status = UserwiseBusinessUnit.indexOf(_selectBusinessUnit);
   // var IsViewInMode = ($("#IsView").val() == '1')
    if (status == -1) {
        readOnlyIPDFormForOtherBU(true);
    }
    else {
        readOnlyIPDFormForOtherBU(false);
    }
}

function readOnlyIPDFormForOtherBU(flag) {
    $('#dvIPDContainer').find('input').attr('readonly', flag).attr('disabled', flag);
    $('#dvIPDContainer').find('textarea').attr('readonly', flag).attr('disabled', flag);
    //$('button').attr('readonly', true).attr('disabled', true);
    $('#dvIPDContainer').find('select').attr('readonly', flag).attr('disabled', flag);
    if(flag)
        $('#dvIPDContainer').find('.operationButton').hide();
    else
        $('#dvIPDContainer').find('.operationButton').show();
}