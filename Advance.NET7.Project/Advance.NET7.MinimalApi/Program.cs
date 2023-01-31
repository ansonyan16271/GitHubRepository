

using Advance.NET7.MinimalApi.DB;
using Advance.NET7.MinimalApi.DB.Models;
using Advance.NET7.MinimalApi.Interfaces;
using Advance.NET7.MinimalApi.Models;
using Advance.NET7.MinimalApi.Services;
using Advance.NET7.MinimalApi.Utility.IOCModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                options.SwaggerDoc("v1", new() { Title = "朝夕教育 MinimalApi-v1版本", Version = "v1" });
                options.SwaggerDoc("v2", new() { Title = "朝夕教育 MinimalApi-v2版本", Version = "v2" });
            });

            //builder.Services.AddTransient<ITestServiceA,TestServiceA>();
            //builder.Services.AddTransient<ITestServiceB,TestServiceB>();

            builder.Services.AddTransient<DbContext, MinimalApiDbContext>();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new MyApplicationModule());
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

            #region 3、MinimalApi传递参数
            //{
            //    app.MapGet("api/ParaInt", (int i) => { return i; });
            //    app.MapGet("api/ParaString", (string j) => { return j; });
            //    app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j);
            //    app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity);
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>commodities);
            //}
            #endregion

            #region 4、MinimalApiSwagger支持和版本控制
            //{
            //    app.MapGet("api/ParaInt", (int i) => { return i; }).WithGroupName("v1");
            //    app.MapGet("api/ParaString", (string j) => { return j; }).WithGroupName("v1");
            //    app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j).WithGroupName("v1");
            //    app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity).WithGroupName("v2");
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>
            //    commodities).WithGroupName("v2");
            //}
            #endregion

            #region 5、IOC+DI
            //{
            //    app.MapPost("TestServiceAShowA", ([FromServices] ITestServiceA testServiceA, [FromBody] Commodity commodity) =>
            //    {
            //        return testServiceA.ShowA();
            //    });

            //    app.MapGet("TestServiceBShowB", ([FromServices] ITestServiceB testServiceB) =>
            //    {
            //        return testServiceB.ShowB();
            //    });
            //}

            #endregion

            #region 6、Autofac替换
            {
                //{
                //    app.MapPost("TestServiceAShowA", ([FromServices] ITestServiceA testServiceA, [FromBody] Models.Commodity commodity) =>
                //    {
                //        return testServiceA.ShowA();
                //    });

                //    app.MapGet("TestServiceBShowB", ([FromServices] ITestServiceB testServiceB, IComponentContext context) =>
                //    {
                //        ITestServiceA testServiceA = context.Resolve<ITestServiceA>();
                //        return testServiceB.ShowB();
                //    });
                //}

            }
            #endregion
            #region 7、IOC+EFCore+分层
            {
                app.MapGet("GetCompanyById", (ICompanyService companyService, int id) => companyService.Find<Company>(id));
                app.MapPost("InsertCompany", (ICompanyService companyService, string companyName) =>
                {
                    return companyService.Insert<Company>(new Company()
                    {
                        CreateTime = DateTime.Now,
                        CreatorId = 1,
                        LastModifierId = 1,
                        LastModifyTime = DateTime.Now,
                        Name = companyName
                    });
                });
                app.MapPost("QueryPage", (ICompanyService companyService, int pageindex, int pagesize) =>
                {
                    return companyService.QueryPage<Company, DateTime?>(c => true, pagesize, pageindex, s => s.CreateTime, false);
                });
            }

            #endregion
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}