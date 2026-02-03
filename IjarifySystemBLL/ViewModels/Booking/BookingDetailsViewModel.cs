using IjarifySystemBLL.ViewModels.Booking;
using IjarifySystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.Booking
{

    public class BookingDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreatedAt { get; set; }

        // Property Details
        public int PropertyID { get; set; }
        public string PropertyTitle { get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public PropertyType? PropertyType { get; set; }

        // User Details
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        // Computed Properties
        public int TotalNights => (Check_Out - Check_In).Days;

        public string StatusBadgeClass => Status switch
        {
            BookingStatus.Pending => "bg-warning text-dark",
            BookingStatus.Approved => "bg-success",
            BookingStatus.Rejected => "bg-danger",
            _ => "bg-secondary"
        };

        public string PropertyTypeDisplay
        {
            get
            {
                if (PropertyType == null)
                    return "Unknown";

                switch (PropertyType.Value)
                {
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Apartment:
                        return "Apartment";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Villa:
                        return "Villa";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Studio:
                        return "Studio";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Townhouse:
                        return "Townhouse";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Office:
                        return "Office";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Clinic:
                        return "Clinic";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Shop:
                        return "Shop";
                    default:
                        return "Unknown";
                }
            }
        }

        public string PropertyTypeIcon
        {
            get
            {
                if (PropertyType == null)
                    return "fas fa-question-circle";

                switch (PropertyType.Value)
                {
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Apartment:
                        return "fas fa-building";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Villa:
                        return "fas fa-hotel";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Studio:
                        return "fas fa-door-open";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Townhouse:
                        return "fas fa-home";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Office:
                        return "fas fa-briefcase";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Clinic:
                        return "fas fa-clinic-medical";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Shop:
                        return "fas fa-store";
                    default:
                        return "fas fa-question-circle";
                }
            }
        }

        public string PropertyTypeBadgeClass
        {
            get
            {
                if (PropertyType == null)
                    return "bg-light text-dark";

                switch (PropertyType.Value)
                {
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Apartment:
                        return "bg-primary";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Villa:
                        return "bg-success";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Studio:
                        return "bg-info";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Townhouse:
                        return "bg-warning text-dark";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Office:
                        return "bg-secondary";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Clinic:
                        return "bg-danger";
                    case IjarifySystemDAL.Entities.Enums.PropertyType.Shop:
                        return "bg-dark";
                    default:
                        return "bg-light text-dark";
                }
            }
        }
    }


}
