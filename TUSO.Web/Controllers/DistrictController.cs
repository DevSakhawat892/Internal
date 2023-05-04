using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Bithy
 * Date created: 11.09.2022
 * Last modified: 12.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.Controllers
{
    public class DistrictController : Controller
    {
        private readonly HttpClient client;
        public DistrictController(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProvinceList = await new DistrictHttpClient(client).ReadProvinces();
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(District district)
        {
            ViewBag.FacilityList = new DistrictHttpClient(client).ReadProvinces();

            if (district != null)
            {
                if (district.OID == 0)
                {
                    var districtAdd = await new DistrictHttpClient(client).Add(district);
                    if (districtAdd == null)
                        return View(district);
                    return RedirectToAction("Index");
                }
                else
                {
                    var districtUpdate = await new DistrictHttpClient(client).Update(district);                    
                    if (districtUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            districtid = districtUpdate.OID.ToString()
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
            var district = await new DistrictHttpClient(client).Delete(OID);
            return RedirectToAction("Index");
        }
    }
}