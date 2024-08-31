namespace JobApplicationTracker.Models.AppModels
{
    public class JobApplication
    {
        public string? Id { get; set; }  
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Resume { get; set; }
        public string JobPostLink { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
