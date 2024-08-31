using AutoMapper;
using JobApplicationTracker.Models.AppModels;
using JobApplicationTracker.Models.DatabaseModels;

namespace JobApplicationTracker.Utility
{
    public class DatabaseMappers:Profile
    {
        public DatabaseMappers() 
        {
            CreateMap<JobApplication, JobApplicationDb>();
        
        }
    }
}
