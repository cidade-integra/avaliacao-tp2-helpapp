using AspNetCoreRateLimit;
using StockApp.API.Middlewares;
using StockApp.Application.Interfaces;
using StockApp.Application.Mappings;
using StockApp.Application.Services;
using StockApp.Application.Settings;
using StockApp.Infra.IoC;
using System.Net;
using System.Net.Mail;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // configuração cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("OpenCors", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);
        // JWT Settings 
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddHttpClient<IPriceQuoteService, PriceQuoteService>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<SmtpClient>(ServiceProvider =>
        {
            var config = ServiceProvider.GetRequiredService<IConfiguration>();
            var emailSettings = config.GetSection("EmailSettings");

            return new SmtpClient(emailSettings["Host"])
            {
                Port = emailSettings.GetValue<int>("Port"),
                EnableSsl = emailSettings.GetValue<bool>("EnableSsl"),
                Credentials = new NetworkCredential(
                    emailSettings["Username"],
                    emailSettings["Password"])
            };
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseErrorHandlerMiddleware(); // handler de manipulação de erros e registro de logs
        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseIpRateLimiting();

        app.UseCors("OpenCors"); // aplicando a config cors na pipeline

        app.UseAuthorization();
        app.MapControllers();

        app.Run();

    }
}
