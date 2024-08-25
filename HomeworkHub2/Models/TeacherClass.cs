using System.ComponentModel.DataAnnotations.Schema;

namespace HomeworkHub2.Models
{
    public class TeacherClass
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }

        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }

    }
}
