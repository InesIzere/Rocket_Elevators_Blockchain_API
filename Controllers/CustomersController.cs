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
    public class CustomersController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public CustomersController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            return await _context.customers.ToListAsync();
        }

//------------------------------ Retrieving just Info of Customer using the e-mail -------------------------------\\

        //GET: api/Customers/email
        [HttpGet("{email}")]
        public object GetEmailCustomer(string email)
        {
            var customer = _context.customers.Where(e=>e.email_of_company_contact == email);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

//--------- Retrieving all Info of Customer (Building, Battery, Columns and Elevators) using the e-mail ---------\\

        //GET: api/Customers/FullInfo/email
        [HttpGet("FullInfo/{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            
            var customer = await _context.customers.Include("Buildings.Batteries.Columns.Elevators")
                                                    .Where(c => c.email_of_company_contact == email)
                                                    .FirstOrDefaultAsync();                     

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        } 

//--------------------------------------------------- Update Customer ---------------------------------------------------------------\\       

        // PUT: api/Customers
        [HttpPut]
        public async Task<ActionResult<Customer>> PutCustomer(Customer customer)
        {
            var customerToUpdate = await _context.customers
                                                .Where(c => c.email_of_company_contact == customer.email_of_company_contact)
                                                .FirstOrDefaultAsync(); 

            if (customerToUpdate == null)
            {
                return NotFound();
            }

            customerToUpdate.company_name = customer.company_name;
            customerToUpdate.full_name_of_company_contact = customer.full_name_of_company_contact;
            customerToUpdate.company_contact_phone = customer.company_contact_phone;
            customerToUpdate.email_of_company_contact = customer.email_of_company_contact;
            customerToUpdate.company_description = customer.company_description;
            customerToUpdate.full_name_of_service_technical_authority = customer.full_name_of_service_technical_authority;
            customerToUpdate.technical_authority_phone_for_service_ = customer.technical_authority_phone_for_service_;
            customerToUpdate.technical_manager_email_for_service = customer.technical_manager_email_for_service;

            await _context.SaveChangesAsync();

            return NoContent();
        }     

        private bool CustomerExists(long id)
        {
            return _context.customers.Any(e => e.id == id);
        }
    }
}
