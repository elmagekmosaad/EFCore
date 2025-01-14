using Web.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Web.Api.Authorization;
using Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSwaggerGenContactInfo();
builder.Services.AddSwaggerGenAuthorizationButton();
builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("DefaultConnection"));
//builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("onlineMysql"));
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationJwtAuth(builder.Configuration.GetSection("jwt").Get<JwtConfiguration>());
builder.Services.AddApplicationAuthorization();

////add this to mvc frontEnd
//builder.Services.AddHttpClient("EFCore", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://localhost:7144/api/");
//    httpClient.DefaultRequestHeaders.Accept.Clear();
//    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//}).ConfigurePrimaryHttpMessageHandler(() =>
//{
//    return new SocketsHttpHandler()
//    {
//        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
//    };
//}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
////add this to mvc frontEnd

var app = builder.Build();

// Configure the HTTP request pipeline.


await app.AddApplicationDataSeedingAsync();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//
app.UseAuthentication();
//
app.UseAuthorization();

app.MapControllers();

app.Run();
