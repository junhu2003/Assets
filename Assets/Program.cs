using AssetsApi.Data;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

var config = builder.Configuration;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
             builder =>
             {
                 builder
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .WithOrigins(config.GetSection("CorsOrigins").Get<string[]>())
                 .AllowCredentials();
             });
});



builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<AssetsAPIDbContext>(option => option.UseInMemoryDatabase("AssetsDb"));
builder.Services.AddDbContext<CategoriesAPIDbContext>(option => option.UseInMemoryDatabase("CategoriesDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
