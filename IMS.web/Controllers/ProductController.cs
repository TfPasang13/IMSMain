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

        public ProductController(ICrudService<ProductInfo> productInfo,
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
            ViewBag.CatagoryInfos = await _catagoryInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);

            var productInfo = await _productInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
           
            return View(productInfo);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            ProductInfo productInfo = new ProductInfo();
            ViewBag.CategoryInfos = await _catagoryInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            productInfo.IsActive = true;
            if (Id > 0)
            {
                productInfo = await _productInfo.GetAsync(Id);
            }
            return View(productInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ProductInfo productInfo)
        {
            ViewBag.CatagoryInfos = await _catagoryInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);

                    if (productInfo.ImageFile != null)
                    {
                        string fileDirectory = $"wwwroot/ProductImage";

                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        string uniqueFileName = Guid.NewGuid() + "_" + productInfo.ImageFile.FileName;
                        string filePath = Path.Combine(Path.GetFullPath($"wwwroot/ProductImage"), uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await productInfo.ImageFile.CopyToAsync(fileStream);
                            productInfo.ImageUrl = $"ProductImage/" + uniqueFileName;

                        }

                    }

                    if (productInfo.Id == 0)
                    {
                        productInfo.CreatedDate = DateTime.Now;
                        productInfo.CreatedBy = userId;
                        productInfo.StoreInfoId = user.StoreId;
                        await _productInfo.InsertAsync(productInfo);

                        TempData["success"] = "Data Added Successfully!";
                    }
                    else
                    {
                        var OrgproductInfo = await _productInfo.GetAsync(productInfo.Id);
                        OrgproductInfo.CatagoryInfoId = productInfo.CatagoryInfoId;
                        OrgproductInfo.ProductName = productInfo.ProductName;
                        OrgproductInfo.ProductDescription = productInfo.ProductDescription;
                        OrgproductInfo.UnitInfoId = productInfo.UnitInfoId;

                        if (productInfo.ImageFile != null)
                        {
                            OrgproductInfo.ImageUrl = productInfo.ImageUrl;
                        }

                        await _productInfo.UpdateAsync(OrgproductInfo);
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
    }
}
