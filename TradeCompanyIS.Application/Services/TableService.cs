using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.Application.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _repository;
        public TableService(ITableRepository repository)
        {
            _repository = repository;
        }

        public async Task<TableDataResponse> GetTableDataAsync(string tableName, CancellationToken token)
        {
            return await _repository.GetTableDataAsync(tableName, token);
        }
        public async Task<List<TableInfoResponse>> GetTablesAsync(CancellationToken token)
        {
            return await _repository.GetTablesAsync(token);
        }
    }
}
