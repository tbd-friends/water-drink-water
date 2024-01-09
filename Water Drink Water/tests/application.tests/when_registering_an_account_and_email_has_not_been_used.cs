namespace application.tests;

// given_an_account_and_email_has_not_been_used
public class when_registering_an_account_and_email_has_not_been_used
{
    [Fact]
    public void account_is_registered()
    {
        // Arrange

        var repository = Substitute.For<IAccountRepository>();

        var sut = new AccountService(repository);

        // Act

        sut.Register("name", "email", "password");

        // Assert 

        repository
            .Received()
            .Add(Arg.Is<Account>(x => x.Email == "email" && x.Name == "name" && !string.IsNullOrEmpty(x.Password)));
    }

    [Fact]
    public void success_is_returned()
    {
        // Arrange

        var repository = Substitute.For<IAccountRepository>();

        var sut = new AccountService(repository);

        // Act

        var result = sut.Register("name", "email", "password");

        // Assert 

        Assert.True(result.IsSuccess);
    }
}