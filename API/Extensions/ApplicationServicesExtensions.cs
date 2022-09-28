using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<StoreContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(config.GetConnectionString("Default"));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(entry => entry.Value!.Errors.Count > 0)
                                                   .SelectMany(x => x.Value!.Errors)
                                                   .Select(x => x.ErrorMessage)
                                                   .ToArray();

                    return new BadRequestObjectResult(new ApiValidationErrorResponse(errors));
                };
            });

            services.AddCors(options => options.AddPolicy(
                "CorsPolicy", 
                policyBuilder => policyBuilder.AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .WithOrigins("http://localhost:4200")
            ));

            return services;
        }
    }
}