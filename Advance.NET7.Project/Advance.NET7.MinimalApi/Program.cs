

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
                options.SwaggerDoc("v1", new() { Title = "��Ϧ���� MinimalApi-v1�汾", Version = "v1" });
                options.SwaggerDoc("v2", new() { Title = "��Ϧ���� MinimalApi-v2�汾", Version = "v2" });
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
            //֧�ֿ���̨��־���
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

            #region 3��MinimalApi���ݲ���
            //{
            //    app.MapGet("api/ParaInt", (int i) => { return i; });
            //    app.MapGet("api/ParaString", (string j) => { return j; });
            //    app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j);
            //    app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity);
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>commodities);
            //}
            #endregion

            #region 4��MinimalApiSwagger֧�ֺͰ汾����
            //{
            //    app.MapGet("api/ParaInt", (int i) => { return i; }).WithGroupName("v1");
            //    app.MapGet("api/ParaString", (string j) => { return j; }).WithGroupName("v1");
            //    app.MapGet("api/ParaIntString", (int i, string j) => i.ToString() + j).WithGroupName("v1");
            //    app.MapGet("api/ParaModel", ([FromBody] Commodity commodity) => commodity).WithGroupName("v2");
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) =>
            //    commodities).WithGroupName("v2");
            //}
            #endregion

            #region 5��IOC+DI
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

            #region 6��Autofac�滻
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

            #region 7��IOC+EFCore+�ֲ�
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

            #region 8��Swagger��չ �ļ��ϴ�ѡ��ť����
            //{
            //    app.MapPost("api/UploadFile", (HttpRequest request) =>
            //    {
            //        var form = request.ReadFormAsync().Result;
            //        return new JsonResult(new
            //        {
            //            Success = true,
            //            Message = "�ϴ��ɹ�",
            //            FileName = form.Files.FirstOrDefault()?.FileName
            //        });

            //    }).Accepts<HttpRequest>("multipart/form-data");

            //}

            #endregion


            #region 9����־���-����̨��־

            //{
            //    //log4net:
            //    //nuget����log4net �����
            //    //nuget����Microsoft.Extensions.Logging.Log4Net.AspNetCore �����

            //    app.MapPost("api/UploadFile", (HttpRequest request) =>
            //    {
            //        app.Logger.LogInformation("================================");
            //        app.Logger.LogInformation("�ļ����ϴ���������");
            //        app.Logger.LogInformation("================================");

            //        var form = request.ReadFormAsync().Result;
            //        return new JsonResult(new
            //        {
            //            Success = true,
            //            Message = "�ϴ��ɹ�",
            //            FileName = form.Files.FirstOrDefault()?.FileName
            //        });

            //    }).Accepts<HttpRequest>("multipart/form-data");

            //    app.MapGet("api/ParaInt", (int i) =>
            //    {

            //        app.Logger.LogInformation("================================");
            //        app.Logger.LogInformation($"����Ϊ��{i}");
            //        app.Logger.LogInformation("================================");

            //        return i;

            //    });
            //    app.MapGet("api/ParaString", (string j) => { return j; });
            //    app.MapGet("api/ParaAll", (int i, string j, DateTime dt) => i.ToString() + j);

            //    app.MapPost("api/ParaModel", ([FromBody] Commodity commodity) =>
            //    {

            //        app.Logger.LogInformation("================================");
            //        app.Logger.LogInformation($"����Ϊ��{Newtonsoft.Json.JsonConvert.SerializeObject(commodity)}");
            //        app.Logger.LogInformation("================================");
            //        return Newtonsoft.Json.JsonConvert.SerializeObject(commodity);
            //    }).WithGroupName("v1");
            //    app.MapPost("api/ParaList", ([FromBody] List<Commodity> commodities) => Newtonsoft.Json.JsonConvert.SerializeObject(commodities)).WithGroupName("v2");

            //}
            #endregion

            #region ������ر���
            //{
            //    Console.WriteLine($"Ӧ�ó�������: {builder.Environment.ApplicationName}");
            //    Console.WriteLine($"��������: {builder.Environment.EnvironmentName}");
            //    Console.WriteLine($"ContentRootĿ¼: {builder.Environment.ContentRootPath}");
            //    Console.WriteLine($"ContentRootFileProviderĿ¼: {Newtonsoft.Json.JsonConvert.SerializeObject(builder.Environment.ContentRootFileProvider)}");
            //}
            #endregion

            #region ·��Լ��
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

                ////����users������int����
                //app.MapGet("/user1/{userId:int}", (int userId) => $"user id is {userId} ����users������int����");

                ////����name�����Ǳ�����5���ַ�
                //app.MapGet("/user2/{username:length(5)}", (string username) => $"user username is {username} ����username�����Ǳ�����5���ַ�");

                ////����username �ַ�������������4���ַ�
                //app.MapGet("/user3/{username:minlength(4)}", (string username) => $"username id is {username} ����username �ַ�������������4���ַ�");

                ////����username �ַ������������8���ַ�
                //app.MapGet("/user4/{username:maxlength(8)}", (string username) => $"username id is {username} ����username �ַ������������8���ַ�");

                ////����username �ַ������Ƚ���8/16���ַ�֮��
                //app.MapGet("/user5/{username:length(8,16)}", (string username) => $"username id is {username} �ַ������Ƚ���8/16���ַ�֮��");

                ////����age ����ֵ�������18
                //app.MapGet("/user6/{age:min(18)}", (string age) => $"age id is {age} ����age ����Ϊ����,��ֵ������ڵ���18");

                ////����age ����ֵ����С��18
                //app.MapGet("/user7/{age:max(18)}", (string age) => $"age id is {age}  ����age ����Ϊ����,��ֵ����С�ڵ���18");

                //////����age   ����ֵ�������18��120֮��
                //app.MapGet("/user8/{age:range(18,20)}", (string age) => $"age id is {age} ����ֵ�������18��120֮��");


                ////����active����΢boolֵ.���Դ�Сд 
                //app.MapGet("/user9/{active:bool}", (string active) => $"user active is {active} ����active����΢boolֵ.���Դ�Сд ");

                ////����dobƥ������DateTime���͵�ֵ
                //app.MapGet("/user10/{dob:datetime}", (string dob) => $"user dob is {dob} ����dobƥ������DateTime���͵�ֵ");

                ////����priceƥ������ decimal���͵�ֵ
                //app.MapGet("/user11/{price:decimal}", (string price) => $"price dob is {price} ����priceƥ������ decimal���͵�ֵ");

                ////����heightƥ������ double���͵�ֵ
                //app.MapGet("/user12/{height:double}", (string height) => $"price dob is {height} ����heightƥ������ double���͵�ֵ");

                ////����heightƥ������ float���͵�ֵ
                //app.MapGet("/user13/{height:float}", (string height) => $"price height is {height} ����heightƥ������ float���͵�ֵ");

                ////����idƥ������ Guid���͵�ֵ
                //app.MapGet("/user14/{id:guid}", (string id) => $"price id is {id} ����idƥ������ Guid���͵�ֵ");

                ////����ticksƥ������ long ���͵�ֵ
                //app.MapGet("/user15/{ticks:long}", (string ticks) => $"ticks id is {ticks} ����ticksƥ������ long ���͵�ֵ");

                ////����ticks  ������Ϣ��������ò���
                //app.MapGet("/user16/{ticks:required}", (string ticks) => $"ticks id is {ticks} ����ticks  ������Ϣ��������ò���");

                ////����ticks  ������Ϣ��������ò��� �ַ���������һ������a-z����ĸ�ַ���ɣ��Ҳ����ִ�Сд��
                //app.MapGet("/user17/{ticks:alpha}", (string ticks) => $"ticks id is {ticks} ����ticks  ������Ϣ��������ò��� �ַ���������һ������a-z����ĸ�ַ���ɣ��Ҳ����ִ�Сд��");


                //////test  �����������regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)  Ҫ��   123-22-4444
                //app.MapGet("/user18/{test:regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)}", (string test) => $"test id is {test} test  �����������regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)  Ҫ��   123-22-4444");
            }
            #endregion

            #region MinimalApi��������

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
                //���ʱ�׼Э��
                //404  405  500 200
                //app.MapGet("/Success", () => Results.Ok("Success"));

                //app.MapGet("/Fail", () => Results.BadRequest("Fail"));
                ////404���
                //app.MapGet("/404", () => Results.NotFound());

                ////���ؽ��
                //app.MapGet("/DownFile", () =>
                //{
                //    string baseurl = System.Environment.CurrentDirectory;
                //    return Results.File($"{baseurl}/File/16�ڸ߼���.jpg", MediaTypeNames.Image.Jpeg, "�߼����16��.jpg");
                //});

                //app.MapGet("/Jsons", () =>
                //{
                //    return Results.Json(new
                //    {
                //        Id = 1234,
                //        Name = "Richard��ʦ"
                //    });
                //});

                //app.MapGet("/Unauthorized", () =>
                //{
                //    return Results.Unauthorized();
                //});

            }
            #endregion

            #region ����������չ
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
                //    Name = "<h3>Richard��ʦ</h3>",
                //    CreateTime = DateTime.Now,
                //    CreatorId = 1,
                //    LastModifierId = 1,
                //    LastModifyTime = DateTime.Now
                //}));
            }
            #endregion

            #region MyRegion
            {
                ////·�ɰ�
                //app.MapGet("/sayhello/{name}", (string name) => $"Hello {name}"); 
                ////·�ɺ�querystring�Ļ��÷�ʽ
                //app.MapGet("/sayhelloNew/{name}", (string name, int? age) => $"my name is {name},age {age}");

                //Get����--ϣ������һ������,�����ܷ�������һ��ʵ��,get������ֱ�Ӵ���ʵ��,��ô�죿--����Ҫ
                //app.MapGet("/Product/GetProductFromQuery/{id}", (Product product, [FromRoute] int id, [FromQuery] string text) => product.ProductList);

            }
            #endregion

            app.UseHttpsRedirection();

            app.UseAuthorization();


            #region ָ������·��
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