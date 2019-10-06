using AdaletliGorevDagitimi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdaletliGorevDagitimi.Database
{
    public class ProjectDbInitializer : CreateDatabaseIfNotExists<ProjectDbContext>
    {
        protected override void Seed(ProjectDbContext context)
        {
            List<string> staffNames = new List<string>() { "Ümit Yılmaz", "Murat Çakır", "Burak Koçyiğit", "Kaan Dedeoğlu", "Kadir Akın", "Oğuzhan Aksu" };
            Dictionary<string, int> jobs = new Dictionary<string, int>() {
                {"1.Görev",1 },{"2.Görev",2 },{"3.Görev",3 },{"4.Görev",4 },{"5.Görev",5 },{"6.Görev",6 } };

            for (int i = 0; i < 6; i++)
            {
                Staff newStaff = new Staff();
                newStaff.Name = staffNames[i];
                newStaff.JobPoint = 3.5m;
                context.Staffs.Add(newStaff);

                Job newJob = new Job();
                newJob.Name = jobs.Keys.ToList()[i];
                newJob.Difficulty = jobs[jobs.Keys.ToList()[i]];
                context.Jobs.Add(newJob);

                context.SaveChanges();
            }
        }
    }
}