using API.Models;

namespace API.IRepository
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> Create(Portfolio portfolio);
        Task<Portfolio> Delete(AppUser user, string symbol);
    }
}
