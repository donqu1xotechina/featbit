using Application.Identity;

namespace Application.IntegrationTests.Identity;

[UsesVerify]
public class IdentityControllerTests : IClassFixture<IdentityApp>
{
    private readonly IdentityApp _app;

    public IdentityControllerTests(IdentityApp app)
    {
        _app = app;
    }

    [Fact]
    public async Task LoginByEmail_RequestValidation()
    {
        var request = new LoginByEmail();
        var response = await _app.PostAsync("/api/v1/identity/login-by-email", request, false);

        await Verify(response);
    }

    [Fact]
    public async Task LoginByEmail_Success()
    {
        var request = new LoginByEmail
        {
            Email = TestUser.Email,
            Password = TestUser.RealPassword
        };
        var response = await _app.PostAsync("/api/v1/identity/login-by-email", request, false);

        await Verify(response);
    }
}