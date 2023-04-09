using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCMock.Models
{

    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a first name.")]

        [StringLength(32, ErrorMessage = "The {0} field must be no more than {1} characters long.")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name.")]


        [StringLength(32, ErrorMessage = "The {0} field must be no more than {1} characters long.")]

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
"{0:yyyy-MM-dd}",
 ApplyFormatInEditMode = true)]

        public DateTime DateOfBirth { get; set; }

        // foreign key
        public int CourseId { get; set; }

        // navigation property
        public Course Course { get; set; }

        public ICollection<StudentHobby> StudentHobbies { get; set; }

      /*  [NotMapped]
        public List<SelectListItem>? hobbiesList { get; set; }

        [NotMapped]
        public string[] Hobbies { get; set; }*/

        public int Age
        {
            get { return DateTime.Now.Year - DateOfBirth.Year; }
        }
    }
}
       
    

   
    




