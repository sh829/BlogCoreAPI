using BlogCore.AuthHelper.OverWrite;
using BlogCore.AuthHelper.Policys;
using BlogCore.Common.Helper;
using BlogCore.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Route("api/Login")]
    public class LoginController:Controller
    {
        readonly ISysUserInfoServices _sysUserInfoServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IRoleServices _roleServices;
        readonly PermissionRequirement _requirement;
        public LoginController(ISysUserInfoServices sysUserInfoServices,IUserRoleServices  userRoleServices,IRoleServices  roleServices, PermissionRequirement requirement)
        {
            _sysUserInfoServices =  sysUserInfoServices;
            _requirement = requirement;
            _roleServices = roleServices;
            _userRoleServices = userRoleServices;
        }
        [HttpPost]
        public async Task<object> GetJwtStr(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;

            // 获取用户的角色名，请暂时忽略其内部是如何获取的，可以直接用 var userRole="Admin"; 来代替更好理解。
            //var userRole = await _sysUserInfoServices.GetUserRoleNameStr(name, pass);
            var userRole = "Admin";
            if (userRole != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = userRole };
                jwtStr =await JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }

            return Ok(new
            {
                success = suc,
                token = jwtStr
            });
        }
        /// <summary>
        /// 获取JWT的方法3
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("JwtToken3.0")]
        public async Task<object> GetJwtToken3(string name="",string pass = "")
        {
            string jwtStr = string.Empty;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
            {
                return new JsonResult(new {
                    success = false,
                    message="用户名或密码不能为空"
                });
            }
            //把密码加密
            pass = MD5Helper.MD5Encrypt32(pass);
            //查询用户
            var user = await _sysUserInfoServices.Query(a => a.uLoginName == name && a.uLoginPWD == pass);
            //如果存在这个用户
            if (user.Count() > 0)
            {
                //获取用户角色
                var userRoles = await _sysUserInfoServices.GetUserRoleNameStr(name, pass);
                //如果是基于用户的授权策略，这里要添加用户，如果是基于角色的授权策略，这里要添加角色
                var claims= new List<Claim>
                {
                    new Claim(ClaimTypes.Name,name),
                    new Claim(JwtRegisteredClaimNames.Jti,user.FirstOrDefault().uID.ToString()),
                    new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return new JsonResult(token);
            }
            else
            {
                return new JsonResult(new
                {
                    success = false,
                    message="认证失败"
                });
            }
        }
    }
}
