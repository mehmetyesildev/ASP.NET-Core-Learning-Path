using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEmailSender,SmtpEmailSender>(i=> 
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSl"),
        builder.Configuration["EmailSender:Username"],
        builder.Configuration["EmailSender:Password"],
        builder.Configuration["EmailSender:Sender"])
    );
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IdentityContext>(Options=>
    Options.UseSqlite(builder.Configuration["ConnectionStrings:SQLite_Conection"]));
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(Options =>
{
    Options.Password.RequiredLength=6;
    Options.Password.RequireNonAlphanumeric=false;
    Options.Password.RequireLowercase=false;
    Options.Password.RequireUppercase = false;
    Options.Password.RequireDigit = false;
    Options.User.RequireUniqueEmail=true;
    // Options.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
    Options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
    Options.Lockout.MaxFailedAccessAttempts=5;
    Options.SignIn.RequireConfirmedEmail=true;
});
builder.Services.ConfigureApplicationCookie(Options =>
{
    Options.LoginPath="/Account/Login";
    Options.AccessDeniedPath="/Account/AccessDenied";
    Options.SlidingExpiration=true;
    Options.ExpireTimeSpan=TimeSpan.FromDays(30);
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    IdentitySeedData.IdentityTestUser(app);

app.Run();
