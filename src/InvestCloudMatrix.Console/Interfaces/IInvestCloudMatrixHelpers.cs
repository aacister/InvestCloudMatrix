namespace InvestCloudMatrix.Console.Interfaces;

public interface IInvestCloudMatrixHelpers
{
    Task FillDataSet(int[,] dataset, string ds, int size);
    
}
