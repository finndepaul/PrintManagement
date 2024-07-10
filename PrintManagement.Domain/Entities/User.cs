using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid TeamId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTIme { get; set; }
        public bool IsActive { get; set; }

        // khóa ngoại
        public virtual List<Notification> Notifications { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual Team Team { get; set; }
        public virtual List<Design> Designs { get; set; }
        public virtual List<ImportCoupon> ImportCoupons { get; set;}
        public virtual List<Project> Projects { get; set; }
        public virtual List<KeyPerformanceIndicators> KeyPerformanceIndicators { get; set;}
        public virtual List<Permissions> Permissions { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
        public virtual List<ConfirmEmail> ConfirmEmails { get; set; }
    }
}
