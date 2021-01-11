using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public decimal Transportation { get; set; }

        public decimal Food { get; set; }

        public decimal View { get; set; }

        public decimal Pricing { get; set; }

        public int Counter { get; set; }

    }
}
