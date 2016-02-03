using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZwhAid
{
    /// <summary>
    /// 
    /// </summary>
    public class CryptographyAid : ZwhBase
    {
        private Encoding encode = Encoding.UTF8;
        /// <summary>
        /// 
        /// </summary>
        public Encoding Encode
        {
            set { encode = value; }
            get { return encode; }
        }

        private string key = "zwhcryptizwhname";
        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            set { key = value; }
            get { return key; }
        }

        private byte[] iv = { 0x7a, 0x77, 0x68, 0x63, 0x72, 0x79, 0x70, 0x74, 0x69, 0x7a, 0x77, 0x68, 0x6e, 0x61, 0x6d, 0x65 };
        /// <summary>
        /// 
        /// </summary>
        public byte[] Iv
        {
            set { iv = value; }
            get { return iv; }
        }

        private string siv = "zwhcryptizwhname";
        /// <summary>
        /// 
        /// </summary>
        public string SIv
        {
            set { siv = value; }
            get { return siv; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CryptographyAid() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encode"></param>
        public CryptographyAid(Encoding encode)
        {
            Encode = encode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="key"></param>
        public CryptographyAid(Encoding encode, string key)
        {
            Encode = encode;
            this.key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public CryptographyAid(Encoding encode, string key, byte[] iv)
        {
            Encode = encode;
            this.key = key;
            this.iv = iv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        public CryptographyAid(Encoding encode, string key, string iv)
        {
            Encode = encode;
            this.key = key;
            this.siv = iv;
            this.iv = Encode.GetBytes(SIv);
        }

        #region AES
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的Base64字符串</returns>
        public string EncryptAES(string text)
        {
            try
            {
                zString = Convert.ToBase64String(EncryptAESByte(text));
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Byte[] EncryptAESByte(string text)
        {
            try
            {
                //分组加密算法
                SymmetricAlgorithm des = Rijndael.Create();
                byte[] inputByteArray = Encode.GetBytes(text);//得到需要加密的字节数组 
                                                              //设置密钥及密钥向量
                des.Key = Encode.GetBytes(Key);
                des.IV = Iv;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        zBytes = ms.ToArray();//得到加密后的字节数组
                        cs.Close();
                        ms.Close();
                    }
                }
            }
            catch { }
            return ZBytes;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptAES(string text)
        {
            try
            {
                byte[] cipherText = Convert.FromBase64String(text);
                SymmetricAlgorithm des = Rijndael.Create();
                des.Key = Encode.GetBytes(Key);
                des.IV = Iv;
                byte[] decryptBytes = new byte[cipherText.Length];
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cs.Read(decryptBytes, 0, decryptBytes.Length);
                        cs.Close();
                        ms.Close();
                        zString = Encode.GetString(decryptBytes).TrimEnd('\0');//将字符串后尾的'\0'去掉
                    }
                }
            }
            catch { }

            return ZString;
        }
        #endregion

        #region MD5（不可逆算法）
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptMD(string text)
        {
            try
            {
                zString = BitConverter.ToString(EncryptMDByte(text)).Replace("-", "");
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Byte[] EncryptMDByte(string text)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] data = Encode.GetBytes(text);
                zBytes = md5.ComputeHash(data);
            }
            catch { }
            return ZBytes;
        }
        #endregion

        #region DES
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的Base64字符串</returns>
        public string EncryptDES(string text)
        {
            try
            {
                zString = Convert.ToBase64String(EncryptDESByte(text));
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Byte[] EncryptDESByte(string text)
        {
            try
            {
                byte[] keyBytes = Encode.GetBytes(Key.Substring(0, 8));
                byte[] keyIV = Encode.GetBytes(siv.Substring(0, 8));
                byte[] inputByteArray = Encode.GetBytes(text);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();
                        zBytes = mStream.ToArray();
                    }
                }
            }
            catch { }

            return ZBytes;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="text">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptDES(string text)
        {
            try
            {
                byte[] keyBytes = Encode.GetBytes(Key.Substring(0, 8));
                byte[] keyIV = new byte[8];
                Array.Copy(Iv, keyIV, 8);
                byte[] inputByteArray = Convert.FromBase64String(text);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();
                        zString = Encode.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }

            return ZString;
        }
        #endregion

        #region Base64
        /// <summary>
        /// Base64加密
        /// </summary>
        public string EncryptBase(Encoding encode, string text)
        {
            try
            {
                byte[] bytes = Encode.GetBytes(text);
                zString = Convert.ToBase64String(bytes);
            }
            catch { }

            return ZString;
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        public string DecryptBase(Encoding encode, string text)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(text);
                zString = Encode.GetString(bytes);
            }
            catch { }

            return ZString;
        }
        #endregion

        #region RSA
        //密钥
        private string RSAKey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns>加密后的字符串</returns>
        public string EncryptRSA(string content)
        {
            try
            {
                zString = Encode.GetString(EncryptRSAByte(content));
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Byte[] EncryptRSAByte(string content)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(RSAKey);
                zBytes = rsa.Encrypt(Encode.GetBytes(content), false);
            }
            catch { }

            return ZBytes;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string DecryptRSA(string content)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                byte[] cipherbytes;
                rsa.FromXmlString(RSAKey);
                cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);
                zString = Encode.GetString(cipherbytes);
            }
            catch { }

            return ZString;
        }
        #endregion

        #region RC4
        /// <summary>
        /// RC4加密
        /// </summary>
        public string EncryptRC(string input)
        {
            try
            {
                StringBuilder result = new StringBuilder();
                int x, y, j = 0;
                int[] box = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    box[i] = i;
                }
                for (int i = 0; i < 256; i++)
                {
                    j = (Key[i % Key.Length] + box[i] + j) % 256;
                    x = box[i];
                    box[i] = box[j];
                    box[j] = x;
                }
                for (int i = 0; i < input.Length; i++)
                {
                    y = i % 256;
                    j = (box[y] + j) % 256;
                    x = box[y];
                    box[y] = box[j];
                    box[j] = x;

                    result.Append((char)(input[i] ^ box[(box[y] + box[j]) % 256]));
                }
                zString = result.ToString();
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// RC4解密
        /// </summary>
        public string DecryptRC(string input)
        {
            try
            {
                zString = EncryptRC(input);
            }
            catch { }

            return ZString;
        }
        #endregion

        #region SHA1加密（不可逆算法）
        public string EncryptSHA(string text)
        {
            try
            {
            }
            catch { }

            return ZString;
        }
        #endregion
    }
}
