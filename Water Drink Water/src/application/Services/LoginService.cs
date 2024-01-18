using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Values;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Application.Services;

public class LoginService(IAccountRepository accountRepository)
{
    public Result<string> Login(string email, string password)
    {
        var account = accountRepository.GetByEmail(email);

        if (account is null || !IsPasswordValid(account, password))
        {
            return Result.Forbidden();
        }

        var session = accountRepository.GetLoginSession(account.Email);

        if (session is not null)
        {
            return Result.Success(session.Token);
        }

        var newSession = new LoginSession
        {
            Token = Guid.NewGuid().ToString(),
            AccountId = account.Id,
            Created = DateTime.UtcNow,
            Expiration = DateTime.UtcNow.AddHours(1)
        };

        accountRepository.AddLoginSession(newSession);

        return Result.Success(newSession.Token);
    }

    private static bool IsPasswordValid(Account account, string password)
    {
        return Password.Compare(password, account.Password);
    }
}