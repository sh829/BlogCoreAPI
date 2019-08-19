using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogCore.Common.Helper;

namespace BlogCore.Model.Seed
{
    public class DbSeed
    {
        private static string GitJsonFileFormat = "https://github.com/anjoy8/Blog.Data.Share/raw/master/Blog.Core.Data.json/{0}.tsv";

        public static async Task SeedAsync(MyContext myContext)
        {
            try
            {
                //codefirst创建数据库 ，如果是第二次执行，注释掉即可
                myContext.CreateTableByEntity(false,

                    //typeof(Module),
                    //typeof(ModulePermission),
                    //typeof(OperateLog),
                    //typeof(PasswordLib),
                    //typeof(Permission),
                    //typeof(Role),
                    //typeof(RoleModulePermission),
                    //typeof(sysUserInfo),
                    //typeof(UserRole),
                    //typeof(Topic),
                    //typeof(TopicDetail),
                    ////以下是业务表
                    //typeof(CustomInfo),
                    //typeof(ExtraProjectInfo),
                    //typeof(ExtraOtherProject),
                    //typeof(Dictionary),
                    //typeof(PartyAdvisoryInfo),
                    //typeof(PartyBookInfo),
                    //typeof(PartyDemands),
                    typeof(GraduationStatistics)
                    //typeof(ExcelMapToModel)
                    );
                //下面就是种子数据
                #region CustomInfo
                if (!await myContext.Db.Queryable<CustomInfo>().AnyAsync())
                {
                    myContext.GetEntityDB<CustomInfo>().Insert(
                        new CustomInfo
                        {
                            Name = "管理员",
                            PhoneNumber = "158888888",
                            Birthday = "11岁",
                            AdvisoryTime = DateTime.Now,
                            ScheduledDate = DateTime.Now,
                            CreatedBy = "wangchao",
                            UpdatedBy = "wangchao",
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,

                        });
                    Console.WriteLine("Done seeding database.");
                    Console.WriteLine();
                }
                #endregion
                #region Module
                if (!await myContext.Db.Queryable<Module>().AnyAsync())
                {
                    myContext.GetEntityDB<Module>().InsertRange(JsonHelper.ParseFormByJson<List<Module>>(GetNetData.Get(string.Format(GitJsonFileFormat, "Module"))));
                    Console.WriteLine("Table:Module created success!");
                }
                else
                {
                    Console.WriteLine("Table:Module already exists...");
                }
                #endregion


                #region Permission
                if (!await myContext.Db.Queryable<Permission>().AnyAsync())
                {
                    myContext.GetEntityDB<Permission>().InsertRange(JsonHelper.ParseFormByJson<List<Permission>>(GetNetData.Get(string.Format(GitJsonFileFormat, "Permission"))));
                    Console.WriteLine("Table:Permission created success!");
                }
                else
                {
                    Console.WriteLine("Table:Permission already exists...");
                }
                #endregion


                #region Role
                if (!await myContext.Db.Queryable<Role>().AnyAsync())
                {
                    myContext.GetEntityDB<Role>().InsertRange(JsonHelper.ParseFormByJson<List<Role>>(GetNetData.Get(string.Format(GitJsonFileFormat, "Role"))));
                    Console.WriteLine("Table:Role created success!");
                }
                else
                {
                    Console.WriteLine("Table:Role already exists...");
                }
                #endregion


                #region RoleModulePermission
                if (!await myContext.Db.Queryable<RoleModulePermission>().AnyAsync())
                {
                    myContext.GetEntityDB<RoleModulePermission>().InsertRange(JsonHelper.ParseFormByJson<List<RoleModulePermission>>(GetNetData.Get(string.Format(GitJsonFileFormat, "RoleModulePermission"))));
                    Console.WriteLine("Table:RoleModulePermission created success!");
                }
                else
                {
                    Console.WriteLine("Table:RoleModulePermission already exists...");
                }
                #endregion


                #region Topic
                if (!await myContext.Db.Queryable<Topic>().AnyAsync())
                {
                    myContext.GetEntityDB<Topic>().InsertRange(JsonHelper.ParseFormByJson<List<Topic>>(GetNetData.Get(string.Format(GitJsonFileFormat, "Topic"))));
                    Console.WriteLine("Table:Topic created success!");
                }
                else
                {
                    Console.WriteLine("Table:Topic already exists...");
                }
                #endregion


                #region TopicDetail
                if (!await myContext.Db.Queryable<TopicDetail>().AnyAsync())
                {
                    myContext.GetEntityDB<TopicDetail>().InsertRange(JsonHelper.ParseFormByJson<List<TopicDetail>>(GetNetData.Get(string.Format(GitJsonFileFormat, "TopicDetail"))));
                    Console.WriteLine("Table:TopicDetail created success!");
                }
                else
                {
                    Console.WriteLine("Table:TopicDetail already exists...");
                }
                #endregion


                #region UserRole
                if (!await myContext.Db.Queryable<UserRole>().AnyAsync())
                {
                    myContext.GetEntityDB<UserRole>().InsertRange(JsonHelper.ParseFormByJson<List<UserRole>>(GetNetData.Get(string.Format(GitJsonFileFormat, "UserRole"))));
                    Console.WriteLine("Table:UserRole created success!");
                }
                else
                {
                    Console.WriteLine("Table:UserRole already exists...");
                }
                #endregion


                #region sysUserInfo
                if (!await myContext.Db.Queryable<sysUserInfo>().AnyAsync())
                {
                    myContext.GetEntityDB<sysUserInfo>().InsertRange(JsonHelper.ParseFormByJson<List<sysUserInfo>>(GetNetData.Get(string.Format(GitJsonFileFormat, "sysUserInfo"))));
                    Console.WriteLine("Table:sysUserInfo created success!");
                }
                else
                {
                    Console.WriteLine("Table:sysUserInfo already exists...");
                }
                #endregion


                Console.WriteLine("Done seeding database.");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception("初始化种子数据失败" + ex.Message);
            }
        }
    }
}
