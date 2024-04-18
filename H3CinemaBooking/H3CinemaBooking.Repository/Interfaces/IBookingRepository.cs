﻿using H3CinemaBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3CinemaBooking.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Booking Create(Booking booking);
        Booking GetById(int id);
        List<Booking> GetAll();
        void DeleteByID(int id);
    }
}
