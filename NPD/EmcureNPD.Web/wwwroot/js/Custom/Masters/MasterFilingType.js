$(document).ready(function () {
    GetFilingTypeList();
});

// #region Get FilingType List
function GetFilingTypeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllFilingType, 'GET', GetFilingTypeListSuccess, GetFilingTypeListError);
}
function GetFilingTypeListSuccess(data) {
    try {
        destoryStaticDataTable('#FilingTypeTable');
        $('#FilingTypeTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#FilingTypeTable tbody').append('<tr><td>' + object.filingTypeName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveFilingTypeModel" data-backdrop="static" data-keyboard="false"  onclick="GetFilingTypeById(' + object.filingTypeId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteFilingTypeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteFilingType(' + object.filingTypeId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#FilingTypeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFilingTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get FilingType By Id
function GetFilingTypeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetFilingTypeByIdUrl + "/" + id, 'GET', GetFilingTypeByIdSuccess, GetFilingTypeByIdError);
}
function GetFilingTypeByIdSuccess(data) {
    try {
        CleareFilingTypeFields();
        $('#SaveFilingTypeModel #FilingTypeID').val(data._object.filingTypeId);
        $('#SaveFilingTypeModel #FilingTypeName').val(data._object.filingTypeName);
        $('#SaveFilingTypeModel #FilingTypeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveFilingTypeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveFilingTypeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFilingTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update FilingType
function AddFilingType() {
    CleareFilingTypeFields();
    $('#SaveFilingTypeModel #FilingTypeTitle').html(AddLabel);
}
function SaveMasterFilingType(form) {
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveFilingType, 'POST', SaveFilingTypeSuccess, SaveFilingTypeError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveFilingTypeSuccess(data) {
    try {
        $('#SaveFilingTypeModel').modal('hide');
        if (data._Success === true) {
            CleareFilingTypeFields();
            toastr.success(RecordInsertUpdate);
            GetFilingTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveFilingTypeError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareFilingTypeFields() {
    $('#SaveFilingTypeModel #IsActive').prop('checked', true);
    $('#SaveFilingTypeModel #FilingTypeID').val("0");
    $('#SaveFilingTypeModel #FilingTypeName').val("");
    $('#DeleteFilingTypeModel #FilingTypeID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete FilingType
function ConfirmationDeleteFilingType(id) {
    $('#DeleteFilingTypeModel #FilingTypeID').val(id);
}
function DeleteFilingType() {
    var tempInAtiveID = $('#DeleteFilingTypeModel #FilingTypeID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteFilingTypeByIdUrl + "/" + tempInAtiveID, 'POST', DeleteFilingTypeByIdSuccess, DeleteFilingTypeByIdError);
}
function DeleteFilingTypeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareFilingTypeFields();
            toastr.success(RecordDelete);
            GetFilingTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteFilingTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion