using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("UserAddress")]
    public partial class UserAddress
    {
        [Key]
        public int AddressId { get; set; }


        public int UserId { get; set; }

        [Required(ErrorMessage ="Street Name is required")]
        [StringLength(70,ErrorMessage ="Cannot be More than 70 Characters")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Street Name is required")]
        [RegularExpression("^[0-9]$", ErrorMessage = "Only Numbers")]
        [StringLength(70, ErrorMessage = "Cannot be More than 70 Characters")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = "Enter valid Zipcode`")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Must be 6 Character Long")]
        public string PostalCode { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required")]
        [Range(1111111111, 9999999999, ErrorMessage = "Must be a valid number")]
        [StringLength(10)]
        public string Mobile { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid emial")]
        [StringLength(100)]
        public string Email { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserAddresses")]
        public virtual User User { get; set; }
    }
}
