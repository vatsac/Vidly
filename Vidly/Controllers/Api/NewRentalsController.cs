using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            using (VidlyEntities1 db = new VidlyEntities1())
            {
                var customer = db.customers.Single(c => c.Id == newRental.CustomerId);
                var movies = db.Movies.Where(m => newRental.MovieIds.Contains(m.ID)).ToList();
                foreach (var item in movies)
                {
                    item.NumberAvailable--;
                    var rental = new Rental
                    {
                        customer = customer,
                        Movie = item,
                        DateRented = DateTime.Now
                    };
                    db.Rentals.Add(rental);
                }
                db.SaveChanges();


                return Ok();
            }
        }
    }
}
