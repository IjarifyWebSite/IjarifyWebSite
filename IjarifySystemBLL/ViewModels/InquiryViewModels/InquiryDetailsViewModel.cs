using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.InquiryViewModels
{
    public class InquiryDetailsViewModel
    {
        public int InquiryId { get; set; }

        // Property Info
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyImage { get; set; }
        public string PropertyLocation { get; set; }
        public decimal PropertyPrice { get; set; }

        // Sender Info
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }

        // Property Owner Info
        public int PropertyOwnerId { get; set; }
        public string PropertyOwnerName { get; set; }
        public string PropertyOwnerEmail { get; set; }
        public string PropertyOwnerPhone { get; set; }

        // Inquiry Details
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}