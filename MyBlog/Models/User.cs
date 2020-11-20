using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        public IFormFile ImagePath { set; get; }
        public string ImagePathName { get; set; }
       


       public ICollection<BlogPost> BlogPosts { get; set; }
       public ICollection<User> Followers { get; set; }
       public ICollection<User> Following { get; set; }

    }
}
