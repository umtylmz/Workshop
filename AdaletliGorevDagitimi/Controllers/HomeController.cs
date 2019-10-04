using AdaletliGorevDagitimi.Database;
using AdaletliGorevDagitimi.Models;
using AdaletliGorevDagitimi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdaletliGorevDagitimi.Controllers
{
    public class HomeController : Controller
    {
        ProjectDbContext _db;
        public HomeController()
        {
            _db = new ProjectDbContext();
        }
        public ActionResult Index()
        {
            JobAssigner();
            return View();
        }
        public JsonResult getStaffNames()
        {
            List<Staff> staffList = _db.Staffs.ToList();
            List<StaffDTO> staffListDTO = new List<StaffDTO>();

            for (int i = 0; i < staffList.Count; i++)
                staffListDTO.Add(new StaffDTO() { Name = staffList[i].Name });

            return Json(staffListDTO, JsonRequestBehavior.AllowGet);
        }
        public void JobListRegenerator()
        {
            List<Staff> staffList = _db.Staffs.ToList();
            List<Job> jobList = _db.Jobs.ToList();

            for (int i = 0; i < staffList.Count; i++)
            {
                for (int j = 0; j < jobList.Count; j++)
                {
                    staffList[i].Jobs.Add(jobList[j]);

                    _db.Entry(staffList[i]).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
            }

        }
        public void JobAssigner()
        {
            bool controlItem = _db.Staffs.Take(1).ToList()[0].Jobs.ToList().Count == 0;

            if (controlItem)
                JobListRegenerator();

            List<Staff> staffList = _db.Staffs.ToList();
            Random rnd = new Random();
            List<Job> tempJobList = new List<Job>();

            for (int i = 0; i < 6; i++)
            {
                while (true)
                {
                    Job job = staffList[i].Jobs.ToList()[rnd.Next(staffList[i].Jobs.ToList().Count)];

                    if (tempJobList.Contains(job))
                        continue;
                    else
                    {
                        if (i < 5)
                        {
                            bool controller = false;

                            foreach (Job item in _db.Staffs.ToList()[i + 1].Jobs.ToList())
                            {
                                if (!tempJobList.Contains(item) && job.ID != item.ID)
                                    break;
                                else
                                    controller = true;
                            }

                            if (controller == true)
                                continue;
                        }

                        tempJobList.Add(job);
                        staffList[i].Jobs.Remove(job);
                        _db.Entry(staffList[i]).State = System.Data.Entity.EntityState.Modified;

                        DailyStaffAndJobRelation newData = new DailyStaffAndJobRelation()
                        {
                            JobID = job.ID,
                            StaffID = staffList[i].ID
                        };
                        _db.Entry(newData).State = System.Data.Entity.EntityState.Added;

                        _db.SaveChanges();

                        break;
                    }
                }
            }
        }
    }
}

