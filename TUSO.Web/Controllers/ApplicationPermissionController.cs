using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

namespace TUSO.Web.Controllers
{
   public class ApplicationPermissionController : Controller
   {
      private readonly HttpClient httpClient;
      public ApplicationPermissionController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      [HttpGet]
      public IActionResult Index()
      {
         return View();


         //ViewBag.RoleList = await new UserAccountHttpClient(httpClient).ReadRoles();
         //ViewBag.ModuleList = await new ApplicationPermissionHttpClient(httpClient).ReadModules();

         //var appPermissionList = await new ApplicationPermissionHttpClient(httpClient).ReadApplicationPermissions();

         //return View(appPermissionList.ToList());
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Index(ApplicationPermission applicationPermission)
      {
         if (applicationPermission != null)
         {
            if (applicationPermission.OID == 0)
            {
               var appPermissionAdded = await new ApplicationPermissionHttpClient(httpClient).Add(applicationPermission);

               if (appPermissionAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var appPermissionUpdate = await new ApplicationPermissionHttpClient(httpClient).Update(applicationPermission);

               if (appPermissionUpdate == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
         }
         else
         {
            return RedirectToAction("Index");
         }
      }
   }
}
