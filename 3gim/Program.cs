using _3gim.Data;
using _3gim.Hubs;
using _3gim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<_3gimDbContext>(
    options => options.UseMySql(connection, ServerVersion.AutoDetect(connection))
);


builder.Services.AddIdentity<_3gimMember, IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = false
    )
    .AddEntityFrameworkStores<_3gimDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<TempHub>("/TempHub");

app.Run();
//app.Run("http://0.0.0.0:3333");
