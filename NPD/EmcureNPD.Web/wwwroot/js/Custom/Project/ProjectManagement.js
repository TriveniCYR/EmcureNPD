$(document).ready(function () {
    GetTaskSubTaskList();
});


//get all task and subtask list
function GetTaskSubTaskList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllTaskSubTaskList + "/" + $('#pidfId').val(), 'GET', GetTaskSubTaskListSuccess, GetTaskSubTaskListError);
}
function GetTaskSubTaskListSuccess(data) {
    try {
        $('#Milestones tbody').html('')
        $.each(data._object, function (index, object) {
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
           
            $('#Milestones tbody').append('<tr><td>' + object.taskName + '</td><td>' + object.taskOwnerName + '</td><td>' + object.statusName + '</td><td>' + object.priorityName + '</td><td>' + startDate + '</td><td>' + endDate + '</td><td>' + object.taskDuration + '</td><td>' + object.totalPercentage + '</td><td>' + updatedDate + '</td><td>  <a class="large-font" style="" href="" title="Edit" data-toggle="modal" data-target="#UpdateModel" data-backdrop="static" data-keyboard="false"  onclick="GetTaskSubTaskById(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a><a class="large-font text-danger" style="" href="" title="Delete" data-toggle="modal" data-target="#DeleteModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteTaskSubTask(' + object.projectTaskId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#Milestones");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetTaskSubTaskListError(x, y, z) {
    toastr.error(ErrorMessage);
}

// getTaskSubTask by id
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
//end getTaskSubTask by id



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
            GetTaskSubTaskList();
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
//end delete

//add task
function AddTaskSubTask() {
    $('#AddModel').modal('show');
}

function ShowAddTaskForm() {
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
function ShowAddSubTaskForm() {
    $('#AddSubTaskModel').modal('show');
    $('#loading').show();
    ajaxServiceMethod($('#hdnBaseURL').val() + FillTaskDropdown, 'GET', GetDropdownsForAddSubTaskSuccess, GetDropdownsForAddSubTaskError);
}
function GetDropdownsForAddSubTaskSuccess(data) {
    try {
        $('#loading').hide();
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
//end add sub task

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

//Not Wokring 
//$(function () {
//    // attach change event to start date field
//    $("#AddTaskStartDate").on("dp.change", function (e) {
//        console.log("Change called");
//        var tempMinDate = new Date(e.date);
//        var setminDate = new Date(tempMinDate);
//        setminDate.setDate(tempMinDate.getDate() + 1);
//        $('#AddTaskStartDate').data("DateTimePicker").minDate(setminDate);
//        $('#AddTaskDuration').val(calculateDaysDifference($("#AddTaskStartDate").val(), $('#AddTaskEndDate').val()));

//    });
//    $("#AddTaskEndDate").on("dp.change", function (e) {
//        console.log("Change called");
//        var tempMinDate = new Date(e.date);
//        var setminDate = new Date(tempMinDate);
//        setminDate.setDate(tempMinDate.getDate() + 1);
//        $('#AddTaskStartDate').data("DateTimePicker").minDate(setminDate);
//        $('#AddTaskDuration').val(calculateDaysDifference($("#AddTaskStartDate").val(), $('#AddTaskEndDate').val()));

//    });

//    $("#AddSubTaskStartDate").on("dp.change", function (e) {
//        console.log("Change called");
//        var tempMinDate = new Date(e.date);
//        var setminDate = new Date(tempMinDate);
//        setminDate.setDate(tempMinDate.getDate() + 1);
//        $('#AddSubTaskStartDate').data("DateTimePicker").minDate(setminDate);
//        $('#AddSubTaskDuration').val(calculateDaysDifference($("#AddSubTaskStartDate").val(), $('#AddSubTaskEndDate').val()));

//    });
//    $("#AddSubTaskEndDate").on("dp.change", function (e) {
//        console.log("Change called");
//        var tempMinDate = new Date(e.date);
//        var setminDate = new Date(tempMinDate);
//        setminDate.setDate(tempMinDate.getDate() + 1);
//        $('#AddSubTaskStartDate').data("DateTimePicker").minDate(setminDate);
//        $('#AddSubTaskDuration').val(calculateDaysDifference($("#AddSubTaskStartDate").val(), $('#AddSubTaskEndDate').val()));

//    });
//});


//calculate days diff
function calculateDaysDifference(start, end) {
    var d1 = start;
    var d2 = end;
    var oneDay = 24 * 60 * 60 * 1000;
    var diff = 0;
    if (d1 && d2) {

        var startDate = new Date(d1.split('-')[2] + '-' + d1.split('-')[1] + '-' + d1.split('-')[0]);
        var endDate = new Date(d2.split('-')[2] + '-' + d2.split('-')[1] + '-' + d2.split('-')[0]);
        diff = Math.round(Math.abs((endDate.getTime() - startDate.getTime()) / (oneDay)));
        console.log(diff);
    }
    return diff;

}