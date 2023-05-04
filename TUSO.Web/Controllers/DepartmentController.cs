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
    public class DepartmentController : Controller
    {
        private readonly HttpClient client;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DepartmentController(HttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// URL: tuso-api/department
        /// </summary>
        /// <returns>List of departments</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// URL: tuso-api/department
        /// </summary>
        /// <param name="department">object to be saved in the table as a row</param>
        /// <returns>Saved object</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Department department)
        {
            if (department != null)
            {
                if (department.OID == 0)
                {
                    var departmentAdded = await new DepartmentHttpClient(client).Add(department);

                    if (departmentAdded == null)
                        return RedirectToAction("Index");

                    return RedirectToAction("Index");
                }
                else
                {
                    var departmentUpdate = await new DepartmentHttpClient(client).Update(department);

                    if (departmentUpdate == null)
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
        /// URL: tuso-api/department/{key}
        /// </summary>
        /// <param name="key">Pirmary key of the table</param>
        /// <returns>Delete a row from the table</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int OID)
        {
            await new DepartmentHttpClient(client).Delete(OID);
            return RedirectToAction("Index");
        }
    }
}