using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        public decimal Transportation { get; set; }

        public decimal Food { get; set; }

        public decimal View { get; set; }

        public decimal Pricing { get; set; }

        public int Counter { get; set; }

    }
}
