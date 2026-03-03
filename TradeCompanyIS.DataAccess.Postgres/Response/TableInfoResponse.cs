namespace TradeCompanyIS.DataAccess.Postgres.Response
{
    public class TableInfoResponse
    {
        public string Name { get; set; } = string.Empty;
        public int Rows { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
