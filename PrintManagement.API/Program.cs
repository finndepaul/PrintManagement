using Microsoft.EntityFrameworkCore;
using PrintManagement.API.Extensions;
using PrintManagement.Application.Handle.HandleEmail;
using PrintManagement.Application.ImplementServices;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Infrastructure.Database.DataContexts;
namespace PrintManagement.API
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Gen db
			builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(Constant.AppSettingKeys.DEFAULT_CONNECTION)));

			// DI
			builder.Services.AddEventBus(builder.Configuration); // sau đó using tay =))

			// Gửi mail
			var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
			builder.Services.AddSingleton(emailConfig);

			// Add services to the container.

			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
