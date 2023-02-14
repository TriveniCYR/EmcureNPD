$(document).ready(function () {
    GetProductStrengthList();
});

// #region Get ProductStrength List
function GetProductStrengthList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllProductStrength, 'GET', GetProductStrengthListSuccess, GetProductStrengthListError);
}
function GetProductStrengthListSuccess(data) {
    try {
        $('#ProductStrengthTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#ProductStrengthTable tbody').append('<tr><td>' + object.productStrengthName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveProductStrengthModel" data-backdrop="static" data-keyboard="false"  onclick="GetProductStrengthById(' + object.productStrengthId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteProductStrengthModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteProductStrength(' + object.productStrengthId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#ProductStrengthTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductStrengthListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get ProductStrength By Id
function GetProductStrengthById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetProductStrengthByIdUrl + "/" + id, 'GET', GetProductStrengthByIdSuccess, GetProductStrengthByIdError);
}
function GetProductStrengthByIdSuccess(data) {
    try {
        $('#SaveProductStrengthModel #ProductStrengthID').val(data._object.productStrengthId);
        $('#SaveProductStrengthModel #ProductStrengthName').val(data._object.productStrengthName);
        $('#SaveProductStrengthModel #ProductStrengthTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveProductStrengthModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveProductStrengthModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductStrengthByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update ProductStrength
function AddProductStrength() {
    CleareProductStrengthFields();
    $('#SaveProductStrengthModel #ProductStrengthTitle').html(AddLabel);
}
function SaveProductStrengthForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveProductStrength, 'POST', SaveProductStrengthFormSuccess, SaveProductStrengthFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveProductStrengthFormSuccess(data) {
    try {
        $('#SaveProductStrengthModel').modal('hide');
        if (data._Success === true) {
            CleareProductStrengthFields();
            toastr.success(RecordInsertUpdate);
            GetProductStrengthList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveProductStrengthFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareProductStrengthFields() {
    $('#SaveProductStrengthModel #IsActive').prop('checked', true);
    $('#SaveProductStrengthModel #ProductStrengthID').val("0");
    $('#SaveProductStrengthModel #ProductStrengthName').val("");
    $('#DeleteProductStrengthModel #ProductStrengthID').val("0");
}
// #endregion

//#region Delete ProductStrength
function ConfirmationDeleteProductStrength(id) {
    $('#DeleteProductStrengthModel #ProductStrengthID').val(id);
}
function DeleteProductStrength() {
    var tempInAtiveID = $('#DeleteProductStrengthModel #ProductStrengthID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteProductStrengthByIdUrl + "/" + tempInAtiveID, 'POST', DeleteProductStrengthByIdSuccess, DeleteProductStrengthByIdError);
}
function DeleteProductStrengthByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareProductStrengthFields();
            toastr.success(RecordDelete);
            GetProductStrengthList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteProductStrengthByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion