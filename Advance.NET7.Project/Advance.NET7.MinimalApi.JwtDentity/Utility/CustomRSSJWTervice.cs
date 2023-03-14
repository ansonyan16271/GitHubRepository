using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Advance.NET7.MinimalApi.JwtDentity.Utility
{
    public class CustomRSSJWTervice : ICustomJWTService
    {
        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public CustomRSSJWTervice(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }

        /// <summary>
        /// 返回token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetToken(CurrentUser user)
        {
            #region 使用加密解密Key  非对称 
            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }
            #endregion

            //准备有效载荷
            Claim[] claims = new[]
             {
               new Claim(ClaimTypes.Name, user.Name!),
               new Claim("NickName",user.NikeName!),
               new Claim(ClaimTypes.Role,user.RoleList!),//传递其他信息    "Role"
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role","teacher"),//传递其他信息    "Role"
                 new Claim(ClaimTypes.Role,"Student"),
                     new Claim(ClaimTypes.Role,"User"),
               new Claim("Description",user.Description!),
               new Claim("Age",user.Age.ToString()),
            };

            //准备加密key
            RsaSecurityKey key = new RsaSecurityKey(keyParams);

            //Sha256 加密方式
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);


            JwtSecurityToken token = new JwtSecurityToken(
                  issuer: _JWTTokenOptions.Issuer,
                  audience: _JWTTokenOptions.Audience,
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(5),//5分钟有效期
                  signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
        #endregion
    }
}
