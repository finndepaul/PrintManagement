using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrintManagement.API.Extensions;
using PrintManagement.Application.Handle.HandleEmail;
using PrintManagement.Application.ImplementServices;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Infrastructure.Database.DataContexts;
using System.Text;
namespace PrintManagement.API
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Gen db
			builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(Constants.AppSettingKeys.DEFAULT_CONNECTION)));

			// DI
			builder.Services.AddEventBus(builder.Configuration); // sau đó using tay =))

			// Gửi mail
			var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
			builder.Services.AddSingleton(emailConfig);

			// authen config
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // cài 2 pakage ...jwt và ...jwtBearer
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = TimeSpan.Zero,
					ValidAudience = builder.Configuration["JWT:ValidAudience"], // Sau đó vào appsetting
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
					// sau đó config ở SwaggerGen
				};
			});

			// Add services to the container.
			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(option =>
			{
				option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Auth Api", Version = "v1" });
				option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "Vui lòng nhập token",
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[]{}
                        // sau đó UseAuthen ở app dưới, trên author
                    }
				});
			});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

			// Authen
			app.UseAuthentication();
			app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
