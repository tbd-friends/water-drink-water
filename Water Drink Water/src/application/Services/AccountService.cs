using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Values;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Application.Services;

public delegate string HashPasswordDelegate(string password);

public class AccountService(IAccountRepository repository, 
    HashPasswordDelegate hashPassword)
{
    public Result Register(string name, string email, string password)
    {
        var account = repository.GetByEmail(email);

        if (account is not null)
        {
            return Result.Conflict();
        }

        repository.Add(new Account
        {
            Name = name,
            Email = email,
            Password = hashPassword(password)
        });

        return Result.Success();
    }
}