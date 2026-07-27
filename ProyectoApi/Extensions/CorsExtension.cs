namespace PersonsApp.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                var allowUrls = configuration.GetSection("AllowURLS").Get<string[]>();
                if (allowUrls == null || allowUrls.Length == 0)
                {
                    allowUrls = [""];
                }
                opt.AddPolicy("CorsPolicy", builder =>
                
                    builder.WithOrigins(allowUrls)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                );
            });

            return services;
        }
    }
}   
