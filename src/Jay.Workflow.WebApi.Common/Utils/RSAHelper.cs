using Jay.Workflow.WebApi.Common.Exceptions;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionVisitors.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    /// <summary>
    /// RSA
    /// </summary>
    public static class RSAHelper
    {
        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerException"></exception>
        public static string EncryptData(string publicKey,string data)
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                using RSA rsa = RSA.Create();
                rsa.ImportFromPem(publicKey);//从Pem格式的公钥文件中加载公钥信息
                byte[] encryptedDatas = rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);

                return Convert.ToBase64String(encryptedDatas);
            }
            catch(Exception ex)
            {
                throw new InternalServerException("加密失败");
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerException"></exception>
        public static string DecryptData(string privateKey,string encryptedData)
        {
            try
            {
                byte[] encryptedDataBytes = Convert.FromBase64String(encryptedData);

                using RSA rsa = RSA.Create();
                rsa.ImportFromPem(privateKey);
                byte[] decryptedData = rsa.Decrypt(encryptedDataBytes, RSAEncryptionPadding.OaepSHA256);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch(Exception ex)
            {
                throw new InternalServerException("解密失败");
            }
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="data">需要加密的内容</param>
        /// <returns></returns>
        public static string EncryptData(string publicKey, string data, RSAEncryptionPadding? rSAEncryptionPadding)
        {
            try
            {
                if (rSAEncryptionPadding == null) rSAEncryptionPadding = RSAEncryptionPadding.OaepSHA256;

                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                using RSA rsa = RSA.Create();

                // 从 PEM 格式的公钥文件中加载公钥信息
                rsa.ImportFromPem(publicKey);

                // 使用公钥加密数据
                byte[] encryptedData = rsa.Encrypt(dataBytes, RSAEncryptionPadding.Pkcs1);

                return Convert.ToBase64String(encryptedData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// RSA 解密
        /// </summary>
        /// <param name="privateKeyText"></param>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public static string DecryptData(string privateKeyText, string encryptedData, RSAEncryptionPadding? rSAEncryptionPadding)
        {
            try
            {
                if (rSAEncryptionPadding == null) rSAEncryptionPadding = RSAEncryptionPadding.OaepSHA256;

                byte[] encryptedDataBytes = Convert.FromBase64String(encryptedData);

                using RSA rsa = RSA.Create(3072);

                // 从 PEM 格式的私钥文件中加载私钥信息
                rsa.ImportFromPem(privateKeyText);

                // 使用私钥解密数据 RSAEncryptionPadding.OaepSHA256 后续和前端统一调整
                byte[] decryptedData = rsa.Decrypt(encryptedDataBytes, rSAEncryptionPadding);


                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 生成 RSA 密钥对并导出为 PEM 格式（跨平台实现）
        /// </summary>
        /// <param name="keySize">密钥长度（默认 3072 位）</param>
        /// <returns>(公钥 PEM 格式, 私钥 PEM 格式)</returns>
        public static (string publicKeyPem,string privateKeyPem) GenerateKeyPair(int keySize = 3072)
        {
            // 使用平台默认实现（Windows: CNG, Linux: OpenSSL）
            using var rsa=RSA.Create(keySize);

            // 导出公钥 (X.509 SubjectPublicKeyInfo 格式)
            byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
            string publicKeyPem = ConvertToPem(publicKeyBytes, "PUBLIC KEY");

            // 导出私钥 (PKCS#8 格式)
            byte[] privateKeyBytes = rsa.ExportPkcs8PrivateKey();
            string privateKeyPem = ConvertToPem(privateKeyBytes, "PRIVATE KEY");

            return (publicKeyPem, privateKeyPem);
        }

        /// <summary>
        /// 将字节数组转换为 PEM 格式
        /// </summary>
        /// <param name="keyBytes"></param>
        /// <param name="keyType"></param>
        /// <returns></returns>
        private static string ConvertToPem(byte[] keyBytes,string keyType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            // 添加 PEM 页眉
            stringBuilder.Append($"-----BEGIN {keyType}-----");
            // Base64 编码，每 64 个字符换行
            stringBuilder.Append(Convert.ToBase64String(keyBytes,Base64FormattingOptions.InsertLineBreaks));
            // 添加 PEM 页脚
            stringBuilder.Append($"-----END {keyType}-----");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 从 PEM 格式加载公钥
        /// </summary>
        /// <param name="publicKeyPem">PEM 格式的公钥字符串</param>
        /// <returns>已加载公钥的 RSA 实例</returns>
        public static RSA LoadPublicKey(string publicKeyPem)
        {
            // 提取 PEM 中的密钥主体（去除页眉页脚，解码 Base64）
            string keyBody =ExtractKeyBody(publicKeyPem);
            byte[] publicKeyBytes = Convert.FromBase64String(keyBody);

            var rsa = RSA.Create();
            // 导入 X.509 格式公钥
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            return rsa;
        }

        /// <summary>
        /// 从 PEM 格式加载私钥
        /// </summary>
        /// <param name="privateKeyPem">PEM 格式的私钥字符串</param>
        /// <returns>返回: 已加载私钥的 RSA 实例</returns>
        public static RSA LoadPrivateKey(string privateKeyPem)
        {
            // 提取 PEM 中的密钥主体
            string keyBody = ExtractKeyBody(privateKeyPem);

            byte[] privateKeyBytes = Convert.FromBase64String(keyBody);

            var rsa = RSA.Create();
            // 导入 PKCS#8 格式私钥
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            return rsa;
        }

        /// <summary>
        /// 提取 PEM 格式中的密钥主体
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        private static string ExtractKeyBody(string pem)
        {
            string[] lines = pem.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if(lines.Length > 1)
            {
                StringBuilder builder = new StringBuilder();
                bool isKeyContent = false;

                foreach(string line in lines)
                {
                    if (line.StartsWith("-----BEGIN"))
                    {
                        isKeyContent = true;
                        continue;
                    }

                    if (line.StartsWith("-----END"))
                    {
                        isKeyContent = false;
                        continue;
                    }

                    if (isKeyContent)
                    {
                        builder.Append(line);
                    }
                }
                return builder.ToString();
            }
            else
            {
                return pem.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "");
            }
        }

        /// <summary>
        /// 使用 OAEP+ OaepSHA256 加密（跨平台）
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="publicKeyPem">PEM 格式公钥</param>
        /// <returns>Base64 编码的密文</returns>
        public static string Encrypt(string plainText, string publicKeyPem)
        {
            using RSA rsa = LoadPublicKey(publicKeyPem);

            // 使用 OAEP+OaepSHA256 填充进行加密
            // 注意：RSA 加密适合小数据量（通常不超过密钥长度/8 - 42 字节）
            byte[] encryptedBytes = rsa.Encrypt(
                Encoding.UTF8.GetBytes(plainText),
                RSAEncryptionPadding.OaepSHA256
            );

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// 使用 OAEP+OaepSHA256 解密（跨平台）
        /// </summary>
        /// <param name="encryptedBase64">Base64 编码的密文</param>
        /// <param name="privateKeyPem">PEM 格式私钥</param>
        /// <returns>明文字符串</returns>
        public static string Decrypt(string encryptedBase64, string privateKeyPem)
        {
            using RSA rsa = LoadPrivateKey(privateKeyPem);

            // 使用与加密相同的填充方式进行解密
            byte[] decryptedBytes = rsa.Decrypt(
                Convert.FromBase64String(encryptedBase64),
                RSAEncryptionPadding.OaepSHA256
            );

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
