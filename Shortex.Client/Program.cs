using Microsoft.EntityFrameworkCore;
using Shortex.BusinessLogic.Services;
using Shortex.BusinessLogic.Services.IServices;
using Shortex.DataAccess.Context;
using Shortex.DataAccess.Repositories;
using Shortex.DataAccess.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddConsole();
//});

// builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IShortUrlService, ShortUrlService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

InitDatabase();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


void InitDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}