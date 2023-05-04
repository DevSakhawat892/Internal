using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

namespace TUSO.Web.Controllers
{
   public class RoleController : Controller
   {
      private readonly HttpClient httpClient;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public RoleController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      /// <summary>
      /// URL: tuso-api/user-roles
      /// </summary>
      /// <returns>List of roles</returns>
      [HttpGet]
      public  IActionResult Index()
      {
         return View();
      }

      /// <summary>
      /// URL: tuso-api/user-role
      /// </summary>
      /// <param name="role">object to be saved in the table as a row</param>
      /// <returns>Saved object</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(Role role)
      {
         if (role != null)
         {
            if (role.OID == 0)
            {
               var roleAdded = await new RoleHttpClient(httpClient).Add(role);

               if (roleAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var roleUpdate = await new RoleHttpClient(httpClient).Update(role);

               if (roleUpdate == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
         }
         else
         {
            return RedirectToAction("Index");
         }
      }

      /// <summary>
      /// URL: tuso-api/designation/{key}
      /// </summary>
      /// <param name="key">Pirmary key of the table</param>
      /// <returns>Delete a row from the table</returns>
      [HttpDelete]
      public async Task<IActionResult> Delete(int OID)
      {
         await new RoleHttpClient(httpClient).Delete(OID);
         return RedirectToAction("Index");
      }
   }
}