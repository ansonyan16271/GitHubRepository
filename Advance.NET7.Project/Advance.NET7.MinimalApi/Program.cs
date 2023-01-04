
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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #region 1、基本使用 
            {
                //app.MapGet("hello", () => "This is hello.");
            }
            #endregion

            #region 2、基于RestFul的多种使用

            {
                app.MapGet("GetRequest", () => "This  is Get Request."); ;
                app.MapPost("PostRequest", () => "This  is Post Request."); ;
                app.MapPut("PutRequest", () => "This  is Put Request."); ;
                app.MapDelete("DeleteRequest", () => "This  is Delete Request."); ;
                app.MapMethods("AllRequest",new string[] { "GET", "POST", "PUT", "DELETE", "OPTIONS", "HEAD" }, () => "This is AllRequest.");
            }
            #endregion
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}