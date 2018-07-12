using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.BLL
{
    /// <summary>
    /// 用户服务基类
    /// </summary>
    public class UserService : BaseService<User>, InterfaceUserService
    {
        public UserService() : base(RepositoryFactory.UserRepository) { }
       

        public ClaimsIdentity CreateIdentity(User user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name,user.UserName));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            _identity.AddClaim(new Claim("DisplayName", user.DisplayName));
            return _identity;
        }

        public bool Exist(string userName)
        {
            return CurrentRepository.Exist(u => u.UserName == userName);
        }

        //public User Find(int userID)
        //{
        //    return CurrentRepository.Find(u => u.UserID == userID);
        //}
        public User Find(string userName)
        {
            return CurrentRepository.Find(u => u.UserName == userName);
        }
        public IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order)
        {
            IQueryable<User> _users = CurrentRepository.Entities;
            switch (order)
            {
                case 0:
                    _users = _users.OrderBy(c => c.UserID);
                    break;
                case 1:
                    _users = _users.OrderByDescending(c => c.UserID);
                    break;
                case 2:
                    _users = _users.OrderBy(c => c.RegistrationTime);
                    break;
                case 3:
                    _users = _users.OrderByDescending(c => c.RegistrationTime);
                    break;
                case 4:
                    _users = _users.OrderBy(c => c.LoginTime);
                    break;
                case 5:
                    _users = _users.OrderByDescending(c => c.LoginTime);
                    break;
                default:
                    _users = _users.OrderByDescending(c => c.UserID);
                    break;
            }
            totalRecord = _users.Count();
            return PageList(_users, pageIndex, pageSize);
        }
    }
}
