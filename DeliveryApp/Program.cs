
using Cronos;
using DeliveryApp.Data;
using DeliveryApp.Data.Repositories;
using DeliveryApp.Services;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlite("Data Source=DeliveryDatabase.db"));

            builder.Services.AddHostedService<CronjobService>();
            builder.Services.AddSingleton<XmlProcessingService>();
            builder.Services.AddHttpClient<WeatherService>();
            builder.Services.AddScoped<WeatherService>();
            builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
            builder.Services.AddScoped<IFeeRepository, FeeRepository>();
            builder.Services.AddScoped<IFeeCalculationService, FeeCalculationService>();
            builder.Services.AddScoped<IXmlProcessingService, XmlProcessingService>();

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
