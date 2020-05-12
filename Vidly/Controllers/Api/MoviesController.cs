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
    public class MoviesController : ApiController
    {
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            using (VidlyEntities1 _context = new VidlyEntities1())
            {
                var moviesQuery = _context.Movies
                .Include(m => m.genre)
                .Where(m => m.NumberAvailable > 0);

                if (!String.IsNullOrWhiteSpace(query))
                    moviesQuery = moviesQuery.Where(m => m.MovieName.Contains(query));

                return moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);

                //return Ok(db.Movies.Include(m => m.genre).ToList().Select(Mapper.Map<Movie, MovieDto>));
            }
        }

        // GET: api/Movie/5
        public IHttpActionResult GetMovie(int id)
        {
            using (VidlyEntities1 db = new VidlyEntities1())
            {
                var movie = db.Movies.SingleOrDefault(m => m.ID == id);
                if (movie == null)
                    return BadRequest();
                return Ok(Mapper.Map<Movie, MovieDto>(movie));
            }
        }

        // POST: api/Movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            using (VidlyEntities1 db = new VidlyEntities1())
            {


                if (!ModelState.IsValid)
                    return BadRequest();
                var movie = Mapper.Map<MovieDto, Movie>(movieDto);
                db.Movies.Add(movie);
                movieDto.ID = movie.ID;
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri + "/" + movie.ID), movieDto);
            }

        }

        // PUT: api/Movie/5
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            using (VidlyEntities1 db = new VidlyEntities1())
            {


                if (!ModelState.IsValid)
                    return BadRequest();
                var movieInDb = db.Movies.SingleOrDefault(m => m.ID == id);
                if (movieInDb == null)
                    return BadRequest();

                Mapper.Map(movieDto, movieInDb);



                db.SaveChanges();
                return Ok();

            }

        }

        // DELETE: api/Movie/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (VidlyEntities1 db = new VidlyEntities1())
            {



                var movieInDb = db.Movies.SingleOrDefault(m => m.ID == id);
                if (movieInDb == null)
                    return BadRequest();
                db.Movies.Remove(movieInDb);
                db.SaveChanges();
                return Ok();

            }
        }
    }
}
