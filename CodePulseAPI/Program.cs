
using CodePulseBL.Repository;
using CodePulseBL.UnitOfWork;
using CodePulseDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodePulseAPI
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

            builder.Services.AddDbContext<CodePulseDbContext>(option =>
            
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    p=>p.MigrationsAssembly(typeof(CodePulseDbContext).Assembly.FullName)));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepo>();

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
