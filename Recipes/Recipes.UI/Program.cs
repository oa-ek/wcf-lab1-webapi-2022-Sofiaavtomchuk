using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipes.Core;
using Recipes.Repos;
using System.Text.Json.Serialization;
using Recipes.Core;
using Recipes.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("RecipesConnection");
builder.Services.AddDbContext<RecipesContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(3600);
    option.Cookie.IsEssential = true;
});

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<RecipesContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<UpdateRepository>();
//builder.Services.AddTransient<LossesRepository>();
//builder.Services.AddTransient<EqRepository>();
builder.Services.AddTransient<UsersRepository>();
builder.Services.AddTransient<InfoDishRepository>();
builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<SaveRepository>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();


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
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();