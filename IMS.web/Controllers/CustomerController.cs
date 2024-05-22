using IMS.Infrastructure.IRepository;
using IMS.Models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IMS.web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICrudService<CustomerInfo> _customerInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(ICrudService<CustomerInfo> customerInfo,
            ICrudService<StoreInfo> storeInfo,
            UserManager<AppUser> userManager) 
        {
            _customerInfo = customerInfo;
            _storeInfo = storeInfo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var customerInfo = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
            return View(customerInfo);
        }
        [HttpGet]
        public async Task <IActionResult> AddEdit(int id)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            if(id > 0)
            {
                customerInfo = await _customerInfo.GetAsync(id);
            }

            return View(customerInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit (CustomerInfo customerInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (customerInfo.Id == 0)
                    {
                        customerInfo.CreatedDate = DateTime.Now;
                        customerInfo.CreatedBy = userId;
                        customerInfo.StoreInfoId = user.StoreId;
                        await _customerInfo.InsertAsync(customerInfo);

                        TempData["success"] = "Data Added Successfully!";
                    }
                    else
                    {
                        var OrgcustomerInfo = await _customerInfo.GetAsync(customerInfo.Id);
                        OrgcustomerInfo.CustomerName = customerInfo.CustomerName;
                        OrgcustomerInfo.Email = customerInfo.Email;
                        OrgcustomerInfo.PhoneNumber = customerInfo.PhoneNumber;
                        OrgcustomerInfo.Address = customerInfo.Address;
                        OrgcustomerInfo.PanNo = customerInfo.PanNo;
                        await _customerInfo.UpdateAsync(OrgcustomerInfo);
                        TempData["success"] = "Data Updated Sucessfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["error"] = "Something went wrong, please try again later";
                    return RedirectToAction(nameof(AddEdit));
                }
            }

            TempData["error"] = "Please input Valid Data";
            return RedirectToAction(nameof(AddEdit));
        }
    }
}
