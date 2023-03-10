using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Advance.NET7.MinimalApi.JwtDentity.Utility
{
    public class CustomHSJWTService : ICustomJWTService
    {
        #region Option注入
        private readonly JWTTokenOptions _jWTTokenOptions;

        public CustomHSJWTService(IOptionsMonitor<JWTTokenOptions> jWTTokenOptions)
        {
            _jWTTokenOptions = jWTTokenOptions.CurrentValue;
        }



        #endregion
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetToken(CurrentUser user)
        {
            //准备有效载荷
            Claim[] claims = new[]
             {
               new Claim(ClaimTypes.Name, user.Name),
               new Claim("NickName",user.NikeName),
               new Claim(ClaimTypes.Role,user.RoleList),//传递其他信息   
               new Claim("Description",user.Description),
               new Claim("Age",user.Age.ToString()),
            };

            //准备加密key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTTokenOptions.SecurityKey));

            //Sha256 加密方式
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                 issuer: _jWTTokenOptions.Issuer,
                 audience: _jWTTokenOptions.Audience,
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(5),//5分钟有效期
                 signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);

            return returnToken;

        }
    }
}
