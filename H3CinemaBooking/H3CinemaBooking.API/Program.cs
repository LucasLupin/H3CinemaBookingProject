using H3CinemaBooking.Repository.Data;
using H3CinemaBooking.Repository.DTO;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Repositories;
using H3CinemaBooking.Repository.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserDetailRepository, UserDetailRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IGenericRepository<Cinema>, GenericRepository<Cinema>>();
builder.Services.AddScoped<IGenericRepository<CinemaHall>, GenericRepository<CinemaHall>>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<Roles>();


builder.Services.AddScoped<IUserDetailService, UserDetailService>();
builder.Services.AddScoped<HashingService>();


//Cors Thread

builder.Services.AddCors(options =>
{
    options.AddPolicy("coffee",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});


//string conStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Samurai;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
//builder.Services.AddDbContext<Dbcontext>(options => options.UseSqlServer(conStr, b => b.MigrationsAssembly("H3CinemaBooking.API")));

var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Dbcontext>(options => options.UseSqlServer(conStr, b => b.MigrationsAssembly("H3CinemaBooking.API")));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("coffee");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
