using InvestCloudMatrix.Console.Interfaces;
using InvestCloudMatrix.Console.Proxies;
using InvestCloudMatrix.Console.Presenters;
using InvestCloudMatrix.Console.Utils;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace InvestCloudMatrix.Console;

public class Application : IApplication
{
    private readonly IInvestCloudMatrixPresenters _presenter;
    private readonly IInvestCloudMatrixProxies _proxy;
    private readonly IInvestCloudMatrixHelpers _helper;
    private readonly ILogger _logger;
    private readonly IConfiguration _config;
    private int _dsSize = 1000;

    public Application(
        IInvestCloudMatrixProxies proxy,
    IInvestCloudMatrixPresenters presenter,
    IInvestCloudMatrixHelpers helper,
    IConfiguration config,
    ILogger logger)
    {
        _presenter = presenter;
        _proxy = proxy;
        _helper = helper;
        _logger = logger;
        _config = config;
        int.TryParse(_config[InvestCloudMatrixConstants.DATA_SET_SIZE], out _dsSize);
    }
    public async Task Run()
    {
        var passphrase = string.Empty;
        var dataSetASize = await _presenter.MatrixInitAsync(_dsSize);
        var dataSetBSize = await _presenter.MatrixInitAsync(_dsSize);

        if (dataSetASize > 0 && dataSetBSize > 0)
        {
            var dataSetA = new int[dataSetASize, dataSetASize];
            var dataSetB = new int[dataSetBSize, dataSetBSize];

            //Fill datasets
            await _helper.FillDataSet(dataSetA, InvestCloudMatrixConstants.DATASET_A, dataSetASize);
            await _helper.FillDataSet(dataSetB, InvestCloudMatrixConstants.DATASET_B, dataSetBSize);

            //Multiply matrices
            var productMatrix = InvestCloudUtils.MultiplyMatrices(dataSetASize, dataSetA, dataSetB);

            //Concatenate matrix
            StringBuilder sb = new StringBuilder();
            for (var r = 0; r < productMatrix.Length; r++)
            {
                for (var c = 0; c < productMatrix.Length; c++)
                {
                    sb.Append(productMatrix[r, c]);
                }
            }

            var strProductMatrix = sb.ToString();

            //Validate
            passphrase = await _presenter.MatrixValidateAsync(strProductMatrix);
            _logger.LogInformation("Passphrase: " + passphrase);
        }

    }
}
