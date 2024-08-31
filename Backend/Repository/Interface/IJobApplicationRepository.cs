using JobApplicationTracker.Models.DatabaseModels;

namespace JobApplicationTracker.Repository.Interface
{
    public interface IJobApplicationRepository
    {
        IEnumerable<JobApplicationDb> getAllJobApplications();
        JobApplicationDb CreateJobApplicationsAsync(JobApplicationDb jobApplications);

        JobApplicationDb getJobApplicationById(int id);
/*        JobApplicationDb FindJobApplicationById(string id);
        JobApplicationDb UpdateJobApplicationById(string id, JobApplicationDb applicationDb);
        bool DeleteJobApplicationById(string id);*/
    }
}
