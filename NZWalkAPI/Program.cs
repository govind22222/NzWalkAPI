using Microsoft.EntityFrameworkCore;
using NZWalkAPI.DB;

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
