using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{

    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

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

using var scope = app.Services.CreateScope();
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var userManager= scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
identityDbContext.Database.Migrate();

if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser() {UserName = "admin",Email = "admin@admin.com",PictureUrl = "/userpictures/aopicture.jpg",Description = "Örnek Açıklama Metnidir!"},"Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "admin2", Email = "admin2@admin.com", PictureUrl = "/userpictures/aopicture.jpg", Description = "Örnek Açıklama Metnidir!" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "admin3", Email = "admin3@admin.com", PictureUrl = "/userpictures/aopicture.jpg", Description = "Örnek Açıklama Metnidir!" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "admin4", Email = "admin4@admin.com", PictureUrl = "/userpictures/aopicture.jpg", Description = "Örnek Açıklama Metnidir!" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "admin5", Email = "admin5@admin.com", PictureUrl = "/userpictures/aopicture.jpg", Description = "Örnek Açıklama Metnidir!" }, "Password12*").Wait();
}
 
app.Run();
