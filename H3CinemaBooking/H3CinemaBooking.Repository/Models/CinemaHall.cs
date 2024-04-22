using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H3CinemaBooking.Repository.Models
{
    public class CinemaHall
    {
        [Key]
        public int HallsID { get; set; }
        public string HallName { get; set; }
        [ForeignKey("CinemaID")]
        public int CinemaID { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
