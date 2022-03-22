

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PhotoAnalyzerApi;
using PhotoAnalyzerApi.Service;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1", builder =>
    {
        builder.WithOrigins("http://localhost:3000").WithMethods("POST", "GET", "PUT", "DELETE").WithHeaders();
    });
});

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<PhotoAnalyzer>().As<IPhotoAnalyzer>();
    builder.RegisterType<PhotoProcessor>().As<IPhotoProcessor>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("Policy1");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
