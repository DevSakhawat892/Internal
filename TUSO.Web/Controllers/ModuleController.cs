using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

namespace TUSO.Web.Controllers
{
   public class ModuleController : Controller
   {
      private readonly HttpClient client;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public ModuleController(HttpClient client)
      {
         this.client = client;
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
      /// URL: https://localhost:7185/module
      /// </summary>
      /// <param name="module">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Module module)
        {
            if (module != null)
            {
                if (module.OID == 0)
                {
                    var moduleAdded = await new ModuleHttpClient(client).Add(module);

                    if (moduleAdded == null)
                        return RedirectToAction("Index");

                    return RedirectToAction("Index");
                }
                else
                {
                    var moduletUpdate = await new ModuleHttpClient(client).Update(module);

                    if (moduletUpdate == null)
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