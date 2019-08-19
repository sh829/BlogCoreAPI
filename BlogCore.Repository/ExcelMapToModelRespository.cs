using BlogCore.IRepository;
using BlogCore.Model.Models;
using BlogCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Repository
{
   public  class ExcelMapToModelRespository:BaseRespository<ExcelMapToModel>, IExcelMapToModelRespository
    {
        public string QueryByExcelName(string name,string modelName)
        {
            return db.Queryable<ExcelMapToModel>().WhereIF(!string.IsNullOrEmpty(name), a =>a.ModelName== modelName && a.ExcelColumn == name).First()?.ModelProperty;
        }
    }
}
