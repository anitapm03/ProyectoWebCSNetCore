using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Helpers;
using ProyectoWebCSNetCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<HelperMails>();
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddSession();

string connectionString =
    builder.Configuration.GetConnectionString("SQLConciertosSolo");

builder.Services.AddTransient<RepositoryConciertos>();
builder.Services.AddTransient<RepositorySesion>();
builder.Services.AddTransient<RepositoryProvincias>();
builder.Services.AddTransient<RepositoryPeticiones>();
builder.Services.AddTransient<RepositorySalas>();
builder.Services.AddTransient<RepositoryArtistas>();
builder.Services.AddTransient<RepositoryGeneros>();
builder.Services.AddTransient<RepositoryPublicaciones>();
builder.Services.AddTransient<RepositoryRelaciones>();
builder.Services.AddDbContext<CSContext>
    (options => options.UseSqlServer(connectionString));

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
