using Carter;
using Microsoft.EntityFrameworkCore;
using Sparks.Api.Database;
using Sparks.Api.Features.Users;
using Sparks.SupabaseClient;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserPoolManagerService>();
builder.Services.AddScoped<SupabaseClient>();
builder.Services.AddCarter();
builder.Services.AddMediatR(o =>
{
    o.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddDbContextFactory<SparksDbContext>(o =>
{
    o.UseNpgsql(configuration.GetSection("ProjectUrl").Value);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapCarter();
app.MapControllers();

app.Run();