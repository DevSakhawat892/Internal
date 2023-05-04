using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Emon
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Web.Controllers
{
    public class RouteTypeController : Controller
    {
        private readonly HttpClient client;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RouteTypeController(HttpClient client)
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
        /// URL: tuso-api/route-type
        /// </summary>
        /// <param name="routeType">Object to be saved in the table as a row.</param>
        /// <returns>Saved object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RouteType routeType)
        {
            if (routeType != null)
            {
                if (routeType.OID == 0)
                {
                    var routeTypeAdd = await new RouteTypeHttpClient(client).Add(routeType);
                    if (routeTypeAdd == null)
                        return RedirectToAction("Index");

                    return RedirectToAction("Index");
                }
                else
                {
                    var routeTypeUpdate = await new RouteTypeHttpClient(client).Update(routeType);

                    if (routeTypeUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            routeTypeId = routeTypeUpdate.OID.ToString()
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
        /// URL: tuso-api/route-type
        /// </summary>
        /// <param name="id">Primary key of the table</param>
        /// <returns>Deletes a row from the table.</returns>
        public async Task<IActionResult> Delete(int OID)
        {
            var counrty = await new RouteTypeHttpClient(client).Delete(OID);

            return RedirectToAction("Index");
        }
    }
}