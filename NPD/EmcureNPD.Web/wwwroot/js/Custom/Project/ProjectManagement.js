var _milestoneInstance;
var _milestoneData = [];
let arrtasklevel = [];
$(document).ready(function () {
    GetProjectDetails(0);
    GetBusinessUnitDetails(bid);

    // Add event listener for opening and closing details
    $('#Milestones tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = _milestoneInstance.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row
            row.child(CustomizeChildContent(row.data())).show();
            tr.addClass('shown');
        }
    });
    HideIfEditPermissionOnly();
    
});
function IsViewModeProject() {
    if ($("#IsView").val() == '1') {
        SetProjectFormReadonly();
        HideIfEditPermissionOnly();
    }
}
function HideIfEditPermissionOnly() {
    if (IsAddPermission != 'True') {
        $("#addTaskButton").prop('disabled', true);
        $(".addSubTaskBtn").prop('hidden', true);
    }
    if (IsUpdatePermission != 'True') {
        $(".editBtn").prop('hidden', true);
    }
    if (IsDeletePermission != 'True'){
        $(".deleteBtn").prop('hidden', true);
    }
}

function SetProjectFormReadonly() {
    $("#addTaskButton").prop('disabled', true);
    $(".addSubTaskBtn").prop('hidden', true);
    $(".editBtn").prop('hidden', true);
    $(".deleteBtn").prop('hidden', true);
}
function CustomizeChildContent(d) {
    try {
        var _projectTaskId = parseInt($(d[0]).val());

        $.grep(data, function (n, i) {
            return n.prop_1 === 'val_11';
        });
    } catch (e) {
    }

    return (
        ''
    );
}
function GetBusinessUnitDetails(bid) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetBusinessUnitDetail + "/" + bid + "/" + $('#pidfId').val(), 'GET', GetBusinessUnitDetailsSuccess, GetBusinessUnitDetailsError);
}
function GetBusinessUnitDetailsSuccess(data) {
    try {
        //business unit details
        $('#BDetailsTable tbody').html('');
        if (data.table.length > 0) {
            $.each(data.table, function (index, object) {
                $('#BDetailsTable tbody').append('<tr><td>' + object.projectName +
                    '</td><td>' + object.country + '</td><td>' + object.inHouses +
                    '</td><td>' + object.strength + '</td><td>' + object.year +
                    '</td><td>' + object.packagingTypeName + '</td><td>' + object.batchSize +
                    '</td><td>' + object.currencyName + '</td><td>' + object.marketSize +
                    '</td><td>' + object.priceDiscounting + '</td><td>' + object.marketGrowth +
                    '</td><td>' + object.suimsVolume + '</td><td>' + object.totalAPIReq + '</td></td></tr>');
            })
        }
        else {
            $('#BDetailsTable tbody').append('<tr><td></td> <td></td> <td></td> <td></td> <td></td> <td></td><td width="20%" style="text-align:center;"><span style="color:red"><b>No Record Found</b></span></td><td></td> <td></td> <td></td> <td></td> <td></td> <td></td></tr>');
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBusinessUnitDetailsError() {
    toastr.error(ErrorMessage);
}
function GetProjectDetails(filterId = 0) {
    $(".status_filter").removeClass("active");
    if (filterId == 1) {
        $("#btnMonthly").addClass("active");
    }
    else if (filterId == 2) {
        $("#btnQuarterly").addClass("active");
    }
    else if (filterId == 3) {
        $("#btnHalfYearly").addClass("active");
    }
    else if (filterId == 4) {
        $("#btnAnnualy").addClass("active");
    }
    else { $("#btnAll").addClass("active"); }
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllData + "/" + $('#pidfId').val() + '-' + filterId, 'GET', GetProjectDetailsSuccess, GetProjectDetailsError);
}
function GetProjectDetailsSuccess(data) {
    try {
        //project details
        $('#loading').hide();
        localStorage.setItem("prjName", data.table[0].projectName);
        $('#pidf_ProjectorProductName').text(data.table[0].projectName);
        $('#pidf_ProductTypeName').text(data.table[0].productType);
        $('#pidf_PlantName').text(data.table[0].plantName);
       // $('#pidf_FormulationName').text(data.table[0].formulation);
        $('#pidf_WorkflowName').text(data.table[0].workFlow);
        $(".StrengthandunitofMeasurementName").empty();
        $.each(data.table2, function (i, List) {
            var newRow = $("<tr class='StrengthandunitofMeasurementName'>");
            var cols = "";
            cols += '<td>' + List.strength + '</td>';
            cols += '<td>' + List.unitofMeasurementName + ' </td>';
            newRow.append(cols);
            $("table.order-list").append(newRow);
        });
        //end
        //File details
        $('#files tbody').html('')
        $.each(data.table1, function (index, file) {
            var link = $('#hdnBaseURL').val() + '/Uploads/PIDF/Medical/' + file.fileName;
            $('#files tbody').append('<tr><td><a href="' + link + '" target="_blank">' + file.fileName + '</a></td></tr>');
            //console.log("Files:" + link)
        });
        //end
        //Businessunit details

        function addLiElement(bunits, bid) {
            var activeClass = bunits.businessUnitId == bid ? 'active' : '';
            var liHtml = `<li class="nav-item p-0">
                  <a class="nav-link ${activeClass}" id="${bunits.businessUnitId}" aria-selected="false">${bunits.businessUnitName}</a>
                </li>`;
            var $li = $(liHtml);
            $li.click(function () {
                $('.nav-link.active').removeClass('active');
                // Add active class to highlight the selected item
                $(this).find('a').addClass('active');
                GetBusinessUnitDetails(bunits.businessUnitId);
                var $span = $('<span>').text(bunits.businessUnitName);
                $('#BHeading').empty().append($span);
            });
            return $li;
        }
        $("#custom-tabs-one-tab").html('');
        $('#custom-tabs-one-tab li').slice(1).remove();
        $.each(data.table3, function (index, bunits) {
            var $li = addLiElement(bunits, bid);
            $('#custom-tabs-one-tab').append($li);
        });
        //end

        _milestoneData = data.table4;

        //tasksubtask list details
        $('#Milestones tbody').html('');
        //$('#ChildMilestones tbody').html('');
        var childRow = "";
        let tableRow = null;
        let mainTaskRowIndex = 0;
        $.each(data.table4, function (index, object) {
            var start = new Date(object.startDate);
            var startDate =start.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
            var end = new Date(object.endDate);
            var endDate = end.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
            if (object.modifyDate != null) {
               var updated = new Date(object.modifyDate);
                var updatedDate =updated.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
            }
            else
                var updatedDate = "";
            if (object.taskLevel == 1) {
                mainTaskRowIndex++;
            }
            var edit = '<a class="large-font editBtn" style="" href="" title="Edit" data-toggle="modal" data-target="#UpdateModel" data-backdrop="static" data-keyboard="false"  onclick="GetTaskSubTaskById(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a>';
            var deleteTag = '<a class="large-font text-danger deleteBtn" style="" href="" title="Delete" data-toggle="modal" data-target="#DeleteModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteTaskSubTask(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>';

            let addSubTaskButton = '<a class="large-font addSubTaskBtn" name="btnaddSubTask" style="" href="" title="Add SubTask" onclick="ShowAddSubTaskForm(\'' + object.projectTaskId + '\', \'' + object.taskName + '\',\'' + object.taskLevel + '\'); return false;"><i class="fa fa-fw fa-plus mr-1"></i> ' + '</a>';
            
            //let displaySubTaskListButton = '<a class="large-font" style="" href="javascript:ShowChildRows(' + object.projectTaskId+');" title="Show Sub task"><i class="fa fa-plus-circle icoShowSubtask' + object.projectTaskId+ '" aria-hidden="true" style="color:#31b131;"></i>' + '</a>';
            let displaySubTaskListButton = '<a class="large-font" style="" href="javascript:ShowChildRows(' + mainTaskRowIndex + ');" title="Show Sub task"><i class="fa fa-plus-circle icoShowSubtask' + mainTaskRowIndex +'" aria-hidden="true" style="color:#31b131;"></i>' + '</a>';

            var _parentClass = (object.taskLevel > 1 ? "treegrid-parent-" + object.parentId + "" : "");
          
            if (object.taskLevel > 1)
            {
                let colorCode =1;
                
                arrtasklevel.push(object.taskLevel);
                for (var i = 0; i < arrtasklevel.length; i++) {
                   
                    let bgColor = "#000" + colorCode + "00d;"
                    if (i == 0) {
                        //tableRow += '<tr title="Sub Task Level:' + object.taskLevel + '" style="display:none;background: ' + bgColor.toString() + '" class="clildrows' + object.parentId + ' treegrid-' + index + ' ' + _parentClass + '"><td><input type="hidden" value="' + object.projectTaskId + '" />' + (object.taskLevel > 1 ? "" : displaySubTaskListButton) + '</td><td><b>Sub Task Level:' + object.taskLevel + ':-</b>' + object.taskName + '</td><td>' + object.fullName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td><div class="progress"><div class="progress-bar" role="progressbar" style="width: ' + object.totalPercentage + '%;" aria-valuenow="' + object.totalPercentage + '" aria-valuemin="0" aria-valuemax="100">' + object.totalPercentage + '%</div></div></td><td>' + updatedDate + '</td><td>' + edit + deleteTag + addSubTaskButton + '</td></tr>';
                        tableRow += '<tr style="display:none;background: ' + bgColor.toString() + '" class="clildrows' + mainTaskRowIndex + ' treegrid-' + index + ' ' + _parentClass + '"><td><input type="hidden" value="' + object.projectTaskId + '" />' + (object.taskLevel > 1 ? "" : displaySubTaskListButton) + '</td><td class="task-level-' + object.taskLevel +'">' + object.taskName + '</td><td>' + object.fullName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td><div class="progress"><div class="progress-bar" role="progressbar" style="width: ' + object.totalPercentage + '%;" aria-valuenow="' + object.totalPercentage + '" aria-valuemin="0" aria-valuemax="100">' + object.totalPercentage + '%</div></div></td><td>' + updatedDate + '</td><td>' + edit + deleteTag + addSubTaskButton + '</td></tr>';
                    }
                    colorCode++;
                 }
            }
            else {
                tableRow += '<tr class="treegrid-' + index + '"><td><input type="hidden" value="' + object.projectTaskId + '" />' + displaySubTaskListButton +'</td><td>' + object.taskName + '</td><td>' + object.fullName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td><div class="progress"><div class="progress-bar" role="progressbar" style="width: ' + object.totalPercentage + '%;" aria-valuenow="' + object.totalPercentage + '" aria-valuemin="0" aria-valuemax="100">' + object.totalPercentage + '%</div></div></td><td>' + updatedDate + '</td><td>' + edit + deleteTag + addSubTaskButton+'</td></tr>';
                
            }
           
        });
        $('#Milestones tbody').append(tableRow);
        $('.tree').treegrid();

        //_milestoneInstance = StaticDataTable("#Milestones");
        //end

        HideIfEditPermissionOnly();
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
    //to hide buttons when page is in viewmode
    IsViewModeProject();
}
function GetProjectDetailsError() {
    toastr.error(ErrorMessage);
}

function GetTaskSubTaskById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetTaskSubTaskByIds + "/" + id, 'GET', GetTaskSubTaskByIdSuccess, GetTaskSubTaskByIdError);
}
function GetTaskSubTaskByIdSuccess(data) {
    try {
        $('#loading').hide();
        $("#TaskName").val(data._object.taskName);
        $('#TaskOwner').empty().append(
            "<option value=''>Please select option</option>"
        );
        $.each(data._object.taskOwner, function (i, List) {
            $("#TaskOwner").append('<option value="' + List.userId + '">' +
                List.fullName + '</option>');
        });

        $('#TaskOwner option[value="' + data._object.editTaskOwnerId + '"]').attr("selected", true);

        $('#TaskPriority').empty().append(
            "<option value=''>Please select option</option>"
        );
        $.each(data._object.priority, function (i, List) {
            $("#TaskPriority").append('<option value="' + List.priorityId + '">' +
                List.priorityName + '</option>');
        });
        $('#TaskPriority option[value="' + data._object.editTaskPriorityId + '"]').attr("selected", true);

        $('#TaskStatus').empty().append(
            "<option value=''>Please select option</option>"
        );
        $.each(data._object.status, function (i, List) {
            $("#TaskStatus").append('<option value="' + List.statusId + '">' +
                List.statusName + '</option>');
        });
        $('#TaskStatus option[value="' + data._object.editTaskStatusId + '"]').attr("selected", true);

        var startDate = new Date(data._object.startDate);
        var endDate = new Date(data._object.endDate);

        // Format the date strings in the "YYYY-MM-DD" format
        var formattedStartDate = startDate.getFullYear() + '-' + ('0' + (startDate.getMonth() + 1)).slice(-2) + '-' + ('0' + startDate.getDate()).slice(-2);
        var formattedEndDate = endDate.getFullYear() + '-' + ('0' + (endDate.getMonth() + 1)).slice(-2) + '-' + ('0' + endDate.getDate()).slice(-2);

        $("#StartDate").val(formattedStartDate);
        $("#EndDate").val(formattedEndDate);

        $("#TaskDuration").val(data._object.taskDuration);
        $("#projectTaskId").val(data._object.projectTaskId)
        $("#pidfid").val(data._object.pidfid)
        $("#taskLevel").val(data._object.taskLevel)
        $("#parentId").val(data._object.parentId)

        if (data._object.editTaskStatusId == 1) {
            $(".disabledPercentage").prop("readOnly", true);
            $("#TaskPercentage").val(data._object.totalPercentage);
        } else if (data._object.editTaskStatusId == 2) {
            $(".disabledPercentage").prop("readOnly", false);
            $("#TaskPercentage").val(data._object.totalPercentage);
        }
        else if (data._object.editTaskStatusId == 3) {
            $(".disabledPercentage").prop("readOnly", true);
            $("#TaskPercentage").val(data._object.totalPercentage);
        }
        setTabIndex("UpdateModel");
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetTaskSubTaskByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//end region

//delete tasksubtask by
function ConfirmationDeleteTaskSubTask(id) {
    $('#DeleteModel #ProjectTaskId').val(id);
}
function DeleteTaskSubTask() {
    var projectId = $('#DeleteModel #ProjectTaskId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteTasksubTask + "/" + projectId, 'POST', DeleteTaskSubSuccess, DeleteTaskSubError);
}
function DeleteTaskSubSuccess(data) {
    try {
        if (data._Success === true) {
            toastr.success(RecordDelete);
            GetProjectDetails();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteTaskSubError(x, y, z) {
    toastr.error(ErrorMessage);
}
//end region

//add task
function AddTaskSubTask() {
    
    $('#AddModel').modal('show');
    setTabIndex("AddModel");
}

function ShowAddTaskForm() {
    CleareTaskFields();
    $('#AddTaskModel').modal('show');
    setTabIndex("AddTaskModel");
    $('#loading').show();
    ajaxServiceMethod($('#hdnBaseURL').val() + FillTaskDropdown, 'GET', GetDropdownsForAddTaskSuccess, GetDropdownsForAddTaskError);
}
function GetDropdownsForAddTaskSuccess(data) {
    try {
        $('#loading').hide();
        $('#AddTaskOwner').empty().append(
            "<option value=''>Please select option</option>"
        );

        $.each(data.taskOwner, function (i, List) {
            $("#AddTaskOwner").append('<option value="' + List.userId + '">' +
                List.fullName + '</option>');
        });

        $('#AddTaskPriority').empty().append(
            "<option value=''>Please select option</option>"
        );

        $.each(data.priority, function (i, List) {
            $("#AddTaskPriority").append('<option value="' + List.priorityId + '">' +
                List.priorityName + '</option>');
        });

        $('#AddTaskStatus').empty().append(
            "<option value=''>Please select option</option>"
        );

        $.each(data.status, function (i, List) {
            $("#AddTaskStatus").append('<option value="' + List.statusId + '">' +
                List.statusName + '</option>');
        });
    }
    catch(e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDropdownsForAddTaskError(x, y, z) {
    toastr.error(ErrorMessage);
}

function HideAddTaskModel() {
    $('#AddTaskModel').modal('hide');
}
//end add task
function HideUpdateModel() {
    $('#UpdateModel').modal('hide');
}
// add sub task
function ShowAddSubTaskForm(parentId, taskName,taskLevel) {
    CleareSubTaskFields();
    //ArrMax = Math.max.apply(Math, arrtasklevel)
    let newTaskLevel = parseInt(taskLevel) + 1;
    
    $('#AddSubTaskModel').modal('show');
    setTabIndex("AddSubTaskModel");
    $('#AddSubTaskofTask').empty().append(
        "<option selected readonly value='" + parentId + "'>" + taskName + "</option>"
    );
    ajaxServiceMethod($('#hdnBaseURL').val() + FillTaskDropdown, 'GET', GetDropdownsForAddSubTaskSuccess, GetDropdownsForAddSubTaskError);
    $("#SubTaskLevel").val(newTaskLevel);
}
function GetDropdownsForAddSubTaskSuccess(data) {
    try {
        $('#loading').hide();
        //$('#AddSubTaskofTask').empty().append(
        //    "<option value=''>Please select option</option>"
        //);

        //$.each(data.task, function (i, List) {
        //    $("#AddSubTaskofTask").append('<option value="' + List.projectTaskId + '">' +
        //        List.taskName + '</option>');
        //});
        $('#AddSubTaskOwner').empty().append(
            "<option value=''>Please select option</option>"
        );

        $.each(data.taskOwner, function (i, List) {
            $("#AddSubTaskOwner").append('<option value="' + List.userId + '">' +
                List.fullName + '</option>');
        });

        $('#AddSubTaskPriority').empty().append(
            "<option value=''>Please select option</option>"
        );

        $.each(data.priority, function (i, List) {
            $("#AddSubTaskPriority").append('<option value="' + List.priorityId + '">' +
                List.priorityName + '</option>');
        });

        $('#AddSubTaskStatus').empty().append(
            "<option value=''>Please select option</option>"
        );

        $.each(data.status, function (i, List) {
            $("#AddSubTaskStatus").append('<option value="' + List.statusId + '">' +
                List.statusName + '</option>');
        });
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDropdownsForAddSubTaskError(x, y, z) {
    toastr.error(ErrorMessage);
}
function HideAddSubTaskModel() {
    $('#AddSubTaskModel').modal('hide');
}
//#end region

$('#AddSubTaskDuration').on('keypress', function () {
    var charCode = window.event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
});

$('#AddSubTaskPercentage').on('keypress', function () {
    var charCode = window.event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
});
//set percentage
function setPercentage(statusID, idx) {
    if (statusID == 1) {
        //Completed Status
        if (idx == 2) {
            $(".disabledSubTaskPercentage").prop("readOnly", true);
            $('#AddSubTaskPercentage').val('0');
        } else if (idx == 1) {
            $(".disabledTaskPercentage").prop("readOnly", true);
            $('#AddTaskPercentage').val('0');
        }
        else if (idx == 0) {
            $(".disabledPercentage").prop("readOnly", true);
            $('#TaskPercentage').val('0');
        }
    }
    else if (statusID == 2) {
        //InProgress status
        if (idx == 2) {
            $(".disabledSubTaskPercentage").prop("readOnly", false);
            $('#AddSubTaskPercentage').val('0');
        } else if (idx == 1) {
            $(".disabledTaskPercentage").prop("readOnly", false);
            $('#AddTaskPercentage').val('0');
        }
        else if (idx == 0) {
            $(".disabledPercentage").prop("readOnly", false);
            $('#TaskPercentage').val('0');
        }
    }
    else if (statusID == 3) {
        //Initial status
        if (idx == 2) {
            $(".disabledSubTaskPercentage").prop("readOnly", true);
            $('#AddSubTaskPercentage').val('100');
        } else if (idx == 1) {
            $(".disabledTaskPercentage").prop("readOnly", true);
            $('#AddTaskPercentage').val('100');
        }
        else if (idx == 0) {
            $(".disabledPercentage").prop("readOnly", true);
            $('#TaskPercentage').val('100');
        }
    }
    else {
        if (idx == 2) {
            $(".disabledSubTaskPercentage").prop("readOnly", false);
            $('#AddSubTaskPercentage').val('0');
        } else if (idx == 1) {
            $(".disabledTaskPercentage").prop("readOnly", false);
            $('#AddTaskPercentage').val('0');
        }
        else if (idx == 0) {
            $(".disabledPercentage").prop("readOnly", false);
            $('#TaskPercentage').val('0');
        }
    }
}
//#end region
$('#AddTaskDuration').on('keypress', function () {
    var charCode = window.event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
});

$('#AddTaskPercentage').on('keypress', function () {
    var charCode = window.event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
});

$(function () {
    $('.datetimepicker').datetimepicker({
        format: 'YYYY-MM-DD'
    });
});

//calculate days diff
function calculateTaskDuration() {
    var startDate = document.getElementById("AddTaskStartDate").value;
    var endDate = document.getElementById("AddTaskEndDate").value;
    if (startDate && endDate) {
        if (startDate > endDate) {
            toastr.error("Start date should be less than end date!", "Invalid Date Range")
            document.getElementById("AddTaskStartDate").value = null;
            return false;
        }
        var start = new Date(startDate);
        var end = new Date(endDate);
        var duration = (end - start) / (1000 * 60 * 60 * 24);
        document.getElementById("AddTaskDuration").value = duration;
        document.getElementById("AddTaskDuration").readOnly = true;
    }
}
function calculateSubTaskDuration() {
    var startDate = document.getElementById("AddSubTaskStartDate").value;
    var endDate = document.getElementById("AddSubTaskEndDate").value;
    if (startDate && endDate) {
        if (startDate > endDate) {
            toastr.error("Start date should be less than end date!", "Invalid Date Range")
            document.getElementById("AddSubTaskStartDate").value = null;
            return false;
        }
        var start = new Date(startDate);
        var end = new Date(endDate);
        var duration = (end - start) / (1000 * 60 * 60 * 24);
        document.getElementById("AddSubTaskDuration").value = duration;
        document.getElementById("AddSubTaskDuration").readOnly = true;
    }
}

//clear fields

function CleareTaskFields() {
    $('#AddTaskModel #TaskName').val("");
    $('#AddTaskModel #AddTaskStartDate').val("");
    $('#AddTaskModel #PriorityId').val("");
    document.getElementById("AddTaskDuration").readOnly = false;
    $('#AddTaskModel #AddTaskDuration').val("");
    $('#AddTaskModel #TaskOwnerId').val("");
    $('#AddTaskModel #AddTaskEndDate').val("");
    $('#AddTaskModel #StatusId').val("");
    document.getElementById("AddTaskPercentage").readOnly = false;
    $('#AddTaskModel #AddTaskPercentage').val("");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    //// Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
function CleareSubTaskFields() {
    $('#AddSubTaskModel #TaskName').val("");
    $('#AddSubTaskModel #AddSubTaskStartDate').val("");
    $('#AddSubTaskModel #PriorityId').val("");
    document.getElementById("AddSubTaskDuration").readOnly = false;
    $('#AddSubTaskModel #AddSubTaskDuration').val("");
    $('#AddSubTaskModel #TaskOwnerId').val("");
    $('#AddSubTaskModel #AddSubTaskEndDate').val("");
    $('#AddSubTaskModel #StatusId').val("");
    $("#taskLevel").val("");
    $("#SubTaskLevel").val("");
    document.getElementById("AddSubTaskPercentage").readOnly = false;
    $('#AddSubTaskModel #AddSubTaskPercentage').val("");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    //// Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
function ShowChildRows(rowindex) {
    //if (id > 0) {
    //    if ($(".clildrows" + id).is(':visible')) {
    //        $(".clildrows" + id).hide();
    //        $(".icoShowSubtask" + id).removeClass("fa fa-minus-circle")
    //        $(".icoShowSubtask" + id).addClass("fa fa-plus-circle")
    //        $(".icoShowSubtask" + id).css("color", "#31b131")
    //    }
    //    else {
    //        $(".clildrows" + id).show()
    //        $(".icoShowSubtask" + id).removeClass("fa fa-plus-circle")
    //        $(".icoShowSubtask" + id).addClass("fa fa-minus-circle")
    //        $(".icoShowSubtask" + id).css("color", "#d33333")
    //    }
    //} 
  
    if ($(".clildrows" + rowindex).is(':visible')) {
        $(".clildrows" + rowindex).hide();
        $(".icoShowSubtask" + rowindex).removeClass("fa fa-minus-circle")
        $(".icoShowSubtask" + rowindex).addClass("fa fa-plus-circle")
        $(".icoShowSubtask" + rowindex).css("color", "#31b131")
        }
        else {
        $(".clildrows" + rowindex).show()
        $(".icoShowSubtask" + rowindex).removeClass("fa fa-plus-circle")
        $(".icoShowSubtask" + rowindex).addClass("fa fa-minus-circle")
        $(".icoShowSubtask" + rowindex).css("color", "#d33333")
        }
  
    
}
function setTabIndex(ModelId) {
    if (ModelId == "AddTaskModel") {
        $(`#${ModelId} input#TaskName.form-control`).attr("tabindex", 1);
        $(`#${ModelId} input#AddTaskStartDate.form-control`).attr("tabindex", 3);
        $(`#${ModelId} select#AddTaskPriority.form-control`).attr("tabindex", 5);
        $(`#${ModelId} input#AddTaskDuration.form-control`).attr("tabindex", 7);
        $(`#${ModelId} select#AddTaskOwner.form-control`).attr("tabindex", 2);
        $(`#${ModelId} input#AddTaskEndDate.form-control`).attr("tabindex", 4);
        $(`#${ModelId} select#AddTaskStatus.form-control`).attr("tabindex", 6);
        $(`#${ModelId} input#AddTaskPercentage.form-control`).attr("tabindex", 8);
    }
    else if (ModelId == "AddSubTaskModel") {
        $(`#${ModelId} input#TaskName.form-control`).attr("tabindex", 1);
        $(`#${ModelId} select#AddSubTaskOwner.form-control`).attr("tabindex", 2);
        $(`#${ModelId} input#AddSubTaskStartDate.form-control`).attr("tabindex", 3);
        $(`#${ModelId} input#AddSubTaskEndDate.form-control`).attr("tabindex", 4);
        $(`#${ModelId} select#AddSubTaskPriority.form-control`).attr("tabindex", 5);
        $(`#${ModelId} select#AddSubTaskStatus.form-control`).attr("tabindex", 6);
        $(`#${ModelId} input#AddSubTaskDuration.form-control`).attr("tabindex", 7);
        $(`#${ModelId} input#AddSubTaskPercentage.form-control`).attr("tabindex", 8);
    }
    else if (ModelId == "UpdateModel") {
        $(`#${ModelId} input#TaskName.form-control`).attr("tabindex", 1);
        $(`#${ModelId} select#TaskOwner.form-control`).attr("tabindex", 2);
        $(`#${ModelId} input#StartDate.form-control`).attr("tabindex", 3);
        $(`#${ModelId} input#EndDate.form-control`).attr("tabindex", 4);
        $(`#${ModelId} select#TaskPriority.form-control`).attr("tabindex", 5);
        $(`#${ModelId} select#TaskStatus.form-control`).attr("tabindex", 6);
        $(`#${ModelId} input#TaskDuration.form-control`).attr("tabindex", 7);
        $(`#${ModelId} input#TaskPercentage.form-control`).attr("tabindex", 8);
    }

    $(`#${ModelId} button.btn.btn-primary`).attr("tabindex", 9);
    $(`#${ModelId} a.btn.btn-danger`).attr("tabindex", 10);
}