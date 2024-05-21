using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IMS.web.Models
{
    public class AppUser :IdentityUser 
    {
       
        public String FirstName { get; set; }
        public string MiddleName { get; set; }
        public String LastName { get; set; }
        public int StoreId { get; set; }
        public string Address { get; set; }
        //public string PhoneNumber { get; set; }
        public string UserRoleId { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set;}

    }
}
