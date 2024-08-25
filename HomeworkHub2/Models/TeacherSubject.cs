using System.ComponentModel.DataAnnotations.Schema;

namespace HomeworkHub2.Models
{
    public class TeacherSubject
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }

        public virtual Teacher? Teacher { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }

        public virtual Subject? Subject { get; set; }
    }
}
