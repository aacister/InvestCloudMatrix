using System.Security.Cryptography;
using System.Text;

namespace InvestCloudMatrix.Console.Utils;

public static class InvestCloudUtils
{

    public static string CreateMD5Hash(string clearText)
    {
        using var myHash = MD5.Create();

        var bytes = Encoding.UTF8.GetBytes(clearText);
        var hash = myHash.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
    public static int[,] MultiplyMatrices(int size, int[,] datSetA, int[,] dataSetB)
    {
        var dataSetResult = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                dataSetResult[i, j] = 0;
                for (int k = 0; k < size; k++)
                {
                    dataSetResult[i, j] += datSetA[i, k] * dataSetB[k, j];
                }
            }
        }
        return dataSetResult;
    }
}