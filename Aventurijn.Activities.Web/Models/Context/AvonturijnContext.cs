using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Aventurijn.Activities.Web.Models.Domain;
using Aventurijn.Activities.Web.Models.Domain.Membership;

namespace Aventurijn.Activities.Web.Models.Context
{
    public class AvonturijnContext : DbContext
    {
        public AvonturijnContext() : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Participation> Participations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>()
                        .HasMany<Role>(r => r.Roles)
                        .WithMany(u => u.Members)
                        .Map(m =>
                            {
                                m.ToTable("webpages_UsersInRoles");
                                m.MapLeftKey("UserId");
                                m.MapRightKey("RoleId");
                            });

            //modelBuilder.Entity<Student>()
            //            .HasRequired<Level>(l => l.Level)
            //            .WithMany(l => l.Students)
            //            .HasForeignKey();
        }

    }
}