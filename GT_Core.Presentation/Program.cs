using GT_Core.Application.Common.Interfaces;
using GT_Core.Infrastructure;
using GT_Core.Presentation.Services;
using NSwag;
using NSwag.Generation.Processors.Security;
using GT_Core.Infrastructure.Identity;
using GT_Core.Infrastructure.Persistence;
using GT_Core.Domain.Entities;
using TS_Core.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<TicketServiceClient>();
builder.Services.AddTransient<StatusServiceClient>();
builder.Services.AddTransient<SeverityServiceClient>();

builder.Services.AddControllersWithViews();

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
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();