$(document).ready(function () {
   ReadRecoveryRequest();
});
var baseapi = "https://localhost:7026/tuso-api/";
var baseUrl = "https://localhost:7185/recoveryrequest/"
var dataarray = [];

function ReadRecoveryRequest() {
    var tablearray = [];
    dataarray = [];
    $.ajax({
        url: baseapi + "recovery-requests",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (res) {
          /*console.log(res[i].status);*/
            for (var i = 0; i < res.length; i++) {
                var table = ['' + (i + 1) + '', '' + res[i].userAccount.username + '', '' + res[i].requestDescription + '', '' + res[i].status];
                var data = { 'sl': (i + 1), 'oid': res[i].oid, 'userId': res[i].userId, 'username': res[i].userAccount.username, 'status': res[i].status, 'requestDescription': res[i].requestDescription };
                tablearray.push(table);
               dataarray.push(data);
            }

           $('.datatables-basic').DataTable({
              data: tablearray,
              dataarray: dataarray,
                columns: [
                    { title: 'sl' },
                    { title: 'Username' },
                    { title: 'Description' },
                    { title: 'Status' },
                    {
                        targets: 1,
                        title: 'Actions',
                        orderable: false,

                       render: function (data, type, full, meta) {
                          if (full[3] == 'false') {
                             return (
                                '<a type = "button" class="deleteRecoveryRequest btn btn-danger" onclick="deleteRecoveryRequest(this)" asp-controller="RecoveryRequest" asp-action="Delete" asp-route-oid = "@Model.oid"> Delete</a>'
                             );
                          }
                          else {
                             return (
                                '<a type="button" class="recoveryRequest btn btn-primary me-2" data-bs-toggle="modal" data-bs-target="#staticBackdrop"> Change </a>'
                               
                             );
                          }
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

$('body').on('click', '.recoveryRequest', function () {
    var dt = $('.datatables-basic').DataTable();
    var row = $(this).parents('tr')[0];
    var sl = dt.row(row).data()[0];
    var ind = dataarray.findIndex(f => f.sl == sl);
    var dta = dataarray[ind];
    console.log(dta);
    $('#userId').val(dta.userId);
    $('#oid').val(dta.oid);
    $('#status').val(dta.status);
});

function deleteRecoveryRequest() {
   var dt = $('.datatables-basic').DataTable();
   var row = $(this).parents('tr')[0];
   var sl = dt.row(row).data()[0];
   var ind = dataarray.findIndex(f => f.sl == sl);
   var dta = dataarray[ind];
   console.log(dta);
   $.ajax({
      url: baseUrl + "delete?oid=dta.oid",
      type: "DELETE",
      dataType: "json",
      contentType: 'application/json',
/*      data: { 'oid': dta.oid },*/
      success: function (res) {
         alert("Recovery request will be deleted.");
      }
   });  
}

function getUser(e) { 
    var user = e.value;
    $("#pp").html('');
    $.ajax({
        url: baseapi + "user-account/" + user,
        type: "GET",
        dataType: "json",
        contextType: "application/josn",
        success: function (res) {
            console.log(res.username);
            if (user == res.username) {
                $("#requestDescription").prop('readonly', false);
                $("#pp").append('<strong style="color: green">Are you ' + res.name + '? If yes, write your problem below.</strong>');
            }
            else {
                $("#requestDescription").prop('readonly', true);
                $("#pp").append('<h2 style="color: red; text-align:center;">Username not matched</h2>');
            }
        },
        error: function (ex) {
            $("#requestDescription").prop('readonly', true);
            $("#pp").append('<h2 style="color: red; text-align:center;">Username not matched!</h2>');
        } 
    });
}

function confirmPasswordValidation() {
   var password = document.querySelector("#password").value;
   var confirmPassword = document.querySelector("#confirmPassword").value;
   console.log(password);
   console.log(confirmPassword);
   if (password == confirmPassword ) {
      $('#message').html('Password match').css('color', 'green');
      $(".recoveryRequest-btn").prop('disabled', false);
   }
   else {
      $('#message').html('Password not match').css('color', 'red');
      $(".recoveryRequest-btn").prop("disabled", true);
   }
}