using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TUSO.Domain.Entities;

namespace TUSO.Domain.Dto
{
   public class ApplicationPermissionDto
   {
      public int OID { get; set; }
      public bool ReadPermission { get; set; }
      public bool CreatePermission { get; set; }
      public bool EditPermission { get; set; }
      public bool DeletePermission { get; set; }
      public int RoleID { get; set; }
      public string RoleName { get; set; }
      public int ModuleID { get; set; }
      public string ModuleName { get; set; }

      //public List<ApplicationPermission> ApplicationPermissions { get; set; }
      public int AllData { get; set; }
   }
}
