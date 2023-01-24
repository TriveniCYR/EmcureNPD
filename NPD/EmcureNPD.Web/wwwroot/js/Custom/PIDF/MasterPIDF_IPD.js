$(document).ready(function () {
    GetRegionList();    
});
// #region Get Region List
function GetRegionList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllRegion + "/" + $('#LogInId').val(), 'GET', GetRegionListSuccess, GetRegionListError);
}
function GetRegionListSuccess(data) {
    try {
        var selRegion = $('#RegionIds').val();
        $.each(data._object, function (index, object) {
            //var newOption = new Option(object.regionName, object.regionId, true, true);
            //$('#RegionId').append(newOption);
            $('#RegionId').append($('<option>').text(object.regionName).attr('value', object.regionId));
            $('#RegionId').select2();
            //$('#RegionId option:eq(0)').val(0);            
        });
        if (selRegion != undefined && selRegion != "") {
            let arr = selRegion.split(',');
            //var nums = arr.map(toNumber);
            var arra = Array.from(arr);// [1, 2];
            $('#RegionId').val(arr);
            $('#RegionId').trigger('change');
            GetCountryList();
        }
        else {
            $('#RegionId').val("-");
            $('#CountryId').select2();
            $('#RegionId').trigger('change');
        }

    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}

function GetRegionListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #region Get Country List
function GetCountryList() {
    if ($('#RegionId').val() != null && $('#RegionId').val() != "") {
        ajaxServiceMethod($('#hdnBaseURL').val() + AllRegionCountry + "/" + $('#RegionId').val(), 'GET', GetCountryListSuccess, GetCountryListError);
    }
}
function GetCountryListSuccess(data) {
    try {
        var selCountry = $('#CountryIds').val();
        $('#CountryId').val("-");
        $('#CountryId').empty();

       
        $.each(data._object, function (index, object) {
            $('#CountryId').append($('<option>').text(object.countryName).attr('value', object.countryId));
            $('#CountryId').select2();
            if (selCountry != undefined && selCountry != "") {
                let arr = selCountry.split(',');                
                var arra = Array.from(arr);
                $('#CountryId').val(arr);
                $('#CountryId').trigger('change');               
            }
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryListError(x, y, z) {
    toastr.error(ErrorMessage);   
}
$('#RegionId').on('select2:select', function (e) {
    // Do something
    var data = e.params.data;
    console.log(JSON.stringify(data));
    //console.log("RegionId::" + $('#RegionId').val())
    //$('#tabList RegionIds').val($('#RegionId').val());
    GetCountryList();
});
/*$('#RegionId').on("change", function (e) { console.log("change"); });*/
$('#RegionId').on('select2:unselect', function (e) {
    // Do something
    var data = e.params.data;
    console.log(JSON.stringify(data));
    GetCountryList();
    //console.log("RegionId::" + $('#RegionId').val())
    //$('#tabList RegionIds').val($('#RegionId').val());

});
$('#CountryId').on('select2:select', function (e) {
    // Do something
    var data = e.params.data;
    console.log(JSON.stringify(data));
    //console.log("CountryId::" + $('#CountryId').val())
    
});
function SaveClick()
{
    $('#SaveType').val('Sv');
    $('#RegionIds').val($('#RegionId').val());
    $('#CountryIds').val($('#CountryId').val());
    console.log("Save");
}
function SaveDraftClick() {
    $('#SaveType').val('Drf');
    $('#RegionIds').val($('#RegionId').val());
    $('#CountryIds').val($('#CountryId').val());
    console.log("DraftSave");
}
function SaveApproveClick() {
    $('#SaveType').val('A');
    $('#RegionIds').val($('#RegionId').val());
    $('#CountryIds').val($('#CountryId').val());
    console.log("SaveAppr");
}
function SaveRejectClick() {
    $('#SaveType').val('R');
    $('#RegionIds').val($('#RegionId').val());
    $('#CountryIds').val($('#CountryId').val());
    console.log("SaveReject");
}
function addRowParent(j) {
    var cn = $('#TotalParent').val();
    var i = parseInt(cn) + 1;
    var table = $('#parentBody');
    //if (i == 1) {
    //    var node = $('#parentRow_0').clone(true);
    //    table.append(node);
    //    $('#parentBody tr:eq(1)').prop('id', 'parentRow_' + i + '');
    //}
    //else {
        var node = $('#parentRow_' + j + '').clone(true);
        table.append(node);

    var rowCount = $("#parentBody tr").length;
    //alert(rowCount);
    $('#parentBody tr:eq(' + (rowCount-1) + ')').prop('id', 'parentRow_' + i + '');
    //}
    $('#TotalParent').val(i);
    $('#parentRow_' + i + ' td:first input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].PatentNumber');
    $('#parentRow_' + i + ' td:first input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].PatentNumber');
    $('#parentRow_' + i + ' td:first input').val(i + 1);
    $('#parentRow_' + i + ' td:eq(1) input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].Type');
    $('#parentRow_' + i + ' td:eq(1) input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].Type');
    $('#parentRow_' + i + ' td:eq(1) input').val('');
    $('#parentRow_' + i + ' td:eq(2) input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].OriginalExpiryDate');
    $('#parentRow_' + i + ' td:eq(2) input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].OriginalExpiryDate');
    $('#parentRow_' + i + ' td:eq(2) input').val('');
    $('#parentRow_' + i + ' td:eq(3) input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].ExtensionExpiryDate');
    $('#parentRow_' + i + ' td:eq(3) input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].ExtensionExpiryDate');
    $('#parentRow_' + i + ' td:eq(3) input').val('');
    $('#parentRow_' + i + ' td:eq(4) input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].Comments');
    $('#parentRow_' + i + ' td:eq(4) input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].Comments');
    $('#parentRow_' + i + ' td:eq(4) input').val('');
    $('#parentRow_' + i + ' td:eq(5) input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].Strategy');
    $('#parentRow_' + i + ' td:eq(5) input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].Strategy');
    $('#parentRow_' + i + ' td:eq(5) input').val('');

    $('#parentRow_' + i + ' td:eq(6) i').prop('id', 'addIcon_' + i + '');
    $('#parentRow_' + i + ' td:eq(6) i').attr('onclick', 'addRowParent(' + i + ')');
    $('#parentRow_' + i + ' td:eq(6) spam i').prop('id', 'deleteIcon_' + i + '');
    $('#parentRow_' + i + ' td:eq(6) spam i').attr('onclick', 'removeRowParent(' + i + ')');
    $('#parentRow_' + i + ' td:eq(6) input').prop('id', 'pidf_IPD_PatentDetailsEntities[' + i + '].PatentDetailsID');
    $('#parentRow_' + i + ' td:eq(6) input').prop('name', 'pidf_IPD_PatentDetailsEntities[' + i + '].PatentDetailsID');
    $('#parentRow_' + i + ' td:eq(6) input').val(0);
}
function removeRowParent(j) {
    $("#parentRow_" + j).remove(); 
    //var cn = $('#TotalParent').val();
    //if (cn > 0) {
    //    var i = parseInt(cn) - 1;
    //    $('#TotalParent').val(i);
    //}
}
function tabClick(val,pidfidval) {    
    //let urlPieces = [location.protocol, '//', location.host]
    //var url = urlPieces.join('') + "/PIDForm/PIDForm?pidfid=" + pidfidval + "&bui=" + val;
    var url =  "/PIDForm/PIDForm?pidfid=" + pidfidval + "&bui=" + val;    
    window.location.href = url;     
}
