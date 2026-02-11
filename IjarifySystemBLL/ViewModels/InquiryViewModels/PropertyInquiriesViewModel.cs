using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.InquiryViewModels
{
    public class PropertyInquiriesViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public List<InquiryItemViewModel> Inquiries { get; set; } = new List<InquiryItemViewModel>();
        public int TotalCount => Inquiries.Count;
    }
}