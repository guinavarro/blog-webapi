using Blog.WebApi.Domain.Interfaces;
using Blog.WebApi.Domain.Interfaces.Repository;
using Blog.WebApi.Domain.Interfaces.Services;
using Blog.WebApi.Domain.Services;
using Blog.WebApi.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.WebApi.Infra
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagsPostRepository, TagsPostRepository>();
            services.AddScoped<IImageFileRepository, ImageFileRepository>();

            return services;
        }
    }
}
