using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            FavoriteAndBlockedTargetUsers = new HashSet<FavoriteAndBlocked>();
            FavoriteAndBlockedUsers = new HashSet<FavoriteAndBlocked>();
            RatingRatingFromNavigations = new HashSet<Rating>();
            RatingRatingToNavigations = new HashSet<Rating>();
            ServiceRequestServiceProviders = new HashSet<ServiceRequest>();
            ServiceRequestUsers = new HashSet<ServiceRequest>();
            UserAddresses = new HashSet<UserAddress>();
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="First Name is required")]
        [StringLength(50, MinimumLength =3, ErrorMessage ="First Name Must be more than 3 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name Must be more than 3 characters")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Please enter valid emial")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage ="Password length must be more than 6 characters")]
        [MaxLength(20, ErrorMessage = "Password length must be less than 20 characters")]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "You must have At least one Upper case, one Lower case, one Digit and one Special Character")]
        [StringLength(20)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage ="Confirm Password is Required")]
        [Compare("Password", ErrorMessage ="Confirm Password must be same as Password")]
        [StringLength(20)]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        [Required(ErrorMessage ="Required")]
        public bool Terms { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Required")]
        public bool Privacy { get; set; }

        [Required(ErrorMessage ="Mobile Number is Required")]
        [Range(1111111111,9999999999, ErrorMessage = "Must be a valid number")]
        [StringLength(10)]
        public string Mobile { get; set; }

        public int UserTypeId { get; set; }

        public int? Gender { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        public string UserProfilePicture { get; set; }

        public bool IsRegisteredUser { get; set; }

        [StringLength(200)]
        public string PaymentGatewayUserRef { get; set; }

        [RegularExpression("^[1-9][0-9]{5}$", ErrorMessage ="Enter valid Zipcode`")]
        [StringLength(6, MinimumLength =6, ErrorMessage ="Must be 6 Character Long")]
        public string ZipCode { get; set; }

        public bool WorksWithPets { get; set; }

        public int? LanguageId { get; set; }

        public int? NationalityId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public bool IsApproved { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string BankTokenId { get; set; }

        [StringLength(50)]
        public string TaxNo { get; set; }

        [InverseProperty(nameof(FavoriteAndBlocked.TargetUser))]
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedTargetUsers { get; set; }
        [InverseProperty(nameof(FavoriteAndBlocked.User))]
        public virtual ICollection<FavoriteAndBlocked> FavoriteAndBlockedUsers { get; set; }
        [InverseProperty(nameof(Rating.RatingFromNavigation))]
        public virtual ICollection<Rating> RatingRatingFromNavigations { get; set; }
        [InverseProperty(nameof(Rating.RatingToNavigation))]
        public virtual ICollection<Rating> RatingRatingToNavigations { get; set; }
        [InverseProperty(nameof(ServiceRequest.ServiceProvider))]
        public virtual ICollection<ServiceRequest> ServiceRequestServiceProviders { get; set; }
        [InverseProperty(nameof(ServiceRequest.User))]
        public virtual ICollection<ServiceRequest> ServiceRequestUsers { get; set; }
        [InverseProperty(nameof(UserAddress.User))]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
