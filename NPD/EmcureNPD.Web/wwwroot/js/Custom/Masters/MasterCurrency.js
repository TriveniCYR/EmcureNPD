$(document).ready(function () {
    GetCurrencyList();
    GetCountryList();
});
// #region Get Country List
function GetCountryList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllCountry, 'GET', GetCountryListSuccess, GetCountryListError);
}
function GetCountryListSuccess(data) {
    try {
        $.each(data._object, function (index, object) {
            $('#CountryId').append($('<option>').text(object.countryName).attr('value', object.countryId));
            $('#CountryId').select2();
            $('#CountryId option:eq(0)').val(0);
            $('#CountryId').val("-");
            $('#CountryId').trigger('change');
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Currency List
function GetCurrencyList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllCurrency, 'GET', GetCurrencyListSuccess, GetCurrencyListError);
}
function GetCurrencyListSuccess(data) {
    try {
        $('#CurrencyTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#CurrencyTable tbody').append('<tr><td>' + object.currencyName + '</td> <td>' + object.currencyCode + '</td> <td>' + object.currencySymbol + '</td> <td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveCurrencyModel" data-backdrop="static" data-keyboard="false"  onclick="GetCurrencyById(' + object.currencyId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteCurrencyModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteCurrency(' + object.currencyId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#CurrencyTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCurrencyListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Currency By Id
function GetCurrencyById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetCurrencyByIdUrl + "/" + id, 'GET', GetCurrencyByIdSuccess, GetCurrencyByIdError);
}
function GetCurrencyByIdSuccess(data) {
    try {
        var countryIds = data._object.countryIds.toString();
        if (countryIds.includes(',')) { countryIds = countryIds.toString().split(','); }

        $('#SaveCurrencyModel #CountryId').val(countryIds);
        $('#SaveCurrencyModel #CountryId').trigger('change');
        

        $('#SaveCurrencyModel #CurrencyCountryMappingId').val(data._object.masterBusinessCountryMappingIds.toString());

        $('#SaveCurrencyModel #CurrencyID').val(data._object.currencyId);
        $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencyName').val(data._object.currencyName);
        $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencyCode').val(data._object.currencyCode);
        $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencySymbol').val(data._object.currencySymbol);
        $('#SaveCurrencyModel #CurrencyTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveCurrencyModel #MasterCurrencyEntity_IsActive').prop('checked', false);
        }
        else {
            $('#SaveCurrencyModel #MasterCurrencyEntity_IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCurrencyByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Currency
function AddCurrency() {
    CleareCurrencyFields();
    $('#SaveCurrencyModel #CurrencyTitle').html(AddLabel);
}
function SaveCurrencyForm(form) {
    var obj = {
        CurrencyId: $('#SaveCurrencyModel #CurrencyID').val(),
        CurrencyName: $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencyName').val(),
        CurrencyCode: $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencyCode').val(),
        CurrencySymbol: $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencySymbol').val(),
        isActive: $('#SaveCurrencyModel #MasterCurrencyEntity_IsActive').prop('checked'),
        countryIds: $('#CountryId').val().toString(),
        masterBusinessCountryMappingIds: $('#SaveCurrencyModel #CurrencyCountryMappingId').val()
    };
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveCurrency, 'POST', SaveCurrencyFormSuccess, SaveCurrencyFormError, JSON.stringify(obj));
    }
    return false;
}
function SaveCurrencyFormSuccess(data) {
    try {
        $('#SaveCurrencyModel').modal('hide');
        if (data._Success === true) {
            CleareCurrencyFields();
            toastr.success(RecordInsertUpdate);
            GetCurrencyList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveCurrencyFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareCurrencyFields() {
    $('#SaveCurrencyModel #MasterCurrencyEntity_IsActive').prop('checked', true);
    $('#SaveCurrencyModel #CurrencyID').val("0");
    $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencyName').val("");
    $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencyCode').val("");
    $('#SaveCurrencyModel #MasterCurrencyEntity_CurrencySymbol').val("");
    $('#DeleteCurrencyModel #CurrencyID').val("0");
    $('#SaveCurrencyModel #CurrencyCountryMappingId').val("");
    $('#SaveCurrencyModel #CountryId').val("");
    $('#SaveCurrencyModel #CountryId').trigger('change');
}
// #endregion

//#region Delete Currency
function ConfirmationDeleteCurrency(id) {
    $('#DeleteCurrencyModel #CurrencyID').val(id);
}
function DeleteCurrency() {
    var tempInAtiveID = $('#DeleteCurrencyModel #CurrencyID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteCurrencyByIdUrl + "/" + tempInAtiveID, 'POST', DeleteCurrencyByIdSuccess, DeleteCurrencyByIdError);
}
function DeleteCurrencyByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareCurrencyFields();
            toastr.success(RecordDelete);
            GetCurrencyList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteCurrencyByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion