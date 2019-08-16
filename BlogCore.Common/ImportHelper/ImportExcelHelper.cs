using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public static bool ImportExcel(Stream stream)
        {
            var data = OfficeHelper.OfficeHelper.ReadStreamToDataTable(stream);
            var column = data.Columns;
            //把datatable和TModel进行对应
            Type modelType =typeof(TModel);
            var props = modelType.GetProperties();
            foreach (DataRow row in data.Rows)
            {
                object[] d = row.ItemArray;
                TModel model = new TModel
                {
                    
                };
            }

            return false;
        }
    }
}
