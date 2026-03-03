using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface ITableService
    {
        Task<TableDataResponse> GetTableDataAsync(string tableName, CancellationToken token);
        Task<List<TableInfoResponse>> GetTablesAsync(CancellationToken token);
    }
}