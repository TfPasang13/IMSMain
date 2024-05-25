using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Models.Entity
{
    public class ProductRateInfo:BaseEntity
    {
        [Required]
        public int CatagoryInfoId { get; set; }
        [Required]
        public int ProductInfoId { get; set; }
        [Required]
        public int StoreInfoId { get; set; }
        [Required]
        public int RackInfoId { get; set; }
        [Required]
        public int SupplierInfoId { get; set; }
        [NotMapped]
        public int UnitInfoId { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float CostPrice { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float SellingPrice { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float Quantity { get; set; }
        [Range(0, float.MaxValue)]
        public float SoldQuantity { get; set; }
        [Range(0, float.MaxValue)]
        public float RemainingQuantity { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public string BatchNo { get; set; }
        [Required]
        public DateTime PurchasedDate { get; set; }
        [Required]
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public virtual CatagoryInfo CatagoryInfo { get; set; }
        public virtual ProductInfo ProductInfo { get; set; }
        public virtual StoreInfo StoreInfo { get; set; }
        public virtual SupplierInfo SupplierInfo { get; set; }
        public virtual RackInfo RackInfo { get; set; }
        public virtual ICollection<ProductInvoiceDetailInfo> ProductInvoiceDetailInfos { get; set; }
        public virtual ICollection<StockInfo> StockInfos { get; set; }
        public virtual ICollection<TransactionInfo> TransactionInfos { get; set; }

    }
}
