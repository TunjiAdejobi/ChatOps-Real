using AutoMapper;
using ChatOps.Common;
using ChatOps.Data.DataContext;
using ChatOps.Model.Models;
using ChatOps.Services.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ChatOps.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly ChatOpsDbContext _dbContext;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public ProfileService(IMapper mapper, ChatOpsDbContext dbContext, IPasswordService passwordService, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        public async Task<Result<ResponseModel>> UserRegistrationAsync(UserProfileRequest userProfile)
        {
            var response = new ResponseModel();
            var existingUser = _dbContext.UserProfiles.Where(x => x.Username == userProfile.Username || x.Email == userProfile.Email).FirstOrDefault();

            if (existingUser == null)
            {
                var result = _mapper.Map<Model.Models.UserProfile>(userProfile);
                // Hash the password
                string salt = _passwordService.CreateSalt();
                string hashedPassword = _passwordService.CreateHash(userProfile.Password, salt);
                result.Password = hashedPassword;
                result.Salt = salt;

                await _dbContext.UserProfiles.AddAsync(result);
                await _dbContext.SaveChangesAsync();
                response.Message = "Operation Successful";
                response.IsSuccessful = true;
                response.HasErrors = false;
                return response;
            }
            else
            {
                return Result.Failure<ResponseModel>($"{existingUser.Username} or {existingUser.Email} already existing");
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginModel model)
        {
            var response = new LoginResponse() { IsSuccessful = false };

            // Find the user by their username
            var user = await _dbContext.UserProfiles.FirstOrDefaultAsync(u => u.Username.ToLower() == model.Username);

            if (user == null)
            {
                response.Message = "User not found";
                return response;
            }

            // Verify the password
            if (user.Password != _passwordService.CreateHash(model.Password, user.Salt))
            {
                response.Message = "No user found with this username and password combination.";
                return response;
            }

            // If the username and password are correct, generate a JWT token
            response = GenerateJwtToken(user.Username);

            response.Message = "Login successful";
            response.IsSuccessful = true;

            return response;
        }

        public LoginResponse GenerateJwtToken(string username)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public async Task<Result<ResponseModel<string>>> ActivateUserAsync(int Id)
        {
            try
            {
                var user = await _dbContext.UserProfiles.FindAsync(Id);
                if (user != null)
                {
                    user.IsActive = true;
                    await _dbContext.SaveChangesAsync();
                    return Result.Success(new ResponseModel<string> 
                    {
                        Message = "User Now Activated" 
                    });
                }
                else
                {
                    return Result.Failure<ResponseModel<string>>("User Not Found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or perform additional error handling
                Console.WriteLine($"Error: {ex.Message}");
                return Result.Failure<ResponseModel<string>>($"Error: {ex.Message}");
            }
        }

        public async Task<Result<ResponseModel<string>>> DeactivateUserAsync(int Id)
        {
            try
            {
                var user = await _dbContext.UserProfiles.FindAsync(Id);
                if (user != null)
                {
                    user.IsActive = false;
                    await _dbContext.SaveChangesAsync();
                    return Result.Success(new ResponseModel<string> 
                    {
                        Message = "User Now Deactivated" 
                    });
                }
                else
                {
                    return Result.Failure<ResponseModel<string>>("User Not Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Result.Failure<ResponseModel<string>>($"Error: {ex.Message}");
            }
        }
        
    }
}

    