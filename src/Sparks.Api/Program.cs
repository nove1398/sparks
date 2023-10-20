using Carter;
using Microsoft.EntityFrameworkCore;
using Sparks.Api.Database;
using Sparks.Api.Features.Users;
using Sparks.Api.Pipelines;
using Sparks.SupabaseClient;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.AddScoped<UserPoolManagerService>();
builder.Services.AddSupaBaseClient(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddMediatR(o =>
{
    o.RegisterServicesFromAssembly(typeof(Program).Assembly);
    o.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
    o.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
});
builder.Services.AddDbContextFactory<SparksDbContext>((sp,o) =>
{
    o.UseNpgsql(configuration["SupabaseClientOptions:ConnectionString"]);
    o.AddInterceptors(new DomainEventInterceptor(sp));
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