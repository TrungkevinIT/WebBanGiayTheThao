using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Services.SanPham;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<QuanLyWebBanGiayContext>(options
  => options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext"))
  );
builder.Services.AddScoped<ISanPhamSevice,SanPhamSevice>();
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
      name: "areas",
      pattern: "{area:exists}/{controller=QuanLySanPham}/{action=TrangQLSanPham}/{id?}"); 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=TrangChu}/{id?}");

app.Run();
