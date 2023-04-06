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

    gantt.init("ganttContainer");
    ajaxServiceMethod($('#hdnBaseURL').val() + "api/Project/GetTaskSubTask" + "/" + id, 'GET', GetTaskSubTaskListSuccess, GetTaskSubTaskListError);
    function GetTaskSubTaskListSuccess(data) {
        let restructeredData = {}
        for (var i = 0; i < data._object.length; i++) {
            let crDate = moment(data._object[i].createdDate).format("YYYY-MM-DD")
            let enDate = moment(data._object[i].endDate).format("YYYY-MM-DD")
            let modifyDate = moment(data._object[i].modifyDate).format("YYYY-MM-DD")
            let startDate = moment(data._object[i].startDate).format("YYYY-MM-DD")
             restructeredData = {
                createdBy: data._object[i].createdBy,
                createdDate: crDate,
                editTaskOwnerId: data._object[i].editTaskOwnerId,
                editTaskPriorityId: data._object[i].editTaskPriorityId,
                editTaskStatusId: data._object[i].editTaskStatusId,
                endDate: enDate,
                modifyBy: data._object[i].modifyBy,
                modifyDate: modifyDate,
                parentId: data._object[i].parentId,
                pidfid: data._object[i].pidfid,
                priorityId: data._object[i].priorityId,
                priorityName: data._object[i].priorityName,
                priorityName: data._object[i].priorityName,
                projectTaskId: data._object[i].projectTaskId,
                startDate: startDate,
                statusId: data._object[i].statusId,
                statusName: data._object[i].statusName,
                taskDuration: data._object[i].taskDuration,
                taskLevel: data._object[i].taskLevel,
                taskName: data._object[i].taskName,
                taskOwnerId: data._object[i].taskOwnerId,
                taskOwnerName: data._object[i].taskOwnerName,
                totalPercentage: data._object[i].totalPercentage
            }
            let ganttdata = '[' + JSON.stringify(restructeredData) + ']';
            gantt.parse({ data: JSON.parse(ganttdata) });
           
        }
        
       // gantt.appendTo("#ganttContainer");
    }
    function GetTaskSubTaskListError() {
        toastr.error("Error");
    }
    
});