using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Database
{
    public class StaffMapping:EntityTypeConfiguration<Staff>
    {
        public StaffMapping()
        {
            HasMany(a => a.Jobs).WithMany(a => a.Staffs);
            HasMany(a => a.DailyStaffAndJobRelations).WithRequired(a => a.Staff).HasForeignKey(a=>a.StaffID);
        }
    }
}