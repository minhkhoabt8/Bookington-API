
using Bookington_Api.Extensions;
using Bookington_Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddUOW();
builder.Services.AddAutoMapper();
builder.Services.AddEvents();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureApiOptions();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddServiceFilters();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
