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

        public DbSet<UserDetail> Costumers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<BookingSeat> BookingSeats { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Area> Areas { get; set; }

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

            modelBuilder.Entity<Cinema>()
                .HasOne(c => c.Area)
                .WithMany(a => a.Cinemas)
                .HasForeignKey(c => c.AreaID); 

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

            //Area Sheet
            modelBuilder.Entity<Area>().HasData(new Area { AreaID = 1, AreaName = "StorKøbenhavn" }, new Area { AreaID = 2, AreaName = "Aalborg"}, new Area { AreaID = 3, AreaName = "Aarhus"}, new Area { AreaID = 4, AreaName = "Esbjerg"}, new Area { AreaID = 5, AreaName = "Frederikssund"}, new Area { AreaID = 6, AreaName = "Herning"}, new Area { AreaID = 7, AreaName = "Hillerød"}, new Area { AreaID = 8, AreaName = "Kolding"}, new Area { AreaID = 9, AreaName = "Køge"}, new Area { AreaID = 10, AreaName = "Nykøbing Falster"}, new Area { AreaID = 11, AreaName = "Næstved"}, new Area { AreaID = 12, AreaName = "Odense"}, new Area { AreaID = 13, AreaName = "Randers"}, new Area { AreaID = 14, AreaName = "Viborg"});

            //Cinema Sheet
            modelBuilder.Entity<Cinema>().HasData(new Cinema { CinemaID = 1, Name = "Palace", Location = "NørreBro", NumberOfHalls = 8, AreaID = 1 }, new Cinema { CinemaID = 2, Name = "Nordisk Biograf", Location = "Fields", NumberOfHalls = 12, AreaID = 1 }, new Cinema { CinemaID = 3, Name = "CineMAX", Location = "FiskeTorvet", NumberOfHalls = 6, AreaID = 1 }, new Cinema { CinemaID = 4, Name = "Aalborg City Syd", Location = "City Syd", NumberOfHalls = 6, AreaID = 2}, new Cinema { CinemaID = 5, Name = "Trøjborg", Location = "Aarhus", NumberOfHalls = 8, AreaID = 3});

            //CinemaHall Sheet
            modelBuilder.Entity<CinemaHall>().HasData(new CinemaHall { HallsID = 1, CinemaID = 1, HallName = "Sal 1" }, new CinemaHall { HallsID = 2, CinemaID = 2, HallName = "Sal 1" }, new CinemaHall { HallsID = 3, CinemaID = 3, HallName = "Sal 1" }, new CinemaHall { HallsID = 4, CinemaID = 4, HallName = "Sal 1"}, new CinemaHall { HallsID = 5, CinemaID = 5, HallName = "Sal 1"});

            //Seats Sheet

            var seats = new List<Seat>();
            var HallIds = new List<int> { 1, 2, 3, 4, 5 };
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
            DateTime showDateTime = new DateTime(2023, 11, 5, 19, 0, 0); // 2023 11/5 19:00:00
            DateTime showDateTime1 = new DateTime(2024, 20, 4, 19, 0, 0); // 2024 20/4 19:00:00
            DateTime showDateTime2 = new DateTime(2024, 20, 4, 15, 0, 0); // 2024 20/4 15:00:00
            DateTime showDateTime3 = new DateTime(2024, 20, 4, 11, 30, 0); // 2024 20/4 11:30:00
            modelBuilder.Entity<Show>().HasData(new Show { ShowID = 1, HallID = 1, MovieID = 1, Price = 125, ShowDateTime = showDateTime }, new Show { ShowID = 2, HallID = 2, MovieID = 2, Price = 110, ShowDateTime = showDateTime1}, new Show { ShowID = 3, HallID = 3, MovieID = 3, Price = 100, ShowDateTime = showDateTime2}, new Show { ShowID = 4, HallID = 4, MovieID = 3, Price = 100, ShowDateTime = showDateTime3}, new Show { ShowID = 5, HallID = 5, MovieID = 3, Price = 100, ShowDateTime = showDateTime1});

            //Role Sheet
            modelBuilder.Entity<Roles>().HasData(new Roles { RoleID = 1, RoleName = "Costumer" }, new Roles { RoleID = 2, RoleName = "Admin" });

            // UserDetail Sheet
            var UserDetails = new List<UserDetail>();
            var hashingService = new HashingService();
            var passwords = new List<string> { "Test123", "Test321", "Test0987654" };
            
            int UserDetailID = 1;  // Start UserDetail IDs from 1
            
            string AdmSalt = hashingService.GenerateSalt();
            string AdmHash = hashingService.HashPassword(passwords[1], AdmSalt);

            modelBuilder.Entity<UserDetail>().HasData(new UserDetail { UserDetailID = 10, Name = "AdminGod", Email = "TestAdmin@gmail.com", PasswordHash = AdmHash, PasswordSalt = AdmSalt, PhoneNumber = "56895423", RoleID = 2, IsActive = true });
            
            foreach (var password in passwords)
            {
                string salt = hashingService.GenerateSalt();
                string hash = hashingService.HashPassword(password, salt);

                UserDetails.Add(new UserDetail
                {
                    UserDetailID = UserDetailID++,
                    Name = "Lucas" + (UserDetailID),
                    Email = $"test{UserDetailID}@example.com",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    PhoneNumber = "12345789" + (UserDetailID).ToString(),
                    RoleID = 1,
                    IsActive = true
                });
            }

            modelBuilder.Entity<UserDetail>().HasData(UserDetails);
            
            //Booking Sheet
            modelBuilder.Entity<Booking>().HasData(new Booking { BookingID = 1, ShowID = 1, CostumerID = 1, Price = 125, NumberOfSeats = 8, IsActive = true }, new Booking { BookingID = 2, ShowID = 2, CostumerID = 2, Price = 110, NumberOfSeats = 12, IsActive = true }, new Booking { BookingID = 3, ShowID = 3, CostumerID = 3, Price = 100, NumberOfSeats = 6, IsActive = true }); ;

            //BookingSeat Sheet

        }
    }
}
