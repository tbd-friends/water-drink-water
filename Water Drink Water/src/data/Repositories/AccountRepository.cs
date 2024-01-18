using Microsoft.EntityFrameworkCore;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Repositories;

public class AccountRepository(IDbContextFactory<ApplicationDbContext> factory) : IAccountRepository
{
    public void Add(Account account)
    {
        using var context = factory.CreateDbContext();

        context.Accounts.Add(account);

        context.SaveChanges();
    }

    public Account? GetByEmail(string email)
    {
        using var context = factory.CreateDbContext();

        return context.Accounts.FirstOrDefault(a => a.Email == email);
    }

    public LoginSession? GetLoginSession(string email)
    {
        using var context = factory.CreateDbContext();

        return (from a in context.Accounts
            join ls in context.LoginSessions on a.Id equals ls.AccountId
            where a.Email == email
            select ls).FirstOrDefault();
    }

    public void AddLoginSession(LoginSession loginSession)
    {
        using var context = factory.CreateDbContext();

        context.LoginSessions.Add(loginSession);

        context.SaveChanges();
    }
}