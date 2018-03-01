using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace ForumRepository
{
    public class ForumContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        { }
    }
}
