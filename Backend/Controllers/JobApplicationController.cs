using AutoMapper;
using JobApplicationTracker.Models.AppModels;
using JobApplicationTracker.Models.DatabaseModels;
using JobApplicationTracker.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeMapping;
using Newtonsoft.Json;
using System;
using System.IO;

namespace JobApplicationTracker.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class JobApplicationController : Controller
    {
        private readonly string folderName = "files";
        private readonly IJobApplicationRepository jobApplicationRepository;

        private readonly IMapper mapper;
        public JobApplicationController(IJobApplicationRepository jobApplicationRepository,IMapper mapper) {
            this.jobApplicationRepository = jobApplicationRepository;
            this.mapper = mapper;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return Json(this.jobApplicationRepository.getAllJobApplications());
        }

        [HttpGet("/download_resume")]
        public IActionResult SendResumeFile([FromQuery]int id)
        {            
            var jobApplication=this.jobApplicationRepository.getJobApplicationById(id);
            var filePath = Path.Combine(folderName,jobApplication.Resume);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // File not found
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            var contentType = MimeUtility.GetMimeMapping(filePath);
            if (contentType == null)
            {
                contentType = "application/octet-stream"; // Fallback for unknown types
            }


            return File(fileBytes, contentType, jobApplication.Resume);
        }

        [HttpPost("/create")]
        public IActionResult Create([FromForm]string jobApplication, [FromForm]IFormFile file)
        {
            var application = JsonConvert.DeserializeObject<JobApplication>(jobApplication);

            if(file!=null && file.Length > 0)
            {
                var randomGuid = Guid.NewGuid().ToString();                
                var newFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(this.folderName, newFileName);
                application.Resume = newFileName;


                if (!Directory.Exists(this.folderName))
                {
                    Directory.CreateDirectory(this.folderName);
                }

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
            }


            var jobApplicaitonDb = this.mapper.Map<JobApplicationDb>(application);
            var savedApplication=this.jobApplicationRepository.CreateJobApplicationsAsync(jobApplicaitonDb);

            return Json(new
            {
                data=savedApplication,
                isSuccess=true
            });
        }     
        

    }
}
