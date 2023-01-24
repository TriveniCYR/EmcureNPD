

$(document).ready(function () {

    /* This function will call when onchange event fired */
    $("#MarketDetailsNewPortCGIDetails").on("change", function () {
        debugger;
        /* Current this object refer to input element */
        var $input = $(this);
        var reader = new FileReader();
        reader.onload = function () {
            $("#imgPreviewMarketdetails").attr("src", reader.result);
        }
        reader.readAsDataURL($input[0].files[0]);
    });
});

