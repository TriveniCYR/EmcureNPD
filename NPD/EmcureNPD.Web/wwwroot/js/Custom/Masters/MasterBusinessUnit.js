$(document).ready(function () {
    GetBusinessUnitList();
    GetRegionList();
});
// #region Get Region List
function GetRegionList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllRegion, 'GET', GetRegionListSuccess, GetRegionListError);
}
function GetRegionListSuccess(data) {
    try {
        $.each(data._object, function (index, object) {
            $('#RegionId').append($('<option>').text(object.regionName).attr('value', object.regionId));
            $('#RegionId').select2({
                dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter')
            });
            //$('#RegionId option:eq(0)').val(0);
            //$('#RegionId').val("-");
            $('#RegionId').trigger('change');
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetRegionListError(x, y, z) {
    toastr.error(ErrorMessage);
}

// #region Get BusinessUnit List
function GetBusinessUnitList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllBusinessUnit, 'GET', GetBusinessUnitListSuccess, GetBusinessUnitListError);
}
function GetBusinessUnitListSuccess(data) {
    try {
        destoryStaticDataTable('#BusinessUnitTable');
        $('#BusinessUnitTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#BusinessUnitTable tbody').append('<tr><td>' + object.businessUnitName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveBusinessUnitModel" data-backdrop="static" data-keyboard="false"  onclick="GetBusinessUnitById(' + object.businessUnitId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a><a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteBusinessUnitModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteBusinessUnit(' + object.businessUnitId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#BusinessUnitTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBusinessUnitListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get BusinessUnit By Id
function GetBusinessUnitById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetBusinessUnitByIdUrl + "/" + id, 'GET', GetBusinessUnitByIdSuccess, GetBusinessUnitByIdError);
}
function GetBusinessUnitByIdSuccess(data) {
    try {
        CleareBusinessUnitFields()
        var regionIds = data._object.regionIds.toString();
        if (regionIds.includes(',')) { regionIds = regionIds.toString().split(','); }

        $('#SaveBusinessUnitModel #RegionId').val(regionIds);
        $('#SaveBusinessUnitModel #RegionId').trigger('change');

        $('#SaveBusinessUnitModel #BusinessUnitRegionMappingId').val(data._object.masterBusinessRegionMappingIds.toString());

        $('#SaveBusinessUnitModel #BusinessUnitID').val(data._object.businessUnitId);
        $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_BusinessUnitName').val(data._object.businessUnitName);
        $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_Latitude').val(data._object.latitude);
        $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_Longitude').val(data._object.longitude);
        $('#SaveBusinessUnitModel #BusinessUnitTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_IsActive').prop('checked', false);
        }
        else {
            $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBusinessUnitByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update BusinessUnit
function AddBusinessUnit() {
    CleareBusinessUnitFields();
    $('#SaveBusinessUnitModel #BusinessUnitTitle').html(AddLabel);
}
function SaveBusinessUnitForm(form) {
    var obj = {
        businessUnitId: $('#SaveBusinessUnitModel #BusinessUnitID').val(),
        businessUnitName: $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_BusinessUnitName').val(),
        isActive: $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_IsActive').prop('checked'),
        regionIds: $('#RegionId').val().toString(),
        masterBusinessRegionMappingIds: $('#SaveBusinessUnitModel #BusinessUnitRegionMappingId').val(),
        Latitude: $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_Latitude').val(),
        Longitude: $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_Longitude').val()
    };
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveBusinessUnit, 'POST', SaveBusinessUnitFormSuccess, SaveBusinessUnitFormError, JSON.stringify(obj));
    }
    return false;
}
function SaveBusinessUnitFormSuccess(data) {
    try {
        $('#SaveBusinessUnitModel').modal('hide');
        if (data._Success === true) {
            CleareBusinessUnitFields();
            toastr.success(RecordInsertUpdate);
            GetBusinessUnitList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveBusinessUnitFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareBusinessUnitFields() {
    $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_IsActive').prop('checked', true);
    $('#SaveBusinessUnitModel #BusinessUnitID').val("0");
    $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_BusinessUnitName').val("");
    $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_Latitude').val("");
    $('#SaveBusinessUnitModel #MasterBusinessUnitEntity_Longitude').val("");
    $('#DeleteBusinessUnitModel #BusinessUnitID').val("0");
    $('#SaveBusinessUnitModel #BusinessUnitRegionMappingId').val("");
    $('#SaveBusinessUnitModel #RegionId').val("");
    $('#SaveBusinessUnitModel #RegionId').trigger('change');
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete BusinessUnit
function ConfirmationDeleteBusinessUnit(id) {
    $('#DeleteBusinessUnitModel #BusinessUnitID').val(id);
}
function DeleteBusinessUnit() {
    var tempInAtiveID = $('#DeleteBusinessUnitModel #BusinessUnitID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteBusinessUnitByIdUrl + "/" + tempInAtiveID, 'POST', DeleteBusinessUnitByIdSuccess, DeleteBusinessUnitByIdError);
}
function DeleteBusinessUnitByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareBusinessUnitFields();
            toastr.success(RecordDelete);
            GetBusinessUnitList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteBusinessUnitByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion