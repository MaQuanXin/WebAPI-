using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}