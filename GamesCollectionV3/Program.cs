using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GamesCollectionV3.Data;
using GamesCollectionV3.Models;
using GamesCollectionV3.Repositories;
using GamesCollectionV3.Repositories.GameRepositories;
using GamesCollectionV3.Repositories.ReviewRepositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GamesCollectionV3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GamesCollectionV3Context") ?? throw new InvalidOperationException("Connection string 'GamesCollectionV3Context' not found.")));
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
