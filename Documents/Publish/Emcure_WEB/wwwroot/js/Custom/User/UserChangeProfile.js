

$(document).ready(function () {
    GetAllMobileCountryIdList();
});

// #region GetAllMobileCountryIdList
function GetAllMobileCountryIdList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllActiveCountryURL, 'GET', GetAllMobileCountryIdListSuccess, GetAllMobileCountryIdListError);
}
function GetAllMobileCountryIdListSuccess(data) {
    try {
        if (data != null)
            $('#MobileCountryId').append('<option value="">-- Select ISD --</option>');
        $(data._object).each(function (index, item) {
            $('#MobileCountryId').append('<option value="' + item.countryId + '">' + item.countryCode + '-' + item.isdcountryCode + '</option>');
        });
        if (parseInt($("#hdnMobileCountryId").val()) > 0) {
            $("#MobileCountryId").val($("#hdnMobileCountryId").val());
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAllMobileCountryIdListError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion
