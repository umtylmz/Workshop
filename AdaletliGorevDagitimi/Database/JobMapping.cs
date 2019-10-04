using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Database
{
    public class JobMapping:EntityTypeConfiguration<Job>
    {
        public JobMapping()
        {
            HasMany(a => a.DailyStaffAndJobRelations).WithRequired(a => a.Job).HasForeignKey(a=>a.JobID);
        }
    }
}