$(document).ready(function () {
    $('#btnExportToPdf').click(() => {
        //$('#btnExportToPdf').css("display", "none");
        //$('aside.main-sidebar.sidebar-dark-primary.elevation-4').css("display", "none");
        //$("i.fas.fa-bars").css("display", "none");
        //$("span#NotificationNo.badge.badge-warning.navbar-badge").css("display", "none");
        ////document.getElementById("frmMA")
        //var pdf = new jsPDF('p', 'pt', 'a4');
        //pdf.addHTML(document.body, function () {
        //    pdf.save('PIDFManagementApproval.pdf');
        //});
        //$('#btnExportToPdf').css("display", "block");
        //$('aside.main-sidebar.sidebar-dark-primary.elevation-4').css("display", "block");
        //$("i.fas.fa-bars").css("display", "block");
        //$("span#NotificationNo.badge.badge-warning.navbar-badge").css("display", "block");

        /**/

        let width = $("#frmMA").width();
        let options = {
            pagesplit: true,
            'background': "#ffffff"
            /* 'width': width*/
            //'fontname':"Times-Roman",
            //'fontsize':"15"
        }

        let pdf = new jsPDF('p', 'pt', 'a4');
        //let pdf = new jsPDF('p', 'mm', 'a4');
        pdf.addHTML($("#frmMA"), options, function () {
            pdf.save('PIDFManagementApproval.pdf');
        });
    });

    $('#btnPrintPDF').click(function () {
        var ProjectName = $("#lblProjectName").text();
        var tdate = new Date();
        var dd = tdate.getDate(); //yields day
        var MM = tdate.getMonth(); //yields month
        var yyyy = tdate.getFullYear(); //yields year
        var hh = tdate.getHours();
        var mins = tdate.getMinutes();
        var currentDate = dd + "-" + (MM + 1) + "-" + yyyy + "-" + hh + "-" + mins;
        var fileName = 'ManagementApproval_' + (ProjectName == null || ProjectName == undefined ? "" : ProjectName.toString().trim()) + '_' + currentDate;
        printElement(fileName);
    });
});
