using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Auth;

namespace WebAPI.Filters
{
    public class MyAuthAttribute : Attribute, IAuthorizationFilter
    {

        public bool AllowMultiple => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            //获取request-->headers-->token

            IEnumerable<string> headers;
            if (actionContext.Request.Headers.TryGetValues(name: "token", out headers))
            {
                //如果获取到了headers里的token
                //token 
                var loginName = JwtTools.Decode(jwtStr: headers.First())["LoginName"].ToString();
                var userId = (int)JwtTools.Decode(jwtStr: headers.First())["UserId"];

                (actionContext.ControllerContext.Controller as ApiController).User = new ApplicationUser(loginName, userId);


                //异步方法-所以要加await
                return await continuation();
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);


            throw new NotImplementedException();
        }
    }
}