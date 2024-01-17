using TbdFriends.WaterDrinkWater.Application.Values;

namespace application.tests;

// given a stored account password should not be plain text
public class when_registering_an_account
{
    [Fact]
    public void password_respects_hash_password_delegate()
    {
        // Arrange 

        var repository = Substitute.For<IAccountRepository>();

        var sut = new AccountService(repository,
            (password) => password.Reverse().ToString()!);

        // Act

        sut.Register("name", "email", "password");

        // Assert

        repository
            .Received()
            .Add(Arg.Is<Account>(x => x.Password == "password".Reverse().ToString()));
    }
}