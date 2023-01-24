$(document).ready(function () {
    GetCountryList();
});

// #region Get Country List
function GetCountryList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllCountry, 'GET', GetCountryListSuccess, GetCountryListError);
}
function GetCountryListSuccess(data) {
    try {
        $('#CountryTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#CountryTable tbody').append('<tr><td>' + object.countryName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" data-toggle="modal" data-target="#SaveCountryModel" data-backdrop="static" data-keyboard="false"  onclick="GetCountryById(' + object.countryId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + EditLabel + '</a> <a class="btn btn-danger" data-toggle="modal" data-target="#DeleteCountryModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteCountry(' + object.countryId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + DeleteLabel + '</a>  </td></tr>');
        });
        StaticDataTable("#CountryTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Country By Id
function GetCountryById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetCountryByIdUrl + "/" + id, 'GET', GetCountryByIdSuccess, GetCountryByIdError);
}
function GetCountryByIdSuccess(data) {
    try {
        $('#SaveCountryModel #CountryID').val(data._object.countryId);
        $('#SaveCountryModel #CountryName').val(data._object.countryName);
        $('#SaveCountryModel #CountryTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveCountryModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveCountryModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Country
function AddCountry() {
    CleareCountryFields();
    $('#SaveCountryModel #CountryTitle').html(AddLabel);
}
function SaveCountryForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveCountry, 'POST', SaveCountryFormSuccess, SaveCountryFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveCountryFormSuccess(data) {
    try {
        $('#SaveCountryModel').modal('hide');
        if (data._Success === true) {
            CleareCountryFields();
            toastr.success(RecordInsertUpdate);
            GetCountryList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveCountryFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareCountryFields() {
    $('#SaveCountryModel #IsActive').prop('checked', true);
    $('#SaveCountryModel #CountryID').val("0");
    $('#SaveCountryModel #CountryName').val("");
    $('#DeleteCountryModel #CountryID').val("0");
}
// #endregion

//#region Delete Country
function ConfirmationDeleteCountry(id) {
    $('#DeleteCountryModel #CountryID').val(id);
}
function DeleteCountry() {
    var tempInAtiveID = $('#DeleteCountryModel #CountryID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteCountryByIdUrl + "/" + tempInAtiveID, 'POST', DeleteCountryByIdSuccess, DeleteCountryByIdError);
}
function DeleteCountryByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareCountryFields();
            toastr.success(RecordDelete);
            GetCountryList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteCountryByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion