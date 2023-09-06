global using contactManagerAPI.Models;
global using contactManagerAPI.Services.AuthServices;
global using contactManagerAPI.Services;

global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Serilog;
using contactManagerAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;

using contactManagerAPI.Services.AuditServices;
using contactManagerAPI.Repositories.NumberRepository;
using contactManagerAPI.Repositories.UserRepository;
using contactManagerAPI.Repositories.NumberRepository;
using contactManagerAPI.Repositories.ContactRepository;
using contactManagerAPI.Repositories.AddressRepository;
using contactManagerAPI.Services.UserServices;
using contactManagerAPI.Services.ContactServices;
using contactManagerAPI.Services.NumberServices;
using contactManagerAPI.Services.AddressServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

IConfiguration config = builder.Configuration;

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INumberRepository, NumberRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<INumberService, NumberService>();
builder.Services.AddScoped<IAddressService, AddressService>();

// builder.Services.AddAuthorization();

builder.Services.AddDbContext<DataContext>(
    options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("ContactManagerDatabase"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure OAuth2 security definition for Swagger
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Paste a valid JWT to use locked endpoints",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );

    // Configure Swagger documentation settings
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo()
        {
            Version = "v1",
            Title = "Contact Management System API",
            Description = "Contact Management System API with basic CRUD operations.",
            Contact = new OpenApiContact()
            {
                Name = "Kyle Adrian del Castillo",
                Url = new Uri("https://github.com/sycasec")
            }
        }
    );

    // Include XML comments for documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value!,
            ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value!,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value!)
            ),
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
