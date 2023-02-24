using System.Diagnostics;
using System.Reflection;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddProblemDetails();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var apidetails = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Product.Api",
        Version = "v1",
        Description = String.Empty,
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
app.UseHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Initialise and seed database
    app.UseSwagger();
    app.UseSwaggerUI();
}

    //app.UseExceptionHandler(c => c.Run(async context =>
    //{
    //    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
    //    var correlationId = context.Request.GetCorrelationId() ?? Guid.NewGuid().ToString();
    //    _logger.LogError(feature.Error, $"Unhandled exception. CorrelationId {correlationId} ");

    //    var result = JsonConvert.SerializeObject(new { error = "Unhandled exception", correlationId, });
    //    context.Response.ContentType = "application/json";
    //    await context.Response.WriteAsync(result);
    //}));    


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();