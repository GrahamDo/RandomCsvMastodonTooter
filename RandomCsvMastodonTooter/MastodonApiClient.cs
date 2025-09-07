using System.Text;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace RandomCsvMastodonTooter;

internal class MastodonApiClient
{
    private RestClient _restClient = null!;

    private void InitialiseClient(string instanceUrl, string token)
    {
        if (string.IsNullOrEmpty(instanceUrl))
            throw new ApplicationException("Missing Mastodon instance URL");
        if (string.IsNullOrEmpty(token))
            throw new ApplicationException("Missing Mastodon token");

        var baseUrl = BuildBaseUrl(instanceUrl);
        var options = new RestClientOptions(baseUrl)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
        };
        _restClient = new RestClient(options);
    }    
    
    private static string BuildBaseUrl(string instanceUrl)
    {
        var baseUrlBuilder = new StringBuilder();
        if (!instanceUrl.StartsWith("https://"))
            baseUrlBuilder.Append("https://");
        baseUrlBuilder.Append(instanceUrl);
        if (!instanceUrl.EndsWith('/'))
            baseUrlBuilder.Append('/');
        baseUrlBuilder.Append("api/v1/");
        return baseUrlBuilder.ToString();
    }    
    
    public async Task Post(string instanceUrl, string token, string tootContent)
    {
        InitialiseClient(instanceUrl, token);
        var status = new MastodonStatus
        {
            Status = tootContent
        };
        var request = new RestRequest("statuses", Method.Post).AddJsonBody(status);
        try
        {
            await _restClient.PostAsync(request);
        }
        catch (HttpRequestException ex)
        {
            if (ex.Message.Contains("Forbidden"))
                throw new ApplicationException("Invalid Mastodon token");

            throw;
        }
    }
}