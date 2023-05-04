using Microsoft.AspNetCore.Mvc;

 
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Bithy
 * Date created: 13.09.2022
 * Last modified: 13.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.Controllers
{
    public class FacilityController : Controller
    {
        private readonly string BaseUrl;
        private readonly HttpClient client;

        public FacilityController(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DistrictList = await new FacilityHttpClient(client).ReadDistricts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Facility facility)
        {
            ViewBag.FacilityList = await new FacilityHttpClient(client).ReadDistricts();

            if (facility != null)
            {
                if (facility.OID == 0)
                {
                    var facilityAdd = await new FacilityHttpClient(client).Add(facility);

                    if (facilityAdd == null)
                        return View(facility);

                    return RedirectToAction("Index");
                }
                else
                {
                    var facilityUpdate = await new FacilityHttpClient(client).Update(facility);
                    if (facilityUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            facilityid = facilityUpdate.OID.ToString()
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


        public async Task<IActionResult> Delete(int OID)
        {
            var facility = await new FacilityHttpClient(client).Delete(OID);
            return RedirectToAction("Index");
        }
    }
}