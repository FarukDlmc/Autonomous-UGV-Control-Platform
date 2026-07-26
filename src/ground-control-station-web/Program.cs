using Microsoft.EntityFrameworkCore;
using WebAutonomousControlStation.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5255"); //Web sunucusunun dış dünyaya açılacağı kapı 

// PostgreSQL Veritabanı Bağlantısı
builder.Services.AddDbContext<WebAutonomousControlStation.Models.BalkarIkaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BalkarIkaDbContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:5255");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapHub<GcsHub>("/gcsHub"); //arayüzün bu adreslere bağlanarak sayfayı yenilemeden verileri akıtmasını sağlar.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}")
    .WithStaticAssets();

app.MapHub<TelemetryHub>("/telemetryHub"); //arayüzün bu adreslere bağlanarak sayfayı yenilemeden verileri akıtmasını sağlar.

app.Run();