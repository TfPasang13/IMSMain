using IMS.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using IMS.Infrastructure;
using IMS.Infrastructure.Repository.CRUD;
using IMS.Infrastructure.IRepository;
using IMS.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

/*Define services to use sqlServer or database*/
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<IMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
    e => e.MigrationsAssembly("IMS.web")));



//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IEmailSender, IMS.Infrastructure.Services.EmailSender>();

builder.Services.AddTransient(typeof(ICrudService<>),typeof (CrudService<>));
builder.Services.AddTransient < IRawSqlRepository, RawSqlRepository >();

var app = builder.Build();

using (var scope=app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    await SeedingData.InitializeAsync(services);    
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
