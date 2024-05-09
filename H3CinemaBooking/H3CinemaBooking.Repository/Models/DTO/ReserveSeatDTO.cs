using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Models.DTO
{
    public class ReserveSeatDTO
    {
        public ReserveSeatDTO() { }
        public int ShowID {  get; set; }

        public int UserID { get; set; }

        public double Price { get; set; }

        public List<SeatDTO>? SeatList { get; set;}

    }
}
