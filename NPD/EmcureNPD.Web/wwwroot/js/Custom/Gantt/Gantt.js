$(document).ready(function () {
    gantt.config.columns = [
        { name: "taskName", label: "Task name", width: "150", resize: true, tree: true },
        { name: "startDate", label: "Start date", width: "80", resize: true, align: "center" },
        { name: "endDate", label: "End Date", align: "center", hide: true },
        { name: "totalPercentage", label: "Percent", align: "center", hide: true },
        { name: "priorityName", label: "Priority", width: "80", resize: true, align: "center" },
        { name: "taskDuration", label: "Duration", width: "80", resize: true, align: "center" },
        { name: "taskOwnerName", label: "Owner", width: "80", resize: true, aligh: "center" },
        { name: "add", width: 44 }
    ];
   
    gantt.config.xml_date = "%Y-%m-%d";

    gantt.init("gantt_here");
    ajaxServiceMethod($('#hdnBaseURL').val() + "api/Project/GetTaskSubTask" + "/" + id, 'GET', GetTaskSubTaskListSuccess, GetTaskSubTaskListError);
    function GetTaskSubTaskListSuccess(data) {
        gantt.parse({ data: data._object });
    }
    function GetTaskSubTaskListError() {
        toastr.error("Error");
    }
    
});