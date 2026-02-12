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
        public string? PropertyImageUrl { get; set; }  //for property offer filter
        public string LocationName { get; set; } = null!;

        #region Computed
        public string StartDate => StartDateRaw.ToString("MMM d", System.Globalization.CultureInfo.InvariantCulture);
        public string EndDate => EndDateRaw.ToString("MMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);

        public int DurationInMonths
        {
            get
            {
                var Months= ((EndDateRaw.Year - StartDateRaw.Year) * 12) + EndDateRaw.Month - StartDateRaw.Month;
                return Months <= 0 ? 1 : Months;
            }
        }
        #endregion
    }
}
