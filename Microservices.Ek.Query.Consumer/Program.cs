using Microservices.Ek.Query.Infrastructure;
using Microservices.Ek.Query.Infrastructure.Consumers;
using Microservices.Ek_Query_Application.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("drivers:notifications:email"));
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHostedService<MicroservicesEkConsumerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
