using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly TradeCompanyDbContext _context;
        public TableRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<List<TableInfoResponse>> GetTablesAsync(CancellationToken token)
        {
            List<TableInfoResponse> tableInfos = new List<TableInfoResponse>();
            var tables = await _context.Database.SqlQueryRaw<string>(
                "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'").ToListAsync(token);

            foreach (var tableName in tables)
            {
                try
                {
                    var countQuery = $"SELECT COUNT(*) FROM \"{tableName}\"";
                    var count = await _context.Database.SqlQueryRaw<int>(countQuery).FirstOrDefaultAsync(token);
                    var sizeQuery = $"SELECT pg_size_pretty(pg_total_relation_size('\"{tableName}\"'))";
                    var size = await _context.Database.SqlQueryRaw<string>(sizeQuery).FirstOrDefaultAsync(token) ?? "0 KB";

                    tableInfos.Add(new TableInfoResponse
                    {
                        Name = tableName,
                        Rows = count,
                        Size = size,
                        Owner = "postgres"
                    });
                }
                catch
                {
                    continue;
                }
            }
            return tableInfos;
        }

        public async Task<TableDataResponse> GetTableDataAsync(string tableName, CancellationToken token)
        {
            var response = new TableDataResponse();

            try
            {
                var dataQuery = $"SELECT * FROM \"{tableName}\"";

                var rows = new List<Dictionary<string, object>>();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = dataQuery;
                    await _context.Database.OpenConnectionAsync(token);

                    using (var result = await command.ExecuteReaderAsync(token))
                    {
                        while (await result.ReadAsync(token))
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < result.FieldCount; i++)
                            {
                                var value = result.GetValue(i);
                                row[result.GetName(i)] = value;
                            }
                            rows.Add(row);
                        }
                    }
                }

                response.Rows = rows;
                response.Total = rows.Count;
                response.Page = 1;
                response.PageSize = rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении данных из таблицы {tableName}: {ex.Message}");
            }

            return response;
        }
    }
}
