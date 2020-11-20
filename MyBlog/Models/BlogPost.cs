using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Title { set; get; }
        public string Content { set; get; }
        [NotMapped]
        public IFormFile ImagePath { set; get; }
        public string ImagePathName { get; set; }
        
        public User User { set; get; }
        public  int UserId { set; get; }


        public IList<BlogPostClaps> BlogPostClaps { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
