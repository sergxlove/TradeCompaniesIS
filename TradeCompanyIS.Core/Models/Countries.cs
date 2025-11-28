namespace TradeCompanyIS.Core.Models
{
    public class Countries
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;

        public static ResultModel<Countries> Create(Guid id, string name)
        {
            return ResultModel<Countries>.Success(new Countries(id, name));
        }

        private Countries(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
