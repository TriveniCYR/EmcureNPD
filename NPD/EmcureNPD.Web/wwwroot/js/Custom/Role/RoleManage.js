$(document).ready(function () {
    // SetupRoleTable();
    $("[id^=MasterModules][type=checkbox]").change();
});

$('#chkView').change(function () {
    $.each($("input[name*='RoleModulePermission.View']"), function (index, item) {
        var Is_Disabled = $(item).prop('disabled');
        if (!Is_Disabled) {
            if ($('#chkView').is(':checked')) {
                $(item).prop('checked', true);
            }
            else {
                $(item).prop('checked', false);
            }
        }
    });

});
$('#chkAdd').change(function () {
    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Add']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Add']").prop('checked', false);
    }
    $("[id^=MasterModules][type=checkbox]").change();
});

$('#chkEdit').change(function () {
    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Edit']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Edit']").prop('checked', false);
    }
    $("[id^=MasterModules][type=checkbox]").change();
});
$('#chkDelete').change(function () {
    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Delete']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Delete']").prop('checked', false);
    }
    $("[id^=MasterModules][type=checkbox]").change();
});
$('#chkApprove').change(function () {
    if ($(this).is(':checked')) {
        $("input[name*='RoleModulePermission.Approve']").prop('checked', true);
    }
    else {
        $("input[name*='RoleModulePermission.Approve']").prop('checked', false);
    }
    $("[id^=MasterModules][type=checkbox]").change();
});
$('#btnSaveRole').click(function () {
    $("[type=checkbox]").attr("disabled", false);
});
$("[id^=MasterModules][type=checkbox]").change(function () {
    var str_chkBoxId = $(this).attr('id');
    var strPartialId = '';
    var Arr_str_chkBoxId = str_chkBoxId.split('_');
    if (Arr_str_chkBoxId.length == 5)
        strPartialId = Arr_str_chkBoxId[0] + '_' + Arr_str_chkBoxId[1] + '_' + Arr_str_chkBoxId[2] + '_' + Arr_str_chkBoxId[3] + '_';
    else {
        strPartialId = Arr_str_chkBoxId[0] + '_' + Arr_str_chkBoxId[1] + '_' + Arr_str_chkBoxId[2] + '_' + Arr_str_chkBoxId[3] + '_'
            + Arr_str_chkBoxId[4] + '_' + Arr_str_chkBoxId[5] + '_' + Arr_str_chkBoxId[6] + '_';
    }


    if (!str_chkBoxId.includes("RoleModulePermission_View")) { // if chchbox is Other than View
        if ($(this).is(':checked')) {
            // checked and disable Row_view
            $('#' + strPartialId + 'View').prop('checked', true);
            $('#' + strPartialId + 'View').attr("disabled", true);

        }
        else {
            //checkForOtherNonViewCheckBox
            CheckForOtherNonViewCheckBox(strPartialId);
        }
    }
});

function CheckForOtherNonViewCheckBox(strPartialId) {
    var Arr_CheckBox_type = ['Add', 'Edit', 'Delete', 'Approve'];
    var flag = false;
    jQuery.each(Arr_CheckBox_type, function (index, item) {
        var Id_checkBox = '#' + strPartialId + item;
        if ($(Id_checkBox).is(':checked')) {
            flag = true;
        }
    });
    if (flag == false) {
        $('#' + strPartialId + 'View').attr("disabled", false);
    }
}




