using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Models.DTO_s
{
    public class AdminUserDTO
    {
        [Key]
        public int AdminUserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
