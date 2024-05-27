using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<SetAttribute> SetAttributes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoachEnrollment> CoachEnrollments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserBlock> UserBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<UserBlock>()
               .WithOne(e => e.Blocker)
               .HasForeignKey(e => e.BlockerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<UserBlock>()
               .WithOne(e => e.Blocked)
               .HasForeignKey(e => e.BlockedId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<SetAttribute>()
               .HasOne<Set>()
               .WithMany(s => s.Field)
               .HasForeignKey(s => s.SetId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<TrainingPlan>()
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ApplicationUser>()
              .HasMany<Message>()
              .WithOne(e => e.Sender)
              .HasForeignKey(e => e.SenderId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<Message>()
               .WithOne(e => e.Receiver)
               .HasForeignKey(e => e.ReceiverId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
              .HasMany<Notification>()
              .WithOne(e => e.Sender)
              .HasForeignKey(e => e.SenderId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<Notification>()
               .WithOne(e => e.Receiver)
               .HasForeignKey(e => e.ReceiverId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<TrainingPlan>()
               .HasMany<Set>()
               .WithOne(e => e.TrainingPlan)
               .HasForeignKey(e => e.TrainingId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Exercise>()
               .HasMany<Set>()
               .WithOne(e => e.Exercise)
               .HasForeignKey(e => e.ExerciseId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<CoachEnrollment>()
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<Subscription>()
               .WithOne(e => e.Coach)
               .HasForeignKey(e => e.CoachId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            // many to many relationship with the user and subscriptions
            modelBuilder.Entity<SubscriptionUser>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<SubscriptionUser>()
                .HasOne(e => e.Subscription)
                .WithMany(e => e.SubscriptionUsers)
                .HasForeignKey(e => e.SubscriptionId);

            modelBuilder.Entity<SubscriptionUser>()
                .HasOne(e => e.User)
                .WithMany(e => e.SubscriptionUsers)
                .HasForeignKey(e => e.UserId);

            /*
            modelBuilder.Entity<Category>()
               .HasMany<Exercise>()
               .WithOne(e => e.Muscle)
               .HasForeignKey(e => e.MuscleId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>()
               .HasMany<Exercise>()
               .WithOne(e => e.Equipment)
               .HasForeignKey(e => e.EquipmentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
            */

        }

    }
}
