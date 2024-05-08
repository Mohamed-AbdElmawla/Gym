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

        public DbSet<TrainingPlan> trainingPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> sets { get; set; }
        public DbSet<Subscription> subscriptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoachEnrollment> coachEnrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany<TrainingPlan>()
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId)
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
