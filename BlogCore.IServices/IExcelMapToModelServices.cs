using BlogCore.IServices.Base;
using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.IServices
{
    public interface IExcelMapToModelServices : IBaseServices<ExcelMapToModel>
    {
        string QueryByExcelName(string name, string modelName);
    }
}
