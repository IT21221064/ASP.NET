using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Name {  get; set; }

        public string? StreetAddress {  get; set; }
        public String? City { get; set; }
        public String? State { get; set; }
        public String? PostalCode { get; set;}
    }
}
