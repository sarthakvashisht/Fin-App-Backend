using API.DTOs;
using API.Models;

namespace API.IRepository
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetAll();
        Task<Comment?>GetById(int id);
        Task<Comment>Create(Comment comment);
        Task<Comment?>Delete(int id);
        Task<Comment?> Update(int id,Comment comment); 
    }
}
