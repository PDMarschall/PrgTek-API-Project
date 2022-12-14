using Microsoft.EntityFrameworkCore;
using PTAP.Core.Interfaces;
using PTAP.Infrastructure;
using PTAP.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<KanyeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KanyeContext") ?? throw new InvalidOperationException("Connection string 'KanyeContext' not found.")));
builder.Services.AddTransient<HttpClient>();
builder.Services.AddTransient<IKanyeClient, KanyeClientDefault>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}

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
    pattern: "{controller=Kanye}/{action=Index}/{id?}");

app.Run();
