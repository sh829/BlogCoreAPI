using BlogCore.Model.Models;
using BlogCore.IServices.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogCore.Model.ViewModels;
using BlogCore.Model;

namespace BlogCore.IServices
{
    public interface IGraduationStatisticsServices : IBaseServices<GraduationStatistics>
    {
        //Task<List<CustomInfoViewModel>> GetCustomInfos();
    }
}
