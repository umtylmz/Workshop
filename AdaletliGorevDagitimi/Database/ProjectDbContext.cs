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
        public ProjectDbContext() : base("server=.;database=ProjectDb;uid=sa;pwd=123")
        {

        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<StaffJobRelation> StaffJobRelations { get; set; }
    }
}