using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using BlogCore.AOP;
using BlogCore.AuthHelper.Policys;
using BlogCore.Common.GlobalVar;
using BlogCore.Common.Helper;
using BlogCore.Common.HttpContextUser;
using BlogCore.Common.MemoryCache;
using BlogCore.Common.Redis;
using BlogCore.IServices;
using BlogCore.MiddleWares;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace BlogCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 部分服务注入 使用netcore自带方法注入
            //缓存 注入
            services.AddScoped<ICahing, MemoryCaching>();
            //reids注入
            services.AddSingleton<IRedisCacheManager, RedisCacheManger>();
            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            #region 初始化数据库
            services.AddScoped<BlogCore.Model.Seed.DbSeed>();
            services.AddScoped<BlogCore.Model.Seed.MyContext>();
            #endregion
            #region AutoMapper
            //在Startup中，注入服务
            services.AddAutoMapper(typeof(Startup));//这是AutoMapper的2.0新特性
            #endregion
            #region CORS
            //跨域方法 一
            services.AddCors(c =>
            {
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequest", policy =>
                {
                    policy.AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方法
                    .AllowAnyHeader()//允许任何请求头
                    .AllowCredentials();//允许cookie
                });
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                //一般采用这种方法
                c.AddPolicy("LimitRequest", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:1818", "http://127.0.0.1:1818", "http://127.0.0.1:2364")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });

            });
            //跨域方法 二 注意下边 Configure 中进行配置
            //services.AddCors();
            #endregion
            #region SwaggerUI Service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v0.1.0",
                    Title = "blog.core.api",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Blog.Core",
                        Email = "243237408@qq.com",
                        Url = "https://www.jianshu.com/u/94102b59cc2a"
                    }
                });
                #region 读取xml信息
                var xmlPath = Path.Combine(basePath, "BlogCore.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlModelPath = Path.Combine(basePath, "BlogCoreModel.xml");
                c.IncludeXmlComments(xmlModelPath);
                #endregion
                #region Token绑定到ConfigureServices
                //添加header验证信息
                var security = new Dictionary<string, IEnumerable<string>> { { "BlogCore", new string[] { } }, };
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("BlogCore", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion

            });

            #endregion
            #region Httpcontext

            // Httpcontext 注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            #endregion
            #region Authorize 权限认证三步走
            //1、如果你只是简单的基于角色授权的，仅仅在 api 上配置，第一步：【1/2 简单角色授权】，第二步：配置【统一认证服务】，第三步：开启中间件

            //2、如果你是用的复杂的基于策略授权，配置权限在数据库，第一步：【3复杂策略授权】，第二步：配置【统一认证服务】，第三步：开启中间件app.UseAuthentication();

            //3、综上所述，设置权限，必须要三步走，授权 + 配置认证服务 + 开启授权中间件，只不过自定义的中间件不能验证过期时间，所以我都是用官方的。
            #region 【第一步：授权】
            #region 1、基于角色的API授权
            // 1【授权】、这个很简单，其他什么都不用做， 只需要在API层的controller上边，增加特性即可，注意，只能是角色的:
            // [Authorize(Roles = "Admin,System")]
            #endregion
            #region 2、基于策略的授权（简单版）
            // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("SystemOrAdmin").Build());
            });
            #endregion
            #region 【3、复杂策略授权】
            #region 参数
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);


            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<AuthHelper.Policys.PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new AuthHelper.Policys.PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                audienceConfig["Issuer"],//发行人
                audienceConfig["Audience"],//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            #endregion

            //【授权】
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name,
                         policy => policy.Requirements.Add(permissionRequirement));
            });
            #endregion
            #endregion
            #region 【第二步：配置认证服务】
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//发行人
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            //2.1【认证】、core自带官方JWT认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });


            //2.2【认证】、IdentityServer4 认证 (暂时忽略)
            //services.AddAuthentication("Bearer")
            //  .AddIdentityServerAuthentication(options =>
            //  {
            //      options.Authority = "http://localhost:5002";
            //      options.RequireHttpsMetadata = false;
            //      options.ApiName = "blog.core.api";
            //  });
            // 注入权限处理器

            services.AddSingleton<IAuthorizationHandler, AuthHelper.Policys.PermissionHandler>();
            services.AddSingleton(permissionRequirement);
            #endregion
            //.netcore自带的依赖注入方式
            //services.AddScoped<IRoleModulePermissionServices, RoleModulePermissionServices>();
            #endregion
            #region autofac
            //实例化autofac容器
            var bulid = new ContainerBuilder();
            //注册要通过反射创建的组件
            //bulid.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
            bulid.RegisterType<BlogRedisCacheAOP>();
            bulid.RegisterType<BlogCacheAOP>();
            bulid.RegisterType<BlogLogAOP>();//可以直接替换其他拦截器
            
            #region 1、服务程序集注入方式 —— 未解耦  =>没有接口层的服务层注入
            //整个程序集注入
            //注意 这个注入的是实现类层，不是接口层，不是IServices
            //var assemblyServices = Assembly.Load("BlogCore.Services");
            ////指定已扫描程序集中的类型注册为提供所有其实现类接口
            //bulid.RegisterAssemblyTypes(assemblyServices).AsImplementedInterfaces();
            //var assemblyRespository = Assembly.Load("BlogCore.Repository");
            //bulid.RegisterAssemblyTypes(assemblyRespository).AsImplementedInterfaces();
            #endregion
            #region 2、程序集注入 —— 实现层级解耦 =>带有接口层的服务注入
            //获取注入绝对路径
            var serviceDllFile = Path.Combine(basePath, "BlogCore.Services.dll");
            //直接采用加载文件的方法
            var assemblyServices = Assembly.LoadFrom(serviceDllFile);
            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            var cacheType = new List<Type>();
            if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            {
                cacheType.Add(typeof(BlogCacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
            {
                cacheType.Add(typeof(BlogLogAOP));
            }
            if(Appsettings.app(new string[] { "AppSettings", "RedisCaching", "Enabled" }).ObjToBool()){
                cacheType.Add(typeof(BlogRedisCacheAOP));
            }
            bulid.RegisterAssemblyTypes(assemblyServices).
                AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()//对目标类型启用接口拦截。拦截器将被确定，通过在类或接口上截取属性, 或添加 InterceptedBy ()
                                              //.InterceptedBy(typeof(BlogLogAOP))
                .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。说人话就是，将拦截器添加到要注入容器的接口或者类之上。
            var repositoryDllFile = Path.Combine(basePath, "BlogCore.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            bulid.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();
            #endregion
            #region 3 没有接口的单独类 class注入
            #endregion
            //将service填充到autofac容器生成器
            bulid.Populate(services);
            var applicationContainer = bulid.Build();
            #endregion
            //第三方IOC接管core内置DI容器
            return new AutofacServiceProvider(applicationContainer);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                #region swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHple V1");
                    c.RoutePrefix = "";
                });
                #endregion
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            #region 第三步：开启认证中间件

            //此授权认证方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件，第二种写法//app.UseMiddleware<JwtTokenAuth>(); 
            app.UseJwtTokenAuth();

            //如果你想使用官方认证，必须在上边ConfigureService 中，配置JWT的认证服务 (.AddAuthentication 和 .AddJwtBearer 二者缺一不可)
            app.UseAuthentication();
            #endregion
            #region CORS
            //跨域的方法一 使用策略 详细策略信息在configureServices中
            app.UseCors("LimitRequest");//将CORS中间件添加到Web应用程序管道中，以允许跨域请求
            //跨域第二 种版本，请要ConfigureService中配置服务 services.AddCors();
            //app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader().AllowAnyMethod());
            #endregion
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
