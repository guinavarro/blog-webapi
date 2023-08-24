using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi.Infra.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _context.Users.Where(_ => _.Email.ToUpper() == email.Trim().ToUpper())
                .FirstOrDefaultAsync();
            // TODO: Validar retorno

            return user;
        }
    }
}
