using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Contracts;

public interface IAccountRepository
{
    void Add(Account account);

    Account? GetByEmail(string email);
}