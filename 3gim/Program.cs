using _3gim.Data;
using _3gim.Hubs;
using _3gim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR(options => options.MaximumReceiveMessageSize = 1024 * 1024 * 1024);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<_3gimDbContext>(
    options => options.UseMySql(connection, ServerVersion.AutoDetect(connection))
);


builder.Services.AddIdentity<_3gimMember, IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = false
    )
    .AddEntityFrameworkStores<_3gimDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<MySqlConnection>(options =>
    new MySqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );

builder.Services.ConfigureApplicationCookie(opt => {
    opt.LoginPath = "/member/login";                 
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<TempHub>("/TempHub");
app.MapHub<CameraHub>("/CameraHub");
app.MapHub<ProduceHub>("/ProduceHub");


app.Run("http://0.0.0.0:3333");
