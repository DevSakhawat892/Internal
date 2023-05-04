using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Emon
 * Date created: 12.09.2022
 * Last modified: 12.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly HttpClient client;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CountryController(HttpClient client)
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
        /// URL: tuso-api/country
        /// </summary>
        /// <param name="country">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        public async Task<IActionResult> Index(Country country)
       {
            if (country != null)
            {
                if (country.OID == 0)
                {
                    var countryAdd = new CountryHttpClient(client).Add(country);
                    if(countryAdd == null)
                        return RedirectToAction("Index");

                    return RedirectToAction("Index");
                }
                else
                {
                    var countryUpdate = await new CountryHttpClient(client).Update(country);

                    if (countryUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            countryId = countryUpdate.OID.ToString()
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
        /// URL: tuso-api/country
        /// </summary>
        /// <param name="id">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int OID)
        {
            await new CountryHttpClient(client).Delete(OID);

            return RedirectToAction("Index");
        }
    }
}