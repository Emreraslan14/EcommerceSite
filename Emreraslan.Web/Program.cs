using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Contexts.EfCoreApp;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Services
builder.Services.AddScoped<UserService>();
#endregion


builder.Services.AddDbContext<AppDbContext>(opt =>
{
	//Environment.GetEnvironmentVariable("CONSTR")
	opt.UseSqlServer("Data Source=.;Initial Catalog=EmreEraslan01;Integrated Security=True;TrustServerCertificate=True;");
}, ServiceLifetime.Transient, ServiceLifetime.Transient);


builder.Services.AddIdentity<User,Role>(opt =>
{
	// username and email
	opt.User.RequireUniqueEmail = true;
	//opt.User.AllowedUserNameCharacters = EFizibilConstants.KullaniciAdiIzinVerilenKarakterler;

	// password
	opt.Password.RequireDigit = true;
	opt.Password.RequireLowercase = true;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireUppercase = true;
	opt.Password.RequiredLength = 3;
	opt.Password.RequiredUniqueChars = 1;
})
	
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
