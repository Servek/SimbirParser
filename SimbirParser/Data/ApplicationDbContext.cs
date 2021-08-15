using Microsoft.EntityFrameworkCore;
using SimbirParser.Data.Models;

namespace SimbirParser.Data
{
    /// <summary>
    /// Main DB Context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">DB context options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /// <summary>
        /// Unique words frequency
        /// </summary>
        public DbSet<UniqueWordFrequency> UniqueWordsFrequency { get; set; }
    }
}
