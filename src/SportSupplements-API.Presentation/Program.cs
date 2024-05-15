
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SportSupplements_API.Core.Models;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Repositories;
using SportSupplements_API.Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

var blobOptionsSection = builder.Configuration.GetSection("BlobOptions");

var connectionString = builder.Configuration.GetSection("SportSupplementDb").Value;

var databaseName = builder.Configuration.GetSection("dbName").Value;

var collectionName = builder.Configuration.GetSection("collectionName").Value;

var blobOptions = blobOptionsSection.Get<BlobOptions>() ?? throw new Exception("Couldn't create blob options object");

builder.Services.Configure<BlobOptions>(blobOptionsSection);

var infrastructureAssembly = typeof(SportSupplementMongoRepository).Assembly;

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(infrastructureAssembly);
});

builder.Services.AddSingleton<ISportSupplementRepository>(provider =>
{
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new Exception($"{connectionString} not found");
    }
    return new SportSupplementMongoRepository(connectionString, databaseName, collectionName);
});


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
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var client = new MongoClient(connectionString);

    var newsDb = client.GetDatabase("SportSupplementDb");

    var newsCollection = newsDb.GetCollection<SportSupplement>("SportSupplement");
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("BlazorWasmPolicy");

app.Run();
