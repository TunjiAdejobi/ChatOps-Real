using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ChatOps.API.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>

        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            if ($"{config["Environment"]}" == "Development")
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "CHAT OPS API",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "CHAT OPS"
                        },
                        Description = "<h2>Description</h2><p>This is the API for the CHATOps platform.</p><br /><h3>Usage</h3><ul><li><strong>Authorization (header)</strong><ul><li><p>The CHATOps API uses OAuth2 to authorize requests.</p><p>The API will issue access tokens in the form of JWTs, which must be sent in the Authorization header as a \"Bearer token\".</p><p>Your credentials should be kept secure! Do not share them in publicly accessible areas such as GitHub, client-side code, etc.</p></li></ul></li></ul>"
                    });

                    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                    //options.OperationFilter<AuthOperationFilter>();
                    //options.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}/Swagger.XML");
                });
            }
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(IApplicationBuilder app, ConfigurationManager config)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs";
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "ChatOps API");
            });
        }

    }
}
