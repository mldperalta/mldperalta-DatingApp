using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger>;
    //logger.LogError(ex, "An error occured during migration");
}


// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
