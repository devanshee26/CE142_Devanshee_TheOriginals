using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Claps
    {
        public int ClapsId { get; set; }
        public string UserId { get; set; }
        

        IList<BlogPostClaps> BlogPostClaps { get; set; }
    }
}
