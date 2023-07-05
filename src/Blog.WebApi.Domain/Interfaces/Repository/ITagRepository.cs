namespace Blog.WebApi.Domain.Interfaces.Repository
{
    public interface ITagRepository: IBaseRepository
    {

        Task<Tuple<bool, int?>> IsTagExists(string tagName);
    }
}
