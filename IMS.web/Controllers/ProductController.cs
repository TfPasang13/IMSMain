using IMS.Infrastructure.IRepository;
using IMS.Models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICrudService<ProductInfo> _productInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly ICrudService<UnitInfo> _unitInfo;
        private readonly ICrudService<CatagoryInfo> _catagoryInfo;
        private readonly UserManager<AppUser> _userManager;

        ProductController(ICrudService<ProductInfo> productInfo,
            ICrudService<StoreInfo> storeInfo,
            ICrudService<UnitInfo> unitInfo,
            ICrudService<CatagoryInfo> catagoryInfo,
            UserManager<AppUser> userManager) 
        {
            _productInfo = productInfo;
            _storeInfo = storeInfo;
            _unitInfo = unitInfo;
            _catagoryInfo = catagoryInfo;
            _userManager = userManager;
        }
        [HttpGet]   
        public async Task <IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var productInfo = await _catagoryInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
           
            return View(productInfo);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            ProductInfo productInfo = new ProductInfo();
            if (Id > 0)
            {
                productInfo = await _productInfo.GetAsync(Id);
            }
            return View(productInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ProductInfo productInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (productInfo.Id == 0)
                    {
                        productInfo.CreatedDate = DateTime.Now;
                        productInfo.CreatedBy = userId;
                        productInfo.StoreInfoId = user.StoreId;
                        productInfo.UnitInfoId = user.UnitId;
                        await _productInfo.InsertAsync(productInfo);

                        TempData["success"] = "Data Added Successfully!";
                    }
                    else
                    {
                        var OrgProductInfo = await _productInfo.GetAsync(productInfo.Id);
                        OrgProductInfo.ProductName = productInfo.ProductName;
                        OrgProductInfo.ProductDescription = productInfo.ProductDescription;
                        OrgProductInfo.ImageUrl = productInfo.ImageUrl;
                        OrgProductInfo.IsActive = productInfo.IsActive;
                        OrgProductInfo.ModifiedDate = DateTime.Now;
                        OrgProductInfo.ModifiedBy = userId;
                        await _productInfo.UpdateAsync(OrgProductInfo);
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
            var productInfo = await _productInfo.GetAsync(id);
            _productInfo.Delete(productInfo);
            TempData["error"] = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
