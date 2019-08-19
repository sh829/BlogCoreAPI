using BlogCore.IRepository.Base;
using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.IRepository
{
    public interface IExcelMapToModelRespository : IBaseRespository<ExcelMapToModel>
    {
        string QueryByExcelName(string name, string modelName);
    }
}
