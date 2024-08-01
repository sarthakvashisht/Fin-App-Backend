using API.DTOs;
using API.Heplers;
using API.Models;

namespace API.IRepository
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAll(QueryObject query);
        Task<Stock> GetById(int id);
        Task<Stock> UpdateStock(int id, UpdateStockDto updateStockDto);
        Task<Stock?> DeleteById(int id);
        Task<Stock?> GetBySymbol(string symbol);
        Task<Stock> Create(Stock createStockDto);
        Task<bool> StockExists(int id);
    }
}
