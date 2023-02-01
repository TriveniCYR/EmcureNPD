
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};
function getFormData($form) {
    var unindexed_array = $form.serializeArray({
        checkboxesAsBools: true
    });
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        if (!indexed_array.hasOwnProperty(n['name'])) {
            indexed_array[n['name']] = n['value'];
        }
    });

    return indexed_array;
}
$.ajaxSetup({
    beforeSend: function (xhr) {
        xhr.setRequestHeader('Authorization', 'Bearer ' + getCookie("EmcureNPDToken"));
    }
});
//Attach the event handler to any element
$(document)
    .ajaxStart(function () {
        //ajax request went so show the loading image
        $('#mainLoader').height("100vh").find("img").show();
    })
    .ajaxStop(function () {
        //got response so hide the loading image
        $('#mainLoader').height("0").find("img").hide();
    });
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}
function setNavigation() {
    var path = window.location.pathname;
    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path);
    var CurrentpathArr = path.split('/');
    
    $(".nav-item a").each(function () {
        debugger;
        var href = $(this).attr('href');
        if (href != '#' && href != undefined) {
            if (path.substring(0, href.length) === href) {
                $(this).addClass('active');
                $(this).parent().parent().parent().addClass('menu-is-opening menu-open');
            }
            /*----start----New logic added----------------*/
            var NavPathArr = href.split('/');
            if (CurrentpathArr[1] == NavPathArr[1]) {
                $(this).addClass('active');
                $(this).parent().parent().parent().addClass('menu-is-opening menu-open');
            }
            /*-----end---New logic added----------------*/
        }
        else {
            $(".nav-treeview .nav-item a").each(function () {
                var href = $(this).attr('href');
                if (path.substring(0, href.length) === href) {
                    $(this).addClass('active');
                }
            });
        }
    });
}

function RemoveDataTable(selector) {
    $(selector).dataTable().fnClearTable();
    $(selector).dataTable().fnDestroy();
}

function StaticDataTable(selector, dom, buttons) {
    if (buttons == null || buttons == undefined) {
        buttons = [
            {
                extend: 'excel', text: '<i class="far fa-file-excel"></i> Export In Excel ', className: "btn-primary", exportOptions: {
                    columns: ':visible'
                }
            },
            { extend: 'colvis', className: "btn-primary" }
        ];
    }
    if (dom == null || dom == undefined) {
        dom = "Bfrtip"
    }

    $(selector).DataTable({
        responsive: true,
        ordering: true,
        lengthChange: true,
        autoWidth: true,
        retrieve: true,
        dom: 'Bfrtip',
        processing: true,
        buttons: buttons,
        language: {
            'loadingRecords': '&nbsp;',
            'processing': '<div class="spinner"></div>'
        }
    });


}
function ajaxServiceMethod(url, type, successCallback, errorCallback, body, header, async, dataType, contentType) {
    if (contentType == null || contentType == undefined) {
        contentType = "application/json;";
    }
    if (dataType == null || dataType == undefined) {
        dataType = "json";
    }
    if (async == null || async == undefined) {
        async = true;
    }

    $.ajax({
        type: type,
        url: url,
        data: body,
        contentType: contentType,
        async: async,
        dataType: dataType,
        success: successCallback,
        error: errorCallback
        //function(xhr, textStatus, errorThrown) {
        //    console.log('error');
        //}
    });
}
$(document).ready(function () {
    setNavigation();
});



