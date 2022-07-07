using Funq;
using ServiceStack;
using NUnit.Framework;
using UnitTestDemo.ServiceModel;
using UnitTestDemo.Tests.AuthTests;

namespace UnitTestDemo.Tests2;

public partial class IntegrationTest
{
    public const string UserName = "user";
    public const string Password = "p@55word";
    public const string LoginUrl = "specialLoginPage.html";
    IServiceClient GetClientWithUserPassword()
    {
        return new JsonServiceClient(ListeningOn)
        {
            UserName = UserName,
            Password = Password
        };
    }

    const string BaseUri = "http://localhost:20001/";
    private readonly ServiceStackHost appHost;
    private string ListeningOn => "http://localhost:2000/";
    protected string WebHostUrl => "http://localhost:2000/";
    public virtual void Configure(Container container)
    {
    }
    public IntegrationTest()
    {
        appHost = new AuthAppHost(WebHostUrl, Configure)
            .Init()
            .Start(ListeningOn);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => appHost.Dispose();

    public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

    [Test]
    public void Does_work_with_BasicAuth()
    {
        try
        {
            var client = GetClientWithUserPassword();
            var request = new Secured { Name = "test" };
            var response = client.Send<SecureResponse>(request);
            Assert.That(response.Result, Is.EqualTo(request.Name));
        }
        catch (WebServiceException webEx)
        {
            Assert.Fail(webEx.Message);
        }
    }
}