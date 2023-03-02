

using Advance.NET7.MinimalApi.DB;
using Advance.NET7.MinimalApi.DB.Models;
using Advance.NET7.MinimalApi.Interfaces;
using Advance.NET7.MinimalApi.Models;
using Advance.NET7.MinimalApi.Services;
using Advance.NET7.MinimalApi.Utility;
using Advance.NET7.MinimalApi.Utility.IOCModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

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
                options.OperationFilter<SwaggerFileUploadFilter>();
            });

            //builder.Services.AddTransient<ITestServiceA,TestServiceA>();
            //builder.Services.AddTransient<ITestServiceB,TestServiceB>();

            builder.Services.AddTransient<DbContext, MinimalApiDbContext>();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new MyApplicationModule());
            });
            //支持控制台日志输出
            //builder.Logging.AddJsonConsole();
            builder.Logging.AddLog4Net("CfgFile/log4net.Config");

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

            #region 8、Swagger扩展 文件上传选择按钮功能
            //{
            //    app.MapPost("api/UploadFile", (HttpRequest request) =>
            //    {
            //        var form = request.ReadFormAsync().Result;
            //        return new JsonResult(new
            //        {
            //            Success = true,
            //            Message = "上传成功",
            //            FileName = form.Files.FirstOrDefault()?.FileName
            //        });

            //    }).Accepts<HttpRequest>("multipart/form-data");

            //}

            #endregion


            #region 9、日志输出-控制台日志

            //{
            //    //log4net:
            //    //nuget引入log4net 程序包
            //    //nuget引入Microsoft.Extensions.Logging.Log4Net.AspNetCore 程序包

            //    app.MapPost("api/UploadFile", (HttpRequest request) =>
            //    {
            //        app.Logger.LogInformation("================================");
            //        app.Logger.LogInformation("文件已上传到服务器");
            //        app.Logger.LogInformation("================================");

            //        var form = request.ReadFormAsync().Result;
            //        return new JsonResult(new
            //        {
            //            Success = true,
            //            Message = "上传成功",
            //            FileName = form.Files.FirstOrDefault()?.FileName
            //        });

            //    }).Accepts<HttpRequest>("multipart/form-data");

            //    app.MapGet("api/ParaInt", (int i) =>
            //    {

            //        app.Logger.LogInformation("================================");
            //        app.Logger.LogInformation($"参数为：{i}");
            //        app.Logger.LogInformation("================================");

            //        return i;

            //    });
            //    app.MapGet("api/ParaString", (string j) => { return j; });
            //    app.MapGet("api/ParaAll", (int i, string j, DateTime dt) => i.ToString() + j);

            //    app.MapPost("api/ParaModel", ([FromBody] Commodity commodity) =>
            //    {

            //        app.Logger.LogInformation("================================");
            //        app.Logger.LogInformation($"参数为：{Newtonsoft.Json.JsonConvert.SerializeObject(commodity)}");
            //        app.Logger.LogInformation("================================");
            //        return Newtonsoft.Json.JsonConvert.SerializeObject(commodity);
            //    }).WithGroupName("v1");
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) => Newtonsoft.Json.JsonConvert.SerializeObject(commodities)).WithGroupName("v2");

            //}
            #endregion

            #region 环境相关变量
            //{
            //    Console.WriteLine($"应用程序名称: {builder.Environment.ApplicationName}");
            //    Console.WriteLine($"环境变量: {builder.Environment.EnvironmentName}");
            //    Console.WriteLine($"ContentRoot目录: {builder.Environment.ContentRootPath}");
            //    Console.WriteLine($"ContentRootFileProvider目录: {Newtonsoft.Json.JsonConvert.SerializeObject(builder.Environment.ContentRootFileProvider)}");
            //}
            #endregion

            #region 路由约束
            {

                //app.MapGet("api/User",  () =>
                //{

                //});

                //app.MapPost("api/User", () =>
                //{

                //});
                //app.MapPut("api/User", () =>
                //{

                //});

                ////参数users必须是int类型
                //app.MapGet("/user1/{userId:int}", (int userId) => $"user id is {userId} 参数users必须是int类型");

                ////参数name必须是必须是5个字符
                //app.MapGet("/user2/{username:length(5)}", (string username) => $"user username is {username} 参数username必须是必须是5个字符");

                ////参数username 字符串长度最少是4个字符
                //app.MapGet("/user3/{username:minlength(4)}", (string username) => $"username id is {username} 参数username 字符串长度最少是4个字符");

                ////参数username 字符串长度最多是8个字符
                //app.MapGet("/user4/{username:maxlength(8)}", (string username) => $"username id is {username} 参数username 字符串长度最多是8个字符");

                ////参数username 字符串长度介于8/16个字符之间
                //app.MapGet("/user5/{username:length(8,16)}", (string username) => $"username id is {username} 字符串长度介于8/16个字符之间");

                ////参数age 整数值必须大于18
                //app.MapGet("/user6/{age:min(18)}", (string age) => $"age id is {age} 参数age 必须为整数,数值必须大于等于18");

                ////参数age 整数值必须小于18
                //app.MapGet("/user7/{age:max(18)}", (string age) => $"age id is {age}  参数age 必须为整数,数值必须小于等于18");

                //////参数age   整数值必须介于18和120之间
                //app.MapGet("/user8/{age:range(18,20)}", (string age) => $"age id is {age} 整数值必须介于18和120之间");


                ////参数active必须微bool值.忽略大小写 
                //app.MapGet("/user9/{active:bool}", (string active) => $"user active is {active} 参数active必须微bool值.忽略大小写 ");

                ////参数dob匹配满足DateTime类型的值
                //app.MapGet("/user10/{dob:datetime}", (string dob) => $"user dob is {dob} 参数dob匹配满足DateTime类型的值");

                ////参数price匹配满足 decimal类型的值
                //app.MapGet("/user11/{price:decimal}", (string price) => $"price dob is {price} 参数price匹配满足 decimal类型的值");

                ////参数height匹配满足 double类型的值
                //app.MapGet("/user12/{height:double}", (string height) => $"price dob is {height} 参数height匹配满足 double类型的值");

                ////参数height匹配满足 float类型的值
                //app.MapGet("/user13/{height:float}", (string height) => $"price height is {height} 参数height匹配满足 float类型的值");

                ////参数id匹配满足 Guid类型的值
                //app.MapGet("/user14/{id:guid}", (string id) => $"price id is {id} 参数id匹配满足 Guid类型的值");

                ////参数ticks匹配满足 long 类型的值
                //app.MapGet("/user15/{ticks:long}", (string ticks) => $"ticks id is {ticks} 参数ticks匹配满足 long 类型的值");

                ////参数ticks  请求信息必须包含该参数
                //app.MapGet("/user16/{ticks:required}", (string ticks) => $"ticks id is {ticks} 参数ticks  请求信息必须包含该参数");

                ////参数ticks  请求信息必须包含该参数 字符串必须由一个或多个a-z的字母字符组成，且不区分大小写。
                //app.MapGet("/user17/{ticks:alpha}", (string ticks) => $"ticks id is {ticks} 参数ticks  请求信息必须包含该参数 字符串必须由一个或多个a-z的字母字符组成，且不区分大小写。");


                //////test  必须符合正则：regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)  要求   123-22-4444
                //app.MapGet("/user18/{test:regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)}", (string test) => $"test id is {test} test  必须符合正则：regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)  要求   123-22-4444");
            }
            #endregion

            #region MinimalApi返回类型

            {
                //app.MapGet("getStringPara", (string str) => str);

                //app.MapGet("getModel", () => new CommodityInfo()
                //{
                //    Id = 1,
                //    CreateTime = DateTime.Now,
                //    CreatorId = 1,
                //});

                //app.MapGet("getModelArray", () => new CommodityInfo[]
                //{
                //    new CommodityInfo() 
                //    {
                //        Id = 1,
                //        CreateTime = DateTime.Now,
                //        CreatorId = 1,
                //        LastModifierId= 1,
                //        Name="Anson"
                //    },
                //    new CommodityInfo()
                //    {
                //        Id = 1,
                //        CreateTime = DateTime.Now,
                //        CreatorId = 1,
                //        LastModifierId= 1,
                //        Name="Anson"
                //    },
                //    new CommodityInfo()
                //    {
                //        Id = 1,
                //        CreateTime = DateTime.Now,
                //        CreatorId = 1,
                //        LastModifierId= 1,
                //        Name="Anson"
                //    },

                //});
                //国际标准协议
                //404  405  500 200
                //app.MapGet("/Success", () => Results.Ok("Success"));

                //app.MapGet("/Fail", () => Results.BadRequest("Fail"));
                ////404结果
                //app.MapGet("/404", () => Results.NotFound());

                ////返回结果
                //app.MapGet("/DownFile", () =>
                //{
                //    string baseurl = System.Environment.CurrentDirectory;
                //    return Results.File($"{baseurl}/File/16期高级班.jpg", MediaTypeNames.Image.Jpeg, "高级班第16期.jpg");
                //});

                //app.MapGet("/Jsons", () =>
                //{
                //    return Results.Json(new
                //    {
                //        Id = 1234,
                //        Name = "Richard老师"
                //    });
                //});

                //app.MapGet("/Unauthorized", () =>
                //{
                //    return Results.Unauthorized();
                //});

            }
            #endregion

            #region 返回类型扩展
            {
                //app.MapGet("/GetHtml/{name}", (string name) => new HtmlResult(@$"<html>
                //            <head><title>Index</title></head>
                //            <body>
                //                <h1>Hello {name}</h1>
                //            </body>
                //        </html>"));

                //app.MapGet("/Get/GetCustomJSON", () => new ExtJsonResult(new CommodityInfo()
                //{
                //    Id = 1234,
                //    Name = "<h3>Richard老师</h3>",
                //    CreateTime = DateTime.Now,
                //    CreatorId = 1,
                //    LastModifierId = 1,
                //    LastModifyTime = DateTime.Now
                //}));
            }
            #endregion

            #region MyRegion
            {
                ////路由绑定
                //app.MapGet("/sayhello/{name}", (string name) => $"Hello {name}"); 
                ////路由和querystring的混用方式
                //app.MapGet("/sayhelloNew/{name}", (string name, int? age) => $"my name is {name},age {age}");

                //Get请求--希望传递一个数据,，接受方参数是一个实体,get请求不能直接传递实体,怎么办？--就需要
                //app.MapGet("/Product/GetProductFromQuery/{id}", (Product product, [FromRoute] int id, [FromQuery] string text) => product.ProductList);

            }
            #endregion

            app.UseHttpsRedirection();

            app.UseAuthorization();


            #region 指定访问路径
            {
                //app.Run("http://localhost:8022");

                //app.Urls.Add("http://localhost:9032");
                //app.Urls.Add("http://localhost:9042");
                //app.Urls.Add("http://localhost:9052");
                //app.Urls.Add("http://localhost:9062");
            }
            #endregion

            app.Run();
        }
    }
}