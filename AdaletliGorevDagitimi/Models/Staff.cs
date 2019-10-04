using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Models
{
    public class Staff : BaseClass
    {
        public string Name { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<DailyStaffAndJobRelation> DailyStaffAndJobRelations { get; set; }
    }
}