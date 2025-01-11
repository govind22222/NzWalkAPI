using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalkAPI.DB;
using NZWalkAPI.DTOModelAutoMappers;
using NZWalkAPI.Repository;
using NZWalkAPI.Repository.IRepository;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Added AppdbContext to the Services by providing the connection string, So that it can be used at many controller by DI.
//Basically below line is adding the AppDBContext to the services and providing the connection string to the AppDBContext.
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NzConnStr")));

//Added other Database for user Role and Credential.
builder.Services.AddDbContext<AuthAppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NzAuthDBConnStr")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//Added some code by Raghav to use Authorication Header to the Swagger to byPass Postman
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZWalk API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference= new OpenApiReference
            {
                Type= ReferenceType.SecurityScheme,
                Id= JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Oauth2",
            Name= JwtBearerDefaults.AuthenticationScheme,
            In= ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

// Injected Service to the Service pipeline, Now it can be used across the application using Dependency Injection.
builder.Services.AddScoped<IRegions, RegionService>();
builder.Services.AddScoped<IWalk, WalkService>();
builder.Services.AddScoped<IAuth, AuthService>();

//Injected the AutoMapper class to the Service pipeline in order to use it at Controllers by DI.
builder.Services.AddAutoMapper(typeof(DtoModelMapper));

//JWT Configuration By Raghav.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

//--------Setting Identity along with JWT Authentication---------
builder.Services.AddIdentityCore<IdentityUser>()  //Enables core identity functionality 
    .AddRoles<IdentityRole>()    //Enables role-based authorization like- "User.IsInRole("Admin")"
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")    //Adds a custom token provider for generating and validating tokens
    .AddEntityFrameworkStores<AuthAppDBContext>()       //Representing the database where user and role information will be stored.
    .AddDefaultTokenProviders();   // Generate tokens for Email Confirmation(A token for verifying user email addresses), Password Reset, Phone Number Confirmation.

//Configuring password validations
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
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
