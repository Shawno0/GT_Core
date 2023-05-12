using GT_Core.API.Services;
using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Middleware;
using GT_Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAPIInfrastructure(builder.Configuration);
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<TicketService>();
builder.Services.AddTransient<SeverityService>();
builder.Services.AddTransient<StatusService>();
builder.Services.AddTransient<CommentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();