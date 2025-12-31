using E_commerce.Core.Services;
using E_commerce.infrastructer.Interfaces;
using E_Commerce.EF.Data;
using E_Commerce.EF.Repositries;
using E_Commerce.EF.Repositries.Service;
using E_Commerce.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))); builder.Services.AddControllers();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Scope Repositories
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddSingleton<IImageServices, ImageService>();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

//Auto Mapper
builder.Services.AddAutoMapper(typeof(Profiles).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
