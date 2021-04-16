using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
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

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Blockchain>> GetBlockchain(long id)
        // {
        //     var blockchain = await _context.blockchains.FindAsync(id);

        //     if (blockchain == null)
        //     {
        //         return NotFound();
        //     }

        //     return blockchain;
        // }


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
        
  // put the address
     // PUT: api/Elevators/id/Status        
        [HttpPut("{id}/node")]
        public async Task<ActionResult<string>> PutBlockchain([FromRoute] long id, Blockchain blockchain)
        {
            if (id != blockchain.id)
            {
                return BadRequest();
            }
            
            if (blockchain.nodeName == "ProjectOffice" || blockchain.nodeName == "MaterialProvider" || blockchain.nodeName == "SolutionManufacturing" ||blockchain.nodeName == "QualitySecurity")
            {
                Blockchain blockchainFound = await _context.blockchains.FindAsync(id);
                blockchainFound.address = blockchain.address;

                try
                {
                    await _context.SaveChangesAsync();
                    return blockchain.nodeName;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!blockchainExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Content("Valid status: ProjectOffice, MaterialProvider, SolutionManufacturing, QualitySecurity . Try again!  ");
        }        

        //post
        [HttpPost("update")]
        public async Task<IActionResult> PutmodifyBlochain(Blockchain cust)
        {
            
            if (cust == null)
            {
                return BadRequest();
            }
            var cus = await _context.blockchains.FindAsync(cust.id);

            
            cus.address = cust.address;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!blockchainExists(cust.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult("it changed");
        }

        [HttpPost]
        public async Task<ActionResult<Blockchain>> PostBlockchain(Blockchain newBlockchain)
        {
            // newBlockchain.nodeName= newBlockchain.nodeName;
            // newBlockchain.address = newBlockchain.address;
           

            _context.blockchains.Add(newBlockchain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

         private bool blockchainExists(long id)
        {
            return _context.blockchains.Any(e => e.id == id);
        }
    //    [HttpPost]
    //     public async Task<IActionResult> PostNewStudent([FromBody]Blockchain student)
    //     {
    //         try{
    //             if (student == null)
    //             {
    //                 return BadRequest();
    //             };

    //             using (var ctx = new RestAPIContext())
    //             {
    //                 ctx.blockchains.Add(new Blockchain()
    //                 {
    //                     nodeName = student.nodeName,
    //                     address = student.address,
                       
                    
    //                 });
    //                 ctx.SaveChanges();
    //                 await _context.SaveChangesAsync();
                    
                    
    //             }


    //         }
    //         catch(Exception e) {
    //                 int i =0;
    //         }
    //         return Ok();
    //     }

      



    } 
    
}