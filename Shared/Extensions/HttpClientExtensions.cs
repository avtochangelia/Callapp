using System.Net.Http.Json;

namespace Shared.Extensions;

public static class HttpClientExtensions
{
    public static Task<T> GetFromJsonWithRetryAsync<T>(this HttpClient client, string uri, TimeSpan? timeOut = null, int retryLeft = 3)
    {
        try
        {
            return client.TrySetTimeout(timeOut).GetFromJsonAsync<T>(uri)!;
        }
        catch
        {
            if (retryLeft > 0)
            {
                return client.GetFromJsonWithRetryAsync<T>(uri, timeOut, retryLeft - 1);
            }

            throw;
        }
    }

    public static HttpClient TrySetTimeout(this HttpClient client, TimeSpan? timeOut)
    {
        try
        {
            if (timeOut.HasValue)
            {
                client.Timeout = timeOut.Value;
            }
        }
        catch (Exception)
        {
            //ignored
        }

        return client;
    }
}