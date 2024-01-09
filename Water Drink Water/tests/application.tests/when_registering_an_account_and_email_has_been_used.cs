using Ardalis.Result;

namespace application.tests;

public class when_registering_an_account_and_email_has_been_used
{
    [Fact]
    public void account_is_not_registered()
    {
        // Arrange 

        var repository = Substitute.For<IAccountRepository>();

        repository
            .GetByEmail("email")
            .Returns(new Account());

        var sut = new AccountService(repository);

        // Act

        sut.Register("name", "email", "password");

        // Assert

        repository
            .DidNotReceive()
            .Add(Arg.Any<Account>());
    }

    [Fact]
    public void conflict_is_returned()
    {
        // Arrange

        var repository = Substitute.For<IAccountRepository>();
        
        repository
            .GetByEmail("email")
            .Returns(new Account());

        var sut = new AccountService(repository);

        // Act

        var result = sut.Register("name", "email", "password");

        // Assert

        Assert.Equal(ResultStatus.Conflict, result.Status);
    }
}