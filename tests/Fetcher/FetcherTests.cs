using System.Net.Http;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace GilGoblin.Tests.Fetcher;

public class FetcherTests
{
    protected HttpClient _client;
    protected MockHttpMessageHandler _handler;

    protected const string contentType = "application/json";

    [SetUp]
    public virtual void SetUp()
    {
        _handler = new MockHttpMessageHandler();
        _client = _handler.ToHttpClient();
    }
}
