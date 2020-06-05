using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TamagotchiAPI.Models;

namespace TamagotchiAPI
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public PetsController(DatabaseContext context)
        {
            _context = context;
        }
    }
}