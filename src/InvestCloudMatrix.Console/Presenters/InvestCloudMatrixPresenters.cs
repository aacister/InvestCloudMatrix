using InvestCloudMatrix.Console.Interfaces;
using InvestCloudMatrix.Console.Models;
using InvestCloudMatrix.Console.Utils;
using Microsoft.Extensions.Logging;

namespace InvestCloudMatrix.Console.Presenters;

public class InvestCloudMatrixPresenters : IInvestCloudMatrixPresenters
{
    private readonly ILogger _logger;
    private readonly IInvestCloudMatrixProxies _proxies;
    public InvestCloudMatrixPresenters(
        IInvestCloudMatrixProxies proxies,
    ILogger logger)
    {
        _proxies = proxies;
        _logger = logger;
    }
    public Task<int> MatrixInitAsync(int size)
    {
        try
        {
            return _proxies.MatrixInitAsync(size);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public Task<int[]> MatrixRowColAsync(string ds, string type, int idx)
    {
        try
        {
            return _proxies.MatrixRowColAsync(ds, type, idx);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public Task<string> MatrixValidateAsync(string str)
    {
        try
        {
            var hash = InvestCloudUtils.CreateMD5Hash(str);
            var passphrase = _proxies.MatrixValidateAsync(hash);
            _logger.LogInformation("Passphrase: " + passphrase);
            return passphrase;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}
