using BlogCore.IRepository;
using BlogCore.IServices;
using BlogCore.Model.Models;
using BlogCore.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Services
{
    public class DictionaryServices:BaseServices<Dictionary>, IDictionaryServices
    {
        IDictionaryRepository _dal;
        public DictionaryServices(IDictionaryRepository dal)
        {
            _dal = dal;
            base.baseDal = _dal;
        }
    }
}
