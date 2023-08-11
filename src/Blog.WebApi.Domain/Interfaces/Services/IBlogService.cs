﻿using Blog.WebApi.Domain.Models;
using Blog.WebApi.Domain.Models.ViewModels;

namespace Blog.WebApi.Domain.Interfaces.Services
{
    public interface IBlogService
    {
        Task<Return<bool>> Post(CreatePostViewModel model);
        Task<Return<PostViewModel>> GetPostById(Guid key);
    }
}
