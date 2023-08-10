using System.Text;
using System.Text.RegularExpressions;

namespace Blog.WebApi.Domain.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string TransformToLowerCase(this string str)
        {
            string withoutAccents = RemoveAccents(str.ToLower());
            return withoutAccents.Replace(" ", "_");
        }
        public static string RemoveAccents(this string str)
        {
            string normalized = str.Normalize(NormalizationForm.FormD);
            string withoutAccents = Regex.Replace(normalized, @"[^a-zA-Z0-9_]", "");
            return withoutAccents;
        }
    }
}
