using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobApplicationTracker.Models.DatabaseModels
{
    [Table("tbl_job_applications")]
    public class JobApplicationDb
    {
        [Column("id")]
        public string? Id { get; set; }

        [Column("job_title ")]
        public string JobTitle { get; set; }

        [Column("job_description")]
        public string JobDescription { get; set; }

        [Column("resume")]
        public string Resume {  get; set; }

        [Column("job_post_link")]
        public string JobPostLink { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("created")]
        public DateTime CreatedAt { get; set; }

        [Column("updated")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
