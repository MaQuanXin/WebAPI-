using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAPI.Auth
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity(string name, int id)
        {
            Name = name;
            Id = id;
        }

        //没有加set 这是C#6.0的一个特性，对外只读，对内自己可写读
        //相当于一下写法
        //public string Name { get; private set; }

        public string Name { get; }

        public int Id { get; set; }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }

        //public string Name => throw new NotImplementedException();

        //public string AuthenticationType => throw new NotImplementedException();

        //public bool IsAuthenticated => throw new NotImplementedException();
    }

    public class ApplicationUser : IPrincipal
    {
        public ApplicationUser(string name, int id)
        {
            Identity = new UserIdentity(name, id);
        }

        public IIdentity Identity { get; }
        //public IIdentity Identity => throw new NotImplementedException();

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}