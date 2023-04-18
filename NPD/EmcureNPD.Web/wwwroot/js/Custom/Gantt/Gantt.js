
$("i.fas.fa-compress").click(function () {
    $("#ganttContainer").css("width", "98%");
});
$("i.fas.fa-expand").click(function () {
    $("#ganttContainer").css("width", "100%");
});




document.addEventListener("DOMContentLoaded", function () {
    
    let arrPeople = [{ key: null, label: "-select-" }];
   
    let taskStatus = [];
    let responsTtaskList = null;
    let isAdd = false;
    
    $("#lblProjectName").text(localStorage.getItem("prjName"));
 

    gantt.config.min_column_width = 50;
    gantt.config.work_time = true;
    //gantt.config.skip_off_time = true;

    gantt.setWorkTime({ day: 6 });
    gantt.setWorkTime({ day: 0 });

    function setDateRange(from, to) {
        from = parseInt(from, 10) || 2021;
        to = parseInt(to, 10) || (from + 1);
        if (to < from) {
            to = from + 1;
        }
        gantt.config.start_date = new Date(from, 0, 1);
        gantt.config.end_date = new Date(to, 0, 1);
    }

    function setScaleConfig(value) {
        switch (value) {
            case "day":
                gantt.config.scale_unit = "day";
                gantt.config.step = 1;
                gantt.config.date_scale = "%d %M";
                gantt.config.subscales = [];
                gantt.config.scale_height = 27;
                gantt.templates.date_scale = null;
                break;
            case "week":
                var weekScaleTemplate = function (date) {
                    var dateToStr = gantt.date.date_to_str("%d %M");
                    var endDate = gantt.date.add(date, -6, "day");
                    var weekNum = gantt.date.date_to_str("%W")(date);
                    return "#" + weekNum + ", " + dateToStr(date) + " - " + dateToStr(endDate);
                };
                gantt.config.scale_unit = "week";
                gantt.config.step = 1;
                gantt.templates.date_scale = weekScaleTemplate;
                gantt.config.subscales = [
                    { unit: "day", step: 1, format: "%j %D" }
                ];
                gantt.config.scale_height = 50;
                break;
            case "month":
                gantt.config.scale_unit = "month";
                gantt.config.date_scale = "%F, %Y";
                gantt.config.subscales = [
                    { unit: "week", format: "Week #%W" }
                ];
                gantt.config.scale_height = 50;
                gantt.templates.date_scale = null;
                break;
            case "year":
                gantt.config.scale_unit = "year";
                gantt.config.date_scale = "%Y";
                gantt.config.min_column_width = 50;
                gantt.config.scale_height = 90;
                gantt.config.subscales = [
                    { unit: "month", step: 1, date: "%M" }
                ];
                gantt.templates.date_scale = null;
                break;
        }
    }

    var func = function (e) {
        e = e || window.event;
        var el = e.target || e.srcElement;
        var value = el.value;
        setScaleConfig(value);
        gantt.render();
    };

    var radios = document.getElementsByName("scale");
    for (var i = 0; i < radios.length; i++) {
        radios[i].onclick = func;
    }
    //end

    function shouldHighlightTask(task) {
        var store = gantt.$resourcesStore;
        var taskResource = task[gantt.config.resource_property],
            selectedResource = store.getSelectedId();
        if (taskResource == selectedResource || store.isChildOf(taskResource, selectedResource)) {
            return true;
        }
    }

    // adding baseline display
    gantt.addTaskLayer({
        renderer: {
            render: function draw_planned(task) {
                if (task.planned_start && task.planned_end) {
                    var sizes = gantt.getTaskPosition(task, task.planned_start, task.planned_end);
                    var el = document.createElement('div');
                    el.className = 'baseline';
                    el.style.left = sizes.left + 'px';
                    el.style.width = sizes.width + 'px';
                    el.style.top = sizes.top + gantt.config.task_height + 13 + 'px';
                    return el;
                }
                return false;
            },
            // define getRectangle in order to hook layer with the smart rendering
            getRectangle: function (task, view) {
                if (task.planned_start && task.planned_end) {
                    return gantt.getTaskPosition(task, task.planned_start, task.planned_end);
                }
                return null;
            }
        }
    });

    gantt.templates.task_class = function (start, end, task) {
        if (task.planned_end) {
            var classes = ['has-baseline'];
            if (end.getTime() > task.planned_end.getTime()) {
                classes.push('overdue');
            }
            return classes.join(' ');
        }
    };

    //End of baseline code

    //base line overdue method
    gantt.templates.rightside_text = function (start, end, task) {
        if (task.planned_end) {
            if (end.getTime() > task.planned_end.getTime()) {
                var overdue = Math.ceil(Math.abs((end.getTime() - task.planned_end.getTime()) / (24 * 60 * 60 * 1000)));
                var text = "<b>Overdue: " + overdue + " days</b>";
                return text;
            }
        }
    };
    //End of baseline overdue method

    //Task Progress method
    gantt.templates.task_class = function (start, end, task) {
        //text in progress
        let progressPercents = task.progress * 100;
        let className = "";
        if (progressPercents == 0) {
            className += " task-no-progress";
        }
        if (progressPercents > 0 && progressPercents < 100) {
            className += " task-in-progress";
        }
        if (progressPercents == 100) {
            className += " task-full-progress";
        }
        return className;
    };
    //end of task progress method

    gantt.templates.grid_row_class = function (start, end, task) {
        var css = [];
        if (gantt.hasChild(task.id)) {
            css.push("folder_row");
        }
        if (task.$virtual) {
            css.push("group_row")
        }
        if (shouldHighlightTask(task)) {
            css.push("highlighted_resource");
        }
        return css.join(" ");
    };

    gantt.templates.task_row_class = function (start, end, task) {
        if (shouldHighlightTask(task)) {
            return "highlighted_resource";
        }
        return "";
    };
    gantt.templates.grid_row_class = function (start, end, task) {
        if (task.$level > 0) {//task.$level > 1
            return "nested_task"
        }
        return "";
    };
    gantt.templates.scale_cell_class = function (date) {
        if (!gantt.isWorkTime(date)) {
            return "weekend";
        }
    };

    gantt.templates.timeline_cell_class = function (task, date) {
        if (!gantt.isWorkTime({ date: date, task: task }))
            return "week_end";
        return "";
    };

    gantt.templates.resource_cell_class = function (start_date, end_date, resource, tasks) {
        var css = [];
        css.push("resource_marker");
        if (tasks.length <= 1) {
            css.push("workday_ok");
        } else {
            css.push("workday_over");
        }
        return css.join(" ");
    };

    gantt.templates.resource_cell_value = function (start_date, end_date, resource, tasks) {
        var html = "<div>"
        if (resourceMode == "hours") {
            html += tasks.length * 8;
        } else {
            html += tasks.length;
        }
        html += "</div>";
        return html;
    };

    function shouldHighlightResource(resource) {
        var selectedTaskId = gantt.getState().selected_task;
        if (gantt.isTaskExists(selectedTaskId)) {
            var selectedTask = gantt.getTask(selectedTaskId),
                selectedResource = selectedTask[gantt.config.resource_property];

            if (resource.id == selectedResource) {
                return true;
            } else if (gantt.$resourcesStore.isChildOf(selectedResource, resource.id)) {
                return true;
            }
        }
        return false;
    }
    function byId(list, id) {
        for (var i = 0; i < list.length; i++) {
            if (list[i].key == id)
                return list[i].label || "";
        }
        return "";
    }

    function parent_progress(id) {
        var temptask;
        gantt.eachParent(function (task) {
            let response = responsTtaskList;
           let progrss = taskStatus.find(x => x.projectTaskId === task.id);
           // let c = taskStatus.find(x => x.projectTaskId === task.id && x.taskLevel>1);
            if (response != undefined && response.length > 1) {
                var children = [];
                //if (progrss.taskLevel > 1) {
                    children = gantt.getChildren(task.id);
                //}
                var child_progress = 0;
                for (var i = 0; i < children.length; i++) {
                    var child = gantt.getTask(children[i])
                   // getchild_progress(child.id);
                    child_progress += parseInt(progrss.totalPercentage);
                }
                task.progress = child_progress / children.length  / 100;//**To be continue..Kp*/
                temptask = task;
            }
            //else {
            //    var children = gantt.getChildren(task.id);
            //    var child_progress = 0;
            //    for (var i = 0; i < children.length; i++) {
            //        var child = gantt.getTask(children[i])
            //        child_progress += (child.progress * 100);
            //    }
            //}
        }, id)
        gantt.render();
        //setTimeout(reloadFunc, 3000);
    }
    function getchild_progress(id) {
        var temptask;
        gantt.eachParent(function (task) {
            let response = responsTtaskList;
            if (response != undefined && response.length > 0) {
                var children = gantt.getChildren(task.id);
                var child_progress = 0;
                for (var i = 0; i < children.length; i++) {
                    var child = gantt.getTask(children[i])
                    child_progress += parseInt(response[i].totalPercentage) //* 100;
                }
            }
            else {
                var children = gantt.getChildren(task.id);
                var child_progress = 0;
                for (var i = 0; i < children.length; i++) {
                    var child = gantt.getTask(children[i])
                    child_progress += (child.progress * 100);
                }
            }
            task.progress = child_progress / children.length * 100 / 100;
            temptask = task;
        }, id)
        gantt.render();
    }
    var resourceTemplates = {
        grid_row_class: function (start, end, resource) {
            var css = [];
            if (gantt.$resourcesStore.hasChild(resource.id)) {
                css.push("folder_row");
                css.push("group_row");
            }
            if (shouldHighlightResource(resource)) {
                css.push("highlighted_resource");
            }
            return css.join(" ");
        },
        task_row_class: function (start, end, resource) {
            var css = [];
            if (shouldHighlightResource(resource)) {
                css.push("highlighted_resource");
            }
            if (gantt.$resourcesStore.hasChild(resource.id)) {
                css.push("group_row");
            }
            return css.join(" ");
        }
    };

    gantt.serverList("resources", []);// placeholder to load resources in /api/data request
    gantt.serverList("priority", [
        { key: 1, label: "Normal" },
        { key: 2, label: "High" },
        { key: 3, label: "Very High" },
        { key: 4, label: "Medium" },
        { key: 5, label: "Low" },
        { key: 6, label: "Very Low" }
    ]);
   // gantt.serverList("people",[]);// placeholder to load woner in /api/data request
    //{ "owner_id": data._object[i].taskOwnerId, "owner": data._object[i].taskOwnerName

    //gantt.serverList("people", [
    //    { key: 1, label: "Kamal" }
    //]);
   
    //
    //GetOwners//
   ajaxServiceMethod($('#hdnBaseURL').val() + FillTaskDropdown, 'GET', 
    
        function GetOwnersSuccess(data) {
            try {


                $.each(data.taskOwner, function (i, List) {
                    arrPeople.push(
                        {
                            key: List.userId,
                            label: List.fullName
                        }
                    )
                });
               
                console.log("arrPeople:" + JSON.stringify(arrPeople))
            }
            catch (e) {
                toastr.error('Error:' + e.message);
            }
        }
    , GetOwnersError);
    function GetOwnersError(x, y, z) {
        toastr.error(ErrorMessage);
    }
    // end GetOwners//
    gantt.serverList("people", arrPeople);
    gantt.locale.labels.section_owner = "Owner";
    gantt.locale.labels.section_priority = "Priority";
    gantt.config.lightbox.sections = [
        { name: "description", height: 38, map_to: "text", type: "textarea", focus: true },
        { name: "owner", height: 22, map_to: "owner_id", type: "select", options: arrPeople },
        { name: "priority", height: 22, map_to: "priority", type: "select", options: gantt.serverList("priority") },
        { name: "time", type: "duration", map_to: "auto" },
        {
            name: "baseline",
            map_to: { start_date: "planned_start", end_date: "planned_end" },
            button: true,
            type: "duration_optional"
        }
    ];
    gantt.locale.labels.section_baseline = "Planned";

    function getResourceTasks(resourceId) {
        var store = gantt.getDatastore(gantt.config.resource_store),
            field = gantt.config.resource_property,
            tasks;

        if (store.hasChild(resourceId)) {
            tasks = gantt.getTaskBy(field, store.getChildren(resourceId));
        } else {
            tasks = gantt.getTaskBy(field, resourceId);
        }
        return tasks;
    }

    var resourceConfig = {
        scale_height: 30,
        subscales: []
    };

    //gantt.config.subscales = [
    //    //{ unit: "day", step: 1, date: "%d, %M" }
    //    { unit: "month", step: 1, date: "%F, %Y" }
    //];
    gantt.plugins({
        grouping: true,
        auto_scheduling: true,
        tooltip: true
    });
    gantt.config.order_branch = true;
    gantt.config.order_branch_free = true;
    gantt.config.auto_scheduling = true;
    gantt.config.auto_scheduling_strict = true;
    gantt.config.work_time = true;
    gantt.config.columns = [
        { name: "text", tree: true, width: 200, resize: true },
        { name: "start_date", align: "center", width: 80, resize: true },
        {
            name: "owner", align: "center", width: 80, label: "Owner", template: function (task) {
                if (task.type == gantt.config.types.project) {
                    return "";
                }
                var store = gantt.getDatastore(gantt.config.resource_store);
                var owner = taskStatus.filter(x => x.projectTaskId === task.id);
                if (owner.length > 0) {
                    return owner[0].taskOwnerName;
                } else {
                    return "Unassigned";
                }
            }, resize: true
        },
        {
            name: "priority", width: 80, align: "center", label: "Priority", template: function (item) {
                return byId(gantt.serverList('priority'), item.priority)
            }, resize: true
        },
        { name: "duration", width: 60, align: "center", resize: true },
       
          { name: "add", width: 44 }
        //{
        //    name: "add", width: 44, align: "center", onrender: function (task, node) {
        //        debugger
        //        console.log(node)
        //        if (item.taskLevel == 1) { node.setAttribute("visible", false); }
        //        else { node.setAttribute("visible", true); }
        //    }
        //}

    ];
    gantt.plugins({
        grouping: true,
        auto_scheduling: true,
        tooltip: true
    });
    gantt.config.resource_store = "resource";
    gantt.config.resource_property = "owner_id";
    gantt.config.order_branch = true;
    gantt.config.open_tree_initially = true;
    gantt.config.order_branch_free = true;
    gantt.config.auto_scheduling = true;
    gantt.config.auto_scheduling_strict = true;
    gantt.config.work_time = true;

    gantt.config.scale_height = 50;
    gantt.config.layout = {
        css: "gantt_container",
        rows: [
            {
                gravity: 2,
                cols: [
                    { view: "grid", group: "grids", scrollY: "scrollVer" },
                    { resizer: true, width: 2 },
                    { view: "timeline", scrollX: "scrollHor", scrollY: "scrollVer" },
                    { view: "scrollbar", id: "scrollVer", group: "vertical" }
                ]
            },
            { view: "scrollbar", id: "scrollHor" }
        ]
    };

    gantt.attachEvent("onTaskLoading", function (task) {
        //task.start_date = gantt.date.parseDate(task.start_date, "xml_date");
        //task.end_date = gantt.date.parseDate(task.end_date, "xml_date");

        task.planned_start = gantt.date.parseDate(task.planned_start, "xml_date");
        task.planned_end = gantt.date.parseDate(task.planned_end, "xml_date");

        return true;
    });

    var resourceMode = "hours";
    gantt.attachEvent("onGanttReady", function () {
        var tooltips = gantt.ext.tooltips;

        gantt.templates.tooltip_text = function (start,end,task) {
            var store = gantt.getDatastore("resource");
            //console.log(store);
            var assignments = task[gantt.config.resource_property] || [];
            var owners = taskStatus.filter(x => x.projectTaskId === task.id);
            var owner = store.getItem(assignments);
            //owners.push(owner.text);
            if (owners.length>0 && owners[0].taskLevel == 1) {
                return "<b>Task:</b> " + task.text + "<br/>" +
                    "<b>Owner:</b>" + owners[0].taskOwnerName + "<br/>" +
                    "<b>Start date:</b> " +
                    gantt.templates.tooltip_date_format(start)
                    + "<br/><b>End date:</b> " + gantt.templates.tooltip_date_format(end) + "<br/>" +
                    "<b>Progress:</b> " + Math.round(owners[0].totalPercentage) + "%";
            }
            else if (owners.length > 0 && owners[0].taskLevel > 1) {
                return "<b>Task:</b> " + task.text + "<br/>" +
                    "<b>Start date:</b> " +
                    gantt.templates.tooltip_date_format(start)
                    + "<br/><b>End date:</b> " + gantt.templates.tooltip_date_format(end) + "<br/>" +
                    "<b>Progress:</b> " + Math.round(owners[0].totalPercentage) + "%";
            }
        };

        var radios = [].slice.call(gantt.$container.querySelectorAll("input[type='radio']"));
        radios.forEach(function (r) {
            gantt.event(r, "change", function (e) {
                var radios = [].slice.call(gantt.$container.querySelectorAll("input[type='radio']"));
                radios.forEach(function (r) {
                    r.parentNode.className = r.parentNode.className.replace("active", "");
                });

                if (this.checked) {
                    resourceMode = this.value;
                    this.parentNode.className += " active";
                    gantt.getDatastore(gantt.config.resource_store).refresh();
                }
            });
        });
    });

    gantt.$resourcesStore = gantt.createDatastore({
        name: gantt.config.resource_store,
        type: "treeDatastore",
        initItem: function (item) {
            //console.log(gantt.config.root_id);
            item.parent = item.ParentId || gantt.config.root_id;
            item[gantt.config.resource_property] = item.ParentId;
            item.text = item.name;
            item.id = parseInt(item.id);
            item.open = true;
            item.key = parseInt(item.id);
            item.owner_id = parseInt(item.id);
            //console.log(item);
            return item;
        }
    });

    // console.log(gantt.$resourcesStore);

    gantt.$resourcesStore.attachEvent("onAfterSelect", function (id) {
        gantt.refreshData();
    });

    //OnAfterTaskUpdate for Parent Task progress
    gantt.attachEvent("onAfterTaskUpdate", function (id, task) {
        responsTtaskList = null;

        parent_progress(id);
        saveUpdateGanttTask(task,id);

        //$(".gantt_task_line").each(function (i, value) {
        //    console.log($(this).attr("aria-label").split(":")[1]);
        //});
       // setTimeout(reloadFunc, 2000);
    });
    


    gantt.attachEvent("onParse", function () {
        var resources = gantt.serverList("resources");
        gantt.$resourcesStore.parse(resources);
        //gantt.serverList("people", [{
        //    key: null, label:"--Select--"
        //}]);
       
        //gantt.serverList("people", arrPeople);
        //console.log(gantt.$resourcesStore);

       // taskStatus.forEach(function (item) { console.log(item) });

       var lightboxOptions = [];
       let res= taskStatus.forEach(function (res) {
            //console.log(res);
           if (res.taskLevel==1) {
               var copy = gantt.copy(res);
               copy.key = parseInt(res.projectTaskId);
               //copy.label = res.text;
               copy.label = res.taskOwnerName;
               copy.text = res.taskOwnerName;
               copy.owner_id = parseInt(res.taskOwnerId);
               copy.name = res.taskOwnerName;
               lightboxOptions.push(copy);
           }
       });
      //  gantt.updateCollection("people", lightboxOptions);

        //let resval = taskStatus.find(res => res.taskLevel == 1);
        //if (resval != undefined) { arrPeople.push({ key: resval.taskOwnerId, label: resval.taskOwnerName }) }
    });

    gantt.attachEvent("onAfterLinkAdd", function (id, item) {
        //setTimeout(reloadFunc, 2000);
    });
    gantt.attachEvent("onAfterTaskDelete", function (id, item) {
        //any custom logic here
        //alert('hi')
        deleteGanttTaskSubTask(id);
    });

    gantt.attachEvent("onAfterTaskAdd", function (id, item) {
        //any custom logic here
        //alert('hi')
        console.log(JSON.stringify(responsTtaskList));
        saveUpdateGanttTask(item, 0);

    });
    function reloadFunc() {
        location.reload();
    }

    gantt.templates.progress_text = function (start, end, task) {
        parent_progress(task.id);
        return "<span style='text-align:left;padding-left: 10px;box-sizing: border-box;color: white;font-weight: bold;'>" + Math.round(task.progress * 100) + "% </span>";
    };

    gantt.config.xml_date = "%Y-%m-%d %H:%i"; // format of dates in XML
    /*for baseline code*/
    gantt.config.task_height = 20;
    gantt.config.row_height = 40;
    gantt.locale.labels.baseline_enable_button = 'Set';
    gantt.locale.labels.baseline_disable_button = 'Remove';
    gantt.config.auto_scheduling = false;
    let gdata = {};
    gantt.init("ganttContainer");
    //gdata = {
    //    "tasks": [
    //        { "id": 1, "text": "Project #1", "start_date": "01-04-2023", "owner": "kamal", "priority": 1, "duration": 1 },
    //        { "id": 2, "text": "Task #1", "start_date": "01-04-2023", "owner": "kamal2", "priority": 2, "duration": 2, "parent": 1 },
    //        { "id": 3, "text": "Task #2", "start_date": "01-04-2023", "owner": "kamal3", "priority": 3, "duration": 3, "parent": 1 }
    //    ],
    //    "links": [
    //        { "id": 1, "source": 1, "target": 2, "type": "1" },
    //        { "id": 2, "source": 2, "target": 3, "type": "0" }
    //    ]
    //}
    //gantt.parse(gdata);
    GetProjectGanttTaskList();
   // gantt.config.auto_scheduling = false;

   // var obj = gantt.json;
   // gantt.init("ganttContainer")//, new Date(2021, 1, 1,0,0,0), new Date(2022, 1, 1,0,0,0)); // initialize gantt
   // gantt.load(JSON.stringify(gdata), "json");

   // var dp = new gantt.dataProcessor($('#hdnBaseURL').val() + "/api/Project/GetTaskSubTask" + "/" + id);
   //// var dp = new gantt.dataProcessor("/api")
   // dp.init(gantt);
   // dp.setTransactionMode("REST-JSON");

     function GetProjectGanttTaskList() {
         ajaxServiceMethod($('#hdnBaseURL').val() + "api/Project/GetTaskSubTask" + "/" + id, 'GET', GetTaskSubTaskListSuccess, GetTaskSubTaskListError);
        function GetTaskSubTaskListSuccess(data) {
            let restructeredData = {}
            for (var i = 0; i < data._object.length; i++) {
                let strtDate = moment(data._object[i].startDate).format("YYYY-MM-DD hh:mm")
                let enDate = moment(data._object[i].endDate).format("YYYY-MM-DD hh:mm")
                let modifyDate = moment(data._object[i].modifyDate).format("YYYY-MM-DD hh:mm")
                let startDate = moment(data._object[i].startDate).format("YYYY-MM-DD hh:mm")
                let subTaskId = data._object[i].taskLevel > 1 ? data._object[i].projectTaskId : 0

                gdata = {
                    "tasks": [
                        //{ "id": data._object[i].pidfid, "text": "Project #1", "start_date": startDate, "owner": data._object[i].taskOwnerName, "priority": data._object[i].priorityId, "duration": data._object[i].taskDuration },
                        { "id": data._object[i].projectTaskId, "text": data._object[i].taskName, "start_date": strtDate, "owner": data._object[i].taskOwnerId, "priority": data._object[i].priorityId, "duration": data._object[i].taskDuration, "parent": data._object[i].parentId },

                    ],
                    "links": [
                        { "id": data._object[i].projectTaskId, "source": data._object[i].parentId, "target": data._object[i].projectTaskId, "type": "0" },
                        //{ "id": data._object[i].projectTaskId, "source": data._object[i].parentId, "target": data._object[i].projectTaskId, "type": "1" },

                    ]
                }
                taskStatus.push({
                    statusId: data._object[i].statusId,
                    projectTaskId: data._object[i].projectTaskId,
                    taskName: data._object[i].taskName,
                    taskOwnerId: data._object[i].taskOwnerId,
                    taskOwnerName: data._object[i].taskOwnerName,
                    taskLevel: data._object[i].taskLevel,
                    totalPercentage: data._object[i].totalPercentage,
                    plannedStartDate: data._object[i].PlannedStartDate,
                    plannedEndDate: data._object[i].PlannedEndDate
                });
               
                // { key: 1, label: "Normal" }
                //name: "owner", height: 22, map_to: "owner_id",
                if (data._object[i].taskLevel == 1) { 
                    people = { "owner_id": data._object[i].taskOwnerId, "owner": data._object[i].taskOwnerName };
                    gantt.serverList("people", [people]);
               
            }
                //    createdDate: crDate,
                //    editTaskOwnerId: data._object[i].editTaskOwnerId,
                //    editTaskPriorityId: data._object[i].editTaskPriorityId,
                //    editTaskStatusId: data._object[i].editTaskStatusId,
                //    endDate: enDate,
                //    modifyBy: data._object[i].modifyBy,
                //    modifyDate: modifyDate,
                //    parentId: data._object[i].parentId,
                //    pidfid: data._object[i].pidfid,
                //    priorityId: data._object[i].priorityId,
                //    priorityName: data._object[i].priorityName,
                //    projectTaskId: data._object[i].projectTaskId,
                //    startDate: startDate,
                //    statusId: data._object[i].statusId,
                //    statusName: data._object[i].statusName,
                //    taskDuration: data._object[i].taskDuration,
                //    taskLevel: data._object[i].taskLevel,
                //    taskName: data._object[i].taskName,
                //    taskOwnerId: data._object[i].taskOwnerId,
                //    taskOwnerName: data._object[i].taskOwnerName,
                //    totalPercentage: data._object[i].totalPercentage
                //}
                gantt.parse(gdata);
            }
            responsTtaskList = data._object;
            isAdd=taskStatus.find(x => x.taskLevel == 1) ? false : true;
        }
        function GetTaskSubTaskListError() {
            toastr.error("Error");
        }
    }
    function saveUpdateGanttTask(taskObjects, ProjectTaskId) {
       //let startDate = moment(taskObjects.start_date).format("MM/DD/YYYY hh:mm");
        //let endDate = moment(taskObjects.start_date).format("MM/DD/YYYY hh:mm");
        let selectedOwner = taskObjects.owner_id == undefined || taskObjects.owner_id == "null" ? taskObjects.owner : taskObjects.owner_id;
        const res = taskStatus.filter(x => x.projectTaskId === ProjectTaskId);
        if (taskObjects.text == "null" || taskObjects.text == "") {
            //toastr.error("TaskName could not be empty!", "Input Validation Error");
            //$("div.gantt_cal_light").show();
            if (res.length > 0)
                taskObjects.text = res[0].taskName;
            else {
                taskObjects.text = "New Task";
            }
            //return false;
        }
     
            let addTask = {
                ProjectTaskId: ProjectTaskId,
                TaskName: taskObjects.text,
                Pidfid: 0,
                PriorityId: taskObjects.priority,
                StartDate: taskObjects.start_date,
                EndDate: taskObjects.end_date,
                StatusId: res.length > 0 ? res[0].statusId : 1,
                TaskOwnerId: selectedOwner, //.owner == undefined ? 0 : taskObjects.owner,
                TotalPercentage: Math.round((taskObjects.progress * 100)),
                ParentId: taskObjects.parent,
                TaskDuration: taskObjects.duration,
                IsGanttUpdate: true,
                PlannedStartDate: taskObjects.planned_start,
                PlannedEndDate: taskObjects.planned_end == undefined ? null : taskObjects.planned_end
            }
            let act = taskObjects.parent == 0 ? "Task" : "SubTask";
            let pidfId = (new URL(location.href)).searchParams.get('pidfid');
            console.log(JSON.stringify(addTask))
            ajaxServiceMethod($('#hdnWebBaseURL').val() + "Project/AddUpdateGanttTask?id=" + pidfId + "&act=" + act, 'POST', SaveTaskSubTaskListSuccess, SaveTaskSubTaskListError, JSON.stringify(addTask), null, null, null, "application/json; charset=utf-8");

            function SaveTaskSubTaskListSuccess(data) {

                GetProjectGanttTaskList();
                setTimeout(reloadFunc, 500);
                toastr.success("Success");
            }
            function SaveTaskSubTaskListError(err) {
                toastr.error(`${err}`, "Input Validation Error");
            }
        
    }
    function deleteGanttTaskSubTask(taskid) {
        taskIdToBeDelete = taskid;
        ajaxServiceMethod($('#hdnBaseURL').val() + "api/Project/DeleteTaskSubTask" + "/" + taskid, 'POST', deleteGanttTaskSubTaskSuccess, deleteGanttTaskSubTaskError);
    }
    function deleteGanttTaskSubTaskSuccess(response) {
        setTimeout(reloadFunc, 500);
        toastr.success(`TaskId:${taskIdToBeDelete} Deleted SuccessFully...!`);
    }
    function deleteGanttTaskSubTaskError() {
        toastr.error(`TaskId:${taskIdToBeDelete} not deleted due to something wrong`);
    }
});
