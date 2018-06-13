using AskME.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AskME.Infrastructure.Data
{
    public class AskMEDbContext : DbContext
    {
        public AskMEDbContext(DbContextOptions<AskMEDbContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<TagFollower> TagFollowers { get; set; }
        public DbSet<QuestionTags> QuestionTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionTags>()
                .HasKey(qt => new { qt.TagId, qt.QuestionId });
            modelBuilder.Entity<QuestionTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(qt => qt.TagId);
            modelBuilder.Entity<QuestionTags>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionTags)
                .HasForeignKey(qt => qt.QuestionId);

            modelBuilder.Entity<TagFollower>()
               .HasKey(tf => new { tf.TagId, tf.UserInfoId });
            modelBuilder.Entity<TagFollower>()
                .HasOne(tf => tf.Tag)
                .WithMany(t => t.TagFollowers)
                .HasForeignKey(tf => tf.TagId);
            modelBuilder.Entity<TagFollower>()
                .HasOne(tf => tf.UserInfo)
                .WithMany(u => u.TagFollowers)
                .HasForeignKey(tf => tf.UserInfoId);


            modelBuilder.Entity<BlogTag>()
                .HasKey(bt => new { bt.TagId, bt.BlogId });
            modelBuilder.Entity<BlogTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogTags)
                .HasForeignKey(bt => bt.TagId);
            modelBuilder.Entity<BlogTag>()
              .HasOne(bt => bt.Blog)
              .WithMany(b => b.BlogTags)
              .HasForeignKey(bt => bt.BlogId);
        }

    }
}
