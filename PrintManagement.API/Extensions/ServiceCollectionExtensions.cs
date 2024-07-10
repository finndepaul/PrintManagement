using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrintManagement.Application.ImplementServices;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using PrintManagement.Infrastructure.Database.DataContexts;
using PrintManagement.Infrastructure.ImplementRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.API.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();	
			services.AddScoped<IBaseRepository<ConfirmEmail>, BaseRepository<ConfirmEmail>>();	

			services.AddScoped<IDbContext, AppDbContext>();
			services.AddTransient<IEmailService, EmailService>(); // xóa using mailkit
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<UserConverter>();

			return services;
		}
	}
}
