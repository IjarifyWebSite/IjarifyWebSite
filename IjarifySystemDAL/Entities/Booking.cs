using IjarifySystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Booking:BaseEntity
    {
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public BookingStatus Status { get; set; }
        public bool IsValid
        {
            get
            {
                if(Check_Out<DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public decimal TotalPrice { get; set; }

        #region Booking-Property
        public int PropertyID { get; set; } 
        public Property Property { get; set; } = null!;
        #endregion

        #region Booking-User
        public int UserID { get; set; }    
        public User user { get; set; } = null!;
        #endregion
    }
}
