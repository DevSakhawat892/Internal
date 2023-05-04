using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Dto;
using TUSO.Web.HttpClients;

/*
 * Created by: Emon
 * Date created: 19.09.2022
 * Last modified: 19.09.2022
 * Modified by: Emon
 */
namespace TUSO.Web.Controllers
{
   public class RecoveryRequestController : Controller
   {
      private readonly HttpClient client;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public RecoveryRequestController(HttpClient client)
      {
         this.client = client;
      }

      [HttpGet]
      public IActionResult Recovery()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Recovery(RecoveryRequestDto recoveryRequest)
      {
         if (recoveryRequest != null)
         {
            if (recoveryRequest.OID == 0)
            {
               var recoveryRequestAdd = await new RecoveryRequestHttpClient(client).Add(recoveryRequest);

               if (recoveryRequestAdd == null)
               {
                  //ViewBag.ErrorMessage = "User Name not match. Please insert valid username";
                  TempData["ErrorMesg"] = "Username not matched. Please insert valid username";
                  return RedirectToAction("Recovery", recoveryRequest);
               }
               return RedirectToAction("Login", "Home");

            }
            else
            {
               var recoveryRequestUpdate = await new RecoveryRequestHttpClient(client).Update(recoveryRequest);

               if (recoveryRequestUpdate == null)
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
      /// 
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public IActionResult Index()
      {
         return View();
      }

      /// <summary>
      /// URL: tuso-api/recovery-request
      /// </summary>
      /// <param name="id">Primary key of the table</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      public async Task<IActionResult> Delete(int OID)
      {
         if (OID > 0)
         {
            var recoveryRequestInDb = await new RecoveryRequestHttpClient(client).Delete(OID);
            if (recoveryRequestInDb == null)
               return RedirectToAction("Index");

            return RedirectToAction("Index");
         }
         else
         {
            return RedirectToAction("Index");
         }
      }
   }
}