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
			services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();	
			services.AddScoped<IBaseRepository<RefreshToken>, BaseRepository<RefreshToken>>();	
			services.AddScoped<IBaseRepository<Permissions>, BaseRepository<Permissions>>();	
			services.AddScoped<IBaseRepository<Team>, BaseRepository<Team>>();	
			services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();	
			services.AddScoped<IBaseRepository<Project>, BaseRepository<Project>>();	
			services.AddScoped<IBaseRepository<Design>, BaseRepository<Design>>();	
			services.AddScoped<IBaseRepository<PrintJobs>, BaseRepository<PrintJobs>>();	
			services.AddScoped<IBaseRepository<ResourceForPrintJob>, BaseRepository<ResourceForPrintJob>>();	
			services.AddScoped<IBaseRepository<ResourcePropertyDetail>, BaseRepository<ResourcePropertyDetail>>();	
			services.AddScoped<IBaseRepository<Bill>, BaseRepository<Bill>>();	
			services.AddScoped<IBaseRepository<Delivery>, BaseRepository<Delivery>>();	

			services.AddTransient<IEmailService, EmailService>(); // xóa using mailkit
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<ITeamService, TeamService>();
			services.AddTransient<ICustomerService, CustomerService>();
			services.AddTransient<IProjectService, ProjectService>();
			services.AddTransient<IDesignService, DesignService>();
			services.AddTransient<IResourseForPrintjobService, ResourseForPrintjobService>();
			services.AddTransient<IDeliveryService, DeliveryService>();

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<ITeamRepository, TeamRepository>();
			services.AddTransient<ICustomerRepostory, CustomerRepository>();
			services.AddTransient<IProjectRepository, ProjectRepository>();

			services.AddTransient<UserConverter>();
			services.AddTransient<TeamConverter>();
			services.AddTransient<CustomerConverter>();
			services.AddTransient<ProjectConverter>();
			services.AddTransient<DesignConverter>();

            services.AddScoped<IDbContext, AppDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
		}
	}
}
