using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Database.Mappings
{
    public class StaffMapping : EntityTypeConfiguration<Staff>
    {
        public StaffMapping()
        {
            Property(a => a.JobPoint).HasPrecision(5, 4);
        }
    }
}