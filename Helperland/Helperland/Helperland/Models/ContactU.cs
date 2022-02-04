using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    public partial class ContactU
    {
        [Key]
        public int ContactUsId { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name Must be more than 3 characters")]
        public string FirstName { get; set; }

        [NotMapped]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name Must be more than 3 characters")]
        public string LastName { get; set; } 

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid emial")]
        [StringLength(200)]
        public string Email { get; set; }


        [StringLength(500, ErrorMessage ="Subject cannot be more than 500 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required")]
        [Range(1111111111, 9999999999, ErrorMessage = "Must be a valid number")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Message is Required")]
        [MinLength(20, ErrorMessage ="Message must be at least 20 Characters long")]
        [RegularExpression("^[ .a-zA-Z0-9:-@_'\",&%$]{1,150}$", ErrorMessage ="Remove special charater except :\".,@-_\"&%$\"")]
        public string Message { get; set; }

        [StringLength(100)]
        public string UploadFileName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }
    }
}
