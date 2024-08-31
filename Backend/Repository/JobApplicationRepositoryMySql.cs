using JobApplicationTracker.Repository.Interface;
using JobApplicationTracker.Models.DatabaseModels;
using JobApplicationTracker.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
namespace JobApplicationTracker.Repository
{
    public class JobApplicationRepositoryMySql : IJobApplicationRepository
    {
        private readonly AppSettings appSettings;

        private readonly MySqlConnection connection;

        public JobApplicationRepositoryMySql(AppSettings appSettings)
        {
            this.appSettings = appSettings;
            this.connection = new MySqlConnection(this.appSettings.DbConnectionString);
            Console.WriteLine("CUSTOM LOG | Database connection string: " + this.appSettings.DbConnectionString);
        }

        public IEnumerable<JobApplicationDb> getAllJobApplications()
        {
            IEnumerable<JobApplicationDb> result = new List<JobApplicationDb>();
            try
            {
                if (!this.connection.State.Equals(ConnectionState.Open))
                {
                    this.connection.Open();
                }
                result = this.connection.Query<JobApplicationDb>("" +
                    "select id as Id, job_title as JobTitle, job_description as JobDescription," +
                    "resume as Resume, job_post_link as JobPostLink,status as Status,created as CreatedAt," +
                    "updated as UpdatedAt, deleted_at as DeletedAt " +
                    "from tbl_job_applications");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.connection.Close();
            }
            return result;
        }

        public JobApplicationDb getJobApplicationById(int id)
        {
            IEnumerable<JobApplicationDb> result = new List<JobApplicationDb>();
            try
            {
                if (!this.connection.State.Equals(ConnectionState.Open))
                {
                    this.connection.Open();
                }

                // Query to select a single job application by ID
                result = this.connection.Query<JobApplicationDb>(
                   "SELECT id as Id, job_title as JobTitle, job_description as JobDescription, " +
                   "resume as Resume, job_post_link as JobPostLink, status as Status, " +
                   "created as CreatedAt, updated as UpdatedAt, deleted_at as DeletedAt " +
                   $"FROM tbl_job_applications WHERE id ={id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.connection.Close();
            }
            return result.FirstOrDefault();
        }


        public JobApplicationDb CreateJobApplicationsAsync(JobApplicationDb jobApplications)
        {
            int result;
            IEnumerable<JobApplicationDb> insertedJobApplication = new List<JobApplicationDb>();
            try
            {
                if (!this.connection.State.Equals(ConnectionState.Open))
                {
                    this.connection.Open();
                }
                result = this.connection.Execute($"INSERT INTO tbl_job_applications " +
                    $"(job_title, job_description, resume, job_post_link, status) " +
                    $"VALUES('{jobApplications.JobTitle}', '{jobApplications.JobDescription}', '{jobApplications.Resume}', '{jobApplications.JobPostLink}', '{jobApplications.Status}');");
                insertedJobApplication = this.connection.Query<JobApplicationDb>("" +
                    "select id as Id, job_title as JobTitle, job_description as JobDescription,resume as Resume, job_post_link as JobPostLink,status as Status,created as CreatedAt,updated as UpdatedAt, deleted_at as DeletedAt " +
                    "FROM tbl_job_applications order by id desc limit 1;");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.connection.Close();
            }
            return insertedJobApplication.First();
        }




    }
}
