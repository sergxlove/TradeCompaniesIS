namespace TradeCompanyIS.DataAccess.Postgres.Response
{
    public class TableDataResponse
    {
        public List<Dictionary<string, object>> Rows { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
