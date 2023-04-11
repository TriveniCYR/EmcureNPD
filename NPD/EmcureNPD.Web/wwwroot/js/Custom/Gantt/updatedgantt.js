document.addEventListener("DOMContentLoaded", function () {
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

            var children = gantt.getChildren(task.id);
            var child_progress = 0;
            for (var i = 0; i < children.length; i++) {
                var child = gantt.getTask(children[i])
                child_progress += (child.progress * 100);
            }
            task.progress = child_progress / children.length / 100;
            temptask = task;
        }, id)
        gantt.render();
        //console.log(temptask);


        if (temptask != undefined) {
            var response1 = gantt.ajax.put({
                headers: {
                    "Content-Type": "application/json"
                },
                url: $('#hdnBaseURL').val() + "api/Project/GetTaskSubTask" + "/" + id, //"https://localhost:44358/api" + "/task/" + temptask.id,
                data: JSON.stringify(temptask)

                //return gantt.ajax.put({
                //    headers: {
                //        "Content-Type": "application/json"
                //    },
                //    url: "https://localhost:44358/api" + "/task/" + temptask.id,
                //    data: JSON.stringify(temptask)
                //success: function (response) {
                //}
            });

            // console.log(response1);

        }
        setTimeout(reloadFunc, 3000);
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

    gantt.locale.labels.section_owner = "Owner";
    gantt.locale.labels.section_priority = "Priority";
    gantt.config.lightbox.sections = [
        { name: "description", height: 38, map_to: "text", type: "textarea", focus: true },
        { name: "owner", height: 22, map_to: "owner_id", type: "select", options: gantt.serverList("people") },
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
                var owner = store.getItem(task[gantt.config.resource_property]);
                if (owner) {
                    return owner.text;
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

        gantt.templates.tooltip_text = function (start, end, task) {
            var store = gantt.getDatastore("resource");
            //console.log(store);
            var assignments = task[gantt.config.resource_property] || [];
            var owners = [];
            var owner = store.getItem(assignments);
            owners.push(owner.text);
            return "<b>Task:</b> " + task.text + "<br/>" +
                "<b>Owner:</b>" + owners.join(",") + "<br/>" +
                "<b>Start date:</b> " +
                gantt.templates.tooltip_date_format(start) + "<br/>" +
                "<b>Progress:</b> " + Math.round(task.progress * 100) + "%";
            //+"<br/><b>End date:</b> " + gantt.templates.tooltip_date_format(end);
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
        parent_progress(id);
        //setTimeout(reloadFunc, 2000);
    });

    gantt.attachEvent("onParse", function () {
        var resources = gantt.serverList("resources");
        gantt.$resourcesStore.parse(resources);
        //console.log(gantt.$resourcesStore);

        var lightboxOptions = [];
        gantt.$resourcesStore.eachItem(function (res) {
            //console.log(res);
            if (!gantt.$resourcesStore.hasChild(res.id)) {
                var copy = gantt.copy(res);
                copy.key = parseInt(res.id);
                //copy.label = res.text;
                copy.label = res.name;
                copy.text = res.name;
                copy.owner_id = parseInt(res.id);
                copy.name = res.name;
                lightboxOptions.push(copy);
            }
        });
        gantt.updateCollection("people", lightboxOptions);
        console.log(gantt.serverList("people"));
    });

    gantt.attachEvent("onAfterLinkAdd", function (id, item) {
        setTimeout(reloadFunc, 2000);
    });

    function reloadFunc() {
        location.reload();
    }

    gantt.templates.progress_text = function (start, end, task) {
        return "<span style='text-align:left;padding-left: 10px;box-sizing: border-box;color: white;font-weight: bold;'>" + Math.round(task.progress * 100) + "% </span>";
    };

    gantt.config.xml_date = "%Y-%m-%d %H:%i"; // format of dates in XML
    /*for baseline code*/
    gantt.config.task_height = 20;
    gantt.config.row_height = 40;
    gantt.locale.labels.baseline_enable_button = 'Set';
    gantt.locale.labels.baseline_disable_button = 'Remove';
    /*end of baseline code */

    //gantt.config.show_tasks_outside_timescale = true;

    // gantt.config.start_date = new Date(2021, 1, 1, 0, 1);
    //gantt.config.end_date = new Date(2022, 1, 1, 0, 1);


    //Default set to false for avoid multiple task update after load [DO NOT DELETE]
    gantt.config.auto_scheduling = false;

    var obj = gantt.json;
    gantt.init("ganttContainer")//, new Date(2021, 1, 1,0,0,0), new Date(2022, 1, 1,0,0,0)); // initialize gantt
    gantt.load({
        "tasks": [
            { "id": 1, "text": "Project #1", "start_date": "01-04-2020", "duration": 18 },
            { "id": 2, "text": "Task #1", "start_date": "02-04-2020", "duration": 8, "parent": 1 },
            { "id": 3, "text": "Task #2", "start_date": "11-04-2020", "duration": 8, "parent": 1 }
        ],
        "links": [
            { "id": 1, "source": 1, "target": 2, "type": "1" },
            { "id": 2, "source": 2, "target": 3, "type": "0" }
        ]
    }, "json");

    //var dp = new gantt.dataProcessor("/api");
    var dp = new gantt.dataProcessor("/api")
    dp.init(gantt);
    dp.setTransactionMode("REST-JSON");

});