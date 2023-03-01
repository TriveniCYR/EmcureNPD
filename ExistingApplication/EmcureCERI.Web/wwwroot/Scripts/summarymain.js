
document.addEventListener("DOMContentLoaded", function () {

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
                gantt.config.scale_height = 50;
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

    //Task Progress method
    gantt.templates.task_text = function (start, end, task) {
        //console.log(task.text.length);
        //console.log($(this).data('task-id'));
        return task.text;
        //return "";
    };
    
    gantt.templates.task_class = function (start, end, task) {
        //text in progress
        let progressPercents = Math.round(task.progress * 100);
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

    gantt.templates.progress_text = function (start, end, task) {        
        return "<span style='text-align:left;padding-left: 10px;box-sizing: border-box;color: white;font-weight: bold;'>" + Math.round(task.progress * 100) + "% </span>";
    };

    gantt.templates.task_class = function (start, end, task) {
        if (task.type == gantt.config.types.project)
            return "hide_project_progress_drag";
    };
    //end of task progress method
       
    
    gantt.plugins({
        tooltip: true
    });       

    gantt.config.columns = [
        { name: "text", label: "Project Name", tree: true, width: 200, resize: true },
        { name: "start_date", label: "Start Date", align: "center", width: 85, resize: true },
        { name: "end_date", label: "End Date", align: "center", width: 85, resize: true },
        {
            name: "progress", label: "Status", align: "center", template: function (progress) {
                return Math.round(progress.progress*100) + "%";
            }, resize: true
        }        
    ];   
   
        
    gantt.config.scale_height = 40;
    gantt.config.row_height = 30;

    gantt.config.auto_scheduling = false;

    gantt.config.layout = {
        css: "gantt_container",
        cols: [
            {
                width: 480,
                min_width: 480,
                rows: [
                    { view: "grid", scrollX: "gridScroll", scrollable: true, scrollY: "scrollVer" },
                    { view: "scrollbar", id: "gridScroll", group: "horizontal" }
                ]
            },
            { resizer: true, width: 1 },
            {
                rows: [
                    { view: "timeline", scrollX: "scrollHor", scrollY: "scrollVer" },
                    { view: "scrollbar", id: "scrollHor", group: "horizontal" }
                ]
            },
            { view: "scrollbar", id: "scrollVer" }
        ]
    };
        

    gantt.attachEvent("onTaskLoading", function (task) {
        task.start_date = gantt.date.parseDate(task.start_date, "xml_date");
        task.end_date = gantt.date.parseDate(task.end_date, "xml_date");
        return true;
    });

    gantt.attachEvent("onGanttReady", function () {
        var tooltips = gantt.ext.tooltips;

        gantt.templates.tooltip_text = function (start, end, task) {
            return "<b>Task:</b> " + task.text + "<br/>" +
                "<b>Start Date:</b> " + gantt.templates.tooltip_date_format(start) +
                "<br/><b>End Date:</b> " + gantt.templates.tooltip_date_format(end) + "<br/>" +
                "<b>Progress:</b> " + Math.round(task.progress*100) + "%";
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
            item.parent = item.ParentId || gantt.config.root_id;
            item[gantt.config.resource_property] = item.ParentId;
            item.text = item.name;
            item.id = parseInt(item.id);
            item.open = true;
            item.key = parseInt(item.id);
            item.owner_id = parseInt(item.id);
            return item;
        }
    });

    var resourceMode = "hours";

    gantt.templates.tooltip_date_format = gantt.date.date_to_str("%j %M, %Y");
    gantt.config.xml_date = "%Y-%m-%d %H:%i"; // format of dates in XML    
    gantt.config.date_grid = "%d-%M-%Y";

    setScaleConfig("year");

    var obj = gantt.json;
    gantt.config.readonly = true;
    gantt.init("ganttContainer"); // initialize gantt
    gantt.load("/api/ganttsummarydata", "json");

    var dp = new gantt.dataProcessor("/api")
    dp.init(gantt);
    dp.setTransactionMode("REST-JSON");
});

$(function () {
    $('#ganttContainer').on("dblclick", ".gantt_row", function () {
        var taskid = $(this).data('task-id');
        window.location.href = "/Ganttnew/GanttDetails?drfid=" + taskid;
    });
});