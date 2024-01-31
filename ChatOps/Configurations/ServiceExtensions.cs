using AutoMapper;
using ChatOps.Service.Interfaces;
using ChatOps.Service.Services;
using ChatOps.Services.Interfaces;
using ChatOps.Services.Services;

namespace ChatOps.API.Configurations
{
    public static class ServiceExtensions
    {
        public static void ServiceConfig(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPasswordService, PasswordService>();

            services.AddSingleton<ITwoFactorAuthentication, TwoFactorAuthentication>();
            services.AddSingleton<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddSingleton<ITwilioSmsService, TwilioSmsService>();
            services.AddSingleton<ITwoFactorAuthService, TwoFactorAuthService>();

        }
    }
}
