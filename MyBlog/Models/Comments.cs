using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
