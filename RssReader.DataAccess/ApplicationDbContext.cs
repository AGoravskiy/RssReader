using RssReader.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.DataAccess
{
    /// <summary>
    /// Represents an application database context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext() : base("DefaultConnection") { }

        /// <summary>
        /// Gets or sets a <see cref="DbSet"/> for <see cref="Channel"/>.
        /// </summary>
        public DbSet<Channel> Channels { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="DbSet"/> for <see cref="Post"/>.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
    }
}
