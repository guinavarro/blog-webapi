using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Models;
using Blog.WebApi.Domain.Models.Entities;
using Blog.WebApi.Domain.Models.ViewModels;

namespace Blog.WebApi.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorRepository _authorRepository;

        public UserService(IUserRepository userRepository, IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<User> FindUserByEmail(string email) => await _userRepository.FindUserByEmail(email);
        public async Task<Author> FindAuthorByName(string name) => await _authorRepository.FindAuthorByName(name);

        public async Task<Return<bool>> Register(User user)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var author = new Author(user.UserName);

                _userRepository.Add(user);
                _authorRepository.Add(author);
                await _userRepository.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return new Return<bool>(true, "Usuário cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Return<bool>(false, "Houve um erro na hora de registrar o Usuário");
            }

        }
    }
}
