using System.Net.Http.Headers;
using InvestCloudMatrix.Console.Models;
using InvestCloudMatrix.Console.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace InvestCloudMatrix.Console.Proxies;

public class InvestCloudMatrixProxies : IInvestCloudMatrixProxies
{
    private readonly ILogger _logger;
    private readonly IConfiguration _config;
    private readonly string uri = "";
    
    public InvestCloudMatrixProxies(
        ILogger logger,
        IConfiguration config
    )
    {
        _logger = logger;
        _config = config;
        uri = _config[InvestCloudMatrixConstants.INVEST_CLOUD_API_URI] ?? "";
    }
    public async Task<int> MatrixInitAsync(int size)
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                AddHttpClientHeaders(client);
                MatrixInitResponse resp = new MatrixInitResponse();
                var apiPath = "api/numbers/init/" + size;
                HttpResponseMessage response = await client.GetAsync(apiPath);
                if (response.IsSuccessStatusCode)
                {
                    resp = await response.Content.ReadAsAsync<MatrixInitResponse>();
                }
                return resp.Success ? resp.Value : throw new Exception("Failed: " + resp.Cause);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int[]> MatrixRowColAsync(string ds, string type, int idx)
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                AddHttpClientHeaders(client);
                MatrixRowColResponse resp = new MatrixRowColResponse();
                var apiPath = "api/numbers/" + ds + "/" + type + "/" + idx;
                HttpResponseMessage response = await client.GetAsync(apiPath);
                if (response.IsSuccessStatusCode)
                {
                    resp = await response.Content.ReadAsAsync<MatrixRowColResponse>();
                }
                return resp.Success ? resp.Value : throw new Exception("Failed: " + resp.Cause);
            }
        }
        catch (Exception) { throw; }


    }

    public async Task<string> MatrixValidateAsync(string hash)
    {
        try
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                AddHttpClientHeaders(client);
                var resp = new MatrixValidateResponse();
                var apiPath = "api/numbers/validate";
                HttpResponseMessage response = await client.PostAsJsonAsync(
                apiPath, hash);
                if (response.IsSuccessStatusCode)
                {
                    resp = await response.Content.ReadAsAsync<MatrixValidateResponse>();
                }
                return resp.Success ? resp.Value : throw new Exception("Failed: " + resp.Cause);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    

    private static void AddHttpClientHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

}