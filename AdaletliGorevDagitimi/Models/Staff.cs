using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Models
{
    public class Staff : BaseClass
    {
        public string Name { get; set; }
        public decimal JobPoint { get; set; }

        public virtual ICollection<StaffJobRelation> StaffJobRelations { get; set; }
    }
}