using BlogCore.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Party")]
    [Authorize(Policy = "Admin")]
    public class PartyController : Controller
    {
        private readonly IPartyServices _PartyServices;
        public PartyController(IPartyServices PartyServices)
        {
            _PartyServices = PartyServices;
        }

        
    }
}
