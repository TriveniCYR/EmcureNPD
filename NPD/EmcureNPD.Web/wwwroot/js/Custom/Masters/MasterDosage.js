$(document).ready(function () {
    GetDosageList();
});

// #region Get Dosage List
function GetDosageList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllDosage, 'GET', GetDosageListSuccess, GetDosageListError);
}
function GetDosageListSuccess(data) {
    try {
        destoryStaticDataTable('#DosageTable');
        $('#DosageTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#DosageTable tbody').append('<tr><td>' + object.dosageName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveDosageModel" data-backdrop="static" data-keyboard="false"  onclick="GetDosageById(' + object.dosageId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteDosageModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteDosage(' + object.dosageId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#DosageTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDosageListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Dosage By Id
function GetDosageById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetDosageByIdUrl + "/" + id, 'GET', GetDosageByIdSuccess, GetDosageByIdError);
}
function GetDosageByIdSuccess(data) {
    try {
        CleareDosageFields();
        $('#SaveDosageModel #DosageID').val(data._object.dosageId);
        $('#SaveDosageModel #DosageName').val(data._object.dosageName);
        $('#SaveDosageModel #DosageTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveDosageModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveDosageModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDosageByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Dosage
function AddDosage() {
    CleareDosageFields();
    $('#SaveDosageModel #DosageTitle').html(AddLabel);
}
function SaveMasterDosage(form) {
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveDosage, 'POST', SaveDosageSuccess, SaveDosageError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveDosageSuccess(data) {
    try {
        $('#SaveDosageModel').modal('hide');
        if (data._Success === true) {
            CleareDosageFields();
            toastr.success(RecordInsertUpdate);
            GetDosageList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveDosageError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareDosageFields() {
    $('#SaveDosageModel #IsActive').prop('checked', true);
    $('#SaveDosageModel #DosageID').val("0");
    $('#SaveDosageModel #DosageName').val("");
    $('#DeleteDosageModel #DosageID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete Dosage
function ConfirmationDeleteDosage(id) {
    $('#DeleteDosageModel #DosageID').val(id);
}
function DeleteDosage() {
    var tempInAtiveID = $('#DeleteDosageModel #DosageID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteDosageByIdUrl + "/" + tempInAtiveID, 'POST', DeleteDosageByIdSuccess, DeleteDosageByIdError);
}
function DeleteDosageByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareDosageFields();
            toastr.success(RecordDelete);
            GetDosageList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteDosageByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion