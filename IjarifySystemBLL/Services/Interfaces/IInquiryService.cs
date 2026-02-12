using IjarifySystemBLL.ViewModels.InquiryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IInquiryService
    {
      
        UserInquiriesViewModel GetUserInquiries(int userId);

   
        PropertyInquiriesViewModel GetPropertyInquiries(int propertyId);

       
        InquiryDetailsViewModel? GetInquiryDetails(int inquiryId);

      
        bool CreateInquiry(CreateInquiryViewModel model);

     
        bool DeleteInquiry(int inquiryId, int userId);
    }
}