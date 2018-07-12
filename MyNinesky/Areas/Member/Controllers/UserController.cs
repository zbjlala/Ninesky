using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MyNinesky.Areas.Member.Models;
using Ninesky.BLL;
using Ninesky.Common;
using Ninesky.IBLL;
using Ninesky.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyNinesky.Areas.Member.Controllers
{

    public class UserController : Controller
    {
        private InterfaceUserService userService;
        #region 属性
        private IAuthenticationManager Authentication { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion
        public UserController()
        {
            userService = new UserService();
        }
      
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            string verificationCode = Security.CreateVerificationText(6);

            Bitmap _img = Picture.CreateVerificationImage(verificationCode,160,30);

            _img.Save(Response.OutputStream,System.Drawing.Imaging.ImageFormat.Jpeg);

            TempData["VerificationCode"] = verificationCode.ToUpper();

            return null; 
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var _user = userService.Find(loginViewModel.UserName);
                if(_user == null)
                {
                    ModelState.AddModelError("UserName","用户名不存在");
                }
                else if(_user.Password == Security.Sha256(loginViewModel.PassWord))
                {
                    _user.LoginTime = System.DateTime.Now;
                    _user.LoginIP = Request.UserHostAddress;
                    userService.Update(_user);
                    var _identity = userService.CreateIdentity(_user,DefaultAuthenticationTypes.ApplicationCookie);
                    Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    Authentication.SignIn(new AuthenticationProperties()
                    { IsPersistent = loginViewModel.RememberMe},_identity);
                    return RedirectToAction("index","Home");
                }
                else
                {
                    ModelState.AddModelError("Password","密码错误");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if(TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != register.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode","验证码不正确");

                return View(register);
            }
            if(ModelState.IsValid)
            {
                if (userService.Exist(register.UserName))
                {
                    ModelState.AddModelError("UserName","用户名已存在");
                }
                else
                {
                    User _user = new User()
                    {
                        UserName = register.UserName,
                        DisplayName = register.DisplayName,
                        Password = Security.Sha256(register.Password),
                        Email = register.Email,
                        Status = 0,
                        RegistrationTime = System.DateTime.Now
                    };
                    _user = userService.Add(_user);
                    if(_user.UserID > 0)
                    {
                        var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                        Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        Authentication.SignIn(_identity);
                        return RedirectToAction("Index","Home");
                        //return Content("注册成功！");
                    }
                    else
                    {
                        ModelState.AddModelError("","注册失败！");
                    }
                }
                
            }
            return View(register);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(Url.Content("~/"));
        }
        /// <summary>
        /// 用户导航栏
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return PartialView();
        }
        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View(userService.Find(User.Identity.Name));
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// 修改资料提交
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Modify()
        {
            var _user = userService.Find(User.Identity.Name);
            if(_user == null)
            {
                ModelState.AddModelError("","用户不存在");
            }
            else
            {
                if(TryUpdateModel(_user,new string[] { "DisplayName","Email"}))
                {
                    if(ModelState.IsValid)
                    {
                        if(userService.Update(_user))
                        {
                            ModelState.AddModelError("","修改成功!");
                        }
                        else
                        {
                            ModelState.AddModelError("","无需要修改的资料");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("","更新模型数据失败");
                }
            }
            return View("Details",_user);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if(ModelState.IsValid)
            {
                var _user = userService.Find(User.Identity.Name);
                if(_user.Password == Security.Sha256(passwordViewModel.Password))
                {
                    _user.Password = Security.Sha256(passwordViewModel.Password);
                    if(userService.Update(_user))
                    {
                        ModelState.AddModelError("","修改密码成功");
                    }
                    else
                    {
                        ModelState.AddModelError("","修改密码失败");
                    }
                }
                else
                {
                    ModelState.AddModelError("","原密码错误");
                }


            }
            return View(passwordViewModel);
        }
    }
}