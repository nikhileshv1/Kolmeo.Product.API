using Kolmeo.WebApi;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

var builder = WebApplication.CreateBuilder(args);
var apiName = "Product.API";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddProblemDetails();
//builder.Services.AddLogging(c => c.ClearProviders());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var apidetails = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = apiName,
        Version = "v1",
        Description = String.Empty,
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //Tell Swagger to use those XML comments.
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



var app = builder.Build();
app.UseHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
    });
}

// Log Unhandled Exception.
app.UseExceptionHandler(c => c.Run(async context =>
{
    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
    var correlationId = context.Request.GetCorrelationId() ?? Guid.NewGuid().ToString();
    app.Logger.LogError(feature?.Error, "Unhandled exception. CorrelationId {correlationId}", correlationId);
    var result = JsonConvert.SerializeObject(new { error = "Unhandled exception", correlationId, });
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(result);
}));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();