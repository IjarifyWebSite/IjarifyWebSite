using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.InquiryViewModels
{
    public class InquiryItemViewModel
    {
        public int InquiryId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyImage { get; set; }
        public string PropertyLocation { get; set; }
        public decimal PropertyPrice { get; set; }

        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }

        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public string PropertyOwnerName { get; set; }
        public int PropertyOwnerId { get; set; }
    }
}