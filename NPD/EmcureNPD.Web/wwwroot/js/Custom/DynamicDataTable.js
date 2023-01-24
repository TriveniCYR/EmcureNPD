function IntializingDataTable(id, setDefaultOrder, ajaxObject, columnObject, rowCallBack, drawCallback) {
    var dataTableInst =
        $('#' + id + '').DataTable({
            order: setDefaultOrder,
            processing: true,
            serverSide: true,
            stateSave: true,
            orderMulti: false,
            filter: true,
            orderCellsTop: true,
            "bLengthChange": true,
            'bSortable': true,
            fixedHeader: true,
            "pageLength": 25,
            dom: 'Bfrtip',
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data));
            },
            stateLoadCallback: function (settings, data) {
                return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance));
            },
            buttons: [
                {
                    extend: 'excel', text: '<i class="far fa-file-excel"></i> Export In Excel ', className: "btn-primary", exportOptions: {
                        columns: ':visible'
                    }
                },
                { extend: 'colvis', className: "btn-primary" }
            ],
            "ajax": ajaxObject,
            "fnRowCallback": rowCallBack,
            "columns": columnObject,
            initComplete: function (settings, json) {

            },
            "drawCallback": function (settings) {
                if (drawCallback != null)
                    drawCallback();
            }
        });
}