using AutoMapper;
using ChatOps.Common;
using ChatOps.Model.Models;
using ChatOps.Services.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilio.Http;

namespace ChatOps.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileManagerController : BaseApiController
    {
        private readonly IProfileService _profileService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileService"></param>
        public ProfileManagerController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-user")]
        [Produces(typeof(Envelope<ResponseModel>))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserProfileRequest profile)
        {
            var result = await _profileService.UserRegistrationAsync(profile);
            Result responseCombined = Result.Combine(result);
            if (responseCombined.IsFailure)
                return Error(responseCombined.Error);
            return Ok(result.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserToken")]
        [AllowAnonymous]
        public async Task<LoginResponse> Tokenize([FromHeader] string Username, [FromHeader] string password)
        {
            LoginResponse resp = new LoginResponse() { IsSuccessful = false };
            try
            {
                var requestModel = new LoginModel()
                {
                    Username = Username.ToLower(),
                    Password = password
                };

                resp = await _profileService.LoginAsync(requestModel);

            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
            }
            return await Task.FromResult(resp);            

        

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Activate-User/{Id}")]
        [Produces(typeof(Envelope<ResponseModel>))]
        public async Task<IActionResult> ActivateUserAsync(int Id)
        {
            var response = await _profileService.ActivateUserAsync(Id);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Deactivate-User/{Id}")]
        [Produces(typeof(Envelope<ResponseModel>))]
        public async Task<IActionResult> DeactivateUser(int Id)
        {
            var response = await _profileService.DeactivateUserAsync(Id);
            Result res = Result.Combine(response);
            if (res.IsFailure)
                return Error(res.Error);
            return Ok(response.Value);
        }
    }
}
