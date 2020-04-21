using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Mock of the <see cref="HttpMessageHandler" />.
/// </summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    /// <summary>
    /// The response to return.
    /// </summary>
    private readonly string response;
    
    /// <summary>
    /// The <see cref="HttpStatusCode" /> to return.
    /// </summary>
    private readonly HttpStatusCode statusCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockHttpMessageHandler" /> class.
    /// </summary>
    /// <param name="statusCode">The <see cref="HttpStatusCode" /> to return.</param>
    /// <param name="response">A string containing the response to return.</param>
    public MockHttpMessageHandler(HttpStatusCode statusCode, string response)
    {
        this.statusCode = statusCode;
        this.response = response;
    }

    /// <summary>
    /// Mocks the <see cref="HttpMessageHandler.SendAsync" /> method,
    /// ignoring the specified request and returning an <see cref="HttpResponseMessage" />
    /// for the status code.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage" /> containing the request. Ignored.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /> containing the cancellation token. Ignored.</param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Create the response message using the specified code
        HttpResponseMessage responseMessage = new HttpResponseMessage {
            StatusCode = this.statusCode
        };

        // If the status code is 200 Ok, set the response
        // content, otherwise set the reason phrase
        if (this.statusCode == HttpStatusCode.OK) {
            responseMessage.Content = new StringContent(this.response);
        }
        else {
            responseMessage.ReasonPhrase = this.response;
        }

        return responseMessage;
    }
}