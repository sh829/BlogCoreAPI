using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogCore.IRepository;
using BlogCore.IServices;
using BlogCore.Model.Models;
using BlogCore.Services.Base;

namespace Blog.Core.Services
{
    public partial class PasswordLibServices : BaseServices<PasswordLib>, IPasswordLibServices
    {
        IPasswordLibRepository _dal;
        public PasswordLibServices(IPasswordLibRepository dal)
        {
            this._dal = dal;
            base.baseDal = dal;
        }

    }
}
