using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Emon
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Emon
 */
namespace TUSO.Web.Controllers
{
    public class PriorityController : Controller
    {
        private readonly string BaseUrl;
        private readonly HttpClient client;
        public PriorityController(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SLAList = await new PriorityHttpClient(client).ReadSLA();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Priority priority)
        {

            if (priority != null)
            {
                if (priority.OID == 0)
                {
                    var priorityAdd = await new PriorityHttpClient(client).Add(priority);
                    if (priorityAdd == null)
                        return View(priority);
                    return RedirectToAction("Index");
                }
                else
                {
                    var priorityUpdate = await new PriorityHttpClient(client).Update(priority);
                    if (priorityUpdate != null)
                    {
                        return RedirectToAction("", new
                        {
                            priorityid = priorityUpdate.OID.ToString()
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
            var priority = await new PriorityHttpClient(client).Delete(OID);
            return RedirectToAction("Index");
        }
    }
}
