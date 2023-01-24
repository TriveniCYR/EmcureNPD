var tableId = "UserTable";
$(document).ready(function () {
    InitializeUserList();
});

//#region Delete User
function ConfirmationDeleteUser(id) {
    $('#DeleteUserModel #UserID').val(id);
}
function DeleteUser() {
    var tempInAtiveID = $('#DeleteUserModel #UserID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteUserByIdUrl + "/" + tempInAtiveID, 'POST', DeleteUserByIdSuccess, DeleteUserByIdError);
}
function DeleteUserByIdSuccess(data) {
    try {
        if (data._Success === true) {
            toastr.success(RecordDelete);
            $('#' + tableId).DataTable().draw();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteUserByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}

function InitializeUserList () {
    var setDefaultOrder = [0, 'asc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllUser,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
        },
        "datatype": "json"
    };

    var columnObject = [
        {
            "data": "fullName", "name": "Full Name"
        },
        {
            "data": "emailAddress", "name": "Email Address"
        },
        {
            "data": "createdDate", "name": "createdDate", "render": function (data, type, row, meta) {
                if (data != 0) {
                    return "<span>" + CustomDateFormat(row.createdDate, 2) + "</span>";
                } else {
                    return "";
                }
            }
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';

                html += '<a class="btn btn-primary" href="/User/UserManage?UserId='+row.userId+'"><i class="fa fa-fw fa-edit mr-1"></i>Edit</a>';
                html += '<a class="btn btn-danger ml-2" data-toggle="modal" data-target="#DeleteUserModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteUser(' + row.userId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i>Delete</a>';

                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

//#endregion