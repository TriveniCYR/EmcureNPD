
let isView = false;
let wishListId = 0;
$(document).ready(function () {
    GetWishListType();
    GetGeographyDropdown();
    let queryserch = location.search.split('&');
    wishListId = queryserch[0].split('=')[1] == undefined ? 0 : queryserch[0].split('=')[1];
    strView = queryserch[1].split('=')[1] == undefined ? false : queryserch[1].split('=')[1];
    var isTrueSet = (strView?.toLowerCase?.() === 'true');
    isView = isTrueSet;
    if (wishListId > 0) {
        GetWishListById(wishListId);
    }
});

function GetWishListType() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetWishListTypeUrl, 'GET', GetWishListTypeSuccess, GetWishListTypeError);
}
function GetWishListTypeSuccess(data) {
    try {
        var option = "<option value='0'>All Type</option>";
        if (data.length > 0) {
            for (var j = 0; j < data.length; j++) {

                option += `<option value=${data[j].wishListTypeId}>${data[j].wishListTyp}</option>`;
            }
            $('#WishListTypeId').append(option);
            $('.dt-buttons').append(`<select class="btn btn-secondary form-control-sm" id="wishListTypeFilterId" style="height: 38.98px">${option}</select>`)[1];
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetWishListTypeError() {
    toastr.error(ErrorMessage);
}



function GetGeographyDropdown() {
    $('#LogInId').val(0);
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPIDF + "/" + $('#LogInId').val(), 'GET', GetGeographyDropdownSuccess, GetGeographyDropdownError);
}
function GetGeographyDropdownSuccess(data) {
    try {
        if (data != null) {
            var _emptyOption = '<option value="">-- Select --</option>';

            $('#GeographyId').append(_emptyOption);


            $(data.MasterBusinessUnits).each(function (index, item) {
                $('#GeographyId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');
            });

            try {
                if ($("#WishListId").val() > 0) {
                    $('#GeographyId').val($('#hdnGeographyId').val());

                }
            } catch (e) {
            }
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetGeographyDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}
$('#GeographyId').change(function (e) {
    if ($(this).val() != "") {
        if (parseInt($(this).val()) > 0) {
            GetCountyByBussinessUnitId($(this).val());
        }
    }
});

function GetCountyByBussinessUnitId(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + getCountryByBusinessUnitId + "/" + id, 'GET', GetCountryByBusinessUnitSuccess, GetCountryByBusinessUnitError);
}
function GetCountryByBusinessUnitSuccess(data) {
    try {
        $('#CountryId').find('option').remove()
        if (data._object != null && data._object.length > 0) {
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#CountryId').append(_emptyOption);
            $(data._object).each(function (index, item) {
                $('#CountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });

            try {
                if ($("#WishListId").val() > 0) {
                    $('#CountryId').val($('#hdnCountryId').val());
                }
            } catch (e) {
            }
        }
    }
    catch (e) {
        toastr.error(ErrorMessage);
    }
}
function GetCountryByBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}

function GetWishListById(wishListId) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetWishListByIdUrl + "/" + wishListId, 'GET', GetWishListByIdSuccess, GetWishListByIdError);
}
function GetWishListByIdSuccess(data) {
    try {
        if (data != null) {

            let dateOfFiling = new Date(data.dateOfFiling);
            let dateOfApproval = new Date(data.dateOfApproval);
            $(".form-control").prop("disabled", isView);
            $("select.form-control").prop("disabled", isView);

            // Format the date strings in the "YYYY-MM-DD" format
            var formatteddateOfFiling = dateOfFiling.getFullYear() + '-' + ('0' + (dateOfFiling.getMonth() + 1)).slice(-2) + '-' + ('0' + dateOfFiling.getDate()).slice(-2);
            var formatteddateOfApproval = dateOfApproval.getFullYear() + '-' + ('0' + (dateOfApproval.getMonth() + 1)).slice(-2) + '-' + ('0' + dateOfApproval.getDate()).slice(-2);
            $("#WishListId").val(data.wishListId);
            $("#WishListTypeId").val(data.wishListTypeId);
            $("#GeographyId").val(data.geographyId);
            $("#hdnGeographyId").val(data.geographyId);
            GetCountyByBussinessUnitId(data.geographyId);
            $('#hdnCountryId').val(data.countryId);
            $("#CountryId").val(data.countryId);
            $("#MoleculeName").val(data.moleculeName);
            $("#Strength").val(data.strength);
            if (data.isInhouseOrInLicensed.includes("InHouse")) {
                $('#chkInhouse').prop('checked', true)
                $('#chkInhouse').val(data.IsInhouseOrInLicensed)
            }
            else if (data.isInhouseOrInLicensed.includes("InLicensed ")) {
                $('#chkInLicensed').prop('checked', true);
                $('#chkInLicensed').val(data.IsInhouseOrInLicensed)
            }
            $("#DateOfFiling").val(formatteddateOfFiling);
            $("#DateOfApproval").val(formatteddateOfApproval);
            $("#NameofVendor").val(data.nameofVendor);
            $("#VendorEvaluationRemark").val(data.vendorEvaluationRemark);
            $("#ReferenceDrugProduct").val(data.referenceDrugProduct);
            $("#Remarks").val(data.referenceDrugProduct);
            if (isView) {
                $(".model-btn").css("display", "none");
                $("#chkInhouse").prop("disabled", isView);
                $("#chkInLicensed").prop("disabled", isView);
            }
            else if (!isView) {
                $(".model-btn").css("display", "block");
                $("#chkInhouse").prop("disabled", isView);
                $("#chkInLicensed").prop("disabled", isView);
            }
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetWishListByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}

function SaveAsDraft() {
    SaveWishList();
}

function Save() {
    if (ValidateOnSave()) {
        SaveWishList();
    }
}
function ValidateOnSave() {
    let isValidated = false;
    if ($("#WishListTypeId").val() == 0) {
        $("#span-WishListTypeId").text("please select type!");
        isValidated = false;
    }
    if ($("#WishListTypeId").val() > 0) {
        $("#span-WishListTypeId").text("");
        isValidated = true;
    }
    if ($("#MoleculeName").val() == "") {
        $("#span-MoleculeName").text("Molecule Name is required");
        isValidated = false;
    }
    if ($("#MoleculeName").val() != "") {
        $("#span-MoleculeName").text("");
        isValidated = true;
    }
    if ($("#Strength").val() == "") {
        $("#span-Strength").text("Strength is required");
        isValidated = false;
    }
    if ($("#Strength").val() != "") {
        $("#span-Strength").text("");
        isValidated = true;
    }
    if ($("#DateOfApproval").val() == "") {
        $("#span-DateOfApproval").text("DateOfApproval is required");
        isValidated = false;
    }
    if ($("#DateOfApproval").val() != "") {
        $("#span-DateOfApproval").text("");
        isValidated = true;
    }
    if ($("#DateOfFiling").val() == "") {
        $("#span-DateOfFiling").text("DateOfFiling is required");
        isValidated = false;
    }
    if ($("#DateOfFiling").val() != "") {
        $("#span-DateOfFiling").text("");
        isValidated = true;
    }
    if ($("#VendorEvaluationRemark").val() == "") {
        $("#span-VendorEvaluationRemark").text("VendorEvaluationRemark is required");
        isValidated = false;
    }
    if ($("#VendorEvaluationRemark").val() != "") {
        $("#span-VendorEvaluationRemark").text("");
        isValidated = true;
    }
    if ($("#NameofVendor").val() == "") {
        $("#span-NameofVendor").text("NameofVendor is required");
        isValidated = false;
    }
    if ($("#NameofVendor").val() != "") {
        $("#span-NameofVendor").text("");
        isValidated = true;
    }
    if ($("#Remarks").val() == "") {
        $("#span-Remarks").text("Remarks is required");
        isValidated = false;
    }
    if ($("#Remarks").val() != "") {
        $("#span-Remarks").text("");
        isValidated = true;
    }

    return isValidated;

}
function SaveWishList() {
    let isInhouseOrInLicensed = "";
    if ($('#chkInhouse').is(':checked')) {
        isInhouseOrInLicensed = $('#chkInhouse').val();
    }
    else if ($('#chkInLicensed').is(':checked')) {
        isInhouseOrInLicensed = $('#chkInLicensed').val();
    }
    let param = {
        WishListId: $("#WishListId").val(),
        WishListTypeId: $("#WishListTypeId").val(),
        GeographyId: $("#GeographyId").val(),
        CountryId: $("#CountryId").val(),
        MoleculeName: $("#MoleculeName").val(),
        Strength: $("#Strength").val(),
        IsInhouseOrInLicensed: isInhouseOrInLicensed,
        DateOfFiling: $("#DateOfFiling").val(),
        DateOfApproval: $("#DateOfApproval").val(),
        NameofVendor: $("#NameofVendor").val(),
        VendorEvaluationRemark: $("#VendorEvaluationRemark").val(),
        ReferenceDrugProduct: $("#ReferenceDrugProduct").val(),
        Remarks: $("#Remarks").val()
    }
    console.log(JSON.stringify(param));
    if ($('#frmWishList').valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveWishListUrl, 'POST', SaveWishListSuccess, SaveWishListError, JSON.stringify(param));
    }
    return false;
}
function SaveWishListSuccess(data) {
    try {
        if (data._Success === true) {
            /* InitializeWishList();*/
            toastr.success(data._Message);
            window.location.href = "/WishList/WishListView";

        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }

}
function SaveWishListError(x, y, z) {
    toastr.error(ErrorMessage);
}