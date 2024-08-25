using System.ComponentModel.DataAnnotations.Schema;

namespace HomeworkHub2.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly? DeadLine { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }

        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }

        public virtual Teacher? Teacher { get; set; }
    }
}
