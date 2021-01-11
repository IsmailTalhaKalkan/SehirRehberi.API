using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class Photo
    {
        public Photo()
        {
            Comments = new List<Comments>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public City City { get; set; }         
        public List<Comments> Comments { get; set; }

    }
}
