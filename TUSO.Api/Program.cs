using Microsoft.EntityFrameworkCore;
using TUSO.Infrastructure;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.Repositories;
using TUSO.Infrastructure.SqlServer;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
///
/// Avoid loop or recursive call of Include method.
///
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); 

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(configuration.GetConnectionString("TusoCon")));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
   options.AddDefaultPolicy(
       builder =>
       {
          builder.WithOrigins("*", "https://localhost:7185")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
       });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapControllers();
app.Run();