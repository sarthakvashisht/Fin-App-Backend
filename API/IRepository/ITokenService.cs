using API.Models;

namespace API.IRepository
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
