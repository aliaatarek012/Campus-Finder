using _CampusFinder.Extenstions;
using _CampusFinderCore.Entities.Identity;
using _CampusFinderInfrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _CampusFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Configure Services
            // Add services to the container.



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5081")
                        .AllowAnyHeader() // Or specify specific headers
                        .AllowAnyMethod() // Or specify HTTP methods (GET, POST, etc.)
                        .AllowCredentials(); // Required if using cookies or credentials
                });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });


            // Configure authentication


            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(o =>
            {
                IConfiguration GoogleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                o.ClientId = GoogleAuthSection["ClientId"];
                o.ClientSecret = GoogleAuthSection["ClientSecret"];




            });

            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            #endregion

            var app = builder.Build();

            #region Update Database

            using var Scope = app.Services.CreateScope(); //Step01, To Can With this Scope Can Ask object from any Serice

            var Services = Scope.ServiceProvider;//Step02, Used to Ask Service from scope 

            //var _dbContext = Services.GetRequiredService<StoreContext>(); //Step03, Ask CLR for Creating Object from DbContext Explicitly  

            var _identityDbContext = Services.GetRequiredService<AppIdentityDbContext>();//Take Object from IdentityDbContext Explicitly
            var _userManager = Services.GetRequiredService<UserManager<AppUser>>(); //Ask CLR Create object from class (UserManager) Explicitly

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>(); //color the error and show specific Text  

            try
            {
                //_dbContext.Database.MigrateAsync(); //Step04 , ,Update-Database
                //StoreContextSeed.SeedAnsync(_dbContext); //Data Seeding

                _identityDbContext.Database.MigrateAsync();//Update Identity Database
                AppIdentityDbContextSeed.SeedUsersAsync(_userManager);
            }

            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has occured during apply the migration");

            }

            #endregion


            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseRouting(); // Must come before UseAuthentication and UseAuthorization

            app.UseCors("AllowSpecificOrigins"); // Use only one CORS policy

            app.UseHttpsRedirection(); // Redirect HTTP to HTTPS (optional during development)

            app.UseAuthentication(); // Authenticate users
            app.UseAuthorization(); // Enforce authorization policies

            app.UseStatusCodePagesWithRedirects("/errors/{0}"); // Handle status codes

            app.MapControllers(); // Map API controllers






            #endregion

            app.Run();
        }
    }
}
