using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Baskets;

namespace Route.Talabat.Core.Entities.Identity
{
    public class Address : BaseEntity
	{
        
        public string FirstName { get; set; } = null!;
     
        public string LastName { get; set; } = null!;
      
        public string Street { get; set; } = null!;
      
        public string City { get; set; } = null!;
      
        public string Country { get; set; } = null!;

       // [JsonIgnore]
        public string ApplicationUserId { get; set; } = null!; // Foreign Key


       // [JsonIgnore]
        public ApplicationUser User { get; set; } = null!; // Navigational Property [ONE]
    }
}
