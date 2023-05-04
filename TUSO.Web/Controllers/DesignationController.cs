using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Sakhawat
 * Date created: 11.09.2022
 * Last modified: 11.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.Controllers
{
   public class DesignationController : Controller
   {
      private readonly HttpClient httpClient;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public DesignationController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      /// <summary>
      /// URL: tuso-api/designations
      /// </summary>
      /// <returns>List of designation</returns>
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         var Department = await new DesignationHttpClient(httpClient).ReadDepartments();
         ViewBag.Department = Department;

         return View();
      }

      /// <summary>
      /// URL: tuso-api/designation
      /// </summary>
      /// <param name="designation">object to be saved in the table as a row</param>
      /// <returns>Saved object</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(Designation designation)
      {
         if (designation != null)
         {
            if (designation.OID == 0)
            {
               var designationAdded = await new DesignationHttpClient(httpClient).Add(designation);

               if (designationAdded == null)
                  return RedirectToAction("Index");

               return RedirectToAction("Index");
            }
            else
            {
               var designationUpdate = await new DesignationHttpClient(httpClient).Update(designation);

               if (designationUpdate == null)
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
         await new DesignationHttpClient(httpClient).Delete(OID);
         return RedirectToAction("Index");
      }
   }
}