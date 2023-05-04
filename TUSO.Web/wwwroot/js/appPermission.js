$(document).ready(function () {
   ReadApplicationPermissions();
   getRole();
   checkAll();
   createCheck();
   readCheck();
   editCheck();
   deleteCheck();
   moduleCheck();
});

function checkAll() {
   $("#selectAll").click(function () {
      $('.check').not(this).prop('checked', this.checked);
   });
   $(".check").click(function () {
      $("#selectAll").prop('checked', false)
   })
}

function createCheck() {
   $("#create").click(function () {
      $('.create').not(this).prop('checked', this.checked);
   })
   $(".create").click(function () {
      $("#create").prop('checked', false)
   })
}

function readCheck() {
   $("#read").click(function () {
      $('.read').not(this).prop('checked', this.checked);
   })
   $(".read").click(function () {
      $("#read").prop('checked', false)
   })
}

function editCheck() {
   $("#edit").click(function () {
      $('.edit').not(this).prop('checked', this.checked);
   })
   $(".edit").click(function () {
      $("#edit").prop('checked', false)
   })
}

function deleteCheck() {
   $("#delete").click(function () {
      $('.delete').not(this).prop('checked', this.checked);
   })
   $(".delete").click(function () {
      $("#delete").prop('checked', false)
   })
}

function moduleCheck(e) {
   var data = e.name;
   var create = ".row" + data;
   var findClass = e.classList[1];
   var classFind = "." + findClass;

   //$(classFind).not(this).click(function () {
   //   $(create).prop('checked', this.checked);
   //});
   //$(create).not(this).click(function () {
   //   $(classFind).prop('checked', false);
   //});

   if (e.checked == true) {
      $(create).prop("checked", true);
   }
   else {
      $(create).prop("checked", false);
   }

   $(create).click(function (x) {
      $(classFind).prop('checked', false);
      if (x.checked == true) {
         x.checked == false;
      }
      else{
         x.checked == true;
      }
      //if (x.checked == false) {
      //   x.checked == true;
      //}
   })
}

function getRole() {
   $('#role option').remove();
   $.ajax({
      url: "https://localhost:7026/tuso-api/user-roles",
      type: "GET",
      dataType: "json",
      contentType: 'application/json',
      success: function (res) {
         $('#role').append($('<option>').text('Select').attr({ 'value': '' }));
         $.each(res, function (index, v) {
            $('#role').append($('<option>').text(v.roleName).attr({ 'value': v.oid }));
         });
      },
      error: function (xhr) {
         console.log(xhr);
      }
   });
}

function ReadApplicationPermissions() {
   appPermissionData = [];
   $.ajax({
      url:"https://localhost:7026/tuso-api/application-permissions",
      type: "GET",
      dataType: "json",
      contentType: 'application/json',
      success: function (res) {
         for (var i = 0; i < res.length; i++) {
            var appPermission = { 'oid': res[i].oid, 'readPermission': res[i].readPermission, 'createPermission': res[i].createPermission, 'editPermission': res[i].editPermission, 'deletePermission': res[i].deletePermission, 'roleID': res[i].roleID, 'roleName': res[i].roleName, 'moduleID': res[i].moduleID, 'moduleName': res[i].moduleName, 'allData': res[i].allData };
            appPermissionData.push(appPermission);
         }
         console.log(appPermissionData);
         var data = "";
         var trNo = 0;
         /*var roleNo = 0;*/
         var moduleNo = 0;
         var readNo = 0;
         var createNo = 0;
         var updateNo = 0;
         var deleteNo = 0;
         if (appPermissionData.length> 0) {
            for (var i = 0; i < appPermissionData.length; i++) {
               data += '<tr class="TrNo_' + trNo + '">'
               /*data += '<td class="Role__' + roleNo + '"></td>'*/
               data += '<td class="Module_' + moduleNo + '"><input type="checkbox" id="ModuleNo_' + moduleNo + '" value=""><span>' + appPermissionData[i].moduleName + '</span></td>'
               data += '<td class="Read_' + readNo + '"><input type="checkbox" id="ModuleNo_' + moduleNo + '" value="appPermissionData[i].createPermission"></td>'
               data += '<td class="Create_' + createNo + '"></td>'
               data += '<td class="Update_' + updateNo + '"></td>'
               data += '<td class="Delete_' + deleteNo + '"></td></tr >'
               trNo++;
               /*roleNo++;*/
               moduleNo
               readNo++;
               createNo++;
               updateNo++;
               deleteNo++;
               console.log(appPermissionData[i].moduleName);
            }
         }
         $("#LoadAppPermission").html(data);
      },
      error: function (xhr) {
         console.log(xhr);
      }
   });
}


function CreateApplicationPermissions() {
   var data =
   {
      'oid': 0,
      'title': $("#title").val(),
      'description': $("#description").val(),
      'dueDate': $("#dueDate").val(),
      'dateReported': $("#dateReported").val(),
      'dateResolved': $("#dateResolved").val(),
      'incidentStatus': $("#incidentStatus").val(),
      'userAccountID': $("#userAccountId").val(),
      'incidentTypeID': $("#incidentTypeID").val()
   };
   console.log(data);
   $.ajax({
      /*url: baseapi + "incident",*/
      url: "https://localhost:7026/tuso-api/incident",
      type: "POST",
      dataType: "json",
      contentType: 'application/json',
      data: JSON.stringify(data),
      success: function (res) {
         console.log(res);
         sendNotification();
         connection.invoke("SendNotification").catch(function (err) {
            event.preventDefault();
         });
      },
      error: function (xhr) {
         sendNotification();
         connection.invoke("SendNotification").catch(function (err) {
            event.preventDefault();
         });
      }
   });
}