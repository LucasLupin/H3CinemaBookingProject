using H3CinemaBooking.Repository.DTO;
using H3CinemaBooking.Repository.Models;
//using H3CinemaBooking.Repository.Models.ManyModel;
using Microsoft.EntityFrameworkCore;

namespace H3CinemaBooking.Repository.Data
{
    public class Dbcontext : DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> options) : base(options)
        {

        }

        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<BookingSeat> BookingSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Movies)
                .WithMany(m => m.Genres)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenre",
                j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieID"),
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreID"));

            modelBuilder.Entity<CinemaHall>()
                .HasOne<Cinema>()
                .WithMany()
                .HasForeignKey(ch => ch.CinemaID)
                .IsRequired();

            modelBuilder.Entity<CinemaHall>()
                .HasMany(h => h.Seats)
                .WithOne(s => s.CinemaHall)
                .HasForeignKey(s => s.HallID);

            modelBuilder.Entity<Show>()
                .HasOne<Movie>()
                .WithMany()
                .HasForeignKey(s => s.MovieID)
                .IsRequired();

            modelBuilder.Entity<Show>()
                .HasOne<CinemaHall>()
                .WithMany()
                .HasForeignKey(s => s.HallID)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Costumer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CostumerID);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Show)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.ShowID)
                .IsRequired();

            modelBuilder.Entity<BookingSeat>()
                .HasOne(bs => bs.Booking)
                .WithMany(b => b.BookingSeats)
                .HasForeignKey(bs => bs.BookingID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookingSeat>()
                .HasOne(bs => bs.Seat)
                .WithMany(s => s.BookingSeats)
                .HasForeignKey(bs => bs.SeatID);

            modelBuilder.Entity<BookingSeat>()
                .Property(bs => bs.Status)
                .HasConversion<int>(); // Denne konvertere Status til Int i Databasen


            //Seeding Sheet - Her laver jeg test data til alle mine tabeller

            //Genre Sheet
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreID = 1, GenreName = "Action" }, new Genre { GenreID = 2, GenreName = "Sci-Fi" }, new Genre { GenreID = 3, GenreName = "Fantasy" });

            //Movie Sheet
            modelBuilder.Entity<Movie>().HasData(new Movie { MovieID = 1, Title = "James Bond 1", Duration = 123, Director = "Peter Hunt" }, new Movie { MovieID = 2, Title = "SuperMan 1", Duration = 133, Director = "Guy Hamilton" }, new Movie { MovieID = 3, Title = "SpiderMan 1", Duration = 126, Director = "Michael Apted" });

            //MovieGenre Sheet
          //  modelBuilder.Entity<MovieGenre>().HasData(new MovieGenre { MovieID = 1, GenreID = 1}, new MovieGenre { MovieID = 1, GenreID = 2 }, new MovieGenre { MovieID = 1, GenreID = 3 }, new MovieGenre { MovieID = 2, GenreID = 1 }, new MovieGenre { MovieID = 2, GenreID = 2 }, new MovieGenre { MovieID = 3, GenreID = 1 });

            //Cinema Sheet
            modelBuilder.Entity<Cinema>().HasData(new Cinema { CinemaID = 1, Name = "Palace", Location = "NørreBro", NumberOfHalls = 8 }, new Cinema { CinemaID = 2, Name = "Nordisk Biograf", Location = "Fields", NumberOfHalls = 12 }, new Cinema { CinemaID = 3, Name = "CineMAX", Location = "FiskeTorvet", NumberOfHalls = 6 });

            //CinemaHall Sheet
            modelBuilder.Entity<CinemaHall>().HasData(new CinemaHall { HallsID = 1, CinemaID = 1, HallName = "Sal 1" }, new CinemaHall { HallsID = 2, CinemaID = 2, HallName = "Sal 1" }, new CinemaHall { HallsID = 3, CinemaID = 3, HallName = "Sal 1" });

            //Seats Sheet

            var seats = new List<Seat>();
            var HallIds = new List<int> { 1, 2, 3 };
            int seatId = 1;

            foreach (var hallId in HallIds)
            {
                char row = 'A';
                int seatNumber = 1;

                for (int i = 1; i <= 200; i++)
                {
                    if (seatNumber > 20)  // hvis der er mere end 20 seat på en række går den til næste
                    {
                        seatNumber = 1;
                        row++;  //Den næste i Alphabet
                    }
                    seats.Add(new Seat { SeatID = seatId++, HallID = hallId, SeatRow = row, SeatNumber = seatNumber++ });
                }
            }
            modelBuilder.Entity<Seat>().HasData(seats.ToList());

            //Show Sheet
            DateTime showDateTime = new DateTime(2023, 11, 5, 19, 0, 0);
            modelBuilder.Entity<Show>().HasData(new Show { ShowID = 1, HallID = 1, MovieID = 1, Price = 125, ShowDateTime = showDateTime }, new Show { ShowID = 2, HallID = 2, MovieID = 2, Price = 110, ShowDateTime = showDateTime}, new Show { ShowID = 3, HallID = 3, MovieID = 3, Price = 100, ShowDateTime = showDateTime});

            // Costumer Sheet
            var customers = new List<Costumer>();
            var hashingService = new HashingService();
            var passwords = new List<string> { "Test123", "Test321", "Test0987654" };

            int customerId = 1;  // Start customer IDs from 1
            foreach (var password in passwords)
            {
                string salt = hashingService.GenerateSalt();
                string hash = hashingService.HashPassword(password, salt);

                customers.Add(new Costumer
                {
                    CostumerID = customerId++,
                    Name = "Lucas" + (customerId),
                    Email = $"test{customerId}@example.com",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    PhoneNumber = "12345789" + (customerId).ToString(),
                    IsActive = true
                });
            }

            modelBuilder.Entity<Costumer>().HasData(customers);

            //Adminuser Sheet
            var AdmPassword = "AdminErSej";
            var Admsalt = hashingService.GenerateSalt();
            var Admhash = hashingService.HashPassword(AdmPassword, Admsalt);

            modelBuilder.Entity<AdminUser>().HasData(new AdminUser { AdminUserID = 1, Name = "AdminGod", Email = "AdminTest@gmail.com", PasswordHash = Admhash, PasswordSalt = Admsalt});

            //Booking Sheet
            modelBuilder.Entity<Booking>().HasData(new Booking { BookingID = 1, ShowID = 1, CostumerID = 1, Price = 125, NumberOfSeats = 8, IsActive = true }, new Booking { BookingID = 2, ShowID = 2, CostumerID = 2, Price = 110, NumberOfSeats = 12, IsActive = true }, new Booking { BookingID = 3, ShowID = 3, CostumerID = 3, Price = 100, NumberOfSeats = 6, IsActive = true }); ;
            //BookingSeat Sheet

        }
    }
}
