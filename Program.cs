using MatchReservationSystem.DbContexts;
using MatchReservationSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MatchReservationSystem.Hubs;
using MatchReservationSystem.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GlobalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GlobalDbContextConnection") ?? throw new InvalidOperationException("Connection string 'GlobalDbContext' not found.")));

builder.Services.AddDbContext<UserIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'UserIdentityContext' not found.")));

//builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
//    //.AddEntityFrameworkStores<UserIdentityDbContext>()
//    .AddDefaultUI()
//    .AddDefaultTokenProviders();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserIdentityDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddScoped<HttpContextUserManager, HttpContextUserManager>();
builder.Services.AddDbContext<UserIdentityDbContext>();

//To avoid hidden HTML5 checkbox as it is useless
builder.Services.Configure<MvcViewOptions>(options =>
    options.HtmlHelperOptions.CheckBoxHiddenInputRenderMode =
        CheckBoxHiddenInputRenderMode.None);

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextManager, HttpContextManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Matches}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<ReservationHub>("/reservationHub");
app.Run();
