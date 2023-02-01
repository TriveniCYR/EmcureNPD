$(document).ready(function () {
    GetProductTypeList();
});

// #region Get ProductType List
function GetProductTypeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllProductType, 'GET', GetProductTypeListSuccess, GetProductTypeListError);
}
function GetProductTypeListSuccess(data) {
    try {
        $('#ProductTypeTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#ProductTypeTable tbody').append('<tr><td>' + object.productTypeName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a href="" title="Edit" data-toggle="modal" data-target="#SaveProductTypeModel" data-backdrop="static" data-keyboard="false"  onclick="GetProductTypeById(' + object.productTypeId + '); return false;"><i class="fa fa-fw fa-edit text-primary mr-1"></i> ' + '</a> <a href="" title="Delete" data-toggle="modal" data-target="#DeleteProductTypeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteProductType(' + object.productTypeId + '); return false;"><i class="fa fa-fw fa-trash text-danger mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#ProductTypeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get ProductType By Id
function GetProductTypeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetProductTypeByIdUrl + "/" + id, 'GET', GetProductTypeByIdSuccess, GetProductTypeByIdError);
}
function GetProductTypeByIdSuccess(data) {
    try {
        $('#SaveProductTypeModel #ProductTypeID').val(data._object.productTypeId);
        $('#SaveProductTypeModel #ProductTypeName').val(data._object.productTypeName);
        $('#SaveProductTypeModel #ProductTypeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveProductTypeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveProductTypeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update ProductType
function AddProductType() {
    CleareProductTypeFields();
    $('#SaveProductTypeModel #ProductTypeTitle').html(AddLabel);
}
function SaveProductTypeForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveProductType, 'POST', SaveProductTypeFormSuccess, SaveProductTypeFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveProductTypeFormSuccess(data) {
    try {
        $('#SaveProductTypeModel').modal('hide');
        if (data._Success === true) {
            CleareProductTypeFields();
            toastr.success(RecordInsertUpdate);
            GetProductTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveProductTypeFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareProductTypeFields() {
    $('#SaveProductTypeModel #IsActive').prop('checked', true);
    $('#SaveProductTypeModel #ProductTypeID').val("0");
    $('#SaveProductTypeModel #ProductTypeName').val("");
    $('#DeleteProductTypeModel #ProductTypeID').val("0");
}
// #endregion

//#region Delete ProductType
function ConfirmationDeleteProductType(id) {
    $('#DeleteProductTypeModel #ProductTypeID').val(id);
}
function DeleteProductType() {
    var tempInAtiveID = $('#DeleteProductTypeModel #ProductTypeID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteProductTypeByIdUrl + "/" + tempInAtiveID, 'POST', DeleteProductTypeByIdSuccess, DeleteProductTypeByIdError);
}
function DeleteProductTypeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareProductTypeFields();
            toastr.success(RecordDelete);
            GetProductTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteProductTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion