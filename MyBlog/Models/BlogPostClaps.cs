using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class BlogPostClaps
    {
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        
        
        public int ClapId { get; set; }
        public Claps Claps { get; set; }
        
    }
}
