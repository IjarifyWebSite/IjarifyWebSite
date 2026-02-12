using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.HomeViewModels
{
    public class HomeViewModel
   
      
        {
            public List<LocationCardViewModel> TopLocations { get; set; } = new List<LocationCardViewModel>();

            
            // public List<PropertyCardViewModel> FeaturedProperties { get; set; } = new List<PropertyCardViewModel>();
            // public int TotalProperties { get; set; }
            // public int TotalAgents { get; set; }
        }

        
    
        public class LocationCardViewModel
        {
            public int Id { get; set; }
            public string City { get; set; } = null!;
            public string Region { get; set; } = null!;
            public string ImageUrl { get; set; } = null!;
            public int PropertyCount { get; set; }
        }
    }

