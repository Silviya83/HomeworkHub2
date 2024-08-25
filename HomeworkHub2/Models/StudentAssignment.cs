using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeworkHub2.Models
{
    public class StudentAssignment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        [ForeignKey(nameof(Assignment))]
       public int AssignmentId { get; set; }
        public virtual Assignment? Assignment { get; set; }

    }
}
