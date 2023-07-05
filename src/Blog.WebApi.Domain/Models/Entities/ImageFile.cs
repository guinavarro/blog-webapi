using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.WebApi.Domain.Models.Entities
{
    public class ImageFile : Base
    {
        public string Name { get; private set; }
        public byte[] Data { get; private set; }
        public string ContentType { get; private set; }

        public ImageFile(string name, byte[] data, string contentType, Guid key) : base(key)
        {
            Name = name;
            Data = data;
            ContentType = contentType;
        }
    }
}
