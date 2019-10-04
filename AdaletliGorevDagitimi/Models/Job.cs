using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Models
{
    public class Job : BaseClass
    {
        public string Name { get; set; }
        public int Difficulty { get; set; }

        public virtual ICollection<StaffJobRelation> StaffJobRelations { get; set; }
    }
}