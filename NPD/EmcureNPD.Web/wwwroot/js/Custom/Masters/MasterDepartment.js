$(document).ready(function () {
    GetDepartmentList();
    GetBusinessUnitList();
});

// #region Get Department List
function GetBusinessUnitList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllBusinessUnit, 'GET', GetBusinessUnitListSuccess, GetBusinessUnitListError);
}
function GetBusinessUnitListSuccess(data) {
    try {
        $.each(data._object, function (index, object) {
            $('#DepartmentBusinessUnitMappingId').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
            $('#DepartmentBusinessUnitMappingId').select2();
            $('#DepartmentBusinessUnitMappingId option:eq(0)').val(0);
            $('#DepartmentBusinessUnitMappingId').val("-");
            $('#DepartmentBusinessUnitMappingId').trigger('change');
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBusinessUnitListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Department List
function GetDepartmentList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllDepartment, 'GET', GetDepartmentListSuccess, GetDepartmentListError);
}
function GetDepartmentListSuccess(data) {
    try {
        $('#DepartmentTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#DepartmentTable tbody').append('<tr><td>' + object.departmentName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveDepartmentModel" data-backdrop="static" data-keyboard="false"  onclick="GetDepartmentById(' + object.departmentId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="btn btn-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteDepartmentModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteDepartment(' + object.departmentId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#DepartmentTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDepartmentListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Department By Id
function GetDepartmentById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetDepartmentByIdUrl + "/" + id, 'GET', GetDepartmentByIdSuccess, GetDepartmentByIdError);
}
function GetDepartmentByIdSuccess(data) {
    try {
        var businessUnitIds = data._object.businessUnitIds.toString();
        if (businessUnitIds.includes(',')) { businessUnitIds = businessUnitIds.toString().split(','); }

        $('#SaveDepartmentModel #DepartmentBusinessUnitMappingId').val(businessUnitIds);
        $('#SaveDepartmentModel #DepartmentBusinessUnitMappingId').trigger('change');

        $('#SaveDepartmentModel #DepartmentBusinessMappingId').val(data._object.businessUnitMappingIds.toString());

        $('#SaveDepartmentModel #DepartmentID').val(data._object.departmentId);
        $('#SaveDepartmentModel #MasterDepartmentEntity_DepartmentName').val(data._object.departmentName);
        $('#SaveDepartmentModel #DepartmentTitle').html(UpdateLabel);

        if (!data._object.isActive) {
            $('#SaveDepartmentModel #MasterDepartmentEntity_IsActive').prop('checked', false);
        }
        else {
            $('#SaveDepartmentModel #MasterDepartmentEntity_IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDepartmentByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Department
function AddDepartment() {
    CleareDepartmentFields();
    $('#SaveDepartmentModel #DepartmentTitle').html(AddLabel);
}
function SaveDepartmentForm(form) {
    var obj = {
        departmentId: $('#SaveDepartmentModel #DepartmentID').val(),
        departmentName: $('#SaveDepartmentModel #MasterDepartmentEntity_DepartmentName').val(),
        isActive: $('#SaveDepartmentModel #MasterDepartmentEntity_IsActive').prop('checked'),
        businessUnitIds: $('#DepartmentBusinessUnitMappingId').val().toString(),
        businessUnitMappingIds: $('#SaveDepartmentModel #DepartmentBusinessMappingId').val()
    };
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveDepartment, 'POST', SaveDepartmentFormSuccess, SaveDepartmentFormError, JSON.stringify(obj));
    }
    return false;
}
function SaveDepartmentFormSuccess(data) {
    try {
        $('#SaveDepartmentModel').modal('hide');
        if (data._Success === true) {
            CleareDepartmentFields();
            toastr.success(RecordInsertUpdate);
            GetDepartmentList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveDepartmentFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareDepartmentFields() {
    $('#SaveDepartmentModel #MasterDepartmentEntity_IsActive').prop('checked', true);
    $('#SaveDepartmentModel #DepartmentID').val("0");
    $('#SaveDepartmentModel #MasterDepartmentEntity_DepartmentName').val("");
    $('#DeleteDepartmentModel #DepartmentID').val("0");
    $('#SaveDepartmentModel #DepartmentBusinessMappingId').val("");
    $('#SaveDepartmentModel #DepartmentBusinessUnitMappingId').val("");
    $('#SaveDepartmentModel #DepartmentBusinessUnitMappingId').trigger('change');
}
// #endregion

//#region Delete Department
function ConfirmationDeleteDepartment(id) {
    $('#DeleteDepartmentModel #DepartmentID').val(id);
}
function DeleteDepartment() {
    var tempInAtiveID = $('#DeleteDepartmentModel #DepartmentID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteDepartmentByIdUrl + "/" + tempInAtiveID, 'POST', DeleteDepartmentByIdSuccess, DeleteDepartmentByIdError);
}
function DeleteDepartmentByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareDepartmentFields();
            toastr.success(RecordDelete);
            GetDepartmentList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteDepartmentByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion