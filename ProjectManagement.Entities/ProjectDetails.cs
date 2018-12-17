using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjecManagement.EntityLayer
{
    [Table("Project_Table")]
    public class ProjectDetails
    {
        [Key]
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
    }
}
