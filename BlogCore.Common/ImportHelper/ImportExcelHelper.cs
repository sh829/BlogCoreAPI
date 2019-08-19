using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;


namespace BlogCore.Common.ImportHelper
{
    public class ImportExcelHelper<TModel> where TModel:class,new()
    {
        //internal SqlSugarClient _db { get; set; }
        //public DbContext _context { get; set; }
        public ImportExcelHelper()
        {
            //DbContext.Init(BaseDBConfig.ConnectionString, (DbType)BaseDBConfig.DbType);
            //_context = DbContext.GetDbContext();
            //_db = context.Db;
        }
        public static IList<TModel> ImportExcel(DataTable data)
        {
            IList<TModel> tList = new List<TModel>();
            //把datatable和TModel进行对应
            Type modelType =typeof(TModel);
            var props = modelType.GetProperties();
            //获取泛型的程序集
            Assembly ass = Assembly.GetAssembly(modelType);
            foreach (DataRow row in data.Rows)
            {
                //泛型实例化
                object _obj = ass.CreateInstance(modelType.FullName);
                foreach(PropertyInfo pro in props)
                {
                    //判断属性的类型是不是string
                    if (pro.PropertyType.Equals(typeof(string)))
                    {
                        string _name = pro.Name;
                        if (data.Columns.Contains(_name))
                        {
                            //给泛型属性赋值
                            pro.SetValue((TModel)_obj, row[_name]?.ToString(), null);
                        }
                      
                    }
                }
                tList.Add((TModel)_obj);
            }

            return tList;
        }
    }
}
