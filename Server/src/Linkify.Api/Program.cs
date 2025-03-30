using Linkify.Api.Common.Conventions;
using Linkify.Api.Common.Handlers;
using Linkify.Api.Common.MiddleWares;
using Linkify.Application;
using Linkify.Infrastructure;
using Linkify.Infrastructure.DataAccessManagers;
using Linkify.Infrastructure.RealtimeManagers.ChatManagers;
using Linkify.Infrastructure.RealtimeManagers.NotificationManagers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseRouteTransformer()));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });

    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Authentication 
builder.Services.AddAuthorization();

var app = builder.Build();

app.CreateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.MapControllers();

// Map SignalR Hub
app.MapHub<ChatHub>("/hubs/chat");
app.MapHub<NotificationHub>("/hubs/notification");

app.Run();
