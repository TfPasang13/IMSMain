using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Models.Entity
{
    public class StoreInfo : BaseEntity
    {
        [Required (ErrorMessage ="Please Input Store Name")]
        [Display(Name ="Store Name")]
        public String StoreName{ get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        [Display (Name ="Phone Number")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public String PhoneNumber { get; set; }
        [Required]
        [Display(Name ="Registration No")]
        public String RegistrationNo { get; set; }
        [Required]
        [Display (Name ="Pan No")]
        public String PanNo { get; set; } 
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        
        public virtual ICollection<CatagoryInfo> CatagoryInfos { get; set; }
        public virtual ICollection<CustomerInfo> CustomerInfos { get; set; }
        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
        public virtual ICollection<ProductInvoiceInfo> ProductInvoiceInfos  { get; set; }
        public virtual ICollection<ProductRateInfo> ProductRateInfos { get; set; } 
        public virtual ICollection<RackInfo> RackInfos { get; set; }
        public virtual ICollection<StockInfo> StockInfos { get; set; }
        public virtual ICollection<SupplierInfo> SuppliersInfos { get; set; }
        public virtual ICollection<TransactionInfo> TransactionInfos { get; set; }  


    }
}
