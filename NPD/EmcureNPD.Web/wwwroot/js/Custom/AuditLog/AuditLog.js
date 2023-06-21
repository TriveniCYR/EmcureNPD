var tableId = "AuditLogTable";
$(document).ready(function () {
    InitializeAuditLogList();
});

function InitializeAuditLogList() {
    var setDefaultOrder = [1, 'desc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllAuditLog,
        "type": "POST",
        //"data": function (d) {
        //    var pageNumber = $('#' + tableId).DataTable().page.info();
        //    d.PageNumber = pageNumber.page;
        //},
        "datatype": "json"
    };

    var columnObject = [
        {
            "data": "moduleName", "name": "Module Name"
        },
        //{
        //    "data": "actionType", "name": "Action Type"
        //},
        {
            "data": "createdDate", "Created Date": "Action", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY h:m");
            }
        },
        {
            "data": "createdBy", "name": "Created By"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                html += '<a type="button" data-toggle="modal" data-target="#AuditLogViewModel" data-backdrop="static" data-keyboard="false" onclick=\'Viewlog(' + row.log + ',' + '"' + row.createdDate + '"' + ',' + '"' + row.createdBy + '")\' class="ml-1 large-font" ><i class="fa fa-fw fa-eye mr-1"></i></a>';
                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

function Viewlog(log, createdDate, createdBy) {
    //const date = new Date(createdDate);
    //const day = date.getDate().toString().padStart(2, '0');
    //const month = (date.getMonth() + 1).toString().padStart(2, '0');
    //const year = date.getFullYear().toString().slice(-2);
    //var formateddate = formatDate(createdDate)
    let jdata = log;
    log = JSON.stringify(log);

    $.ajax({
        url: "/AuditLog/AuditLogPartialView",
        type: "POST",
        data: { CreatedDate: createdDate, CreatedBy: createdBy, log: log },
        success: function (result) {
            console.log(jdata)
            let htm = `<div class="row"> <div class="col-3">
            <label class="control-label">Created By</label>
                        </div>
                        <div class="col-3">
                             <label class="form-control">${createdBy}</label>
                        </div>
                        <div class="col-3">
                            <label class="control-label">Created Date</label>
                            
                        </div>
                        <div class="col-3">
                             <label class="form-control">${createdDate}</label>
                        </div><br><hr>`;
            if (jdata != null) {
                for (var i = 0; i < jdata.length; i++) {
                    htm +=`<div class="col-4 mb-4">
                        <label for="propertyName" class="form-label">Property Name</label>
                        <input type="text" class="form-control" id="propertyName" disabled="" value="${jdata[i].DisplayName}">
                    </div>
                    <div class="col-4 mb-4">
                        <label for="propertyName" class="form-label">Old Value</label>
                        <input type="text" class="form-control" id="oldValue" disabled="" value="${jdata[i].OldValue}">
                    </div>
                     <div class="col-4 mb-4">
                        <label for="propertyName" class="form-label">New Value</label>
                        <input type="text" class="form-control" id="newValue" disabled="" value="${jdata[i].NewValue}">
                    </div>`
                    if (jdata[i].DisplayName.includes("BusinessUnitsByUser")) {
                       
                        htm += `<div class="col-4 mb-4">
                        <label for="propertyName" class="form-label">Property Name</label>
                        <input type="text" class="form-control" id="propertyName" disabled="" value="BusinessUnitsByUser">
                    </div>
                    <div class="col-4 mb-4">
                        <label for="oldValueddl" class="form-label">Old Value</label>
                                 <select class="form-control readOnlyUpdate" id="oldValueddl" data-val="[${jdata[i].OldValue}]"  required="required" multiple="multiple"></select>
                                 </div>
                    <div class="col-4 mb-4">
                        <label for="newValueddl" class="form-label">New Value</label>
                    <select class="form-control readOnlyUpdate newValueddl" id="newValueddl"  required="required"  multiple="multiple"></select>
                    </div> 
                    `;
                        
                        GetAllDropdown();
                        let childJobjnewval = jdata[i].NewValue.split(',');
                        let childJobjOldval = jdata[i].OldValue.split(',');
                       //for (var x = 0; x < childJobjnewval.length; x++) {
                       //    //$("#newValueddl").find('option[value="' + parseInt( childJobjnewval[x]) + '"]').prop('selected', true);
                       //    $("#newValueddl").val(parseInt(childJobjnewval[x].toString())//.find(`option`).find(`value = "${parseInt(childJobjnewval[x])}"`).prop('selected', true);
                       //
                       //}
                       //for (var y = 0; y < childJobjOldval.length; y++) {
                       //    //$("#oldValueddl").find('option[value="' + parseInt(childJobjOldval[y]) + '"]').prop('selected', true);
                       //    $("#oldValueddl").val(parseInt(childJobjOldval[y].toString())//find(`option`).find(`value="${parseInt(childJobjOldval[y])}"`).prop('selected', true);
                       //    $("#oldValueddl").trigger("change");
                       //    $("#oldValueddl").trigger('close');
                       //}
                        $.each($(".newValueddl"), function (intex, item) {
                            if (childJobjnewval.includes(item)) { 
                            $(this).select2('val', item).prop('selected', true);
                        }
                        });
                       // $("#newValueddl").select2("val", childJobjnewval);
                       //let newVals = [parseInt(jdata[i].NewValue)];
                       //$("#newValueddl").val(val).change();//.select2().select2('val', childJobjnewval);
                       // $("#newValueddl").trigger("change");
                       // $("#newValueddl").trigger('close');
                       // $("#oldValueddl")//.select2().select2('val', childJobjOldval);
                        //$("#newValueddl").select2(childJobjnewval);
                        //$("#oldValueddl").select2(childJobjOldval);
                        
                    }
                    if (jdata[i].DisplayName == "RNDPhaseWiseBudgetRawData") {
                        console.log(main(jdata[i].NewValue, 'NewValue'));
                    }

                }
            }
            htm += '</div>';
            $("#AuditLogViewModel").find(".modal-body").html(htm);
            
            $("#AuditLogViewModel").modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            //console.log("Error: " + textStatus + " - " + errorThrown);
        }
    });
}

//find nested array of object//
function main(obj = {}, property) {
    const views = [];

    function traverse(o) {
        for (var i in o) {
            if (i === property) views.push(o[i]);
            if (!!o[i] && typeof (o[i]) == "object") traverse(o[i]);
        }
    }

    traverse(obj);
    return views;
}
//get all dropdown//
function GetAllDropdown() {
  let  _PIDFID = 0;
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPBF + "/" + _PIDFID, 'GET', GetPBFDropdownSuccess, GetPBFDropdownError);
}
function GetPBFDropdownSuccess(data) {
    try {
        if (data != null) {
            $.each(data.MasterBusinessUnit, function (index, object) {
                $('#newValueddl').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
                $('#oldValueddl').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
            });
            $('#newValueddl').select2();//{ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter'), placeholder: "Select BusinessUnit..", }
            $('#newValueddl').trigger('change');
            $('#oldValueddl').select2();
            $('#oldValueddl').trigger('change');
          //$(data.MasterBERequirement).each(function (index, item) {
          //    $('#BERequirementId').append('<option value="' + item.beRequirementId + '">' + item.beRequirementName + '</option>');
          //});
          //$(data.MasterDosage).each(function (index, item) {
          //    $('#PbfDosageFormId').append('<option value="' + item.dosageId + '">' + item.dosageName + '</option>');
          //});
          //$(data.MasterPlant).each(function (index, item) {
          //    $('#PlantId').append('<option value="' + item.plantId + '">' + item.plantNameName + '</option>');
          //});
          //$(data.MasterPlant).each(function (index, item) {
          //    $('#ProductTypeId_Tab').append('<option value="' + item.plantId + '">' + item.plantNameName + '</option>');
          //});
          //$(data.MasterWorkflow).each(function (index, item) {
          //    $('#WorkflowId').append('<option value="' + item.workflowId + '">' + item.workflowName + '</option>');
          //});
          //$(data.MasterFilingType).each(function (index, item) {
          //    $('#FillingTypeId').append('<option value="' + item.filingTypeId + '">' + item.filingTypeName + '</option>');
          //});
          //$(data.MasterFormRnDDivision).each(function (index, item) {
          //    $('#PbfFormRNDDivisionId').append('<option value="' + item.formRnDDivisionId + '">' + item.formRnDDivisionName + '</option>');
          //});
          //$(data.MasterPackagingType).each(function (index, item) {
          //    $('#PbfPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
          //});
            //$(data.MasterManufacturing).each(function (index, item) {
            //    $('#PbfManufacturingId').append('<option value="' + item.manufacturingId + '">' + item.manufacturingName + '</option>');
            //});
           
                }
            } catch (e) {
            }
        }
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}