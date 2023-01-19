
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
                    Title = "��Ϧ���� MinimalApi-v1�汾",
                    Version = "v1"
                });
                options.SwaggerDoc("v2", new()
                {
                    Title = "��Ϧ���� MinimalApi-v2�汾",
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
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", $" ��Ϧ���� MinimalApi-v1�汾 v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", $" ��Ϧ���� MinimalApi-v2�汾 v2");
                    
                });
            }
            #region 1������ʹ�� 
            {
                //app.MapGet("hello", () => "This is hello.");
            }
            #endregion

            #region 2������RestFul�Ķ���ʹ��

            //{
            //    app.MapGet("GetRequest", () => "This  is Get Request."); ;
            //    app.MapPost("PostRequest", () => "This  is Post Request."); ;
            //    app.MapPut("PutRequest", () => "This  is Put Request."); ;
            //    app.MapDelete("DeleteRequest", () => "This  is Delete Request."); ;
            //    app.MapMethods("AllRequest",new string[] { "GET", "POST", "PUT", "DELETE", "OPTIONS", "HEAD" }, () => "This is AllRequest.");
            //}
            #endregion

            //#region 3��MinimalApi���ݲ���
            //{
            //    app.MapGet("api/ParaInt", (int i) => { return i; });
            //    app.MapGet("api/ParaString", (string j) => { return j; });
            //    app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j);
            //    app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity);
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>
            //    commodities);
            //}
            //#endregion

            #region 4��MinimalApiSwagger֧�ֺͰ汾����
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