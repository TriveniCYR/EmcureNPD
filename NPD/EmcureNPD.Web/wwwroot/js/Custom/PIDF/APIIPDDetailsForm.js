
$(document).ready(function () {
    SetDivReadonly();
    if (SaveStatus != '' && SaveStatus != undefined)
    {
        if (SaveStatus == 'Saved successfully.')
            toastr.success(SaveStatus);
        else
            toastr.error(SaveStatus);
    }
});

$(window).on("load", function () {
    console.log("window loaded");
});

$('#btnPreview').click(function () {
    debugger;
    var $input = $("#MarketDetailsNewPortCGIDetails");
    var reader = new FileReader();
    reader.onload = function () {
        $("#imgPreviewMarketdetails").attr("src", reader.result);
    }
    reader.readAsDataURL($input[0].files[0]);
});

$('#Save').click(function () {
    $("#SaveType").val('Save');    
});
$('#SaveDraft').click(function () {
    $("#SaveType").val('SaveDraft');
});
function SetDivReadonly() {
    $("#CommercialPIDFScreen").find("input, submit, textarea, a, select").attr("disabled", "disabled");
    $("#CommercialPIDFScreen").find("button, submit, a").hide();
    $("#PIDFScreen").find("#collapseButton").show();
    $("#PIDFScreen").find("#PIDFormcollapseButton").show();
    $("#CommercialPIDFScreen").find("#CommercialcollapseButton").show();
    
    $("#CommercialcollapseButton").click();
    $("#PIDFormcollapseButton").click();
    $("#collapseButton").click();    
}
function ShowPopUpAPIIPD() {
    $("#CancelModelAPIIPD").find("button, submit, a").show();
    $('#CancelModelAPIIPD').modal('show');
}





  


