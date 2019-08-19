using BlogCore.IRepository;
using BlogCore.IServices;
using BlogCore.Model.Models;
using BlogCore.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Services
{
    public class ExcelMapToModelServices:BaseServices<ExcelMapToModel>, IExcelMapToModelServices
    {
        IExcelMapToModelRespository _dal;
        public ExcelMapToModelServices(IExcelMapToModelRespository dal)
        {
            _dal = dal;
            base.baseDal = _dal;
        }
        public string QueryByExcelName(string name, string modelName)
        {
            return _dal.QueryByExcelName(name, modelName);
        }
    }
}
