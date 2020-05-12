using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsSubscribedToNewsletter { get; set; }
        public int MembershipTypeId { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
    }
}