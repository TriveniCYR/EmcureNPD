$(document).ready(function () {
    $("div#tabs").tabs();
    var i = 0;
    //$("#businessUnitTabs li").eq(i).addClass('active');
    var busId = $("#BusinessUnitId").val();
    $("#tab_" + busId).addClass('active');
    //$('#parentRow_' + i + ' td:first input').val(i + 1);
    //console.log("businessUnitTabs:::" +$('#businessUnitTabs li').children());
    //$('#businessUnitTabs li').children().each(function () {
    //    console.log($(this).attr('id'));
    //});
});