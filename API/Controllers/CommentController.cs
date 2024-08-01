using API.DTOs;
using API.Extensions;
using API.IRepository;
using API.Mapper;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    { private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepo commentRepo,IStockRepo stockRepo, UserManager<AppUser> userManager) 
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }
        // GET: api/<CommentController>
        [HttpGet]
        public async Task<IActionResult>Get()
        {
            var comment= await _commentRepo.GetAll();
            var commentModel = comment.Select(x => x.ToCommenDto());
            return Ok(commentModel);
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment=await _commentRepo.GetById(id);
            if(comment == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(comment.ToCommenDto());
            }
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Post(int id,[FromBody] CreateCommentDto commentDto)
        {
            if(! await _stockRepo.StockExists(id))
            {
                return BadRequest();
            }
            //getting commenter username
            var username = User.GetUsername();
            var appUser= await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommenFromCretaeDto(id);
            //attaching commenter id to comment
            commentModel.AppUserId = appUser.Id;

            var comment=await _commentRepo.Create(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommenDto());

        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCommentDto commentDto)
        {
            var comment= await _commentRepo.Update(id,commentDto.ToCommenFromUpdate());
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommenDto());
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment=await _commentRepo.Delete(id);
            if(comment == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(comment);
            }
        }
    }
}
