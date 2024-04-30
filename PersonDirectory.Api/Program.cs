using PersonDirectory.DI;
using Microsoft.OpenApi.Models;
using PersonDirectory.Api.Filters;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Api.Middlewares;
using PersonDirectory.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
new DependencyResolver(builder.Configuration).Resolve(builder.Services);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Person Directory", Version = "v1" });
    options.OperationFilter<AcceptLanguageOperationFilter>();
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AcceptLanguageHeaderMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
}

app.Run();
