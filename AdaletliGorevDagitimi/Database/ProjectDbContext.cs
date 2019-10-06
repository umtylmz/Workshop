using AdaletliGorevDagitimi.Database.Mappings;
using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Database
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext() : base("Server=.;Database=ProjectDb;Trusted_Connection=True;")
        {
            System.Data.Entity.Database.SetInitializer<ProjectDbContext>(new ProjectDbInitializer());
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<StaffJobRelation> StaffJobRelations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StaffMapping());
            modelBuilder.Configurations.Add(new StaffJobRelationMapping());
        }
    }
}