using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace H3CinemaBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class CinemaBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    CinemaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfHalls = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.CinemaID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    UserDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.UserDetailID);
                });

            migrationBuilder.CreateTable(
                name: "CinemaHalls",
                columns: table => new
                {
                    HallsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinemaID = table.Column<int>(type: "int", nullable: false),
                    HallName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHalls", x => x.HallsID);
                    table.ForeignKey(
                        name: "FK_CinemaHalls_Cinemas_CinemaID",
                        column: x => x.CinemaID,
                        principalTable: "Cinemas",
                        principalColumn: "CinemaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.GenreID, x.MovieID });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallID = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    SeatRow = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_Seats_CinemaHalls_HallID",
                        column: x => x.HallID,
                        principalTable: "CinemaHalls",
                        principalColumn: "HallsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ShowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ShowDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ShowID);
                    table.ForeignKey(
                        name: "FK_Shows_CinemaHalls_HallID",
                        column: x => x.HallID,
                        principalTable: "CinemaHalls",
                        principalColumn: "HallsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowID = table.Column<int>(type: "int", nullable: false),
                    CostumerID = table.Column<int>(type: "int", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Shows_ShowID",
                        column: x => x.ShowID,
                        principalTable: "Shows",
                        principalColumn: "ShowID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_UserDetail_CostumerID",
                        column: x => x.CostumerID,
                        principalTable: "UserDetail",
                        principalColumn: "UserDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingSeats",
                columns: table => new
                {
                    BookingSeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSeats", x => x.BookingSeatID);
                    table.ForeignKey(
                        name: "FK_BookingSeats_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingSeats_Seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "CinemaID", "Location", "Name", "NumberOfHalls" },
                values: new object[,]
                {
                    { 1, "NørreBro", "Palace", 8 },
                    { 2, "Fields", "Nordisk Biograf", 12 },
                    { 3, "FiskeTorvet", "CineMAX", 6 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreID", "GenreName" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Sci-Fi" },
                    { 3, "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieID", "Director", "Duration", "Title" },
                values: new object[,]
                {
                    { 1, "Peter Hunt", 123, "James Bond 1" },
                    { 2, "Guy Hamilton", 133, "SuperMan 1" },
                    { 3, "Michael Apted", 126, "SpiderMan 1" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Costumer" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "UserDetail",
                columns: new[] { "UserDetailID", "Email", "IsActive", "Name", "PasswordHash", "PasswordSalt", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { 1, "test2@example.com", true, "Lucas2", "LDTj4fL+murijIX4i0e7kw==", "rHsgyk01D3muGG8TQ5n15A==", "123457892", 1 },
                    { 2, "test3@example.com", true, "Lucas3", "4PqVVu+vw9SexUovyaJ6Pg==", "JoWUUBn2EkYp1ew6PttyLw==", "123457893", 1 },
                    { 3, "test4@example.com", true, "Lucas4", "72uEcJT5Tqd0lUU+2tKV2w==", "4tSDtPLAzPFczUEP1uQPaA==", "123457894", 1 },
                    { 10, "TestAdmin@gmail.com", true, "AdminGod", "sgovuM0XNqkIo7dVGhUVlA==", "V/ZdJB3yF9OE6iOPgs50tg==", "56895423", 2 }
                });

            migrationBuilder.InsertData(
                table: "CinemaHalls",
                columns: new[] { "HallsID", "CinemaID", "HallName" },
                values: new object[,]
                {
                    { 1, 1, "Sal 1" },
                    { 2, 2, "Sal 1" },
                    { 3, 3, "Sal 1" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatID", "HallID", "SeatNumber", "SeatRow" },
                values: new object[,]
                {
                    { 1, 1, 1, "A" },
                    { 2, 1, 2, "A" },
                    { 3, 1, 3, "A" },
                    { 4, 1, 4, "A" },
                    { 5, 1, 5, "A" },
                    { 6, 1, 6, "A" },
                    { 7, 1, 7, "A" },
                    { 8, 1, 8, "A" },
                    { 9, 1, 9, "A" },
                    { 10, 1, 10, "A" },
                    { 11, 1, 11, "A" },
                    { 12, 1, 12, "A" },
                    { 13, 1, 13, "A" },
                    { 14, 1, 14, "A" },
                    { 15, 1, 15, "A" },
                    { 16, 1, 16, "A" },
                    { 17, 1, 17, "A" },
                    { 18, 1, 18, "A" },
                    { 19, 1, 19, "A" },
                    { 20, 1, 20, "A" },
                    { 21, 1, 1, "B" },
                    { 22, 1, 2, "B" },
                    { 23, 1, 3, "B" },
                    { 24, 1, 4, "B" },
                    { 25, 1, 5, "B" },
                    { 26, 1, 6, "B" },
                    { 27, 1, 7, "B" },
                    { 28, 1, 8, "B" },
                    { 29, 1, 9, "B" },
                    { 30, 1, 10, "B" },
                    { 31, 1, 11, "B" },
                    { 32, 1, 12, "B" },
                    { 33, 1, 13, "B" },
                    { 34, 1, 14, "B" },
                    { 35, 1, 15, "B" },
                    { 36, 1, 16, "B" },
                    { 37, 1, 17, "B" },
                    { 38, 1, 18, "B" },
                    { 39, 1, 19, "B" },
                    { 40, 1, 20, "B" },
                    { 41, 1, 1, "C" },
                    { 42, 1, 2, "C" },
                    { 43, 1, 3, "C" },
                    { 44, 1, 4, "C" },
                    { 45, 1, 5, "C" },
                    { 46, 1, 6, "C" },
                    { 47, 1, 7, "C" },
                    { 48, 1, 8, "C" },
                    { 49, 1, 9, "C" },
                    { 50, 1, 10, "C" },
                    { 51, 1, 11, "C" },
                    { 52, 1, 12, "C" },
                    { 53, 1, 13, "C" },
                    { 54, 1, 14, "C" },
                    { 55, 1, 15, "C" },
                    { 56, 1, 16, "C" },
                    { 57, 1, 17, "C" },
                    { 58, 1, 18, "C" },
                    { 59, 1, 19, "C" },
                    { 60, 1, 20, "C" },
                    { 61, 1, 1, "D" },
                    { 62, 1, 2, "D" },
                    { 63, 1, 3, "D" },
                    { 64, 1, 4, "D" },
                    { 65, 1, 5, "D" },
                    { 66, 1, 6, "D" },
                    { 67, 1, 7, "D" },
                    { 68, 1, 8, "D" },
                    { 69, 1, 9, "D" },
                    { 70, 1, 10, "D" },
                    { 71, 1, 11, "D" },
                    { 72, 1, 12, "D" },
                    { 73, 1, 13, "D" },
                    { 74, 1, 14, "D" },
                    { 75, 1, 15, "D" },
                    { 76, 1, 16, "D" },
                    { 77, 1, 17, "D" },
                    { 78, 1, 18, "D" },
                    { 79, 1, 19, "D" },
                    { 80, 1, 20, "D" },
                    { 81, 1, 1, "E" },
                    { 82, 1, 2, "E" },
                    { 83, 1, 3, "E" },
                    { 84, 1, 4, "E" },
                    { 85, 1, 5, "E" },
                    { 86, 1, 6, "E" },
                    { 87, 1, 7, "E" },
                    { 88, 1, 8, "E" },
                    { 89, 1, 9, "E" },
                    { 90, 1, 10, "E" },
                    { 91, 1, 11, "E" },
                    { 92, 1, 12, "E" },
                    { 93, 1, 13, "E" },
                    { 94, 1, 14, "E" },
                    { 95, 1, 15, "E" },
                    { 96, 1, 16, "E" },
                    { 97, 1, 17, "E" },
                    { 98, 1, 18, "E" },
                    { 99, 1, 19, "E" },
                    { 100, 1, 20, "E" },
                    { 101, 1, 1, "F" },
                    { 102, 1, 2, "F" },
                    { 103, 1, 3, "F" },
                    { 104, 1, 4, "F" },
                    { 105, 1, 5, "F" },
                    { 106, 1, 6, "F" },
                    { 107, 1, 7, "F" },
                    { 108, 1, 8, "F" },
                    { 109, 1, 9, "F" },
                    { 110, 1, 10, "F" },
                    { 111, 1, 11, "F" },
                    { 112, 1, 12, "F" },
                    { 113, 1, 13, "F" },
                    { 114, 1, 14, "F" },
                    { 115, 1, 15, "F" },
                    { 116, 1, 16, "F" },
                    { 117, 1, 17, "F" },
                    { 118, 1, 18, "F" },
                    { 119, 1, 19, "F" },
                    { 120, 1, 20, "F" },
                    { 121, 1, 1, "G" },
                    { 122, 1, 2, "G" },
                    { 123, 1, 3, "G" },
                    { 124, 1, 4, "G" },
                    { 125, 1, 5, "G" },
                    { 126, 1, 6, "G" },
                    { 127, 1, 7, "G" },
                    { 128, 1, 8, "G" },
                    { 129, 1, 9, "G" },
                    { 130, 1, 10, "G" },
                    { 131, 1, 11, "G" },
                    { 132, 1, 12, "G" },
                    { 133, 1, 13, "G" },
                    { 134, 1, 14, "G" },
                    { 135, 1, 15, "G" },
                    { 136, 1, 16, "G" },
                    { 137, 1, 17, "G" },
                    { 138, 1, 18, "G" },
                    { 139, 1, 19, "G" },
                    { 140, 1, 20, "G" },
                    { 141, 1, 1, "H" },
                    { 142, 1, 2, "H" },
                    { 143, 1, 3, "H" },
                    { 144, 1, 4, "H" },
                    { 145, 1, 5, "H" },
                    { 146, 1, 6, "H" },
                    { 147, 1, 7, "H" },
                    { 148, 1, 8, "H" },
                    { 149, 1, 9, "H" },
                    { 150, 1, 10, "H" },
                    { 151, 1, 11, "H" },
                    { 152, 1, 12, "H" },
                    { 153, 1, 13, "H" },
                    { 154, 1, 14, "H" },
                    { 155, 1, 15, "H" },
                    { 156, 1, 16, "H" },
                    { 157, 1, 17, "H" },
                    { 158, 1, 18, "H" },
                    { 159, 1, 19, "H" },
                    { 160, 1, 20, "H" },
                    { 161, 1, 1, "I" },
                    { 162, 1, 2, "I" },
                    { 163, 1, 3, "I" },
                    { 164, 1, 4, "I" },
                    { 165, 1, 5, "I" },
                    { 166, 1, 6, "I" },
                    { 167, 1, 7, "I" },
                    { 168, 1, 8, "I" },
                    { 169, 1, 9, "I" },
                    { 170, 1, 10, "I" },
                    { 171, 1, 11, "I" },
                    { 172, 1, 12, "I" },
                    { 173, 1, 13, "I" },
                    { 174, 1, 14, "I" },
                    { 175, 1, 15, "I" },
                    { 176, 1, 16, "I" },
                    { 177, 1, 17, "I" },
                    { 178, 1, 18, "I" },
                    { 179, 1, 19, "I" },
                    { 180, 1, 20, "I" },
                    { 181, 1, 1, "J" },
                    { 182, 1, 2, "J" },
                    { 183, 1, 3, "J" },
                    { 184, 1, 4, "J" },
                    { 185, 1, 5, "J" },
                    { 186, 1, 6, "J" },
                    { 187, 1, 7, "J" },
                    { 188, 1, 8, "J" },
                    { 189, 1, 9, "J" },
                    { 190, 1, 10, "J" },
                    { 191, 1, 11, "J" },
                    { 192, 1, 12, "J" },
                    { 193, 1, 13, "J" },
                    { 194, 1, 14, "J" },
                    { 195, 1, 15, "J" },
                    { 196, 1, 16, "J" },
                    { 197, 1, 17, "J" },
                    { 198, 1, 18, "J" },
                    { 199, 1, 19, "J" },
                    { 200, 1, 20, "J" },
                    { 201, 2, 1, "A" },
                    { 202, 2, 2, "A" },
                    { 203, 2, 3, "A" },
                    { 204, 2, 4, "A" },
                    { 205, 2, 5, "A" },
                    { 206, 2, 6, "A" },
                    { 207, 2, 7, "A" },
                    { 208, 2, 8, "A" },
                    { 209, 2, 9, "A" },
                    { 210, 2, 10, "A" },
                    { 211, 2, 11, "A" },
                    { 212, 2, 12, "A" },
                    { 213, 2, 13, "A" },
                    { 214, 2, 14, "A" },
                    { 215, 2, 15, "A" },
                    { 216, 2, 16, "A" },
                    { 217, 2, 17, "A" },
                    { 218, 2, 18, "A" },
                    { 219, 2, 19, "A" },
                    { 220, 2, 20, "A" },
                    { 221, 2, 1, "B" },
                    { 222, 2, 2, "B" },
                    { 223, 2, 3, "B" },
                    { 224, 2, 4, "B" },
                    { 225, 2, 5, "B" },
                    { 226, 2, 6, "B" },
                    { 227, 2, 7, "B" },
                    { 228, 2, 8, "B" },
                    { 229, 2, 9, "B" },
                    { 230, 2, 10, "B" },
                    { 231, 2, 11, "B" },
                    { 232, 2, 12, "B" },
                    { 233, 2, 13, "B" },
                    { 234, 2, 14, "B" },
                    { 235, 2, 15, "B" },
                    { 236, 2, 16, "B" },
                    { 237, 2, 17, "B" },
                    { 238, 2, 18, "B" },
                    { 239, 2, 19, "B" },
                    { 240, 2, 20, "B" },
                    { 241, 2, 1, "C" },
                    { 242, 2, 2, "C" },
                    { 243, 2, 3, "C" },
                    { 244, 2, 4, "C" },
                    { 245, 2, 5, "C" },
                    { 246, 2, 6, "C" },
                    { 247, 2, 7, "C" },
                    { 248, 2, 8, "C" },
                    { 249, 2, 9, "C" },
                    { 250, 2, 10, "C" },
                    { 251, 2, 11, "C" },
                    { 252, 2, 12, "C" },
                    { 253, 2, 13, "C" },
                    { 254, 2, 14, "C" },
                    { 255, 2, 15, "C" },
                    { 256, 2, 16, "C" },
                    { 257, 2, 17, "C" },
                    { 258, 2, 18, "C" },
                    { 259, 2, 19, "C" },
                    { 260, 2, 20, "C" },
                    { 261, 2, 1, "D" },
                    { 262, 2, 2, "D" },
                    { 263, 2, 3, "D" },
                    { 264, 2, 4, "D" },
                    { 265, 2, 5, "D" },
                    { 266, 2, 6, "D" },
                    { 267, 2, 7, "D" },
                    { 268, 2, 8, "D" },
                    { 269, 2, 9, "D" },
                    { 270, 2, 10, "D" },
                    { 271, 2, 11, "D" },
                    { 272, 2, 12, "D" },
                    { 273, 2, 13, "D" },
                    { 274, 2, 14, "D" },
                    { 275, 2, 15, "D" },
                    { 276, 2, 16, "D" },
                    { 277, 2, 17, "D" },
                    { 278, 2, 18, "D" },
                    { 279, 2, 19, "D" },
                    { 280, 2, 20, "D" },
                    { 281, 2, 1, "E" },
                    { 282, 2, 2, "E" },
                    { 283, 2, 3, "E" },
                    { 284, 2, 4, "E" },
                    { 285, 2, 5, "E" },
                    { 286, 2, 6, "E" },
                    { 287, 2, 7, "E" },
                    { 288, 2, 8, "E" },
                    { 289, 2, 9, "E" },
                    { 290, 2, 10, "E" },
                    { 291, 2, 11, "E" },
                    { 292, 2, 12, "E" },
                    { 293, 2, 13, "E" },
                    { 294, 2, 14, "E" },
                    { 295, 2, 15, "E" },
                    { 296, 2, 16, "E" },
                    { 297, 2, 17, "E" },
                    { 298, 2, 18, "E" },
                    { 299, 2, 19, "E" },
                    { 300, 2, 20, "E" },
                    { 301, 2, 1, "F" },
                    { 302, 2, 2, "F" },
                    { 303, 2, 3, "F" },
                    { 304, 2, 4, "F" },
                    { 305, 2, 5, "F" },
                    { 306, 2, 6, "F" },
                    { 307, 2, 7, "F" },
                    { 308, 2, 8, "F" },
                    { 309, 2, 9, "F" },
                    { 310, 2, 10, "F" },
                    { 311, 2, 11, "F" },
                    { 312, 2, 12, "F" },
                    { 313, 2, 13, "F" },
                    { 314, 2, 14, "F" },
                    { 315, 2, 15, "F" },
                    { 316, 2, 16, "F" },
                    { 317, 2, 17, "F" },
                    { 318, 2, 18, "F" },
                    { 319, 2, 19, "F" },
                    { 320, 2, 20, "F" },
                    { 321, 2, 1, "G" },
                    { 322, 2, 2, "G" },
                    { 323, 2, 3, "G" },
                    { 324, 2, 4, "G" },
                    { 325, 2, 5, "G" },
                    { 326, 2, 6, "G" },
                    { 327, 2, 7, "G" },
                    { 328, 2, 8, "G" },
                    { 329, 2, 9, "G" },
                    { 330, 2, 10, "G" },
                    { 331, 2, 11, "G" },
                    { 332, 2, 12, "G" },
                    { 333, 2, 13, "G" },
                    { 334, 2, 14, "G" },
                    { 335, 2, 15, "G" },
                    { 336, 2, 16, "G" },
                    { 337, 2, 17, "G" },
                    { 338, 2, 18, "G" },
                    { 339, 2, 19, "G" },
                    { 340, 2, 20, "G" },
                    { 341, 2, 1, "H" },
                    { 342, 2, 2, "H" },
                    { 343, 2, 3, "H" },
                    { 344, 2, 4, "H" },
                    { 345, 2, 5, "H" },
                    { 346, 2, 6, "H" },
                    { 347, 2, 7, "H" },
                    { 348, 2, 8, "H" },
                    { 349, 2, 9, "H" },
                    { 350, 2, 10, "H" },
                    { 351, 2, 11, "H" },
                    { 352, 2, 12, "H" },
                    { 353, 2, 13, "H" },
                    { 354, 2, 14, "H" },
                    { 355, 2, 15, "H" },
                    { 356, 2, 16, "H" },
                    { 357, 2, 17, "H" },
                    { 358, 2, 18, "H" },
                    { 359, 2, 19, "H" },
                    { 360, 2, 20, "H" },
                    { 361, 2, 1, "I" },
                    { 362, 2, 2, "I" },
                    { 363, 2, 3, "I" },
                    { 364, 2, 4, "I" },
                    { 365, 2, 5, "I" },
                    { 366, 2, 6, "I" },
                    { 367, 2, 7, "I" },
                    { 368, 2, 8, "I" },
                    { 369, 2, 9, "I" },
                    { 370, 2, 10, "I" },
                    { 371, 2, 11, "I" },
                    { 372, 2, 12, "I" },
                    { 373, 2, 13, "I" },
                    { 374, 2, 14, "I" },
                    { 375, 2, 15, "I" },
                    { 376, 2, 16, "I" },
                    { 377, 2, 17, "I" },
                    { 378, 2, 18, "I" },
                    { 379, 2, 19, "I" },
                    { 380, 2, 20, "I" },
                    { 381, 2, 1, "J" },
                    { 382, 2, 2, "J" },
                    { 383, 2, 3, "J" },
                    { 384, 2, 4, "J" },
                    { 385, 2, 5, "J" },
                    { 386, 2, 6, "J" },
                    { 387, 2, 7, "J" },
                    { 388, 2, 8, "J" },
                    { 389, 2, 9, "J" },
                    { 390, 2, 10, "J" },
                    { 391, 2, 11, "J" },
                    { 392, 2, 12, "J" },
                    { 393, 2, 13, "J" },
                    { 394, 2, 14, "J" },
                    { 395, 2, 15, "J" },
                    { 396, 2, 16, "J" },
                    { 397, 2, 17, "J" },
                    { 398, 2, 18, "J" },
                    { 399, 2, 19, "J" },
                    { 400, 2, 20, "J" },
                    { 401, 3, 1, "A" },
                    { 402, 3, 2, "A" },
                    { 403, 3, 3, "A" },
                    { 404, 3, 4, "A" },
                    { 405, 3, 5, "A" },
                    { 406, 3, 6, "A" },
                    { 407, 3, 7, "A" },
                    { 408, 3, 8, "A" },
                    { 409, 3, 9, "A" },
                    { 410, 3, 10, "A" },
                    { 411, 3, 11, "A" },
                    { 412, 3, 12, "A" },
                    { 413, 3, 13, "A" },
                    { 414, 3, 14, "A" },
                    { 415, 3, 15, "A" },
                    { 416, 3, 16, "A" },
                    { 417, 3, 17, "A" },
                    { 418, 3, 18, "A" },
                    { 419, 3, 19, "A" },
                    { 420, 3, 20, "A" },
                    { 421, 3, 1, "B" },
                    { 422, 3, 2, "B" },
                    { 423, 3, 3, "B" },
                    { 424, 3, 4, "B" },
                    { 425, 3, 5, "B" },
                    { 426, 3, 6, "B" },
                    { 427, 3, 7, "B" },
                    { 428, 3, 8, "B" },
                    { 429, 3, 9, "B" },
                    { 430, 3, 10, "B" },
                    { 431, 3, 11, "B" },
                    { 432, 3, 12, "B" },
                    { 433, 3, 13, "B" },
                    { 434, 3, 14, "B" },
                    { 435, 3, 15, "B" },
                    { 436, 3, 16, "B" },
                    { 437, 3, 17, "B" },
                    { 438, 3, 18, "B" },
                    { 439, 3, 19, "B" },
                    { 440, 3, 20, "B" },
                    { 441, 3, 1, "C" },
                    { 442, 3, 2, "C" },
                    { 443, 3, 3, "C" },
                    { 444, 3, 4, "C" },
                    { 445, 3, 5, "C" },
                    { 446, 3, 6, "C" },
                    { 447, 3, 7, "C" },
                    { 448, 3, 8, "C" },
                    { 449, 3, 9, "C" },
                    { 450, 3, 10, "C" },
                    { 451, 3, 11, "C" },
                    { 452, 3, 12, "C" },
                    { 453, 3, 13, "C" },
                    { 454, 3, 14, "C" },
                    { 455, 3, 15, "C" },
                    { 456, 3, 16, "C" },
                    { 457, 3, 17, "C" },
                    { 458, 3, 18, "C" },
                    { 459, 3, 19, "C" },
                    { 460, 3, 20, "C" },
                    { 461, 3, 1, "D" },
                    { 462, 3, 2, "D" },
                    { 463, 3, 3, "D" },
                    { 464, 3, 4, "D" },
                    { 465, 3, 5, "D" },
                    { 466, 3, 6, "D" },
                    { 467, 3, 7, "D" },
                    { 468, 3, 8, "D" },
                    { 469, 3, 9, "D" },
                    { 470, 3, 10, "D" },
                    { 471, 3, 11, "D" },
                    { 472, 3, 12, "D" },
                    { 473, 3, 13, "D" },
                    { 474, 3, 14, "D" },
                    { 475, 3, 15, "D" },
                    { 476, 3, 16, "D" },
                    { 477, 3, 17, "D" },
                    { 478, 3, 18, "D" },
                    { 479, 3, 19, "D" },
                    { 480, 3, 20, "D" },
                    { 481, 3, 1, "E" },
                    { 482, 3, 2, "E" },
                    { 483, 3, 3, "E" },
                    { 484, 3, 4, "E" },
                    { 485, 3, 5, "E" },
                    { 486, 3, 6, "E" },
                    { 487, 3, 7, "E" },
                    { 488, 3, 8, "E" },
                    { 489, 3, 9, "E" },
                    { 490, 3, 10, "E" },
                    { 491, 3, 11, "E" },
                    { 492, 3, 12, "E" },
                    { 493, 3, 13, "E" },
                    { 494, 3, 14, "E" },
                    { 495, 3, 15, "E" },
                    { 496, 3, 16, "E" },
                    { 497, 3, 17, "E" },
                    { 498, 3, 18, "E" },
                    { 499, 3, 19, "E" },
                    { 500, 3, 20, "E" },
                    { 501, 3, 1, "F" },
                    { 502, 3, 2, "F" },
                    { 503, 3, 3, "F" },
                    { 504, 3, 4, "F" },
                    { 505, 3, 5, "F" },
                    { 506, 3, 6, "F" },
                    { 507, 3, 7, "F" },
                    { 508, 3, 8, "F" },
                    { 509, 3, 9, "F" },
                    { 510, 3, 10, "F" },
                    { 511, 3, 11, "F" },
                    { 512, 3, 12, "F" },
                    { 513, 3, 13, "F" },
                    { 514, 3, 14, "F" },
                    { 515, 3, 15, "F" },
                    { 516, 3, 16, "F" },
                    { 517, 3, 17, "F" },
                    { 518, 3, 18, "F" },
                    { 519, 3, 19, "F" },
                    { 520, 3, 20, "F" },
                    { 521, 3, 1, "G" },
                    { 522, 3, 2, "G" },
                    { 523, 3, 3, "G" },
                    { 524, 3, 4, "G" },
                    { 525, 3, 5, "G" },
                    { 526, 3, 6, "G" },
                    { 527, 3, 7, "G" },
                    { 528, 3, 8, "G" },
                    { 529, 3, 9, "G" },
                    { 530, 3, 10, "G" },
                    { 531, 3, 11, "G" },
                    { 532, 3, 12, "G" },
                    { 533, 3, 13, "G" },
                    { 534, 3, 14, "G" },
                    { 535, 3, 15, "G" },
                    { 536, 3, 16, "G" },
                    { 537, 3, 17, "G" },
                    { 538, 3, 18, "G" },
                    { 539, 3, 19, "G" },
                    { 540, 3, 20, "G" },
                    { 541, 3, 1, "H" },
                    { 542, 3, 2, "H" },
                    { 543, 3, 3, "H" },
                    { 544, 3, 4, "H" },
                    { 545, 3, 5, "H" },
                    { 546, 3, 6, "H" },
                    { 547, 3, 7, "H" },
                    { 548, 3, 8, "H" },
                    { 549, 3, 9, "H" },
                    { 550, 3, 10, "H" },
                    { 551, 3, 11, "H" },
                    { 552, 3, 12, "H" },
                    { 553, 3, 13, "H" },
                    { 554, 3, 14, "H" },
                    { 555, 3, 15, "H" },
                    { 556, 3, 16, "H" },
                    { 557, 3, 17, "H" },
                    { 558, 3, 18, "H" },
                    { 559, 3, 19, "H" },
                    { 560, 3, 20, "H" },
                    { 561, 3, 1, "I" },
                    { 562, 3, 2, "I" },
                    { 563, 3, 3, "I" },
                    { 564, 3, 4, "I" },
                    { 565, 3, 5, "I" },
                    { 566, 3, 6, "I" },
                    { 567, 3, 7, "I" },
                    { 568, 3, 8, "I" },
                    { 569, 3, 9, "I" },
                    { 570, 3, 10, "I" },
                    { 571, 3, 11, "I" },
                    { 572, 3, 12, "I" },
                    { 573, 3, 13, "I" },
                    { 574, 3, 14, "I" },
                    { 575, 3, 15, "I" },
                    { 576, 3, 16, "I" },
                    { 577, 3, 17, "I" },
                    { 578, 3, 18, "I" },
                    { 579, 3, 19, "I" },
                    { 580, 3, 20, "I" },
                    { 581, 3, 1, "J" },
                    { 582, 3, 2, "J" },
                    { 583, 3, 3, "J" },
                    { 584, 3, 4, "J" },
                    { 585, 3, 5, "J" },
                    { 586, 3, 6, "J" },
                    { 587, 3, 7, "J" },
                    { 588, 3, 8, "J" },
                    { 589, 3, 9, "J" },
                    { 590, 3, 10, "J" },
                    { 591, 3, 11, "J" },
                    { 592, 3, 12, "J" },
                    { 593, 3, 13, "J" },
                    { 594, 3, 14, "J" },
                    { 595, 3, 15, "J" },
                    { 596, 3, 16, "J" },
                    { 597, 3, 17, "J" },
                    { 598, 3, 18, "J" },
                    { 599, 3, 19, "J" },
                    { 600, 3, 20, "J" }
                });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "ShowID", "HallID", "MovieID", "Price", "ShowDateTime" },
                values: new object[,]
                {
                    { 1, 1, 1, 125.0, new DateTime(2023, 11, 5, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, 110.0, new DateTime(2023, 11, 5, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, 100.0, new DateTime(2023, 11, 5, 19, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingID", "CostumerID", "IsActive", "NumberOfSeats", "Price", "ShowID" },
                values: new object[,]
                {
                    { 1, 1, true, 8, 125.0, 1 },
                    { 2, 2, true, 12, 110.0, 2 },
                    { 3, 3, true, 6, 100.0, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CostumerID",
                table: "Bookings",
                column: "CostumerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShowID",
                table: "Bookings",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_BookingID",
                table: "BookingSeats",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_SeatID",
                table: "BookingSeats",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHalls_CinemaID",
                table: "CinemaHalls",
                column: "CinemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieID",
                table: "MovieGenre",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallID",
                table: "Seats",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_HallID",
                table: "Shows",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieID",
                table: "Shows",
                column: "MovieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSeats");

            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "CinemaHalls");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
