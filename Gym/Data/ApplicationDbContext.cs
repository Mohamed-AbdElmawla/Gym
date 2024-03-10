using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public List<TrainingPlan> trainingPlans { get; set; }
        public List<Exercise> Exercises { get; set; }
        public List<Set> sets { get; set; }
        public List<Subscription> subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>()
               .HasMany<TrainingPlan>()
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId)
               .IsRequired();

            modelBuilder.Entity<TrainingPlan>()
               .HasMany<Set>()
               .WithOne(e => e.TrainingPlan)
               .HasForeignKey(e => e.TrainingId)
               .IsRequired();

            modelBuilder.Entity<Exercise>()
               .HasMany<Set>()
               .WithOne(e => e.Exercise)
               .HasForeignKey(e => e.ExerciseId)
               .IsRequired();
        }

    }
}
