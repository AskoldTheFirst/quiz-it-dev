using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KramarDev.Quiz.DAL.Database;

public class QuizDbContext : IdentityDbContext<User>
{
    public QuizDbContext()
    {
    }

    public QuizDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Test>()
            .HasMany(e => e.TestQuestions)
            .WithOne(e => e.Test)
            .HasForeignKey(e => e.TestId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Test>()
            .Property(t => t.State)
            .HasConversion<byte>();

        builder.Entity<Test>()
            .ToTable(t => t.HasCheckConstraint(
                "CK_Test_State",
                "[State] in (0, 1, 2)"));

        builder.Entity<Question>()
            .HasMany(e => e.TestQuestions)
            .WithOne(e => e.Question)
            .HasForeignKey(e => e.QuestionId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Topic>()
            .HasMany(e => e.Questions)
            .WithOne(e => e.Topic)
            .HasForeignKey(e => e.TopicId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Topic>()
            .HasMany(e => e.Tests)
            .WithOne(e => e.Topic)
            .HasForeignKey(e => e.TopicId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
            );

        builder.Entity<Test>().HasIndex(e => e.Username);
        builder.Entity<Test>().HasIndex(e => e.FinalScore);

        builder.Entity<Message>().Property(c => c.Data).HasDefaultValueSql("getdate()");
        builder.Entity<Message>().HasIndex(c => c.Username);
    }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Topic> Topics { get; set; }

    public DbSet<Test> Tests { get; set; }

    public DbSet<TestQuestion> TestQuestions { get; set; }

    public DbSet<Message> Messages { get; set; }
}
