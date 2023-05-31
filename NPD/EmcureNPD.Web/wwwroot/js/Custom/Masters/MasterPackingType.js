$(document).ready(function () {
    GetPackingTypeList();
    $('input[type="number"]').on("keypress keyup blur", function (event) {
        var patt = new RegExp(/[0-9]*[.]{1}[0-9]{2}/i);
        var matchedString = $(this).val().match(patt);
        if (matchedString) {
            $(this).val(matchedString);
        }
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            let checkval = parseInt($(this).val());
            if (checkval < 0) {
                $(this).val("");
            }

            event.preventDefault();
        }
    });
});

// #region Get PackingType List
function GetPackingTypeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllPackingType, 'GET', GetPackingTypeListSuccess, GetPackingTypeListError);
}
function GetPackingTypeListSuccess(data) {
    try {
        destoryStaticDataTable('#PackingTypeTable');
        $('#PackingTypeTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#PackingTypeTable tbody').append('<tr><td>' + object.packingTypeName + '</td><td>' + (object.packingCost == null ? "" : object.packingCost) + '</td><td>' + (object.ref == null ? "" : object.ref) + '</td><td>' + (object.unit == null ? "":object.unit) + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SavePackingTypeModel" data-backdrop="static" data-keyboard="false"  onclick="GetPackingTypeById(' + object.packingTypeId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeletePackingTypeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeletePackingType(' + object.packingTypeId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#PackingTypeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPackingTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get PackingType By Id
function GetPackingTypeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPackingTypeByIdUrl + "/" + id, 'GET', GetPackingTypeByIdSuccess, GetPackingTypeByIdError);
}
function GetPackingTypeByIdSuccess(data) {
    try {
        ClearePackingTypeFields();
        $('#SavePackingTypeModel #PackingTypeID').val(data._object.packingTypeId);
        $('#SavePackingTypeModel #PackingTypeName').val(data._object.packingTypeName);
        $('#SavePackingTypeModel #PackingCost').val(data._object.packingCost);
        $('#SavePackingTypeModel #Ref').val(data._object.ref);
        $('#SavePackingTypeModel #Unit').val(data._object.unit);
        $('#SavePackingTypeModel #PackingTypeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SavePackingTypeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SavePackingTypeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPackingTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update PackingType
function AddPackingType() {
    ClearePackingTypeFields();
    $('#SavePackingTypeModel #PackingTypeTitle').html(AddLabel);
}
function SaveMasterPackingType(form) {
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePackingType, 'POST', SavePackingTypeSuccess, SavePackingTypeError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePackingTypeSuccess(data) {
    try {
        $('#SavePackingTypeModel').modal('hide');
        if (data._Success === true) {
            ClearePackingTypeFields();
            toastr.success(RecordInsertUpdate);
            GetPackingTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SavePackingTypeError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function ClearePackingTypeFields() {
    $('#SavePackingTypeModel #IsActive').prop('checked', true);
    $('#SavePackingTypeModel #PackingTypeID').val("0");
    $('#SavePackingTypeModel #PackingTypeName').val("");
    $('#SavePackingTypeModel #PackingCost').val("");
    $('#SavePackingTypeModel #Ref').val("");
    $('#SavePackingTypeModel #Unit').val("");
    $('#DeletePackingTypeModel #PackingTypeID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete PackingType
function ConfirmationDeletePackingType(id) {
    $('#DeletePackingTypeModel #PackingTypeID').val(id);
}
function DeletePackingType() {
    var tempInAtiveID = $('#DeletePackingTypeModel #PackingTypeID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeletePackingTypeByIdUrl + "/" + tempInAtiveID, 'POST', DeletePackingTypeByIdSuccess, DeletePackingTypeByIdError);
}
function DeletePackingTypeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            ClearePackingTypeFields();
            toastr.success(RecordDelete);
            GetPackingTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeletePackingTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion