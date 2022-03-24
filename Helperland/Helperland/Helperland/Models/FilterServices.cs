using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models
{
    public class FilterServices
    {
        public int? ServiceRequestId { get; set; }

        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage = "Enter valid Zipcode")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Must be 6 Character Long")]
        public string? PostalCode { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid emial")]
        [StringLength(100)]
        public string? Email { get; set; }

        public int CustomerId { get; set; }

        public int ServiceProviderId { get; set; }

        public int UserId { get; set; }

        public int UserTypeId { get; set; }

        [Range(1111111111, 9999999999, ErrorMessage = "Must be a valid number")]
        [StringLength(10)]
        public string? Mobile { get; set; }
        public int Status { get; set; }

        public bool HasIssue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FromDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //[GreaterThan("FromDate", ErrorMessage ="To Date Must be Greater than From Date")]
        public DateTime? ToDate { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (FromDate < ToDate)
        //    {
        //        yield return new ValidationResult(
        //            errorMessage: "ToDate must be greater than FromDate",
        //            memberNames: new[] { "ToDate" }
        //       );
        //    }
        //}

        //IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        //{
        //    List<ValidationResult> res = new List<ValidationResult>();
        //    if (FromDate >= ToDate)
        //    {
        //        ValidationResult mss = new ValidationResult("ToDate must be greater than FromDate");
        //        res.Add(mss);

        //    }
        //    return res;
        //}
    }
}
