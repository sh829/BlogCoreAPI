using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogCore.IRepository;
using BlogCore.Model.Models;
using BlogCore.Repository.Base;

namespace BlogCore.Repository
{
    public partial class PasswordLibRepository : BaseRespository<PasswordLib>, IPasswordLibRepository
    {

    }
}
