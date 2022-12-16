using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Recipes.Core;
using Recipes.Repos;
using System.Reflection;
using System.Text.Json.Serialization;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Recipes App Project API",
        Description = "Laboratory work 2022, Ostroh Academy",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "sofiia.avtomchuk@oa.edu.ua",
            Name = "Sofia Avtomchuk",
        }
    });
});

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


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<InfoDishRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
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
