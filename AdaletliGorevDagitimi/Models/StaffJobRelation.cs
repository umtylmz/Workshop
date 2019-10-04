using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Models
{
    public class StaffJobRelation : BaseClass
    {
        public StaffJobRelation()
        {
            Date = DateTime.Now;
        }

        public DateTime Date { get; set; }
        public int StaffID { get; set; }
        public int JobID { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual Job Job { get; set; }
    }
}