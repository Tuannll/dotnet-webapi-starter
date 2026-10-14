using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Infrastructures;
using WebAPIDemo.Middleware;
using WebAPIDemo.Services.Abstracts;
using WebAPIDemo.Services.Implements;

namespace WebAPIDemo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

        // Configure input validation response format
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var firstError = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault() ?? "Dữ liệu đầu vào không hợp lệ.";

                var response = new
                {
                    isSuccess = false,
                    statusCode = 400,
                    message = firstError
                };
                return new BadRequestObjectResult(response);
            };
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new Microsoft.OpenApi.OpenApiInfo { Title = "Demo Web API", Version = "v1" }
            );
        });

        var app = builder.Build();

        // Global exception middleware
        app.UseMiddleware<GlobalExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        // Map controller endpoints (attribute routing)
        app.MapControllers();

        app.Run();
    }
}
