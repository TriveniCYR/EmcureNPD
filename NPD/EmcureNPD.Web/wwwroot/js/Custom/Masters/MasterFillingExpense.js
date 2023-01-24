$(document).ready(function () {
    GetFillingExpenseList();
});

// #region Get FillingExpense List
function GetFillingExpenseList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllFillingExpense, 'GET', GetFillingExpenseListSuccess, GetFillingExpenseListError);
}
function GetFillingExpenseListSuccess(data) {
    try {
        $('#FillingExpenseTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#FillingExpenseTable tbody').append('<tr><td>' + object.expenseRegionName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" data-toggle="modal" data-target="#SaveFillingExpenseModel" data-backdrop="static" data-keyboard="false"  onclick="GetFillingExpenseById(' + object.expenseRegionId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + EditLabel + '</a> <a class="btn btn-danger" data-toggle="modal" data-target="#DeleteFillingExpenseModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteFillingExpense(' + object.expenseRegionId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + DeleteLabel + '</a>  </td></tr>');
        });
        StaticDataTable("#FillingExpenseTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFillingExpenseListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get FillingExpense By Id
function GetFillingExpenseById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetFillingExpenseByIdUrl + "/" + id, 'GET', GetFillingExpenseByIdSuccess, GetFillingExpenseByIdError);
}
function GetFillingExpenseByIdSuccess(data) {
    try {
        $('#SaveFillingExpenseModel #ExpenseRegionId').val(data._object.expenseRegionId);
        $('#SaveFillingExpenseModel #ExpenseRegionName').val(data._object.expenseRegionName);
        $('#SaveFillingExpenseModel #FillingExpenseTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveFillingExpenseModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveFillingExpenseModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFillingExpenseByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update FillingExpense
function AddFillingExpense() {
    CleareFillingExpenseFields();
    $('#SaveFillingExpenseModel #FillingExpenseTitle').html(AddLabel);
}
function SaveFillingExpenseForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveFillingExpense, 'POST', SaveFillingExpenseFormSuccess, SaveFillingExpenseFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveFillingExpenseFormSuccess(data) {
    try {
        $('#SaveFillingExpenseModel').modal('hide');
        if (data._Success === true) {
            CleareFillingExpenseFields();
            toastr.success(RecordInsertUpdate);
            GetFillingExpenseList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveFillingExpenseFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareFillingExpenseFields() {
    $('#SaveFillingExpenseModel #IsActive').prop('checked', true);
    $('#SaveFillingExpenseModel #ExpenseRegionId').val("0");
    $('#SaveFillingExpenseModel #ExpenseRegionName').val("");
    $('#DeleteFillingExpenseModel #ExpenseRegionId').val("0");
}
// #endregion

//#region Delete FillingExpense
function ConfirmationDeleteFillingExpense(id) {
    $('#DeleteFillingExpenseModel #ExpenseRegionId').val(id);
}
function DeleteFillingExpense() {
    var tempInAtiveID = $('#DeleteFillingExpenseModel #ExpenseRegionId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteFillingExpenseByIdUrl + "/" + tempInAtiveID, 'POST', DeleteFillingExpenseByIdSuccess, DeleteFillingExpenseByIdError);
}
function DeleteFillingExpenseByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareFillingExpenseFields();
            toastr.success(RecordDelete);
            GetFillingExpenseList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteFillingExpenseByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion