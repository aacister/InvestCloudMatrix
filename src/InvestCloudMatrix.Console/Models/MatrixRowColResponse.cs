namespace InvestCloudMatrix.Console.Models;

public class MatrixRowColResponse
{
    public int[] Value { get; set; } = [];
    public string Cause { get; set; } = string.Empty;
    public bool Success { get; set; } = false;
}