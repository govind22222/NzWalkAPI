using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Added By Raghvendra to use API Versioning(Like- https://localhost:7173/api/v1/countries)
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Enables version reporting in responses
    options.AssumeDefaultVersionWhenUnspecified = true; // Assume default if not specified
    options.DefaultApiVersion = new ApiVersion(1, 0); // Set default version to 1.0
    options.ApiVersionReader = new QueryStringApiVersionReader("api-version"); // Enable versioning via query string
}).AddMvc();

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
