using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebBanGiayTheThao.Data;
using WebBanGiayTheThao.Services.SanPham;
using WebBanGiayTheThao.Services.SlideShow;
using WebBanGiayTheThao.Services;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
// Ðãng kí service cho User DuyKhang 8:50 08/01/2026
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<WebBanGiayTheThao.Services.IThuongHieuService, WebBanGiayTheThao.Services.ThuongHieuService>();
builder.Services.AddScoped<WebBanGiayTheThao.Services.IVoucherServices, WebBanGiayTheThao.Services.VoucherServices>();

builder.Services.AddDbContext<QuanLyWebBanGiayContext>(options
  => options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext"))
  );
builder.Services.AddScoped<ISanPhamService,SanPhamService>();
builder.Services.AddScoped<ISlideShowService,SlideShowService>();
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
