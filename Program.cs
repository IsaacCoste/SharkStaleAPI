using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;

namespace SharkStyleApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var ConStr = builder.Configuration.GetConnectionString("ConStr");
        builder.Services.AddDbContext<Contexto>(options =>
            options.UseNpgsql(ConStr));

        builder.WebHost.UseUrls("http://*:8080");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SharkStyle API v1");
                c.RoutePrefix = "swagger"; // Swagger estará en /swagger
            });

            app.UseCors("AllowAll");
            // app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
    }
}
