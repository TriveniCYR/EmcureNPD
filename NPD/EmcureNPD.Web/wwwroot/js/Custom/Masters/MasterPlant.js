$(document).ready(function () {
    GetPlantList();
});

// #region Get Plant List
function GetPlantList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllPlant, 'GET', GetPlantListSuccess, GetPlantListError);
}
function GetPlantListSuccess(data) {
    try {
        $('#PlantTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#PlantTable tbody').append('<tr><td>' + object.plantNameName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SavePlantModel" data-backdrop="static" data-keyboard="false"  onclick="GetPlantById(' + object.plantId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeletePlantModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeletePlant(' + object.plantId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#PlantTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPlantListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Plant By Id
function GetPlantById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPlantByIdUrl + "/" + id, 'GET', GetPlantByIdSuccess, GetPlantByIdError);
}
function GetPlantByIdSuccess(data) {
    try {
        $('#SavePlantModel #PlantID').val(data._object.plantId);
        $('#SavePlantModel #PlantNameName').val(data._object.plantNameName);
        $('#SavePlantModel #PlantTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SavePlantModel #IsActive').prop('checked', false);
        }
        else {
            $('#SavePlantModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPlantByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Plant
function AddPlant() {
    ClearePlantFields();
    $('#SavePlantModel #PlantTitle').html(AddLabel);
}
function SavePlantForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePlant, 'POST', SavePlantFormSuccess, SavePlantFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePlantFormSuccess(data) {
    try {
        $('#SavePlantModel').modal('hide');
        if (data._Success === true) {
            ClearePlantFields();
            toastr.success(RecordInsertUpdate);
            GetPlantList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SavePlantFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function ClearePlantFields() {
    $('#SavePlantModel #IsActive').prop('checked', true);
    $('#SavePlantModel #PlantID').val("0");
    $('#SavePlantModel #PlantNameName').val("");
    $('#DeletePlantModel #PlantID').val("0");
}
// #endregion

//#region Delete Plant
function ConfirmationDeletePlant(id) {
    $('#DeletePlantModel #PlantID').val(id);
}
function DeletePlant() {
    var tempInAtiveID = $('#DeletePlantModel #PlantID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeletePlantByIdUrl + "/" + tempInAtiveID, 'POST', DeletePlantByIdSuccess, DeletePlantByIdError);
}
function DeletePlantByIdSuccess(data) {
    try {
        if (data._Success === true) {
            ClearePlantFields();
            toastr.success(RecordDelete);
            GetPlantList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeletePlantByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion