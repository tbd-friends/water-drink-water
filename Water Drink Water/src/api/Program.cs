using System.Text;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TbdFriends.WaterDrinkWater.Application.Services;
using TbdFriends.WaterDrinkWater.Application.Values;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFastEndpoints();

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(configure =>
    configure.UseSqlite(
        builder.Configuration.GetConnectionString("default")));

builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<AccountService>(provider => new AccountService(
    provider.GetRequiredService<IAccountRepository>(),
    (password) => new Password(password)));

builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<JwtService>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["auth:issuer"],
            ValidAudience = builder.Configuration["auth:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["auth:audience"]!))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseFastEndpoints();

app.Run();