$(document).ready(function () {
   ReadRole();
});

var BaseApi = "https://localhost:7026/tuso-api/";
var dataarray = [];

function ReadRole() {
   var tablearray = [];
   dataarray = [];
   $.ajax({
      url: BaseApi + "user-roles",
      type: "GET",
      dataType: "json",
      contentType: 'application/json',
      success: function (res) {
         console.log(res);
         for (var i = 0; i < res.length; i++) {
            var table = ['' + (i + 1) + '', '' + res[i].roleName + '', '' + res[i].description + ''];
            var data = { 'sl': (i + 1), 'oid': res[i].oid, 'roleName': res[i].roleName, 'description': res[i].description };
            tablearray.push(table);
            dataarray.push(data);
         }

         $('.datatables-basic').DataTable({
            data: tablearray,
            columns: [
               { title: 'sl' },
               { title: 'role name' },
               { title: 'description' },
               {
                  targets: 1,
                  title: 'Actions',
                  orderable: false,
                  render: function (data, type, full, meta) {
                     return (
                        '<div class="d-inline-flex">' +
                        '<a class="pe-1 dropdown-toggle hide-arrow text-primary" data-bs-toggle="dropdown">' +
                        feather.icons['more-vertical'].toSvg({ class: 'font-small-4' }) +
                        '</a>' +
                        '<div class="dropdown-menu dropdown-menu-end">' +
                        '<a href="javascript:;" class="dropdown-item" id="btnEdit">' +
                        feather.icons['edit'].toSvg({ class: 'me-50 font-small-4' }) +
                        'Edit</a>' +
                        '<a href="javascript:;" class="dropdown-item delete-record deleteRole" id="btnDelete" class="dropdown-item delete-record" asp-action="Delete" asp-controller="Role" asp-route-OID="@Model.OID" onclick="if(!confirm(\'Are you sure you want to delete this item?\')) return false;">' +
                        feather.icons['trash-2'].toSvg({ class: 'me-50 font-small-4' }) +
                        'Delete</a>' +
                        '</div>' +
                        '</div>'
                     );
                  }
               }
            ],

            /* data ordering using column position */
            order: [[0, 'asc']],
            dom: '<"card-header border-bottom p-1"<"head-label"><"dt-action-buttons text-end"B>><"d-flex justify-content-between align-items-center mx-0 row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>t<"d-flex justify-content-between mx-0 row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
            displayLength: 10,
            lengthMenu: [10, 50, 100],
            buttons: [
               {
                  extend: 'collection',
                  className: 'btn btn-outline-secondary dropdown-toggle me-2',
                  text: feather.icons['share'].toSvg({ class: 'font-small-4 me-50' }) + 'Export',
                  buttons: [
                     {
                        extend: 'print',
                        text: feather.icons['printer'].toSvg({ class: 'font-small-4 me-50' }) + 'Print',
                        className: 'dropdown-item',
                        exportOptions: { columns: [0, 1, 2, 3, 4] }
                     },
                     {
                        extend: 'pdf',
                        text: feather.icons['clipboard'].toSvg({ class: 'font-small-4 me-50' }) + 'Pdf',
                        className: 'dropdown-item',
                        exportOptions: { columns: [0, 1, 2, 3, 4] }
                     },
                     {
                        extend: 'excel',
                        text: feather.icons['file'].toSvg({ class: 'font-small-4 me-50' }) + 'Excel',
                        className: 'dropdown-item',
                        exportOptions: { columns: [0, 1, 2, 3, 4] }
                     }
                  ],
                  init: function (api, node, config) {
                     $(node).removeClass('btn-secondary');
                     $(node).parent().removeClass('btn-group');
                     setTimeout(function () {
                        $(node).closest('.dt-buttons').removeClass('btn-group').addClass('d-inline-flex');
                     }, 50);
                  }
               },
               {
                  text: feather.icons['plus'].toSvg({ class: 'me-50 font-small-4' }) + 'Add New',
                  className: 'create-new btn btn-primary',
                  attr: {
                     'data-bs-toggle': 'modal',
                     'data-bs-target': '#modals-slide-in'
                  },
                  init: function (api, node, config) {
                     $(node).removeClass('btn-secondary');
                  }
               }
            ],

            responsive: {
               details: {
                  display: $.fn.dataTable.Responsive.display.modal({
                     header: function (row) {
                        var data = row.data();
                        return 'Details of ' + data['full_name'];
                     }
                  }),
                  type: 'column',
                  renderer: function (api, rowIdx, columns) {
                     var data = $.map(columns, function (col, i) {
                        return col.title !== '' // ? Do not show row in modal popup if title is blank (for check box)
                           ? '<tr data-dt-row="' +
                           col.rowIdx +
                           '" data-dt-column="' +
                           col.columnIndex +
                           '">' +
                           '<td>' +
                           col.title +
                           ':' +
                           '</td> ' +
                           '<td>' +
                           col.data +
                           '</td>' +
                           '</tr>'
                           : '';
                     }).join('');

                     return data ? $('<table class="table"/>').append('<tbody>' + data + '</tbody>') : false;
                  }
               }
            },
            language: {
               paginate: {
                  // remove previous & next text from pagination
                  previous: '&nbsp;',
                  next: '&nbsp;'
               }
            }
         });
      },
      error: function (xhr) {
         console.log(xhr);
      }
   });
}

$('body').on('click', '#btnEdit', function () {
    var dt = $('.datatables-basic').DataTable();
    var row = $(this).parents('tr')[0];
    var sl = dt.row(row).data()[0];
    var ind = dataarray.findIndex(f => f.sl == sl);
    var dta = dataarray[ind];
    $('#oid').val(dta.oid);
    $('#roleName').val(dta.roleName);
    $('#description').val(dta.description);
    $('#modals-slide-in').modal('show');
});


$('body').on('click', '#btnDelete', function () {
    var dt = $('.datatables-basic').DataTable();
    var row = $(this).parents('tr')[0];
    var sl = dt.row(row).data()[0];
    var ind = dataarray.findIndex(f => f.sl == sl);
    var dta = dataarray[ind];
    console.log(dta.oid);
    console.log(typeof (dta.oid));

    $.ajax({
        //url: baseUrl + "Delete/OID?OID=" + dta.oid,
        url: BaseApi + "user-role/" + dta.oid,
        type: "DELETE",
        dataType: "json",
        contentType: 'application/json',
        data: { OID: dta.oid },
        success: function (res) {
            //console.log(res);
            //alert("Delete Successfully");
            window.location.href = '/Role/Index';
            window.location.reload();
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
});

