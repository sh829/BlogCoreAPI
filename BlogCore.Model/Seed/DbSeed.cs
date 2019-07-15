using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Model.Seed
{
    public class DbSeed
    {
        public static async Task SeedAsync(MyContext myContext)
        {
            try
            {
                //codefirst创建数据库 ，如果是第二次执行，注释掉即可
                //myContext.CreateTableByEntity(false, typeof(CustomInfo));
                //下面就是种子数据
                #region CustomInfo
                if(!await myContext.Db.Queryable<CustomInfo>().AnyAsync())
                {
                    myContext.GetEntityDB<CustomInfo>().Insert(
                        new CustomInfo {
                            Name = "校长",
                            PhoneNumber = "158888888",
                            Birthday = DateTime.Now,
                            AdvisoryTime=DateTime.Now,
                            ScheduledDate=DateTime.Now,
                            PredictNumber=10,
                            CreatedBy="wangchao",
                            UpdatedBy="wangchao",
                            CreateTime=DateTime.Now,
                            UpdateTime=DateTime.Now,

                        });
                    Console.WriteLine("Done seeding database.");
                    Console.WriteLine();
                }
                #endregion
            }
            catch(Exception ex)
            {
                throw new Exception("初始化种子数据失败" + ex.Message);
            }
        }
    }
}
