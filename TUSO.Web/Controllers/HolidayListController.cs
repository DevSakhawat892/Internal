using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Bithy
 * Date created: 22.09.2022
 * Last modified: 22.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.Controllers
{
    public class HolidayListController : Controller
    {
        private readonly string BaseUrl;
        private readonly HttpClient client;

        public HolidayListController(HttpClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeekend()
        {
            List<HolidayList>? weekday = await new HolidayListHttpClient(client).GetAllWeekEnd();
            List<HolidayList> list = new List<HolidayList>(weekday.ToList());

            return Json(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HolidayList holidayList)
        {
            await new HolidayListHttpClient(client).PostAllWeekEnd(holidayList);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddVacation(HolidayList hlist, DateTime from, DateTime to)
        {
            await new HolidayListHttpClient(client).PostAllVacation(hlist, from, to);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteWeekend(int holidayListID)
        {
            await new HolidayListHttpClient(client).DeleteWeekEnd(holidayListID);
            return Json("Index");
        }  
    }
}