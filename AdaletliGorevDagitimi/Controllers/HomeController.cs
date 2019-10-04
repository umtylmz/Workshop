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
        public JsonResult GetStaffNames()
        {
            List<Staff> staffList = _db.Staffs.ToList();
            List<StaffDTO> staffListDTO = new List<StaffDTO>();

            for (int i = 0; i < staffList.Count; i++)
                staffListDTO.Add(new StaffDTO() { Name = staffList[i].Name });

            return Json(staffListDTO, JsonRequestBehavior.AllowGet);
        }

        public void JobAssigner()
        {
            List<Job> jobList = _db.Jobs.ToList();
            List<Staff> staffList = _db.Staffs.ToList();
            staffList.Sort((a, b) => decimal.Compare(a.JobPoint, b.JobPoint));

            int counter = staffList.Count / 2 - 1;
            int counter2 = 1;

            Random rnd = new Random();

            for (int i = 0; i < staffList.Count; i++)
            {
                if (staffList[counter].JobPoint > 3.50m)
                {
                    Job assignedJob = jobList[rnd.Next(0, jobList.Count / 2 + 1)];

                    jobList.RemoveAll(a => a.ID == assignedJob.ID);

                    StaffJobRelation newStaffJobRelation = new StaffJobRelation();
                    newStaffJobRelation.JobID = assignedJob.ID;
                    newStaffJobRelation.StaffID = staffList[counter].ID;

                    _db.StaffJobRelations.Add(newStaffJobRelation);
                    _db.SaveChanges();

                    staffList.RemoveAt(counter);
                }
                else if (staffList[counter].JobPoint < 3.50m)
                {
                    Job assignedJob = jobList[rnd.Next(jobList.Count / 2 + 1, jobList.Count)];

                    jobList.RemoveAll(a => a.ID == assignedJob.ID);

                    StaffJobRelation newStaffJobRelation = new StaffJobRelation();
                    newStaffJobRelation.JobID = assignedJob.ID;
                    newStaffJobRelation.StaffID = staffList[counter].ID;

                    _db.StaffJobRelations.Add(newStaffJobRelation);
                    _db.SaveChanges();

                    staffList.RemoveAt(counter);
                }


                counter += counter2;
                counter2 = counter2 % 2 == 0 ? (Math.Abs(counter2) + 1) : (Math.Abs(counter2) + 1) * (-1);
            }

            for (int i = staffList.Count-1; i >= 0;) //hem buranın hem yukardaki forların içini ayarlayacağım, sonra tekrarlayan kodları metoda çevireceğim, sonra puan hesaplama metodunu yazacağım bitecek
            {
                Job assignedJob = jobList[rnd.Next(jobList.Count)];

                jobList.RemoveAll(a => a.ID == assignedJob.ID);

                StaffJobRelation newStaffJobRelation = new StaffJobRelation();
                newStaffJobRelation.JobID = assignedJob.ID;
                newStaffJobRelation.StaffID = staffList[i].ID;

                _db.StaffJobRelations.Add(newStaffJobRelation);
                _db.SaveChanges();

                staffList.RemoveAt(i);
            }
        }
    }
}

