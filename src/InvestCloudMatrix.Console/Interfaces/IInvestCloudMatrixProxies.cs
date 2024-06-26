using InvestCloudMatrix.Console.Models;

namespace InvestCloudMatrix.Console.Interfaces;

public interface IInvestCloudMatrixProxies
{
    Task<int> MatrixInitAsync(int size);
    Task<int[]> MatrixRowColAsync(string ds, string type, int idx);
    Task<string> MatrixValidateAsync(string str);
    
}
