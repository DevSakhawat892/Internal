using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Emon
 * Date created: 18.09.2022
 * Last modified: 18.09.2022
 * Modified by: Emon
 */
namespace TUSO.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient client;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CategoryController(HttpClient client)
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
        /// URL: tuso-api/category
        /// </summary>
        /// <param name="category">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        public async Task<IActionResult> Index(IncidentType category)
        {
            if (category != null)
            {
                if (category.OID == 0)
                {
                    var categoryAdd = new CategoryHttpClient(client).Add(category);
                    if (categoryAdd == null)
                        return RedirectToAction("Index");

                    return RedirectToAction("Index");
                }
                else
                {
                    var categoryUpdate = await new CategoryHttpClient(client).Update(category);

                    if (categoryUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            countryId = categoryUpdate.OID.ToString()
                        });
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// URL: tuso-api/incident-type
        /// </summary>
        /// <param name="id">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        public async Task<IActionResult> Delete(int OID)
        {
            var category = await new CategoryHttpClient(client).Delete(OID);

            return RedirectToAction("Index");
        }
    }
}