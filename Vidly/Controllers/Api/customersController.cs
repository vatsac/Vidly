using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class customersController : ApiController
    {
        public IHttpActionResult GetCustomers(string query = null)
        {
            using (VidlyEntities1 _context= new VidlyEntities1())
            {
                var customersQuery = _context.customers
                .Include(c => c.MembershipType);

                if (!String.IsNullOrWhiteSpace(query))
                    customersQuery = customersQuery.Where(c => c.Name.Contains(query));

                var customerDtos = customersQuery
                    .ToList()
                    .Select(Mapper.Map<customer, CustomerDto>);

                return Ok(customerDtos);

            }

           
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            using(VidlyEntities1 db=new VidlyEntities1())
            {
                var customer = db.customers.SingleOrDefault(c => c.Id == id);

                if (customer == null)
                    return NotFound();

                return Ok(Mapper.Map<customer, CustomerDto>(customer));
            }
            
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            using(VidlyEntities1 _context=new VidlyEntities1())
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var customer = Mapper.Map<CustomerDto, customer>(customerDto);
                _context.customers.Add(customer);
                _context.SaveChanges();

                customerDto.Id = customer.Id;
                return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);

            }
           
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            using (VidlyEntities1 _context = new VidlyEntities1())
            {


                if (!ModelState.IsValid)
                    return BadRequest();

                var customerInDb = _context.customers.SingleOrDefault(c => c.Id == id);

                if (customerInDb == null)
                    return NotFound();

                Mapper.Map(customerDto, customerInDb);

                _context.SaveChanges();

                return Ok();
            }
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            using(VidlyEntities1 _context=new VidlyEntities1())
            {
                var customerInDb = _context.customers.SingleOrDefault(c => c.Id == id);

                if (customerInDb == null)
                    return NotFound();

                _context.customers.Remove(customerInDb);
                _context.SaveChanges();

                return Ok();

            }
           
        }
    }
}
