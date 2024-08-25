using System.ComponentModel.DataAnnotations.Schema;

namespace HomeworkHub2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [ForeignKey(nameof(Class))]

        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }

    }
}
