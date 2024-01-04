using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Contexts.EfCoreApp;
using Emreraslan.DataAccess.Repos.Abstract;
using Emreraslan.DataAccess.Repos.Concrete;
using Emreraslan.Services.Abstract;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

#region Services

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DataSeedingService>();
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddSingleton(typeof(IGenericRepo<Product>), serviceProvider =>
{
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Now you can use dbContext within this scope
        return new GenericRepo<Product>(dbContext);
    }    
});

builder.Services.AddSingleton(typeof(IGenericRepo<Vendor>), serviceProvider =>
{
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Now you can use dbContext within this scope
        return new GenericRepo<Vendor>(dbContext);
    }
});
builder.Services.AddSingleton(typeof(IGenericRepo<Order>), serviceProvider =>
{
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Now you can use dbContext within this scope
        return new GenericRepo<Order>(dbContext);
    }
});
builder.Services.AddSingleton(typeof(IGenericRepo<Category>), serviceProvider =>
{
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Now you can use dbContext within this scope
        return new GenericRepo<Category>(dbContext);
    }
});

builder.Services.AddDistributedMemoryCache(); // Use in-memory cache for session storage

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

#endregion


builder.Services.AddDbContext<AppDbContext>(opt =>
{
	//Environment.GetEnvironmentVariable("CONSTR")
	opt.UseSqlServer("Data Source=.;Initial Catalog=EmreEraslanDb;Integrated Security=True;TrustServerCertificate=True;");
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

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

//using (var scope = app.Services.CreateScope())
//{
//    var dataSeedingService = scope.ServiceProvider.GetRequiredService<DataSeedingService>();

//    await dataSeedingService.SeedRolesAsync();
//    await dataSeedingService.SeedAdminUserAsync();
//}


app.Run();
