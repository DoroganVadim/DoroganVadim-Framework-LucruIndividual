using LucruIndividual.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace LucruIndividual.DataLayer
{
    public class SocialPlatformContext : DbContext
    {
        public SocialPlatformContext(DbContextOptions<SocialPlatformContext> options) : base(options)
        {
            ;
        }
        public DbSet<Post> posts { get; set; }
        public DbSet<FriendRelation> friendRelations { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Profile> profiles { get; set; }
        public DbSet<PostImage> postImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FriendRelation>()
                .HasOne(fr => fr.user1)
                .WithMany()
                .HasForeignKey(fr => fr.user1Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FriendRelation>()
                .HasOne(fr => fr.user2)
                .WithMany()
                .HasForeignKey(fr => fr.user2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
