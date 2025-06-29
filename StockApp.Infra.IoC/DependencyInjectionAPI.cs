using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockapp.Application.Interfaces;
using StockApp.Application.Interfaces;
using StockApp.Application.Mappings;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using StockApp.Infra.Data.Repositories;

namespace StockApp.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddScoped<IStockDashboardRepository, StockDashboardRepository>();
            services.AddScoped<IStockDashboardService, StockDashboardService>();

            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductImportService, ProductImportService>();
            services.AddScoped<INotificationEmailService, NotificationEmailService>();
            services.AddScoped<IUserAuditLogRepository, UserAuditLogRepository>();
            services.AddScoped<IUserAuditService, UserAuditService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseService, PurchaseService>();


            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var myhandlers = AppDomain.CurrentDomain.Load("StockApp.Application");
            services.AddMediatR(myhandlers);

            return services;
        }
    }
}