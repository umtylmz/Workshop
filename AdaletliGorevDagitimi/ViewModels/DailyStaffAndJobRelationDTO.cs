using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.ViewModels
{
    public class DailyStaffAndJobRelationDTO
    {
        public DateTime Date { get; set; }
        public string StaffName { get; set; }
        public string JobName { get; set; }
    }
}