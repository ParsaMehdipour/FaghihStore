using System.Net.Http.Json;
using System.Text.Json;

namespace SH.Infrastructure.Services;

public class HttpClientService
{
    public string BaseAddress { get; set; }

    public HttpClient _httpClient { get; }

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public HttpClientService SetBaseAddress(string baseAddress)
    {
        ArgumentException.ThrowIfNullOrEmpty(baseAddress);

        BaseAddress = baseAddress;

        return this;
    }

    public async Task<TResponseModel> Send<TRequestModel, TResponseModel>(TRequestModel requestModel, string url, CancellationToken cancellationToken, JsonSerializerOptions jsonOptions = null)
    {
        _httpClient.BaseAddress = new Uri(BaseAddress);

        var requestJsonString = JsonSerializer.Serialize(requestModel);

        var result = await _httpClient.GetFromJsonAsync<TResponseModel>(requestUri: url + requestJsonString, jsonOptions, cancellationToken);

        return result;
    }

    public async Task<TResponseModel> Send<TResponseModel>(string url, CancellationToken cancellationToken, JsonSerializerOptions jsonOptions = null)
    {
        _httpClient.BaseAddress = new Uri(BaseAddress);

        var result = await _httpClient.GetFromJsonAsync<TResponseModel>(requestUri: url, jsonOptions, cancellationToken);

        return result;
    }

    public async Task<TResponseModel> Post<TRequestModel, TResponseModel>(TRequestModel requestModel, string url, CancellationToken cancellationToken, JsonSerializerOptions jsonOptions = null)
    {
        _httpClient.BaseAddress = new Uri(BaseAddress);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUri: url, requestModel, cancellationToken);

        response.EnsureSuccessStatusCode();

        TResponseModel result = await response.Content.ReadFromJsonAsync<TResponseModel>(jsonOptions, cancellationToken);

        return result;
    }
}
