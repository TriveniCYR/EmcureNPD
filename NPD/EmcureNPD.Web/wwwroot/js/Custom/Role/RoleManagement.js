$(document).ready(function () {
    SetupRoleTable();

    //var placeHolderElement = $("#PlaceHolderHere");

    //$('button[data-toggle="modal"]').click(function (event) {
    //    var url = $(this).data('url');
    //    $.get(url).done(function (data) {
    //        placeHolderElement.html(data);
    //        placeHolderElement.find('.modal').modal('show');
    //        console.log(data);

    //    });

    //});


});
function SetupRoleTable() {
    StaticDataTable("#RoleTable");
}

function CleareRoleFields() {
    $('#DeleteRoleModel #RoleID').val("0");
}

//#region Delete Role
function ConfirmationRole(id) {
    alert('Test ' + id);
    $('#DeleteRoleModel #RoleID').val(id);
}
function DeleteRole() {
    var tempInAtiveID = $('#DeleteRoleModel #RoleID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteRoleByIdUrl + "/" + tempInAtiveID, 'POST', DeleteRoleByIdSuccess, DeleteRoleByIdError);
}
function DeleteRoleByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareRoleFields();
            toastr.success(RecordDelete);
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteRoleByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion