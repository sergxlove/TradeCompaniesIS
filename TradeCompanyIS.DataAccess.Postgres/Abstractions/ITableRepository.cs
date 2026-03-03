using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface ITableRepository
    {
        Task<TableDataResponse> GetTableDataAsync(string tableName, CancellationToken token);
        Task<List<TableInfoResponse>> GetTablesAsync(CancellationToken token);
    }
}