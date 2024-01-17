using Microsoft.EntityFrameworkCore;
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

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(configure =>
    configure.UseSqlite(
        builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<AccountService>(provider => new AccountService(
    provider.GetRequiredService<IAccountRepository>(),
    (password) => new Password(password)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();