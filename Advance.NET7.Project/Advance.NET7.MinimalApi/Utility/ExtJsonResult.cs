using System.Net.Mime;
using System.Text;

namespace Advance.NET7.MinimalApi.Utility
{
    public class ExtJsonResult
    {
        private readonly object _obj;
        public ExtJsonResult(object obj)
        {
            _obj = obj;
        }

        /// <summary>
        /// 自定义扩展返回JSON
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ExecuteAsync(HttpContext httpContext)
        {
            string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(_obj);

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(jsonResult);
            return httpContext.Response.WriteAsJsonAsync(jsonResult);
        }
    }
}
