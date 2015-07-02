using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}