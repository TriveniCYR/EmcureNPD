$(document).ready(function () {
    //GetUserDropdown();
    GetBusinessUnitList();
    GetDepartmentList();
    GetAllRoleList();
    if (parseInt($('#UserId').val()) > 0) {
        $('.readOnlyUpdate').attr("readonly", "readonly").css("pointer-events", "none");
        //GetRegionByBusinessUnit();
    }
    $('#BusinessUnitId').change(function () {
        debugger;
        /*GetDepartmentCountryByBusinessUnit();*/  GetRegionByBusinessUnit();
    });
    $('#RegionId').change(function () {
        GetCountryByRegion();
    });
   
});

function GetBusinessUnitList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetUserDropDown, 'GET', GetUserDropdownSuccess, GetUserDropdownError);
}
function GetUserDropdownSuccess(data) {
    try {
        $.each(data.BusinessUnit, function (index, object) {
            $('#BusinessUnitId').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
            $('#BusinessUnitId').select2();
            $('#BusinessUnitId option:eq(0)').val(0);
            $('#BusinessUnitId').val("-");
            $('#BusinessUnitId').trigger('change');
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetUserDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #region Get BusinessUnit by department id
function GetRegionByBusinessUnit() {
    $('#RegionId option').remove();
   /* $('#RegionId').append($('<option>').text("--Select--").attr('value', 0));*/
    if (parseInt($('#BusinessUnitId').val()) > 0) {        
        ajaxServiceMethod($('#hdnBaseURL').val() + GetRegionByBusinessUnitURL + "?BusinessUnitIds=" + $('#BusinessUnitId').val(), 'GET', GetRegionByBusinessUnitSuccess, GetRegionByBusinessUnitError);
    }
}
function GetRegionByBusinessUnitSuccess(data) {

    try {
        if (data != null) {            
            $(data.Region).each(function (index, item) {
                $('#RegionId').append($('<option>').text(item.regionName).attr('value', item.regionId));
                $('#RegionId').select2();
                $('#RegionId option:eq(0)').val(0);
                $('#RegionId').val("-"); 
                $('#RegionId').trigger('change');
            });
            if (parseInt($('#UserId').val()) > 0) {
                $("#RegionId").val($("#hdnRegionId").val());                
            }
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetRegionByBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion
// #region Get Country By Region
function GetCountryByRegion() {
    $('#CountryId option').remove();   
   /* $('#CountryId').append($('<option>').text("--Select--").attr('value', 0));*/
        console.log($('#RegionId').val());
        ajaxServiceMethod($('#hdnBaseURL').val() + GetCountryByRegionURL + "?RegionIds=" + $('#RegionId').val(), 'GET', GetCountryByRegionSuccess, GetCountryByRegionError);
    }
function GetCountryByRegionSuccess(data) {

    try {
        if (data != null) {
            $(data.Country).each(function (index, item) {
                $('#CountryId').append($('<option>').text(item.countryName).attr('value', item.countryId));
                $('#CountryId').select2();
                $('#CountryId option:eq(0)').val(0);
                $('#CountryId').val("-");
            });
            if (parseInt($('#UserId').val()) > 0) {
                $("#CountryId").val($("#hdnCountryId").val());
            }
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryByRegionError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion
// #region GetDepartmentList
function GetDepartmentList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetDepartmentListURL,'GET', GetDepartmentListSuccess, GetDepartmentListError);
}
function GetDepartmentListSuccess(data) {
    try {
        if (data != null)
            $(data._object).each(function (index, item) {
                $('#DepartmentId').append($('<option>').text(item.departmentName).attr('value', item.departmentId));
                $('#DepartmentId').select2();
                $('#DepartmentId option:eq(0)').val(0);
                $('#DepartmentId').val("-");
            });
        if (parseInt($('#UserId').val()) > 0) {
            $("#DepartmentId").val($("#hdnDepartmentId").val());
        }
        } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDepartmentListError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion
// #region GetDepartmentList
function GetAllRoleList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllRoleURL, 'GET', GetAllRoleListSuccess, GetAllRoleListError);
}
function GetAllRoleListSuccess(data) {
    try {
        if (data != null)
            $('#RoleId').append('<option value="">-- Select Role --</option>');
        $(data._object).each(function (index, item) {
            $('#RoleId').append('<option value="' + item.roleId + '">' + item.roleName + '</option>');
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAllRoleListError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion




