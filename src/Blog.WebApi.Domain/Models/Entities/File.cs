using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Domain.Models.Entities
{
    public sealed class File : Base
    {
        public string Name { get; private set; }
        public byte[] Data { get; private set; }
        public string ContentType { get; private set; }
    }
}
