using IMS.Infrastructure.IRepository;
using IMS.Models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly ICrudService<CatagoryInfo> _categoryInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<AppUser> _userManager;

        public CatagoryController(ICrudService<CatagoryInfo> categoryInfo,
            ICrudService<StoreInfo> storeInfo,
            UserManager<AppUser> userManager)
        {
            _categoryInfo = categoryInfo;
            _storeInfo = storeInfo;
            _userManager = userManager;
        }

        [HttpGet] 
        public async Task <IActionResult> Index()
        {
            
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var catagoryInfo = await _categoryInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId );
            return View(catagoryInfo);
        }

         [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            CatagoryInfo catagoryInfo = new CatagoryInfo();
            if (Id > 0)
            {
                catagoryInfo = await _categoryInfo.GetAsync(Id);
            }
            return View(catagoryInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(CatagoryInfo catagoryInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (catagoryInfo.Id == 0)
                    {
                        catagoryInfo.CreatedDate = DateTime.Now;
                        catagoryInfo.CreatedBy = userId;
                        catagoryInfo.StoreInfoId=user.StoreId;
                        await _categoryInfo.InsertAsync(catagoryInfo);

                        TempData["success"] = "Data Added Successfully!";
                    }
                    else
                    {
                        var OrgcatagoryInfo = await _categoryInfo.GetAsync(catagoryInfo.Id);
                        OrgcatagoryInfo.CatagoryName = catagoryInfo.CatagoryName;
                        OrgcatagoryInfo.CatagoryDescription = catagoryInfo.CatagoryDescription;
                        OrgcatagoryInfo.IsActive = catagoryInfo.IsActive;
                        OrgcatagoryInfo.ModifiedDate = DateTime.Now;
                        OrgcatagoryInfo.ModifiedBy = userId;
                        await _categoryInfo.UpdateAsync(OrgcatagoryInfo);
                        TempData["success"] = "Data Updated Successfully";
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

        public async Task<IActionResult> Delete(int id)
        {
            var catagoryInfo = await _categoryInfo.GetAsync(id);
            _categoryInfo.Delete(catagoryInfo);
            TempData["error"] = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
