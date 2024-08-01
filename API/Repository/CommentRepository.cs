using API.Data;
using API.DTOs;
using API.IRepository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CommentRepository : ICommentRepo
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> Create(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> Delete(int id)
        {
            var comment=await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return comment;
            }
            return null;
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _context.Comments.Include(a=>a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetById(int id)
        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);    
        }

        public async Task<Comment?> Update(int id, Comment comment)
        {
            var existingcomment= await _context.Comments.FindAsync(id);
            if(existingcomment==null)
            {
                return null;
            }
            existingcomment.Title = comment.Title;
            existingcomment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return existingcomment;
        }
    }
}
