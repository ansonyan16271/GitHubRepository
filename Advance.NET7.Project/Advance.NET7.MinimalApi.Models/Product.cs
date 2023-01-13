using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advance.NET7.MinimalApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public List<string>? ProductList { get; set; }

        /// <summary>
        /// 接受一个字符串参数--Get请求是可以传递的---TryParse方法--就是用来把字符串中的数据绑定到实体中的属性中去
        /// </summary>
        /// <param name="productStr"></param>
        /// <param name="provider"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool TryParse(string? productStr, IFormatProvider? provider, out Product? product)
        {
            var addresses = productStr?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (addresses != null && addresses.Any())
            {
                product = new Product { ProductList = addresses.ToList() };
                return true;
            }
            product = new Product();
            return false;
        }


        //public static ValueTask<Product?> BindAsync(HttpContext context, ParameterInfo parameter)
        //{
        //    string productStr = context.Request.Query["product"];
        //    string queryString = context.Request.Query["text"].FirstOrDefault();

        //    string productStr2 = context.Request.Headers["product"];
        //    string productStr3 = context.Request.QueryString.Value;

        //    return ValueTask.FromResult<Product?>(new Product()
        //    {
        //        Name = queryString
        //    });
        //}


    }
}
