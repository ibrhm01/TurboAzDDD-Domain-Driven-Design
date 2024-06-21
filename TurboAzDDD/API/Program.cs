using System.Reflection;
using Application.Services;
using Domain;
using Domain.Entities;
using Domain.ENUMs;
using Domain.Services;
using Infrastructure.Data.Context;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.EnableAnnotations();
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;

    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);

    options.Password.RequireDigit = true;


}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//builder.Services.AddScoped<IVehicleService, VehicleService>();
//builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IMarketService, MarketService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IBodyTypeService, BodyTypeService>();
builder.Services.AddScoped<IFuelTypeService, FuelTypeService>();
builder.Services.AddScoped<IDriveTypeService, DriveTypeService>();
builder.Services.AddScoped<ITransmissionService, TransmissionService>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();


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

