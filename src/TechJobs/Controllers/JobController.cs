﻿using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job job = jobData.Find(id);
            JobFieldsViewModel jobFieldsViewModel = new JobFieldsViewModel();
            return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                Location location = jobData.Locations.Find(newJobViewModel.LocationID);
                Employer employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                CoreCompetency competency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                PositionType position = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);

                Job newJob = new Job
                {
                    Location = location,
                    Employer = employer,
                    CoreCompetency = competency,
                    PositionType = position,
                    Name = newJobViewModel.Name
                };

                jobData.Jobs.Add(newJob);

                return Redirect(string.Format("/Job?id={0}", newJob.ID));
            }
            return View(newJobViewModel);
        }
    }
}
