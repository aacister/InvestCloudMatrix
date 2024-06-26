namespace InvestCloudMatrix.Console.Models;

public class MatrixRowColRequest
{
    public required string DataSet { get; set; }
    public required string Type { get; set; }
    public int Idx { get; set; }
}
