using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;

namespace SharkStyleApi
{
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
                options.UseSqlServer(ConStr));

            var app = builder.Build();

                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
