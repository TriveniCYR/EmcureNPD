let tableId = "WishListTable";
let isView = false;
var table = null;
var wishListData = null;
$(document).ready(function () {
    InitializeWishList();
    GetWishListType();
    GetGeographyDropdown();

    table = $('#WishListTable').DataTable();
   
});
$(document).on('change', '#wishListTypeFilterId', function () {
    var key = $("#wishListTypeFilterId option:selected").text();
    if($(this).val()>0)
        table.search(key, false).draw();
    else
        table.search("", false).draw();//table.clear().rows.add($('#WishListTable').DataTable().data).draw();
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
            $('.dt-buttons').append(`<select class="btn btn-secondary form-control-sm" id="wishListTypeFilterId" style="height: 50px">${option}</select>`)[1];
            }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetWishListTypeError() {
    toastr.error(ErrorMessage);
}

function InitializeWishList() {
   
    var setDefaultOrder = [0, 'desc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + GetAllGetWishList,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
            //console.log(d);
            //console.log(d.data);
        },
        "datatype": "json"
    };
    var columnObject = [
        {
            "data": "wishListTyp"
        },
        {
            "data": "moleculeName"
        },
        {
            "data": "strength", "name": "Strength"
        },
        {
            "data": "dateOfFiling", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY"); //h:m");
            }
        },
        {
            "data": "dateOfApproval", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY"); //h:m");
            }
        },
        {
            "data": "isInhouseOrInLicensed", "name": "IsInhouse/InLicensed"
        },
        {
            "data": "nameofVendor", "name": "Name of Vendor"
        },
        {
            "data": "vendorEvaluationRemark", className: '', "title": "Evaluation Remark", "render": function (data, type, row, meta) {
                return '<span class="btn btn-sm" style="background-color:' + data + ';color:whitesmoke;">' + data +'</span>';
            }
        },
        {
            "data": "createdOn", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY h:m");
            }
        },
        {
            "data": null, className: 'notexport pidf-100 actionColumn', "title": "Action", "render": function (data, type, row, meta) {
                html = '';
                //_screenId == "1") {
                var _PIDFForm = '/PIDF/PIDF?PIDFId=' + row.pidfid;
                var _enable = (row.pidfStatusID == 1 || row.pidfStatusID == 2);

                //if (IsEditPIDF) {
                html += '<a class="large-font" style="color:#007bff;" href="javascript:ShowModal(' + row.wishListId + ',' + false + ')"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                html += '<a class="large-font" style="color:#007bff;" href="javascript:ShowModal(' + row.wishListId + ',' + true + ')"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                   // }
                //}
                return html;
            }
        }
    ]

   IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
    
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

//$(document).on('change', '#wishListTypeFilterId', function () {
    
//    var value = $("#wishListTypeFilterId option:selected").text(); 

//    var input, filter, table, tr, td, i;
//   // input = document.getElementById("wishListTypeFilterId");
//    filter = value.toUpperCase();
//    table = document.getElementById("WishListTable");
//    tr = table.getElementsByTagName("tr");
//    for (i = 0; i < tr.length; i++) {
//        td = tr[i].getElementsByTagName("td")[0];
//        if (td) {
//            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
//                tr[i].style.display = "";
//            }
//            else if ($(this).val() == 0)
//            {
//                td.innerHTML.toUpperCase();
//                tr[i].style.display = "";
//            }
//            else {
//                tr[i].style.display = "none";
//            }
//        }
//    }
    
//})


function GetCountyByBussinessUnitId(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + getCountryByBusinessUnitId + "/" +id, 'GET', GetCountryByBusinessUnitSuccess, GetCountryByBusinessUnitError);
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
function ShowModal(wishListId, isViewRow) {
    isView = isViewRow;
    if (wishListId > 0) {
      
        GetWishListById(wishListId);
        if (isView) { $("#wishListTitle").text("View Wish List"); }
        else { $("#wishListTitle").text("Update Wish List"); }
        
    }
    else {
        $(".form-control").prop("disabled", isView);
        $("select.form-control").prop("disabled", isView);
        $(".model-btn").css("display", "block");
        $("#chkInhouse").prop("disabled", isView);
        $("#chkInLicensed").prop("disabled", isView);
        $("#WishListId").val(0);
        $('#hdnCountryId').val(0);
        $("#WishListTypeId").val(0);
        $("#GeographyId").val(0);
        $("#CountryId").val(0);
        $("#MoleculeName").val("");
        $("#Strength").val("");
        $('#chkInhouse').prop('checked', false)
        $('#chkInLicensed').prop('checked', false);
        $("#DateOfFiling").val(null);
        $("#DateOfApproval").val(null);
        $("#NameofVendor").val("");
        $("#VendorEvaluationRemark").val("");
        $("#ReferenceDrugProduct").val("");
        $("#Remarks").val("");
        $("#wishListTitle").text("Add Wish List");
    }
    $("#AddUpdateWishListModel").modal('show');
}
function HideModel() {
        isView = false;
        $(".form-control").prop("disabled", isView);
        $("#WishListId").val(0);
        $('#hdnCountryId').val(0);
        $("#WishListTypeId").val(0);
        $("#GeographyId").val(0);
        $("#CountryId").val(0);
        $("#MoleculeName").val("");
        $("#Strength").val("");
        $('#chkInhouse').prop('checked', false)
        $('#chkInLicensed').prop('checked', false);
        $("#DateOfFiling").val(null);
        $("#DateOfApproval").val(null);
        $("#NameofVendor").val("");
        $("#VendorEvaluationRemark").val("");
        $("#ReferenceDrugProduct").val("");
        $("#Remarks").val("");
    $("#AddUpdateWishListModel").modal('hide');
}
function SaveAsDraft() {
    SaveWishList();
}

function Save() {
    SaveWishList();
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
        WishListTypeId:$("#WishListTypeId").val(),
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
            HideModel();
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