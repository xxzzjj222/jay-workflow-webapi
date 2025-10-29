using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Common.Models;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Jay.Workflow.WebApi.Service.Controllers.Util
{
    /// <summary>
    /// 安全控制器
    /// </summary>
    [Route("api/security")]
    public class SecurityController : UtilController
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="loginService"></param>
        public SecurityController(ILoginService loginService, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 生成 RSA 密钥对并导出为 PEM 格式
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("rsa/key-pair")]
        public KeyPairDto GenerateRSAKeyPair()
        {
            var result = RSAHelper.GenerateKeyPair();
            return new KeyPairDto { PublicKeyPem=result.publicKeyPem, PrivateKeyPem=result.privateKeyPem };
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("rsa/encrypt")]
        public string RSAEncrypt(string password)
        {
            var publicKeyPem = _configuration["Security:UserPassword:PublicKeyPem"];
            var encryptData=RSAHelper.EncryptData(publicKeyPem, password);
            return encryptData;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptPassword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("rsa/decrypt")]
        public string RSADecrypt(string encryptPassword)
        {
            var privateKeyPem = _configuration["Security:UserPassword:PrivateKeyPem"];
            var password = RSAHelper.DecryptData(privateKeyPem, encryptPassword);
            return password;
        }
    }
}
