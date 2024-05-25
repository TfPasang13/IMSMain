using IMS.Infrastructure.IRepository;
using IMS.Models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMS.web.Controllers
{
    public class ProductRateController : Controller
    {
        private readonly ICrudService<ProductRateInfo> _productRateInfo;
        private readonly ICrudService<CatagoryInfo> _catagoryInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly ICrudService<ProductInfo> _productInfo;
        private readonly ICrudService<SupplierInfo> _supplierInfo;
        private readonly ICrudService<RackInfo> _rackInfo;
        private readonly ICrudService<TransactionInfo> _transactionInfo;
        private readonly ICrudService<StockInfo> _stockInfo;
        private readonly ICrudService<UnitInfo> _unitInfo;
        private readonly UserManager<AppUser> _userManager;

        public ProductRateController(ICrudService<ProductRateInfo> productRateInfo,
            ICrudService<CatagoryInfo> catagoryInfo,
           ICrudService<StoreInfo> storeInfo,
           ICrudService<ProductInfo> productInfo,
           ICrudService<SupplierInfo> supplierInfo,
           ICrudService<RackInfo> rackInfo,
           ICrudService<TransactionInfo> transactionInfo,
           ICrudService<StockInfo> stockInfo,
           ICrudService<UnitInfo> unitInfo)
        {
            _productRateInfo = productRateInfo;
            _catagoryInfo = catagoryInfo;
            _storeInfo = storeInfo;
            _productInfo = productInfo;
            _supplierInfo = supplierInfo;
            _rackInfo = rackInfo;
            _transactionInfo = transactionInfo;
            _stockInfo = stockInfo;
            _unitInfo = unitInfo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.CatagoryInfos = await _catagoryInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.SuppliersInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true);
            var rateRateInfo = await _productRateInfo.GetAllAsync();

            return View(rateRateInfo);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            ProductRateInfo productRateInfo = new ProductRateInfo();
            ViewBag.CatagoryInfos = await _catagoryInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.SuppliersInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true);
            productRateInfo.PurchasedDate = DateTime.Now;
            productRateInfo.IsActive = true;

            if (Id > 0)
            {
                productRateInfo = await _productRateInfo.GetAsync(Id);
            }
            return View(productRateInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ProductRateInfo productRateInfo)
        {
            ViewBag.CatagoryInfos = await _catagoryInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.SuppliersInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true);

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);

                    if (productRateInfo.Id == 0)
                    {
                        productRateInfo.CreatedDate = DateTime.Now;
                        productRateInfo.CreatedBy = userId;
                        productRateInfo.StoreInfoId = user.StoreId;
                        var rateInfoId = await _productRateInfo.InsertAsync(productRateInfo);



                        TransactionInfo transactionInfo = new TransactionInfo();

                        transactionInfo.TransactionType = "Purchase";
                        transactionInfo.CatagoryInfoId = productRateInfo.CatagoryInfoId;
                        transactionInfo.ProductInfoId = productRateInfo.ProductInfoId;
                        transactionInfo.UnitInfoId = productRateInfo.UnitInfoId;
                        transactionInfo.ProductRateInfoId = rateInfoId;
                        transactionInfo.Rate = productRateInfo.CostPrice;
                        transactionInfo.Quantity = productRateInfo.Quantity;
                        transactionInfo.TotalAmount = productRateInfo.CostPrice * productRateInfo.Quantity;
                        transactionInfo.IsActive = true;
                        transactionInfo.CreatedDate = DateTime.Now;
                        transactionInfo.CreatedBy = userId;
                        transactionInfo.StoreInfoId = user.StoreId;
                        await _transactionInfo.InsertAsync(transactionInfo);

                        var stockdetail = await _stockInfo.GetAsync(p => p.ProductInfoId == productRateInfo.ProductInfoId);
                        if (stockdetail == null)
                        {

                            StockInfo stockInfo = new StockInfo();

                            stockInfo.CatagoryInfoId = productRateInfo.CatagoryInfoId;
                            stockInfo.ProductInfoId = productRateInfo.ProductInfoId;
                            stockInfo.ProductRateInfoId = rateInfoId;
                            stockInfo.Quantity = productRateInfo.Quantity;
                            stockInfo.IsActive = true;
                            stockInfo.CreatedDate = DateTime.Now;
                            stockInfo.CreatedBy = userId;
                            stockInfo.StoreInfoId = user.StoreId;
                            await _stockInfo.InsertAsync(stockInfo);
                        }
                        else
                        {
                            var qty = stockdetail.Quantity = productRateInfo.Quantity;
                            stockdetail.Quantity = qty;
                            stockdetail.ModifiedDate = DateTime.Now;
                            stockdetail.ModifiedBy = userId;
                            await _stockInfo.UpdateAsync(stockdetail);
                        }

                        TempData["success"] = "Data Added Successfully!";
                    }
                    else
                    {
                        var OrgproductRateInfo = await _productRateInfo.GetAsync(productRateInfo.Id);
                        var productId = OrgproductRateInfo.ProductInfoId;
                        OrgproductRateInfo.CatagoryInfoId = productRateInfo.CatagoryInfoId;
                        OrgproductRateInfo.ProductInfoId = productRateInfo.ProductInfoId;
                        OrgproductRateInfo.CostPrice = productRateInfo.CostPrice;
                        OrgproductRateInfo.SellingPrice = productRateInfo.SellingPrice;
                        OrgproductRateInfo.Quantity = productRateInfo.Quantity;
                        OrgproductRateInfo.BatchNo = productRateInfo.BatchNo;
                        OrgproductRateInfo.PurchasedDate = productRateInfo.PurchasedDate;
                        OrgproductRateInfo.ExpiryDate = productRateInfo.ExpiryDate;
                        OrgproductRateInfo.SupplierInfoId = productRateInfo.SupplierInfoId;
                        OrgproductRateInfo.RackInfoId = productRateInfo.RackInfoId;
                        OrgproductRateInfo.IsActive = productRateInfo.IsActive;
                        OrgproductRateInfo.ModifiedDate = DateTime.Now;
                        OrgproductRateInfo.ModifiedBy = userId;
                        await _productRateInfo.UpdateAsync(OrgproductRateInfo);


                        TransactionInfo transactionInfo = new TransactionInfo();

                        transactionInfo.TransactionType = "Purchase";
                        transactionInfo.CatagoryInfoId = productRateInfo.CatagoryInfoId;
                        transactionInfo.ProductInfoId = productRateInfo.ProductInfoId;
                        transactionInfo.UnitInfoId = productRateInfo.UnitInfoId;
                        transactionInfo.ProductRateInfoId = OrgproductRateInfo.Id;
                        transactionInfo.Rate = productRateInfo.CostPrice;
                        transactionInfo.Quantity = productRateInfo.Quantity;
                        transactionInfo.TotalAmount = productRateInfo.CostPrice * productRateInfo.Quantity;
                        transactionInfo.IsActive = true;
                        transactionInfo.CreatedDate = DateTime.Now;
                        transactionInfo.CreatedBy = userId;
                        transactionInfo.StoreInfoId = user.StoreId;
                        await _transactionInfo.InsertAsync(transactionInfo);


                        var stockdetail = await _stockInfo.GetAsync(p => p.ProductInfoId == productRateInfo.ProductInfoId);
                        if (stockdetail == null)
                        {

                            StockInfo stockInfo = new StockInfo();

                            stockInfo.CatagoryInfoId = productRateInfo.CatagoryInfoId;
                            stockInfo.ProductInfoId = productRateInfo.ProductInfoId;
                            stockInfo.ProductRateInfoId = OrgproductRateInfo.Id;
                            stockInfo.Quantity = productRateInfo.Quantity;
                            stockInfo.IsActive = true;
                            stockInfo.CreatedDate = DateTime.Now;
                            stockInfo.CreatedBy = userId;
                            stockInfo.StoreInfoId = user.StoreId;
                            await _stockInfo.InsertAsync(stockInfo);
                        }
                        else
                        {
                            var qty = stockdetail.Quantity = productRateInfo.Quantity;
                            stockdetail.Quantity = qty;
                            stockdetail.ModifiedDate = DateTime.Now;
                            stockdetail.ModifiedBy = userId;
                            await _stockInfo.UpdateAsync(stockdetail);
                        }

                        TempData["success"] = "Data Added Successfully!";
                    
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
        [HttpPost]
        [Route("/api/ProductRate/getproduct")]
        public async Task<IActionResult> GetProduct(int CatagoryId)
        {
            var productList = await _productInfo.GetAllAsync(p => p.CatagoryInfoId == CatagoryId);

            return Json(new { productList });
        }

        [HttpPost]
        [Route("/api/ProductRate/getUnit")]
        public async Task<IActionResult> GetUnit(int ProductId)
        {
            var product = await _productInfo.GetAsync(ProductId);

            return Json(new { product });
        }
    }
}

