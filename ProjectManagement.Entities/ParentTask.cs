using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjecManagement.EntityLayer
{
    [Table("Parent_Task_Table")]
    public class ParentTask
    {
        [Key]
        public int ParentTaskId { get; set; }
        public string ParentTaskTitle { get; set; }
    }
}
