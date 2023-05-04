using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Dto;
using TUSO.Domain.Entities;
using TUSO.Web.HttpClients;

/*
 * Created by: Bithy
 * Date created: 13.09.2022
 * Last modified: 24.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Web.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly string BaseUrl;
        private readonly HttpClient client;
        public UserAccountController(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        #region UserAccount Create
        public async Task<IActionResult> Create()
        {
            var users = new UserAccountDto { };

            ViewBag.RoleList = await new UserAccountHttpClient(client).ReadRoles();
            ViewBag.CountryList = await new UserAccountHttpClient(client).ReadCountries();
            //ViewBag.ProvinceList = await new UserAccountHttpClient(client).ReadProvinces();
            //ViewBag.DistrictList = await new UserAccountHttpClient(client).ReadDistricts();
            //ViewBag.FacilityList = await new UserAccountHttpClient(client).ReadFacilities();
            ViewBag.DepartmentList = await new UserAccountHttpClient(client).ReadDepartments();
            //ViewBag.DesignationList = await new UserAccountHttpClient(client).ReadDesignations();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAccountDto user)
        {
            var userAdded = await new UserAccountHttpClient(client).Add(user);

            if (userAdded == null)
                return View(user);

            return RedirectToAction("Index");
        }
        #endregion

        #region UserAccount Edit
        public async Task<IActionResult> Edit(long id)
        {
            var users = await new UserAccountHttpClient(client).ReadUserAccountByKey(id);

            if (users == null)
                return RedirectToAction("Index");

            ViewBag.RoleList = await new UserAccountHttpClient(client).ReadRoles();
            ViewBag.CountryList = await new UserAccountHttpClient(client).ReadCountries();
            //ViewBag.ProvinceList = await new UserAccountHttpClient(client).ReadProvinces();
            //ViewBag.DistrictList = await new UserAccountHttpClient(client).ReadDistricts();
            //ViewBag.FacilityList = await new UserAccountHttpClient(client).ReadFacilities();
            ViewBag.DepartmentList = await new UserAccountHttpClient(client).ReadDepartments();
            //ViewBag.DesignationList = await new UserAccountHttpClient(client).ReadDesignations();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserAccount user)
        {
            var userUpdate = await new UserAccountHttpClient(client).Update(user);

            if (userUpdate == null)
                return View(user);

            return RedirectToAction("Index", new
            {
                id = userUpdate.OID.ToString()
            });
        }
        #endregion

        #region Recovery Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecoveryRequest(RecoveryRequestDto recoveryRequestDto)
        {
            var changePassword = await new UserAccountHttpClient(client).RecoveryRequest(recoveryRequestDto);

            if (changePassword == null)
                //return View(recoveryRequestDto);
                return RedirectToAction("Index", "RecoveryRequest");

            return RedirectToAction("Index", "RecoveryRequest");
        }
        #endregion

        #region User details
        [HttpGet]
        public async Task<IActionResult> Details(long userId)
        {
            var userDetail = await new UserAccountHttpClient(client).GetUserDetails(userId);

            if (userDetail != null)
            {
                return View("Details", userDetail);
            }

            return RedirectToAction("Index");
        }
        #endregion

        public async Task<IActionResult> Delete(int OID)
        {
            var district = await new UserAccountHttpClient(client).Delete(OID);
            return RedirectToAction("Index");
        }
    }
}