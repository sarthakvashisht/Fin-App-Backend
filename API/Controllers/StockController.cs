using API.Data;
using API.DTOs;
using API.Heplers;
using API.IRepository;
using API.Mapper;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;
        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }
        // GET: api/<StockController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stock = await _stockRepo.GetAll(query);
            var stockDto=stock.Select(x=>x.ToStockDto()).ToList();
            return Ok(stockDto);
        }

       // GET api/<StockController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var stock= await _stockRepo.GetById(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        // POST api/<StockController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStockDto stockDto)
        {
            var stock=  stockDto.ToStockFromCreateDto();
            await _stockRepo.Create(stock);
            return Ok(stock.ToStockDto());
        }

        // PUT api/<StockController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UpdateStockDto stockModel)
        {
            var stock=await _stockRepo.UpdateStock(id,stockModel);
            if(stock != null)
            {
                return Ok(stock);
            }
            else
            {
                return NotFound();
            }
            
        }

        // DELETE api/<StockController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteById(id);
            if (stock != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
