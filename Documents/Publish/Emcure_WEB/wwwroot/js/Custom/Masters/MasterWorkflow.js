$(document).ready(function () {
    GetWorkflowList();
});

// #region Get Workflow List
function GetWorkflowList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllWorkflow, 'GET', GetWorkflowListSuccess, GetWorkflowListError);
}
function GetWorkflowListSuccess(data) {
    try {
        destoryStaticDataTable('#WorkflowTable');
        $('#WorkflowTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#WorkflowTable tbody').append('<tr><td>' + object.workflowName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveWorkflowModel" data-backdrop="static" data-keyboard="false"  onclick="GetWorkflowById(' + object.workflowId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteWorkflowModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteWorkflow(' + object.workflowId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#WorkflowTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetWorkflowListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Workflow By Id
function GetWorkflowById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetWorkflowByIdUrl + "/" + id, 'GET', GetWorkflowByIdSuccess, GetWorkflowByIdError);
}
function GetWorkflowByIdSuccess(data) {
    try {
        CleareWorkflowFields();
        $('#SaveWorkflowModel #WorkflowID').val(data._object.workflowId);
        $('#SaveWorkflowModel #WorkflowName').val(data._object.workflowName);
        $('#SaveWorkflowModel #WorkflowTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveWorkflowModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveWorkflowModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetWorkflowByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Workflow
function AddWorkflow() {
    CleareWorkflowFields();
    $('#SaveWorkflowModel #WorkflowTitle').html(AddLabel);
}
function SaveWorkflowForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveWorkflow, 'POST', SaveWorkflowFormSuccess, SaveWorkflowFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveWorkflowFormSuccess(data) {
    try {
        $('#SaveWorkflowModel').modal('hide');
        if (data._Success === true) {
            CleareWorkflowFields();
            toastr.success(RecordInsertUpdate);
            GetWorkflowList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveWorkflowFormError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareWorkflowFields() {
    $('#SaveWorkflowModel #IsActive').prop('checked', true);
    $('#SaveWorkflowModel #WorkflowID').val("0");
    $('#SaveWorkflowModel #WorkflowName').val("");
    $('#DeleteWorkflowModel #WorkflowID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete Workflow
function ConfirmationDeleteWorkflow(id) {
    $('#DeleteWorkflowModel #WorkflowID').val(id);
}
function DeleteWorkflow() {
    var tempInAtiveID = $('#DeleteWorkflowModel #WorkflowID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteWorkflowByIdUrl + "/" + tempInAtiveID, 'POST', DeleteWorkflowByIdSuccess, DeleteWorkflowByIdError);
}
function DeleteWorkflowByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareWorkflowFields();
            toastr.success(RecordDelete);
            GetWorkflowList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteWorkflowByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion