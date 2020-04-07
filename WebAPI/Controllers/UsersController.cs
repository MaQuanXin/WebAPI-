using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Auth;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    /**
     * 1.通过特性的方式进行jwt校验
     * 特性与过滤器 auth
     * 
     * 
     * 2.在校验过程中读取到jwt中的账号信息并存入User.Identity.Name
     * 3.可以在任意Action方法中读取User.Identity.Name属性里的数据
     */


    [RoutePrefix(prefix: "api/users")]
    public class UsersController : ApiController
    {
        [Route(template: "login")]
        public IHttpActionResult Loign(Models.LoginViewModel model)
        {
            //1.引用jwt
            //2.jwttools

            //User Identity //是一个接口类型 Name 是一个字符串

            if (ModelState.IsValid)
            {
                return Ok(new Models.ResponseData()
                {
                    //token
                    Data = JwtTools.Encoder(new Dictionary<string, object>()
                    {
                        { "LoginName",model.LoginName},
                        { "UserId",213}
                    })
                });
            }
            //传递上来的数据未通过校验
            else
            {
                return Ok(new Models.ResponseData() { Code = 500, ErrorMsg = "校验失败" });
            }


            //微软推荐获取身份信息
            //User.Identity.Name;


            //如果传递上来的数据通过了实体类型校验
            //骚操作 return ModelState.IsValid ? Ok(new Models.ResponseData()) : Ok(new Models.ResponseData() { Code = 500, ErrorMsg = "校验失败" });
            //if (ModelState.IsValid)
            //{
            //    return Ok(new Models.ResponseData());
            //}
            ////传递上来的数据未通过校验
            //else
            //{
            //    return Ok(new Models.ResponseData() { Code = 500, ErrorMsg = "校验失败" });
            //}
        }

        [MyAuth]
        [HttpGet]
        [Route(template: "getuser")]
        public IHttpActionResult GetUserInfo()
        {
            return Ok(new Models.ResponseData() { Data = ((UserIdentity)User.Identity).Id });
            //return Ok(new Models.ResponseData() { Data = User.Identity.Name });
            //return Ok(User.Identity.Name);
        }
    }
}
