
using Advance.NET7.MinimalApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advance.NET7.MinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "朝夕教育 MinimalApi-v1版本",
                    Version = "v1"
                });
                options.SwaggerDoc("v2", new()
                {
                    Title = "朝夕教育 MinimalApi-v2版本",
                    Version  = "v2"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.EnableTryItOutByDefault();
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", $" 朝夕教育 MinimalApi-v1版本 v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", $" 朝夕教育 MinimalApi-v2版本 v2");
                    
                });
            }
            #region 1、基本使用 
            {
                //app.MapGet("hello", () => "This is hello.");
            }
            #endregion

            #region 2、基于RestFul的多种使用

            //{
            //    app.MapGet("GetRequest", () => "This  is Get Request."); ;
            //    app.MapPost("PostRequest", () => "This  is Post Request."); ;
            //    app.MapPut("PutRequest", () => "This  is Put Request."); ;
            //    app.MapDelete("DeleteRequest", () => "This  is Delete Request."); ;
            //    app.MapMethods("AllRequest",new string[] { "GET", "POST", "PUT", "DELETE", "OPTIONS", "HEAD" }, () => "This is AllRequest.");
            //}
            #endregion

            //#region 3、MinimalApi传递参数
            //{
            //    app.MapGet("api/ParaInt", (int i) => { return i; });
            //    app.MapGet("api/ParaString", (string j) => { return j; });
            //    app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j);
            //    app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity);
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>
            //    commodities);
            //}
            //#endregion

            #region 4、MinimalApiSwagger支持和版本控制
            {
                app.MapGet("api/ParaInt", (int i) => { return i; }).WithGroupName("v1");
                app.MapGet("api/ParaString", (string j) => { return j; }).WithGroupName("v1");
                app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j).WithGroupName("v1");
                app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity).WithGroupName("v2");
                app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>
                commodities).WithGroupName("v2");
            }
            #endregion
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}