using ProyectoApi.Database;
using ProyectoApi.Services.Songs;
using ProyectoApi.Services.Checkout;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicStoreDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ISongsService, SongsService>();
builder.Services.AddTransient<ICheckoutService, ProyectoApi.Services.Checkout.CheckoutService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();


var app = builder.Build();

StripeConfiguration.ApiKey = app.Configuration.GetValue<string>("Stripe:SecretKey") ?? "sk_test_placeholder";

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();