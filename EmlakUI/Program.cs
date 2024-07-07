
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Data;
using EmlakUI.Areas.Admin.Identity;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UserAdmin, IdentityRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
});


builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Admin/Admin/Login";
    opt.LogoutPath = "/Admin/Admin/LogOut";
    opt.AccessDeniedPath = "/Admin/Admin/AccessDenied";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAdvertService, AdvertManager>();
builder.Services.AddScoped<IUserService, BusinessLayer.Concrete.UserManager>();
builder.Services.AddScoped<ICityService, CityManager>();
builder.Services.AddScoped<IDistrictService, DistrictManager>();
builder.Services.AddScoped<IGeneralSettingsService, GeneralSettingsManager>();
builder.Services.AddScoped<IImagesService, ImagesManager>();
builder.Services.AddScoped<ISituationService, SituationManager>();
builder.Services.AddScoped<ITypeService, TypeManager>();
builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<IGeneralSettingsRepository, GeneralSettingsRepository>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<ISituationRepository, SituationRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();

//session
builder.Services.AddSession();

var app = builder.Build();

app.PrepareDatabase().GetAwaiter().GetResult();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Admin}/{action=Login}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "User",
        pattern: "{area:exists}/{controller=User}/{action=Login}/{id?}"
    );


});
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Admin}/{action=Index}/{id?}"
    );



app.Run();
