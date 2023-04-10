
$(document).ready(function () {
    
   // SetupRoleTable();
});

$('#chkView').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.View']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.View']").prop('checked', false);
    }
});

$('#chkView').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.View']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.View']").prop('checked', false);
    }
});

$('#chkView').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.View']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.View']").prop('checked', false);
    }
});

$('#chkAdd').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Add']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Add']").prop('checked', false);
    }
});

$('#chkEdit').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Edit']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Edit']").prop('checked', false);
    }
});
$('#chkDelete').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Delete']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Delete']").prop('checked', false);
    }
});
$('#chkApprove').change(function () {

    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Approve']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Approve']").prop('checked', false);
    }
});

