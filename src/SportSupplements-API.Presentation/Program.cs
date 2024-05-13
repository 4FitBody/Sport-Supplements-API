using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Data;
using SportSupplements_API.Infrastructure.Repositories;
using SportSupplements_API.Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

var blobOptionsSection = builder.Configuration.GetSection("BlobOptions");

var blobOptions = blobOptionsSection.Get<BlobOptions>() ?? throw new Exception("Couldn't create blob options object");

builder.Services.Configure<BlobOptions>(blobOptionsSection);

var infrastructureAssembly = typeof(SportSupplementDbContext).Assembly;

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(infrastructureAssembly);
});

var connectionString = builder.Configuration.GetConnectionString("SportSupplementsDb");

builder.Services.AddDbContext<SportSupplementDbContext>(dbContextOptionsBuilder =>
{
    dbContextOptionsBuilder.UseNpgsql(connectionString, o =>
    {
        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

builder.Services.AddScoped<ISportSupplementRepository, SportSupplementSqlRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "4FitBody (api for working staff)",
        Version = "v1"
    });
});

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorWasmPolicy", corsBuilder =>
    {
        corsBuilder
            .WithOrigins("http://localhost:5160")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("BlazorWasmPolicy");

app.Run();
