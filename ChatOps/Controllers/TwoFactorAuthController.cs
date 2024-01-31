using ChatOps.Model.Models.RequestModel;
using ChatOps.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatOps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TwoFactorAuthController : BaseApiController
    {
        private readonly ITwoFactorAuthService _twoFactorAuthService;

        public TwoFactorAuthController(ITwoFactorAuthService twoFactorAuthService)
        {
            _twoFactorAuthService = twoFactorAuthService;
        }

        [HttpPost("send-code")]
        public IActionResult SendVerificationCode([FromBody] VerificationCodeRequest model)
        {
            try
            {
                // Assuming model contains UserId and PhoneNumber
                _twoFactorAuthService.SendVerificationCode(model.Id, model.PhoneNumber);
                return Ok("Verification code sent successfully");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("verify-code")]
        public IActionResult VerifyCode([FromBody] AccessCodeRequest model)
        {
            try
            {
                // Assuming model contains UserId and EnteredCode
                bool isCodeValid = _twoFactorAuthService.VerifyCode(model.Id, model.AccessCode);

                if (isCodeValid)
                {
                    // Code is valid, you can proceed with further actions (e.g., authentication, registration)
                    return Ok("Verification successful. Proceed with further actions.");
                }
                else
                {
                    // Code is invalid, handle accordingly (e.g., return an error response)
                    return BadRequest("Invalid verification code.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}
