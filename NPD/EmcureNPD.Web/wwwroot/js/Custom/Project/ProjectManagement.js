$(document).ready(function () {
    GetProjectDetails();
    GetBusinessUnitDetails(bid);
});
function GetBusinessUnitDetails(bid) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetBusinessUnitDetail + "/" + bid + "/" + $('#pidfId').val(), 'GET', GetBusinessUnitDetailsSuccess, GetBusinessUnitDetailsError);
}
function GetBusinessUnitDetailsSuccess(data) {
    try {
        //business unit details
        $('#BDetailsTable tbody').html('');
        $.each(data.table, function (index, object) {
            $('#BDetailsTable tbody').append('<tr><td>' + object.country + '</td><td>' + object.strength + '</td><td>' + object.packSize + '</td><td>' + object.packing + '</td><td>' + object.firstYearPrice + '</td><td>' + object.FirstYearQty + '</td><td>' + object.firstYearVolume + '</td><td>' + object.secondYearPrice + '</td><td>' + object.secondYearQty + '</td><td>' + object.secondYearvolume + '</td><td>' + object.thirdYearPrice + '</td><td>' + object.thirdYearQty + '</td><td>' + object.thirdYearVolume + '</td><td>' + object.currency + '</td></td></tr>');
        })
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBusinessUnitDetailsError() {
    toastr.error(ErrorMessage);
}
function GetProjectDetails() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllData + "/" + $('#pidfId').val(), 'GET', GetProjectDetailsSuccess, GetProjectDetailsError);
}
function GetProjectDetailsSuccess(data) {
    try {
        //project details
        console.log(data);
        $('#loading').hide();
        $('#pidf_ProjectorProductName').text(data.table[0].projectName);
        $('#pidf_ProductTypeName').text(data.table[0].productType);
        $('#pidf_PlantName').text(data.table[0].plantName);
        $('#pidf_FormulationName').text(data.table[0].formulation);
        $('#pidf_WorkflowName').text(data.table[0].workFlow);;
        $.each(data.table2, function (i, List) {
            var newRow = $("<tr>");
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
            var link = $('#hdnBaseUrl').val() + '/Uploads/PIDF/Medical/' + file.fileName;
            $('#files tbody').append('<tr><td><a href="' + link + '">' + file.fileName + '</a></td></tr>');
        });
        //end
        //Businessunit details
        $('#custom-tabs-one-tab li').slice(1).remove();
        $.each(data.table3, function (index, bunits) {
            var $li = $('<li class="nav-item p-0"></li>');
            var $a = $('<a class="nav-link" id="' + bunits.businessUnitId + '" aria-selected="false">' + bunits.businessUnitName + '</a>');
            if (bunits.businessUnitId == bid) {
                var $span = $('<span>').text(bunits.businessUnitName);
                $('#BHeading').empty().append($span);
                $a.addClass('active');
            }
            $a.click(function () {
                $('.nav-link.active').removeClass('active');
                // Add active class to highlight the selected item
                $(this).addClass('active');
                GetBusinessUnitDetails(bunits.businessUnitId);
                var $span = $('<span>').text(bunits.businessUnitName);
                $('#BHeading').empty().append($span);
            });
            $li.append($a);
            $li.insertAfter($('#custom-tabs-one-tab li').eq(0));
        });
        //end
        //tasksubtask list details
        $('#Milestones tbody').html('')
        $.each(data.table4, function (index, object) {
            var start = new Date(object.startDate);
            var startDate = start.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
            var end = new Date(object.endDate);
            var endDate = end.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
            if (object.modifyDate != null) {
                var updated = new Date(object.modifyDate);
                var updatedDate = updated.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
            }
            else
                var updatedDate = "";
            var edit = '<a class="large-font" style="" href="" title="Edit" data-toggle="modal" data-target="#UpdateModel" data-backdrop="static" data-keyboard="false"  onclick="GetTaskSubTaskById(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a>';
            var deleteTag = '<a class="large-font text-danger" style="" href="" title="Delete" data-toggle="modal" data-target="#DeleteModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteTaskSubTask(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>';
            if (object.taskLevel == 1) {
                deleteTag += '<a class="large-font" style="" href="" title="Add SubTask" onclick="ShowAddSubTaskForm(\'' + object.projectTaskId + '\', \'' + object.taskName + '\'); return false;"><i class="fa fa-fw fa-plus mr-1"></i> ' + '</a>';
            }


            //var tableRow = '<tr><td>' + object.taskName + '</td><td>' + object.fullName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td>' + object.totalPercentage + '</td><td>' + updatedDate + '</td><td>' + edit + deleteTag + '</td></tr>';
            var tableRow = '<tr><td>' + object.taskName + '</td><td>' + object.fullName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td><div class="progress"><div class="progress-bar" role="progressbar" style="width: ' + object.totalPercentage + '%;" aria-valuenow="' + object.totalPercentage + '" aria-valuemin="0" aria-valuemax="100">' + object.totalPercentage + '%</div></div></td><td>' + updatedDate + '</td><td>' + edit + deleteTag + '</td></tr>';
            $('#Milestones tbody').append(tableRow);

            //$('#Milestones tbody').append('<tr><td>' + object.taskName + '</td><td>' + object.fullName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td>' + object.totalPercentage + '</td><td>' + updatedDate + '</td><td>  <a class="large-font" style="" href="" title="Edit" data-toggle="modal" data-target="#UpdateModel" data-backdrop="static" data-keyboard="false"  onclick="GetTaskSubTaskById(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a><a class="large-font text-danger" style="" href="" title="Delete" data-toggle="modal" data-target="#DeleteModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteTaskSubTask(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a> </td></tr>');
        });
        //StaticDataTable("#Milestones");
        //end
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
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
        $("#tasklevel").val(data._object.taskLevel)
        $("#parentId").val(data._object.parentId)


        if (data._object.editTaskStatusId == 1) {
            $(".disabledPercentage").prop("readOnly", true);
            $("#TaskPercentage").val(data_object.totalPercentage);
        } else if (data._object.editTaskStatusId == 2) {
            $(".disabledPercentage").prop("readOnly", false);
            $("#TaskPercentage").val(data._object.totalPercentage);
        }
        else if (data._object.editTaskStatusId == 3) {
            $(".disabledPercentage").prop("readOnly", true);
            $("#TaskPercentage").val(data._object.totalPercentage);
        }
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
}

function ShowAddTaskForm() {
    CleareTaskFields();
    $('#AddTaskModel').modal('show');
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
function ShowAddSubTaskForm(parentId, taskName) {
    CleareSubTaskFields();
    $('#AddSubTaskModel').modal('show');
    $('#AddSubTaskofTask').empty().append(
        "<option selected readonly value='" + parentId + "'>" + taskName + "</option>"
    );
    ajaxServiceMethod($('#hdnBaseURL').val() + FillTaskDropdown, 'GET', GetDropdownsForAddSubTaskSuccess, GetDropdownsForAddSubTaskError);
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
        var start = new Date(startDate);
        var end = new Date(endDate);
        var duration = (end - start) / (1000 * 60 * 60 * 24); // difference in days
        document.getElementById("AddTaskDuration").value = duration;
        document.getElementById("AddTaskDuration").readOnly = true;
    }
}
function calculateSubTaskDuration() {
    var startDate = document.getElementById("AddSubTaskStartDate").value;
    var endDate = document.getElementById("AddSubTaskEndDate").value;
    if (startDate && endDate) {
        var start = new Date(startDate);
        var end = new Date(endDate);
        var duration = (end - start) / (1000 * 60 * 60 * 24); // difference in days
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
    document.getElementById("AddSubTaskPercentage").readOnly = false;
    $('#AddSubTaskModel #AddSubTaskPercentage').val("");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    //// Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}