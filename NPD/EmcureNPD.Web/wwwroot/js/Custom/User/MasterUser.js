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
            "data": "roleName", "name": "Role Name"
        },
        {
            "data": "businessUnitName", "name": "Business Unit Name"
        },
        {
            "data": "departmentName", "name": "Department Name"
        },
        {
            "data": "mobileNumber", "name": "Mobile Number"
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
            "data": "isActive", "name": "Active", "render": function (data, type, row, meta) {
                if (data) {
                    return "<span class='text-success text-bold'>Yes</span>";
                } else {
                    return "<span class='text-danger text-bold'>No</span>";
                }
            }
        },
        {
            "data": "userId", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';

                html += '<a title="Edit" class="large-font" style="'+IsEditAllow+'" href="/User/UserManage?UserId='+row.userId+'"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                html += '<a title="Delete" class="large-font text-danger" style="' + IsDeleteAllow +'" data-toggle="modal" data-target="#DeleteUserModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteUser(' + row.userId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i></a>';

                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject, {
        left: 2,
        right: 2
    });
}

//#endregion