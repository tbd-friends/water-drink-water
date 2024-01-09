using TbdFriends.WaterDrinkWater.Application.Values;

namespace application.tests;

// given a stored account password should not be plain text
public class when_registering_an_account
{
    [Fact]
    public void password_is_not_stored_in_plain_text()
    {
        // Arrange 

        var repository = Substitute.For<IAccountRepository>();

        var sut = new AccountService(repository, 
            (password) => new Password(password));

        // Act

        sut.Register("name", "email", "password");

        // Assert

        repository
            .Received()
            .Add(Arg.Is<Account>(x => x.Password != "password"));
    }
}