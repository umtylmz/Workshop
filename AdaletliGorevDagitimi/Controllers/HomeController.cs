using AdaletliGorevDagitimi.Database;
using AdaletliGorevDagitimi.Models;
using AdaletliGorevDagitimi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        List<Staff> staffList;
        StaffJobRelation newStaffJobRelation;
        Job assignedJob;

        public JsonResult GetStaffNames()
        {
            staffList = _db.Staffs.ToList();
            List<StaffDTO> staffListDTO = new List<StaffDTO>();

            for (int i = 0; i < staffList.Count; i++)
                staffListDTO.Add(new StaffDTO() { ID = staffList[i].ID, Name = staffList[i].Name });

            return Json(staffListDTO, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStaffJobs()
        {
            List<StaffJobRelation> relations = _db.StaffJobRelations.ToList();
            List<StaffJobRelationDTO> relationsDTO = new List<StaffJobRelationDTO>();
            List<StaffJobRelationDTO> orderedRelationsDTO = new List<StaffJobRelationDTO>();

            for (int i = 0; i < relations.Count; i++)
                relationsDTO.Add(new StaffJobRelationDTO() { Date = relations[i].Date, JobName = relations[i].Job.Name, StaffID = relations[i].StaffID, });

            int counter = relationsDTO.Count;

            for (int i = 0; i < counter / 6; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    orderedRelationsDTO.Add(relationsDTO.Find(a => a.StaffID == j));
                    relationsDTO.Remove(relationsDTO.Find(a => a.StaffID == j));
                }
            }

            return Json(orderedRelationsDTO, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJobPoints()
        {
            staffList = _db.Staffs.ToList();
            List<StaffDTO> staffListDTO = new List<StaffDTO>();

            for (int i = 0; i < staffList.Count; i++)
                staffListDTO.Add(new StaffDTO() { ID = staffList[i].ID, JobPoint = staffList[i].JobPoint });

            return Json(staffListDTO, JsonRequestBehavior.AllowGet);
        }
        public void JobAssigner()
        {
            List<Job> jobList = _db.Jobs.ToList();
            staffList = _db.Staffs.ToList();
            List<Job> assignableJobList;

            Random rnd = new Random();

            for (int i = 0; i < staffList.Count; i++)
            {

                if (staffList[i].JobPoint > 3.5m)
                {
                    assignableJobList = jobList.Where(a => a.Difficulty < 4 & a.Difficulty >= 1).ToList();

                    if (assignableJobList.Count == 0)
                        continue;
                    else
                        assignedJob = assignableJobList[rnd.Next(assignableJobList.Count)];

                    jobList.Remove(assignedJob);

                    AddNewStaffJobRelation(i);

                    staffList[i] = null;
                }
                else if (staffList[i].JobPoint < 3.5m)
                {
                    assignableJobList = jobList.Where(a => a.Difficulty > 3).ToList();

                    if (assignableJobList.Count == 0)
                        continue;
                    else
                        assignedJob = assignableJobList[rnd.Next(assignableJobList.Count)];

                    jobList.Remove(assignedJob);

                    AddNewStaffJobRelation(i);

                    staffList[i] = null;
                }
            }

            for (int i = 0; i < staffList.Count; i++)
            {
                if (staffList[i] == null)
                    continue;

                assignableJobList = jobList.ToList();

                if (assignableJobList.Count == 0)
                    continue;
                else
                    assignedJob = assignableJobList[rnd.Next(assignableJobList.Count)];

                jobList.Remove(assignedJob);

                AddNewStaffJobRelation(i);
            }

            JobPointCalculator();
        }
        public void JobPointCalculator()
        {
            List<StaffJobRelation> staffJobRelationList = _db.StaffJobRelations.ToList();
            staffList = _db.Staffs.ToList();

            for (int i = 0; i < staffList.Count; i++)
            {
                staffList[i].JobPoint = staffJobRelationList.Where(a => a.StaffID == staffList[i].ID).Sum(a => a.Job.Difficulty) / (decimal)staffJobRelationList.Where(a => a.StaffID == staffList[i].ID).Count();

                _db.Entry(staffList[i]).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public void AddNewStaffJobRelation(int i)
        {
            newStaffJobRelation = new StaffJobRelation();
            newStaffJobRelation.JobID = assignedJob.ID;
            newStaffJobRelation.StaffID = staffList[i].ID;

            _db.StaffJobRelations.Add(newStaffJobRelation);
            _db.SaveChanges();
        }

    }
}

