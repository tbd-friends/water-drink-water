using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Values;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Application.Services;

public class LoginService(
    IAccountRepository accountRepository,
    JwtService tokenService)
{
    public Result<string> Login(string email, string password)
    {
        var account = accountRepository.GetByEmail(email);

        if (account is null || !IsPasswordValid(account, password))
        {
            return Result.Forbidden();
        }

        var token = tokenService.GenerateToken($"wdw|{account.Id}", account.Email);

        return Result.Success(token);
    }

    private static bool IsPasswordValid(Account account, string password)
    {
        return Password.Compare(password, account.Password);
    }
}