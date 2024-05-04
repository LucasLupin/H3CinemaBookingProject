using H3CinemaBooking.Repository.DTO;
using H3CinemaBooking.Repository.Interfaces;
using H3CinemaBooking.Repository.Models;
using H3CinemaBooking.Repository.Models.DTO;
using H3CinemaBooking.Repository.Models.DTO_s;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace H3CinemaBooking.Repository.Service
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly HashingService _hashingService = new HashingService();
        private readonly IConfiguration _configuration;


        public UserDetailService(IUserDetailRepository userDetailRepository, IConfiguration configuration)
        {
            _userDetailRepository = userDetailRepository;
            _configuration = configuration;
        }

        public List<UserDetailDTO> GetAllUserDetail()
        {
            var userdetail = _userDetailRepository.GetAll();
            return userdetail.Select(c => new UserDetailDTO
            {
                UserDetailID = c.UserDetailID,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                RoleID = c.RoleID,
                Bookings = c.Bookings,
                IsActive = c.IsActive
            }).ToList();
        }

        public UserDetailDTO GetUserDetailById(int id)
        {
            var userdetail = _userDetailRepository.GetById(id);
            if (userdetail == null) return null;
            return new UserDetailDTO
            {
                UserDetailID = userdetail.UserDetailID,
                Name = userdetail.Name,
                Email = userdetail.Email,
                PhoneNumber = userdetail.PhoneNumber,
                RoleID= userdetail.RoleID,
                Bookings = userdetail.Bookings,
                IsActive = userdetail.IsActive
            };
        }

        public (string Hash,string Salt) CreateUserDetail(UserDetail userdetail)
        {
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(userdetail.PasswordHash, newSalt);
            userdetail.PasswordSalt = newSalt;
            userdetail.PasswordHash = newHash;

            return (newHash, newSalt);
        }
        public bool DeleteUserdetail(int id)
        {
            var userdetail = _userDetailRepository.GetById(id);
            if (userdetail != null)
            {
                _userDetailRepository.DeleteByID(id, userdetail);
                return true;
            }
            return false;
        }

        public string RegisterUser(RegisterUserDTO registerUserDetail)
        {
            UserDetail userDetail = new();
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(registerUserDetail.Password, newSalt);
            userDetail.PasswordSalt = newSalt;
            userDetail.PasswordHash = newHash;
            userDetail.Email = registerUserDetail.Email;
            userDetail.Name = registerUserDetail.Name;
            userDetail.PhoneNumber = registerUserDetail.PhoneNumber;
            userDetail.IsActive = true;
            userDetail.RoleID = 1;
            var result = _userDetailRepository.Create(userDetail);
            if (result != null)
            {
                return CreateToken(result);
            }
            return "false";
        }

        public UserDetail RegisterCostumerWithoutToken(RegisterUserDTO registerUserDetail)
        {
            UserDetail userDetail = new();
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(registerUserDetail.Password, newSalt);
            userDetail.PasswordSalt = newSalt;
            userDetail.PasswordHash = newHash;
            userDetail.Email = registerUserDetail.Email;
            userDetail.Name = registerUserDetail.Name;
            userDetail.PhoneNumber = registerUserDetail.PhoneNumber;
            userDetail.IsActive = true;
            userDetail.RoleID = 1;

            var result = _userDetailRepository.Create(userDetail);

            return (result);
        }

        public UserDetail RegisterAdminWithoutToken(RegisterUserDTO registerUserDetail)
        {
            UserDetail userDetail = new();
            string newSalt = _hashingService.GenerateSalt();
            string newHash = _hashingService.HashPassword(registerUserDetail.Password, newSalt);
            userDetail.PasswordSalt = newSalt;
            userDetail.PasswordHash = newHash;
            userDetail.Email = registerUserDetail.Email;
            userDetail.Name = registerUserDetail.Name;
            userDetail.PhoneNumber = registerUserDetail.PhoneNumber;
            userDetail.IsActive = true;
            userDetail.RoleID = 2;

            var result = _userDetailRepository.Create(userDetail);

            return (result);
        }

        public string Login(LoginUserDTO loginUserDetail)
        {
            var user = _userDetailRepository.GetByEmail(loginUserDetail.Email);
            string salt = user.PasswordSalt;
            string hash = _hashingService.HashPassword(loginUserDetail.Password, salt);
            if (hash == user.PasswordHash)
            {
                return CreateToken(user);
            }
            return "False";
        }

        private string CreateToken(UserDetail userDetail)
        {
            //TODO: Lav så den bruger en rolle metode der konverterer id til rolle navn
            var roleInfo = _userDetailRepository.GetRole(userDetail.RoleID);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userDetail.Email),
                new Claim(ClaimTypes.Name, userDetail.Name),
                new Claim(ClaimTypes.MobilePhone, userDetail.PhoneNumber)
            };

            if (roleInfo != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleInfo.RoleName));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return (jwt);
        }


        public bool CheckIfUserExistsFromEmail(string Email)
        {
            var userdetial = _userDetailRepository.GetByEmail(Email);
            if (userdetial != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfUserExistsFromNumber(string PhoneNumber)
        {
            var userdetial = _userDetailRepository.GetByPhoneNumber(PhoneNumber);
            if (userdetial != null)
            {
                return true;
            }
            return false;
        }

        public List<string> ValidateUserInput<T>(T userObjekt)
        {
            List<string> errors = new();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var value = property.GetValue(userObjekt) as string;

                // Skip properties that are null
                if (value == null)
                {
                    continue;
                }

                // Email validation
                if (property.Name.Equals("Email", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrEmpty(value) &&
                    !IsValidEmail(value))
                {
                    errors.Add("Invalid email format.");
                }

                // Phone number validation
                if (property.Name.Equals("PhoneNumber", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrEmpty(value) &&
                    !IsValidPhoneNumber(value))
                {
                    errors.Add("Invalid phone number format.");
                }

                // Password validation
                if (property.Name.Equals("Password", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrEmpty(value) &&
                    !IsValidPassword(value))
                {
                    errors.Add("Password does not meet the required criteria.");
                }

                // Email validation
                if (property.Name.Equals("Name", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrEmpty(value) &&
                    !IsNameValid(value))
                {
                    errors.Add("Name cannot contain numbers.");
                }

            }

            return errors;
        }

        private bool IsNameValid(string name)
        {
            var nameRegex = @"^[^\d]*$";
            return Regex.IsMatch(name, nameRegex);
        }
        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = @"^[123456789]\d{7}$";
            return Regex.IsMatch(phoneNumber, phoneRegex);
        }

        private bool IsValidPassword(string password)
        {
            // Minimum 8 characters, at least one uppercase letter, one lowercase letter, and one digit
            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, passwordRegex);
        }
    }
}
