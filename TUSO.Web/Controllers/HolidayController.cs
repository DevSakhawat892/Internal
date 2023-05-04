using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022, 21.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.Controllers
{
    public class HolidayController : Controller
    {
        private readonly string BaseUrl;
        private readonly HttpClient client;
       
        public HolidayController(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Holiday holiday)
        {
            if (holiday != null)
            {
                if (holiday.OID == 0)
                {
                    var holidayAdd = await new HolidayHttpClient(client).Add(holiday);
                    if (holidayAdd == null)
                        return View(holiday);
                    return RedirectToAction("Index");
                }
                else
                {
                    var holidayUpdate = await new HolidayHttpClient(client).Update(holiday);
                    if (holidayUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            holidayid = holidayUpdate.OID.ToString()
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
            var district = await new HolidayHttpClient(client).Delete(OID);
            return RedirectToAction("Index");
        }
    }
}