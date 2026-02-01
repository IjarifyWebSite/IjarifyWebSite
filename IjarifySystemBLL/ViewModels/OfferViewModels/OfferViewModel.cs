using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.OfferViewModels
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartDateRaw { get; set; } 
        public DateTime EndDateRaw { get; set; } 
        public decimal DiscountPercentage { get; set; }
        public string PropertyTitle { get; set; } = null!;
        public string LocationName { get; set; } = null!;

        #region Computed
        public string StartDate => StartDateRaw.ToString("yyyy-MM-dd");
        public string EndDate => EndDateRaw.ToString("yyyy-MM-dd");
        #endregion
    }
}
