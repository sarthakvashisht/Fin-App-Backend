using API.Data;
using API.DTOs;
using API.Heplers;
using API.IRepository;
using API.Mapper;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StockRepository : IStockRepo
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> Create(Stock stockDto)
        {
            
            await _context.Stocks.AddAsync(stockDto);
            await _context.SaveChangesAsync();
            return stockDto;
        }

        public async Task<Stock?> DeleteById(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAll(QueryObject query)
        {
            var stock= _context.Stocks.Include(x => x.Comments).ThenInclude(a=>a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock=stock.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.Sortby))
            {
                if(query.Sortby.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stock=query.IsDescending?stock.OrderByDescending(s=>s.Symbol):stock.OrderBy(s=>s.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            //var stockModel = stock.Select(s => s.ToStockDto());

        }

        public async Task<Stock> GetById(int id)
        {
            var stock=await _context.Stocks.Include(x=>x.Comments).FirstOrDefaultAsync(x => x.Id == id);
            if (stock != null)
            {
                return stock;
            }
            else
            {
                return null;
            }
        }

        public async Task<Stock?> GetBySymbol(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(x=>x.Id == id);
        }

        public async Task<Stock> UpdateStock(int id, UpdateStockDto updateStockDto)
        {
            var stock=await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                stock.Symbol = updateStockDto.Symbol;
                stock.MarketCap = updateStockDto.MarketCap;
                stock.Purchase = updateStockDto.Purchase;
                stock.Industry = updateStockDto.Industry;
                stock.CompanyName = updateStockDto.CompanyName;
                stock.LastDiv = updateStockDto.LastDiv;
                await _context.SaveChangesAsync();
                return stock;
            }
            else
            {
                return null;
            }
        }
    }
}
