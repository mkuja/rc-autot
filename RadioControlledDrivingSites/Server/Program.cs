global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using RadioControlledDrivingSites.Server.DbContext;
using RadioControlledDrivingSites.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

string connectionString;
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    connectionString = builder.Configuration.GetConnectionString("DevSqlServer");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("SqlServer");
}
builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlServer(connectionString);
}, ServiceLifetime.Transient);



builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RC Car Driving Sites v1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
