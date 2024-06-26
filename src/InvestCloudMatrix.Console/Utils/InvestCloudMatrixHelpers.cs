using System.Data;
using InvestCloudMatrix.Console.Interfaces;

namespace InvestCloudMatrix.Console.Utils;

public class InvestCloudMatrixHelpers : IInvestCloudMatrixHelpers
{
    private readonly IInvestCloudMatrixPresenters _presenter;

    public InvestCloudMatrixHelpers(
        IInvestCloudMatrixPresenters presenter){
        _presenter = presenter;
    }

    public async Task FillDataSet(int[,] dataset, string ds, int size){
        for(var r=0; r<size; r++){
            await FillDatasetValues(dataset, ds, r, size);
        }
        
    }

    private async Task<int> FillDatasetValues(int[,] dataset, string ds, int r, int colSize){
        var rowsVal = await _presenter.MatrixRowColAsync(
                        ds,
                        InvestCloudMatrixConstants.DATASET_ROW,
                        r);
        var rowVal = rowsVal.Length > 0 ? rowsVal[0] : throw new Exception("Error retrieving row value.");
        for(var c=0; c<colSize; c++){
            var colVal = await RetrieveColVal(ds, c);
            if(colVal != null){
                //TODO: Realized I don't understand how the api is working
                dataset[r,c] = (int)colVal;
            }
        }
        return rowVal;
    }

    private  async Task<int?> RetrieveColVal(string ds, int c){
        var valsCol = await _presenter.MatrixRowColAsync(
                        ds,
                        InvestCloudMatrixConstants.DATASET_COL,
                        c);
        return valsCol.Length > 0 ?  valsCol[0] : null;
    }

}

