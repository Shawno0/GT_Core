using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Middleware;
using GT_Core.Domain.Entities;
using GT_Core.Infrastructure;
using GT_Core.Infrastructure.Identity;
using GT_Core.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentationInfrastructure(builder.Configuration);
//builder.Services.AddSingleton<EntityCache<string, ApplicationUser>>();
//builder.Services.AddSingleton<EntityCache<int, Ticket>>();
//builder.Services.AddSingleton<EntityCache<int, Status>>();
//builder.Services.AddSingleton<EntityCache<int, Severity>>();
builder.Services.AddSingleton<ITokenHandler, TokenHandler>();
builder.Services.AddTransient<UserServiceClient>();
builder.Services.AddTransient<TicketServiceClient>();
builder.Services.AddTransient<EntityServiceClient<int, Status>>();
builder.Services.AddTransient<EntityServiceClient<int, Severity>>();

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

app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();