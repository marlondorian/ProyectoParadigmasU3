using ProyectoApi.Database;
using ProyectoApi.Services.Songs;
using ProyectoApi.Services.Checkout;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicStoreDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ISongsService, SongsService>();
builder.Services.AddTransient<ICheckoutService, ProyectoApi.Services.Checkout.CheckoutService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();