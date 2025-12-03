using TradeCompanyIS.Application.Requests;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface IJwtProviderService
    {
        string? GenerateToken(JwtRequest request);
    }
}