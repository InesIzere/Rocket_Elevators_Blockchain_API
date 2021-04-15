using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainsController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public BlockchainsController(RestAPIContext context)
        {
            _context = context;

        }
    //get all nodeName

    [HttpGet]
        public async Task<ActionResult<IEnumerable<Blockchain>>> Getcustomers()
        {
            return await _context.blockchains.ToListAsync();
        }
//----------------------------------- Retrieving all information from a specific Building -----------------------------------\\

        // GET: api/Buildings/id

        [HttpGet("{id}")]
        public async Task<ActionResult<Blockchain>> GetBlockchain(long id)
        {
            var blockchain = await _context.blockchains.FindAsync(id);

            if (blockchain == null)
            {
                return NotFound();
            }

            return blockchain;
        }


        [HttpGet("nodeName/{node}")]
        public object GetEmailCustomer(string node)
        {
            var blockchain = _context.blockchains.Where(e=>e.nodeName == node);

            if (blockchain == null)
            {
                return NotFound();
            }

            return blockchain;
        }
    }
}