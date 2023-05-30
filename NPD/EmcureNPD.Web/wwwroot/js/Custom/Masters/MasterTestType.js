$(document).ready(function () {
    GetTestTypeList();
});

// #region Get TestType List
function GetTestTypeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllTestType, 'GET', GetTestTypeListSuccess, GetTestTypeListError);
}
function GetTestTypeListSuccess(data) {
    try {
        destoryStaticDataTable('#TestTypeTable');
        $('#TestTypeTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#TestTypeTable tbody').append('<tr><td>' + object.testTypeName + '</td><td>' + (object.testTypeCode == null ? "" : object.testTypeCode) + '</td><td>' + (object.testTypePrice == null ? "" : object.testTypePrice) + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveTestTypeModel" data-backdrop="static" data-keyboard="false"  onclick="GetTestTypeById(' + object.testTypeId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteTestTypeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteTestType(' + object.testTypeId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#TestTypeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetTestTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get TestType By Id
function GetTestTypeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetTestTypeByIdUrl + "/" + id, 'GET', GetTestTypeByIdSuccess, GetTestTypeByIdError);
}
function GetTestTypeByIdSuccess(data) {
    try {
        CleareTestTypeFields();
        $('#SaveTestTypeModel #TestTypeID').val(data._object.testTypeId);
        $('#SaveTestTypeModel #TestTypeName').val(data._object.testTypeName);
        $('#SaveTestTypeModel #TestTypeCode').val(data._object.testTypeCode);
        $('#SaveTestTypeModel #TestTypePrice').val(data._object.testTypePrice);
        $('#SaveTestTypeModel #TestTypeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveTestTypeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveTestTypeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetTestTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update TestType
function AddTestType() {
    CleareTestTypeFields();
    $('#SaveTestTypeModel #TestTypeTitle').html(AddLabel);
}
function SaveMasterTestType(form) {
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveTestType, 'POST', SaveTestTypeSuccess, SaveTestTypeError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveTestTypeSuccess(data) {
    try {
        $('#SaveTestTypeModel').modal('hide');
        if (data._Success === true) {
            CleareTestTypeFields();
            toastr.success(RecordInsertUpdate);
            GetTestTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveTestTypeError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareTestTypeFields() {
    $('#SaveTestTypeModel #IsActive').prop('checked', true);
    $('#SaveTestTypeModel #TestTypeID').val("0");
    $('#SaveTestTypeModel #TestTypeName').val("");
    $('#SaveTestTypeModel #TestTypeCode').val("");
    $('#SaveTestTypeModel #TestTypePrice').val("");
    $('#DeleteTestTypeModel #TestTypeID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete TestType
function ConfirmationDeleteTestType(id) {
    $('#DeleteTestTypeModel #TestTypeID').val(id);
}
function DeleteTestType() {
    var tempInAtiveID = $('#DeleteTestTypeModel #TestTypeID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteTestTypeByIdUrl + "/" + tempInAtiveID, 'POST', DeleteTestTypeByIdSuccess, DeleteTestTypeByIdError);
}
function DeleteTestTypeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareTestTypeFields();
            toastr.success(RecordDelete);
            GetTestTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteTestTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion