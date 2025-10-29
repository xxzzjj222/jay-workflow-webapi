using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Models
{
    /// <summary>
    /// 密钥对
    /// </summary>
    public class KeyPairDto
    {
        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKeyPem { get; set; }
        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKeyPem { get; set; }
    }
}
