using ChatOps.Common;
using ChatOps.Model.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Interfaces
{
    public interface IProfileService
    {
        Task<Result<ResponseModel>> UserRegistrationAsync(UserProfileRequest userProfile);
        Task<LoginResponse> LoginAsync(LoginModel model);
        LoginResponse GenerateJwtToken(string username);

        Task<Result<ResponseModel<string>>> ActivateUserAsync(int Id);
        Task<Result<ResponseModel<string>>> DeactivateUserAsync(int Id);               
        
    }
}
