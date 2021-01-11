using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Models
{
    
    public class Comments
    {
        
        public int Id { get; set; }

        public int PhotoId { get; set; }
        
        public string CommentDetail { get; set; }

        public int UpVote{ get; set; }      



    }
}
