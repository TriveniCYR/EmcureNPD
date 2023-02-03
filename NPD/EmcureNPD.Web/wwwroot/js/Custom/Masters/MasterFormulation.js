$(document).ready(function () {
    GetFormulationList();
});

// #region Get Formulation List
function GetFormulationList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllFormulation, 'GET', GetFormulationListSuccess, GetFormulationListError);
}
function GetFormulationListSuccess(data) {
    try {
        RemoveDataTable("#FormulationTable");
        $('#FormulationTable tbody').html('')
        
        $.each(data._object, function (index, object) {
            $('#FormulationTable tbody').append('<tr><td>' + object.formulationName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveFormulationModel" data-backdrop="static" data-keyboard="false"  onclick="GetFormulationById(' + object.formulationId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="btn btn-danger" style="' + IsDeleteAllow +'" href="" title="Delete" cursor="pointer" data-toggle="modal" data-target="#DeleteFormulationModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteFormulation(' + object.formulationId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#FormulationTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFormulationListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Formulation By Id
function GetFormulationById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetFormulationByIdUrl + "/" + id, 'GET', GetFormulationByIdSuccess, GetFormulationByIdError);
}
function GetFormulationByIdSuccess(data) {
    try {
        $('#SaveFormulationModel #FormulationID').val(data._object.formulationId);
        $('#SaveFormulationModel #FormulationName').val(data._object.formulationName);
        $('#SaveFormulationModel #FormulationTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveFormulationModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveFormulationModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFormulationByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Formulation
function AddFormulation() {
    CleareFormulationFields();
    $('#SaveFormulationModel #FormulationTitle').html(AddLabel);
}
function SaveFormulationForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveFormulation, 'POST', SaveFormulationFormSuccess, SaveFormulationFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveFormulationFormSuccess(data) {
    try {
        $('#SaveFormulationModel').modal('hide');
        if (data._Success === true) {
            CleareFormulationFields();
            toastr.success(RecordInsertUpdate);
            GetFormulationList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveFormulationFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareFormulationFields() {
    $('#SaveFormulationModel #IsActive').prop('checked', true);
    $('#SaveFormulationModel #FormulationID').val("0");
    $('#SaveFormulationModel #FormulationName').val("");
    $('#DeleteFormulationModel #FormulationID').val("0");
}
// #endregion

//#region Delete Formulation
function ConfirmationDeleteFormulation(id) {
    $('#DeleteFormulationModel #FormulationID').val(id);
}
function DeleteFormulation() {
    var tempInAtiveID = $('#DeleteFormulationModel #FormulationID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteFormulationByIdUrl + "/" + tempInAtiveID, 'POST', DeleteFormulationByIdSuccess, DeleteFormulationByIdError);
}
function DeleteFormulationByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareFormulationFields();
            toastr.success(RecordDelete);
            GetFormulationList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteFormulationByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion


// #region Get Audit Log By Id
function GetAuditById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + AuditLogByIdUrl + "/" + id+"/11", 'GET', GetAuditLogByIdSuccess, GetAuditLogByIdError);
}
function GetAuditLogByIdSuccess(data) {
    try {
        $("#LogViewModel #logBody").html("");
        var AuditDisplay = "<table class='table table-condensed' cellpadding='5'>";
        for (var i = 0; i < data._object.length; i++) {
            AuditDisplay = AuditDisplay + "<tr class='active'><td colspan='2'>Modify date: " + data._object[i].createdDate + "</td>";
            AuditDisplay = AuditDisplay + "<td>Action type: " + data._object[i].actionType + "</td><td>Id: " + data._object[i].entityId + "</td></tr>";
            AuditDisplay = AuditDisplay + "<tr class='text-warning'><td>Field name</td><td>Display name</td><td>Before change</td><td>After change</td></tr>";
            for (var j = 0; j < data._object[i].log.length; j++) {
                AuditDisplay = AuditDisplay + "<tr>";
                AuditDisplay = AuditDisplay + "<td>" + data._object[i].log[j].propertyName + "</td>";
                AuditDisplay = AuditDisplay + "<td>" + data._object[i].log[j].displayName + "</td>";
                AuditDisplay = AuditDisplay + "<td>" + data._object[i].log[j].oldValue + "</td>";
                AuditDisplay = AuditDisplay + "<td>" + data._object[i].log[j].newValue + "</td>";
                AuditDisplay = AuditDisplay + "</tr>";
            }
        }
        AuditDisplay = AuditDisplay + "</table>" >
            $("#LogViewModel #logBody").html(AuditDisplay);
        console.log(data);
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAuditLogByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion