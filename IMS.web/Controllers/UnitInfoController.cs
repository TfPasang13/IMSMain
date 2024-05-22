using IMS.Infrastructure.IRepository;
using IMS.Models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class UnitInfoController : Controller
    {
        private readonly ICrudService<UnitInfo> _unitInfo;
        private readonly UserManager<AppUser> _userManager;

        public UnitInfoController(ICrudService<UnitInfo> unitInfo,
            UserManager<AppUser> userManager) 
        {
           _unitInfo = unitInfo;
           _userManager = userManager;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var unitInfo = await _unitInfo.GetAllAsync();
            return View(unitInfo);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            UnitInfo unitInfo = new UnitInfo();
            if (Id > 0)
            {
                unitInfo = await _unitInfo.GetAsync(Id);
            }
            return View(unitInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(UnitInfo unitInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (unitInfo.Id == 0)
                    {
                        unitInfo.CreatedDate = DateTime.Now;
                        unitInfo.CreatedBy = userId;
                        await _unitInfo.InsertAsync(unitInfo);

                        TempData["success"] = "Data Added Successfully!";
                    }
                    else
                    {
                        var OrgunitInfo = await _unitInfo.GetAsync(unitInfo.Id);
                        OrgunitInfo.UnitName = unitInfo.UnitName;
                        OrgunitInfo.IsActive = unitInfo.IsActive;
                        OrgunitInfo.ModifiedDate = DateTime.Now;
                        OrgunitInfo.ModifiedBy = userId;
                        await _unitInfo.UpdateAsync(unitInfo);
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
            var unitInfo = await _unitInfo.GetAsync(id);
            _unitInfo.Delete(unitInfo);
            TempData["error"] = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
