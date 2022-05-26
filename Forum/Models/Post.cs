using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Post : TextGive
    {
      public string Title { get; set; }
      public List<Comment> Comments { get; set; }
       public List<Like> Likes { get; set; }


    }
}
