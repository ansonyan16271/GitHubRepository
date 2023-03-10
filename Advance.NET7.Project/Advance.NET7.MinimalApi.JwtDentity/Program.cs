using Advance.NET7.MinimalApi.JwtDentity;
using Advance.NET7.MinimalApi.JwtDentity.Utility;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

builder.Services.AddTransient<ICustomJWTService,CustomHSJWTService>();

builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

app.MapGet("Get", () => new List<int>() { 1, 2, 3, 4, 6, 7 });

app.MapPost("Login", (string name, string password, ICustomJWTService _iJWTService) =>
{
    //在这里需要去数据库中做数据验证
    if ("Richard".Equals(name) && "123456".Equals(password))
    {
        //从数据库中查询出来的
        var user = new CurrentUser()
        {
            Id = 123,
            Name = "Richard",
            Age = 36,
            NikeName = "金牌讲师Richard老师",
            Description = ".NET架构师",
            RoleList = "admin"
        };
        //就应该生成Token 
        string token = _iJWTService.GetToken(user);
        return JsonConvert.SerializeObject(new
        {
            result = true,
            token
        });
    }
    else
    {
        return JsonConvert.SerializeObject(new
        {
            result = false,
            token = ""
        });
    }
});


app.Run();


