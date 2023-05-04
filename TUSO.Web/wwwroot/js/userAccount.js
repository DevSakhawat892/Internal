
var baseurl = "https://localhost:7281/";
var baseapi = "https://localhost:7026/tuso-api/";
var dataarray = [];


$(document).ready(function () {
    ReadUserAccount();

    $('#Province').attr('disabled', true);
    $('#District').attr('disabled', true);
    $('#Facility').attr('disabled', true);
    $('#Designation').attr('disabled', true);
});

function ProvinceByCountry (e) {    
    var id = e.target.value;
    $('#Province').attr('disabled', false);
    $('#Province').empty();
    $('#Province').append('<option>--Select Province--</option>');
    $.ajax({
        url: baseapi + 'province/country/' + id,
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (result) {
            $.each(result, function ( i, data) {
                $('#Province').append('<option value=' + data.oid + '>' + data.provinceName + '</option>');
            });
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
};

function DistrictByProvince(e) {    
    var id = e.target.value;
    $('#District').attr('disabled', false);
    $('#District').empty();
    $('#District').append('<option>--Select District--</option>');
    $.ajax({
        url: baseapi + 'district/province/' + id,
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#District').append('<option value=' + data.oid + '>' + data.districtName + '</option>');
            });
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
};

function FacilityByDistrict(e) {
    var id = e.target.value;
    $('#Facility').attr('disabled', false);
    $('#Facility').empty();
    $('#Facility').append('<option>--Select Facility--</option>');
    $.ajax({
        url: baseapi + 'facility/district/' + id,
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#Facility').append('<option value=' + data.oid + '>' + data.facilityName + '</option>');
            });
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
};

function DesignationByDepartment(e) {
    var id = e.target.value;
    $('#Designation').attr('disabled', false);
    $('#Designation').empty();
    $('#Designation').append('<option>--Select Designation--</option>');
    $.ajax({
        url: baseapi + 'designation/department/' + id,
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#Designation').append('<option value=' + data.oid + '>' + data.designationName + '</option>');
            });
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
};

function ReadUserAccount() {
   var tablearray = [];
    dataarray = [];
    $.ajax({
        url: baseapi + "user-accounts",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (res) {
            console.log(res);
            for (var i = 0; i < res.length; i++) {
                //var table = ['' + (i + 1) + '', '' + res[i].provinces.provinceName + '', '' + res[i].districtName + ''];
                //var data = { 'sl': (i + 1), 'oid': res[i].oid, 'districtName': res[i].districtName, 'provinceID': res[i].provinceID };
                //var table = ['' + (i + 1) + '', '' + res[i].full_name + '', '' + res[i].role + '', '' + res[i].provinces.provinceName + '', '' + res[i].districtName + '', '' + res[i].facilityName + '', '' + res[i].email + '', '' + res[i].phone + ''];
                //var data = { 'sl': (i + 1), 'oid': res[i].oid, 'full_name': res[i].full_name, 'provinceID': res[i].provinceID, 'districtID': res[i].districtID, 'facilityID': res[i].facilityID, 'email': res[i].email, 'phone': res[i].phone };

                var table = ['' + (i + 1) + '', '' + res[i].name + '', '' + res[i].roles.roleName + '', '' + res[i].provinceID + '', '' + res[i].districtID + '', '' + res[i].facilityID + '', '' + res[i].designations.designationName + '', '' + res[i].email + '', '' + res[i].cellphone + ''];
                var data = { 'sl': (i + 1), 'oid': res[i].oid, 'name': res[i].name, 'roleID': res[i].roleID, 'provinceID': res[i].provinceID, 'districtID': res[i].districtID, 'facilityID': res[i].facilityID, 'designationID': res[i].designationID, 'email': res[i].email, 'cellphone': res[i].cellphone, };

                tablearray.push(table);
                dataarray.push(data);
            }
            
            $('.datatables-basic').DataTable({
                data: tablearray,
                columns: [
                    { title: 'sl' },
                    { title: 'full name' },
                    { title: 'role' },  
                    { title: 'province' },
                    { title: 'district' },
                    { title: 'facility' },
                    { title: 'designation' },
                    { title: 'email' },
                    { title: 'phone' },
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
                                '<a href="javascript:;" class="dropdown-item" id="btnDetail">' +
                                feather.icons['file-text'].toSvg({ class: 'me-50 font-small-4' }) +
                                'Details</a>' +
                                '<a href="javascript:;" class="dropdown-item delete-record" id="btnDelete"  asp-action="Delete" asp-controller="UserAccount" asp-route-OID="@Model.OID">' +
                                feather.icons['trash-2'].toSvg({ class: 'me-50 font-small-4' }) +
                                'Delete</a>' +
                                '</div>' +
                                '</div>' +
                                '<a href="javascript:;" class="item-edit" id="btnEdit">' +
                                feather.icons['edit'].toSvg({ class: 'me-50 font-small-4' }) +
                                '</a>'
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
                            id: 'insertButton'
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

/*For Users update*/
$('body').on('click', '#btnEdit', function () {
  var dt= $('.datatables-basic').DataTable();
   var row = $(this).parents('tr')[0];
   var sl = dt.row(row).data()[0];
    var ind = dataarray.findIndex(f => f.sl == sl);
    var dta = dataarray[ind];

    window.location.href = "/UserAccount/Edit/" + dta.oid;
});


$('body').on('click', '#btnDelete', function () {
    var dt = $('.datatables-basic').DataTable();
    var row = $(this).parents('tr')[0];
    var sl = dt.row(row).data()[0];
    var ind = dataarray.findIndex(f => f.sl == sl);
    var dta = dataarray[ind];

    $.ajax({
        url: baseurl + "UserAccount/Delete",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        data: { OID: dta.oid },
        success: function (res) {
            /*console.log(res);*/
            alert("Delete Successfully");
            window.location.reload();
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
});

$('body').on('click', '#insertButton', function () {
    window.location.href = "/UserAccount/Create";
});

/*For Users Details*/
$('body').on('click', '#btnDetail', function () {
    var dt = $('.datatables-basic').DataTable();
    var row = $(this).parents('tr')[0];
    var sl = dt.row(row).data()[0];
    var ind = dataarray.findIndex(f => f.sl == sl);
    var dta = dataarray[ind];

    window.location.href = "/UserAccount/Details/userId?userId=" + dta.oid;
});