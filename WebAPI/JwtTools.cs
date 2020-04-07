using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public class JwtTools
    {
        public static string Key { get; set; } = "Hello World";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encoder(Dictionary<string, object> payload, string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = Key;
            }

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            //添加一个jwt的时效串
            payload.Add("timeout", DateTime.Now.AddDays(1));

            return encoder.Encode(payload, key);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="jwtStr">token</param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Decode(string jwtStr, string secret = null)
        {
            if (string.IsNullOrEmpty(secret))
            {
                secret = Key;
            }

            try
            {

                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var json = decoder.Decode(jwtStr, secret, verify: true);


                //string ->Dictionary
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if((DateTime)result["timeout"] < DateTime.Now)
                {
                    throw new Exception("jwt已经过期，请重新登录");
                }
                result.Remove("timeout");
                return result;

            }
            catch (TokenExpiredException)
            {
                throw;
                //Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw;
                //Console.WriteLine("Token has invalid signature");
            }
        }
    }
}