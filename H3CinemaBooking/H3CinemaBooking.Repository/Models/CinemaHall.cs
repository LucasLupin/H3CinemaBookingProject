using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace H3CinemaBooking.Repository.Models
{
    public class CinemaHall
    {
        [Key]
        public int HallsID { get; set; }
        public int CinemaID { get; set; }
        public string HallName { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
