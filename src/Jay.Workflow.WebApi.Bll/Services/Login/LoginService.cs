using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Common.Cache;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Dal.Interfaces.User;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.Login
{
    public class LoginService:ILoginService
    {
        private readonly IUserDalService _userDalService;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public LoginService(IUserDalService userDalService,IConfiguration configuration,ICacheService cacheService)
        {
            _userDalService = userDalService;
            _configuration = configuration;
            _cacheService = cacheService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<string> Login(LoginReq req)
        {
            //1. 登录失败次数校验
            var loginFailCountCacheKey = GetLoginFailCountCacheKey(req.UserPhone);
            var loginFailCount = await _cacheService.GetAsync<int>(loginFailCountCacheKey);
            if (loginFailCount > 5)
            {
                throw new InvalidParameterException("账号登录失败次数过多，暂停使用，请稍后再试！");
            }

            //2. 获取登录用户信息
            var user=await _userDalService.GetUserByUserPhoneAsync(req.UserPhone).ConfigureAwait(false);
            if (user == null)
            {
                throw new NotFoundException($"用户{req.UserPhone}不存在！");
            }

            //3. 密码校验
            var privateKey = _configuration["Security:UserPassword:PrivateKeyPem"];
            var decryptData = RSAHelper.DecryptData(privateKey, req.Password);
            var verify = BCrypt.Net.BCrypt.Verify(decryptData, user.Password);
            if (!verify)
            {
                await _cacheService.IncrementAsync(loginFailCountCacheKey, 1, TimeSpan.FromMinutes(10)).ConfigureAwait(false);
                throw new InvalidParameterException("密码错误！");
            }

            //4. 密码正确
            if (loginFailCount > 0)
            {
                await _cacheService.DeleteAsync(loginFailCountCacheKey).ConfigureAwait(false);
            }

            //5. 生成token
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid,user.UserId.ToString()),
                new Claim(ClaimTypes.MobilePhone,user.UserPhone),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jay@19961128!Alice@19961226!Now@20251029"));
            var token = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddDays(1),
                    issuer:"jay",
                    audience:"su",
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var jwtTokenCacheKey = GetLoginTokenCacheKey(user.UserId);
            await _cacheService.SetAsync(jwtTokenCacheKey, jwtToken, TimeSpan.FromDays(1));

            return jwtToken;
        }

        private string GetLoginFailCountCacheKey(string phone)
        {
            return $"LoginFailCount_{phone}";
        }

        private string GetLoginTokenCacheKey(Guid userId)
        {
            return $"LoginToken_{userId}";
        }
    }
}
