using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Text;

namespace Advance.NET7.MinimalApi.Utility
{
    public class HtmlResult : IResult
    {
        private readonly string _html;
        public HtmlResult(string html) 
        {
            _html = html;   
        }

        /// <summary>
        /// 带有HttpContext参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Html;
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_html);
            return httpContext.Response.WriteAsJsonAsync(_html);
        }
    }
}
