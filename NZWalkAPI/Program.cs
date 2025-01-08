using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalkAPI.DB;
using NZWalkAPI.DTOModelAutoMappers;
using NZWalkAPI.Repository;
using NZWalkAPI.Repository.IRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Added AppdbContext to the Services by providing the connection string, So that it can be used at many controller by DI.
//Basically below line is adding the AppDBContext to the services and providing the connection string to the AppDBContext.
builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NzConnStr")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injected Service to the Service pipeline, Now it can be used across the application using Dependency Injection.
builder.Services.AddScoped<IRegions, RegionService>();
builder.Services.AddScoped<IWalk, WalkService>();
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
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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
