using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int ID { get; set; }
        public string MovieName { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> NumberInStock { get; set; }
        public Nullable<int> NumberAvailable { get; set; }
        public int GenreID { get; set; }

        











            }
}