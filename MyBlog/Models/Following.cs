using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Following
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowsId { get; set; }

    }
}
