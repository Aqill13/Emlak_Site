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

// Servisləri konteynerə əlavə etmək
builder.Services.AddControllersWithViews();

// Identity və DbContext konfiqurasiyası
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UserAdmin, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

// Authentication və Authorization konfiqurasiyası
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie("AdminScheme", options =>
{
    options.Cookie.Name = "Admin.Cookie";
    options.LoginPath = "/Admin/Admin/Login";
    options.AccessDeniedPath = "/Admin/Admin/AccessDenied";
})
.AddCookie("UserScheme", options =>
{
    options.Cookie.Name = "User.Cookie";
    options.LoginPath = "/User/Account/Login";
    options.AccessDeniedPath = "/User/Account/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("admin")
              .AddAuthenticationSchemes("AdminScheme"));
    options.AddPolicy("UserPolicy", policy =>
        policy.RequireRole("User")
              .AddAuthenticationSchemes("UserScheme"));
});*/

// HttpContextAccessor əlavə etmək
builder.Services.AddHttpContextAccessor();

// Scoped servisələr
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
builder.Services.AddScoped<IUsersService, UsersManager>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IFavoritesService, FavoritesManager>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Session idarə etmək
builder.Services.AddSession();

var app = builder.Build();

// Verilənlər bazasını hazırlamaq və təmin etmək
app.PrepareDatabase().GetAwaiter().GetResult();

// Təstiq mühitində olmadıqda istisna idarəetməsi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS-yə yönləndirmə və statik faylları təmin etmək
app.UseHttpsRedirection();
app.UseStaticFiles();

// Yol istifadəsini idarə etmək
app.UseRouting();

// Authorization və session idarəetməsini idarə etmək
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Endpointləri qurmag
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

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Admin}/{action=Index}/{id?}"
    );
});

app.Run();
