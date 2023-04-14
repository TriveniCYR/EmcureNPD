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
        /*$('#mainLoader').height("100vh").find("img").show();*/
        $('#loading-wrapper').show();
    })
    .ajaxStop(function () {
        //got response so hide the loading image
        /*$('#mainLoader').height("0").find("img").hide();*/
        setTimeout(function () { $('#loading-wrapper').hide(); }, 500);
    });
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}
function setNavigation() {
    var path = window.location.pathname;
    var currenthref = window.location.href;
    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path);
    var CurrentpathArr = path.split('/');
    if (path == `/PIDF/PIDFList`) {
        $(".nav-item a").each(function () {
            var IshrefFound = false;
            var href = $(this).attr('href');
            if (currenthref.split('?')[1] == href.split('?')[1]) {
                $(this).addClass('active');
                $(this).parent().parent().parent().addClass('menu-is-opening menu-open');
                IshrefFound = true;
                return false;
            }
        }
        )
    };
    if (path.includes('PIDF') || path.includes('PBF') || path.includes('Audit')) {
        $('#parentPIDF').addClass('menu-is-opening menu-open');        
    }

    $(".nav-item a").each(function () {
        var IshrefFound = false;
        var href = $(this).attr('href');
        if (href==undefined) {
            return;
        }
        var NavPathArr = href.split('/');
        if (href != '#' && href != undefined) {
            if (path.substring(0, href.length) === href) {
                $(this).addClass('active');
                $(this).parent().parent().parent().addClass('menu-is-opening menu-open');
                IshrefFound = true;
                return false;
            }
            /*----start----New logic added----------------*/
            if (CurrentpathArr[1] == NavPathArr[1] && !IshrefFound) {
                // $(this).addClass('active');
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

    var _datatableInstance = $(selector).DataTable({
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
    return _datatableInstance;
}
function destoryStaticDataTable(selector) {
    if ($.fn.DataTable.isDataTable(selector)) {
        $(selector).DataTable().clear().destroy();
    }
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
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
$(document).ready(function () {
    setNavigation();
    if ($('.scrollElement').length > 0) {
        $('.scrollElement').overlayScrollbars({
            className: "os-theme-dark"
        });
    }
});
function ApproveRejectClick(type, PIDFID, ScreenId, URL) {
    if (PIDFID != undefined && PIDFID != "" && PIDFID != null) {
        if (type == "A") {
            $('#ApproveRejectModel').find('#ApproveRejectTitle').html(_ApproveTitle);
            $('#ApproveRejectModel').find('#ApproveRejectLabel').html(_ApproveConfirm);
        } else {
            $('#ApproveRejectModel').find('#ApproveRejectTitle').html(_RejectTitle);
            $('#ApproveRejectModel').find('#ApproveRejectLabel').html(_RejectConfirm);
        }
        $('#hdnStatuspidfIds').val(PIDFID);
        $('#hdnStatusSaveType').val(type);
        $('#hdnStatusscreenId').val(ScreenId);
        $('#hdnStatusReturnUrl').val(URL);
        $('#ApproveRejectModel').modal('show');
    } else {
        toastr.error("Select Pidf");
    }
}
function ApproveRejectConfirm() {
    var _PIDFIds = $('#hdnStatuspidfIds').val();
    if ($('#txtStatusComment').val() != "") {
        if (_PIDFIds != undefined && _PIDFIds != "" && _PIDFIds != null) {
            var objApproveRejectList = [];
            var _numberPIDFIds = _PIDFIds.split(",").map(Number);
            $.each(_numberPIDFIds, function (index, item) {
                objApproveRejectList.push({ pidfId: item });
            });

            var objIds = {
                saveType: $('#hdnStatusSaveType').val(),
                PidfIds: objApproveRejectList,
                screenId: $('#hdnStatusscreenId').val(),
                comment: $('#txtStatusComment').val()
            };
            ajaxServiceMethod($('#hdnBaseURL').val() + ApproveRejectDeletePidf, 'POST', SaveApproveRejectSuccess, SaveApproveRejectError, JSON.stringify(objIds));
        }
        $('#ApproveRejectModel').modal('hide');
    }
    else {
        toastr.error("Please Enter Comments!","ERROR:")
    }
}
function SaveApproveRejectSuccess(data) {
    try {
        if (data._Success === true) {
            toastr.success(data._Message);
            window.location = $('#hdnStatusReturnUrl').val();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveApproveRejectError(x, y, z) {
    toastr.error(ErrorMessage);
}
function getPIDFAccordion(url, _PIDFId, divId) {
    var html = '<div class="card collapsed-card"><div class="card-header bg-primary"><h3 class="card-title mb-0"><button id="btnPIDFAccordionHeader" type="button" class="btn btn-tool" data-card-widget="collapse">Product Identification Form</button></h3><div class="card-tools"><button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse"><i class="fas fa-plus"></i></button></div></div><div class="card-body p-0"><div id="dvPIDFContainerAccordion"></div></div></div>';
    $.get(url, {
        PIDFId: _PIDFId, _Partial: true, IsViewMode: true
    }, function (content) {
        $("#" + divId).html(html);
        $("#" + divId).find("#dvPIDFContainerAccordion").html(content);
    });
}
function getIPDAccordion(url, _PIDFId, _PIDFBusinessUnitId, divId) {
    var html = '<div class="card collapsed-card"><div class="card-header bg-primary"><h3 class="card-title mb-0"><button id="btnIPDAccordionHeader" type="button" class="btn btn-tool" data-card-widget="collapse">Internal Patent Details</button></h3><div class="card-tools"><button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse"><i class="fas fa-plus"></i></button></div></div><div class="card-body p-0"><div id="dvIPDContainerAccordion"></div></div></div>';
    $.get(url, {
        PIDFId: _PIDFId, bui: _PIDFBusinessUnitId, _Partial: true, IsView: 1
    }, function (content) {
        $("#" + divId).html(html);
        $("#" + divId).find("#dvIPDContainerAccordion").html(content);
    });
}
function getCommercialAccordion(url, _PIDFId, _PIDFBusinessUnitId, divId) {
    var html = '<div class="card collapsed-card"><div class="card-header bg-primary"><h3 class="card-title mb-0"><button id="btnCommercialAccordionHeader" type="button" class="btn btn-tool" data-card-widget="collapse">Commercial Details</button></h3><div class="card-tools"><button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse"><i class="fas fa-plus"></i></button></div></div><div class="card-body p-0"><div id="dvCommercialContainerAccordion"></div></div></div>';
    $.get(url, {
        pidfid: _PIDFId, bui: _PIDFBusinessUnitId, _Partial: true, IsView: 1
    }, function (content) {
        $("#" + divId).html(html);
        $("#" + divId).find("#dvCommercialContainerAccordion").html(content);
    });
}
function CancelPopup() {
    $('#CancelModel').modal('show');
}
function formatDate(dateStr) {
    function z(n) { return (n < 10 ? '0' : '') + n }
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var b = dateStr.split(/\D+/);
    var d = new Date(b[0], --b[1], b[2], b[3], b[5], b[5]);
    return d.getDate() + ' ' + months[d.getMonth()] + ' ' + d.getFullYear() +
        ' ' + z(d.getHours() % 12 || 12) + ':' + z(d.getMinutes()) +
        ' ' + (d.getHours() < 12 ? 'am' : 'pm');
}