﻿namespace Blog.WebApi.Domain.Models.Entities
{
    public class User : Base
    {
        public string Email  { get; private set; }
        public string Password { get; private set; }
    }
}
