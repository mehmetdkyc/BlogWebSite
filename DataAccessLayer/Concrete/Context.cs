using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(x => x.Writer)
                .WithMany(y => y.Comments)
                .HasForeignKey(z => z.WriterID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<About>? Abouts { get; set; }
        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Writer>? Writers { get; set; }
        public DbSet<NewsLetter>? NewsLetters { get; set; }
        public DbSet<BlogRating>? BlogRatings { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Message>? Messages { get; set; }
    }

}
