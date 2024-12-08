
using API.Mappings;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<EcommerceContext>(
					opt => 
						opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
				);

			builder.Services.AddSingleton<IConnectionMultiplexer>(
				c =>
				{
					var config = builder.Configuration.GetConnectionString("Redis");
					return ConnectionMultiplexer.Connect(config);
                }
			);

			builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

            //builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			builder.Services.AddScoped<ITokenGenerationService, TokenGenerationService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<EcommerceContext>()
                .AddDefaultTokenProviders();

            //builder.Services.AddAutoMapper(typeof(MappingProfiles));
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


			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<EcommerceContext>();
			// Create an instance of ILogger specifically for EcommerceCotextSeed
			var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
			var logger = loggerFactory.CreateLogger<EcommerceContextSeed>();

			try
			{
				await context.Database.MigrateAsync();

				var ecommerceContextSeed = new EcommerceContextSeed(logger);
				await ecommerceContextSeed.SeedDataAsync(context);
				await ApplicationIdentityContextSeed.SeedAsync(services);
            }
			catch(Exception ex)
			{
				logger.LogError(ex, "An error occured during migration");
			}
            app.Run();
		}
	}
}
