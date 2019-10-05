using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Database.Mappings
{
    public class StaffJobRelationMapping:EntityTypeConfiguration<StaffJobRelation>
    {
        public StaffJobRelationMapping()
        {
            HasRequired(a => a.Staff).WithMany(a => a.StaffJobRelations).HasForeignKey(a=>a.StaffID);
            HasRequired(a => a.Job).WithMany(a => a.StaffJobRelations).HasForeignKey(a => a.JobID);
        }
    }
}