using System.ComponentModel.DataAnnotations;

namespace MVCMock.Models
{
    public class StudentHobby
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int HobbyId { get; set; }
        public Hobbies Hobbies { get; set; }
    }
}
