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
    //��������Ҫȥ���ݿ�����������֤
    if ("Richard".Equals(name) && "123456".Equals(password))
    {
        //�����ݿ��в�ѯ������
        var user = new CurrentUser()
        {
            Id = 123,
            Name = "Richard",
            Age = 36,
            NikeName = "���ƽ�ʦRichard��ʦ",
            Description = ".NET�ܹ�ʦ",
            RoleList = "admin"
        };
        //��Ӧ������Token 
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


