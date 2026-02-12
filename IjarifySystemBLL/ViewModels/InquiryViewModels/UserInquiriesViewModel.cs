using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.InquiryViewModels
{
    public class UserInquiriesViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<InquiryItemViewModel> Inquiries { get; set; } = new List<InquiryItemViewModel>();
        public int TotalCount => Inquiries.Count;
    }
}