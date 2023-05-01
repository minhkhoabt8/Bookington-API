
using Bookington.Infrastructure.Hubs;
using Bookington_Api.Extensions;
using Bookington_Api.Hubs;
using Bookington_Api.Middlewares;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddUOW();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper();
builder.Services.AddSignalRService();
builder.Services.AddEvents();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureApiOptions();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddServiceFilters();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureSignalROptions();
builder.Services.AddDateOnlyTimeOnlyStringConverters();

builder.Services.AddCronJob();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(opt => opt
    .WithOrigins("https://localhost:44360")
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationUserHub>("/notificationHub");


app.Run();
