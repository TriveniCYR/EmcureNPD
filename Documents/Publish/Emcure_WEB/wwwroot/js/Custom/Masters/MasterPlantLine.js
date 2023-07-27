$(document).ready(function () {
    GetPlantLineList();
    GetPlantList();
});

// #region Get PlantLine List
function GetPlantLineList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllPlantLine, 'GET', GetPlantLineListSuccess, GetPlantLineListError);
}
function GetPlantLineListSuccess(data) {
    try {
        destoryStaticDataTable('#PlantLineTable');
        $('#PlantLineTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#PlantLineTable tbody').append('<tr><td>' + object.lineName + '</td><td>' + (object.lineCost == null ? "" : object.lineCost) + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SavePlantLineModel" data-backdrop="static" data-keyboard="false"  onclick="GetPlantLineById(' + object.lineId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow + '" href="" title="Delete" data-toggle="modal" data-target="#DeletePlantLineModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeletePlantLine(' + object.lineId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#PlantLineTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPlantLineListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion
// #region Get Plant List
function GetPlantList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllActivePlants, 'GET', GetPlantListSuccess, GetPlantListError);
}
function GetPlantListSuccess(data) {
    try {
        $('#PlantId').html('')
        let optionhtml = '<option value = "0">--Select--</option>';
        $.each(data._object, function (index, object) {
            optionhtml += '<option value="' +
                object.plantId + '">' + object.plantNameName + '</option>';
        });
        $("#PlantId").append(optionhtml);

        $("select#PlantId option").each(function (index, value) {
            if (this.value === selectedPlantId) {
                $("select#PlantId").prop('selectedIndex', index);
                return;
            }
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPlantListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion
// #region Get PlantLine By Id
function GetPlantLineById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPlantLineByIdUrl + "/" + id, 'GET', GetPlantLineByIdSuccess, GetPlantLineByIdError);
}
function GetPlantLineByIdSuccess(data) {
    try {
        ClearePlantLineFields();
        $('#PlantLineModel').modal('show');
        $('#PlantLineModel #LineId').val(data._object.lineId);
        $('#PlantLineModel #PlantId').val(data._object.plantId);
        $('#PlantLineModel #LineName').val(data._object.lineName);
        $('#PlantLineModel #LineCost').val(data._object.lineCost);
        $('#PlantLineModel #PlantLineTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#PlantLineModel #IsActive').prop('checked', false);
        }
        else {
            $('#PlantLineModel #IsActive').prop('checked', true);
        }

    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPlantLineByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update PlantLine
function AddPlantLine() {
    ClearePlantLineFields();
    $('#PlantLineModel #PlantLineTitle').html(AddLabel);
}
function SaveMasterPlantLine(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePlantLine, 'POST', SavePlantLineSuccess, SavePlantLineError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePlantLineSuccess(data) {
    try {
        $('#PlantLineModel').modal('hide');
        if (data._Success === true) {
            ClearePlantLineFields();
            toastr.success(RecordInsertUpdate);
            GetPlantLineList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SavePlantLineError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function ClearePlantLineFields() {
    $('#PlantLineModel #IsActive').prop('checked', true);
    $('#PlantLineModel #LineId').val("0");
    $('#PlantLineModel #LineName').val("");
    $('#PlantLineModel #LineCost').val("");
    $('#PlantLineModel #PlantId').val("0");
    $('#DeletePlantLineModel #LineId').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete PlantLine
function ConfirmationDeletePlantLine(id) {
    $('#DeletePlantLineModel #LineId').val(id);
}
function DeletePlantLine() {
    var tempInAtiveID = $('#DeletePlantLineModel #LineId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeletePlantLineByIdUrl + "/" + tempInAtiveID, 'POST', DeletePlantLineByIdSuccess, DeletePlantLineByIdError);
}
function DeletePlantLineByIdSuccess(data) {
    try {
        if (data._Success === true) {
            ClearePlantLineFields();
            toastr.success(RecordDelete);
            GetPlantLineList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeletePlantLineByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion