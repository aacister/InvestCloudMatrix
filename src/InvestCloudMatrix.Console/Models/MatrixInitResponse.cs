namespace InvestCloudMatrix.Console.Models;

public class MatrixInitResponse
{
    public int Value { get; set; } = 0;
    public string Cause { get; set; } = string.Empty;
    public bool Success { get; set; } = false;
}