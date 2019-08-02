using BlogCore.IRepository;
using BlogCore.Model.Models;
using BlogCore.Model.ViewModels;
using BlogCore.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Repository
{
    public class PartyBookInfoRepository : BaseRespository<PartyBookInfo>, IPartyBookInfoRespository
    {
        public async Task<List<PartyBookInfoViewModel>> GetPartyBookInofs(int page, int pageSize, string key,string strOrderByFileds)
        {

            var info = await db.Queryable<CustomInfo, PartyBookInfo>(
                (c, p) => new object[] {
                    JoinType.Left,p.CustomId==c.Id
                 }
                )
                .WhereIF(!string.IsNullOrEmpty(key), (c,p)=>c.IsDelete!=true&&c.Name.Contains(key))
                .Select((c, p) => new PartyBookInfoViewModel
                {
                    CustomId = c.Id,
                    BookInfoId = p.Id,
                    AdvisoryTime = c.AdvisoryTime,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Age = c.Birthday,
                    ScheduledDate = c.ScheduledDate,
                    ActualNumber = p.ActualNumber,
                    TopicName = p.TopicName,
                    TopicColor = p.TopicColor,
                    Price = p.Price,
                    BuffetCost = p.BuffetCost,
                    FoodCost = p.FoodCost,
                    TotalCost = p.TotalCost
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
               // .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                .ToListAsync();
            return info;
        }
    }
}
